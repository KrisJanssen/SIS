// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LikeComparison.cs" company="">
//   
// </copyright>
// <summary>
//   A class capable of performing a like comparison of strings -
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace DevDefined.Tools.Text
{
    using System.Collections;

    /// <summary>
    /// A class capable of performing a like comparison of strings - 
    /// </summary>
    /// <remarks>
    /// This class follows the same matching rules as the SQL 2000 LIKE clause, and was originally written for the 
    /// 1.1 Framework, so it may look a little dated now.
    /// </remarks>
    public class LikeComparison
    {
        #region Fields

        /// <summary>
        /// The _apply escape character.
        /// </summary>
        private readonly bool _applyEscapeCharacter;

        /// <summary>
        /// The _case insensitive.
        /// </summary>
        private readonly bool _caseInsensitive;

        /// <summary>
        /// The _escape character.
        /// </summary>
        private readonly char _escapeCharacter;

        /// <summary>
        /// The _match expression.
        /// </summary>
        private readonly string _matchExpression;

        /// <summary>
        /// The _atoms.
        /// </summary>
        private LikeAtom[] _atoms;

        /// <summary>
        /// The _atoms length.
        /// </summary>
        private int _atomsLength;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LikeComparison"/> class. 
        /// Constructor - creates a LikeComparison instance which can be used for testing with
        /// via the Compare(object) method at a later point in time. 
        /// </summary>
        /// <remarks>
        /// This will have no escape character available.
        /// </remarks>
        /// <param name="matchExpression">
        /// The like string expression ie. "[bcs]and%"
        /// </param>
        /// <param name="caseInsensitive">
        /// is the matching case insensitive?
        /// </param>
        public LikeComparison(string matchExpression, bool caseInsensitive)
        {
            this._caseInsensitive = caseInsensitive;
            this._applyEscapeCharacter = false;

            if (this._caseInsensitive)
            {
                this._matchExpression = matchExpression.ToUpper();
            }
            else
            {
                this._matchExpression = matchExpression;
            }

            this.Initialise();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LikeComparison"/> class. 
        /// Constructor - creates a LikeComparison instance when can be used for testing with
        /// via the Compare(object) method at a later point in time.		
        /// </summary>
        /// <param name="matchExpression">
        /// The like string expression ie. "[bcs]and%"
        /// </param>
        /// <param name="escapeCharacter">
        /// The escape character, use this before any other character to "escape" it, so that its not interpreted in anything but the literal sense.
        /// </param>
        /// <param name="caseInsensitive">
        /// is the matching case insensitive?
        /// </param>
        public LikeComparison(string matchExpression, char escapeCharacter, bool caseInsensitive)
        {
            this._caseInsensitive = caseInsensitive;

            if (this._caseInsensitive)
            {
                this._matchExpression = matchExpression.ToUpper();
                this._escapeCharacter = char.ToUpper(escapeCharacter);
            }
            else
            {
                this._matchExpression = matchExpression;
                this._escapeCharacter = escapeCharacter;
            }

            this._applyEscapeCharacter = true;

            this.Initialise();
        }

        #endregion

        #region Enums

        /// <summary>
        /// The types of expression which occur at each index position of the expression.
        /// </summary>
        private enum LikeExpressionType
        {
            /// <summary>
            /// The single character.
            /// </summary>
            SingleCharacter, 

            /// <summary>
            /// The ignore character.
            /// </summary>
            IgnoreCharacter, 

            /// <summary>
            /// The wild card.
            /// </summary>
            WildCard, 

            /// <summary>
            /// The character set.
            /// </summary>
            CharacterSet, 

            /// <summary>
            /// The not in character set.
            /// </summary>
            NotInCharacterSet
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Compare the supplied object (string or null) to the expression - this will return true when a match occurs
        /// as per the LIKE rules.
        /// </summary>
        /// <param name="o">
        /// the object to compare against the match expression this LikeComparison instance was constructed with.
        /// </param>
        /// <returns>
        /// True if a the supplied object matched the expression, or if the instance was null and this
        /// LikeComparison instance has no atoms instantiated (which would require an empty like statement to be
        /// supplied)
        /// </returns>
        public bool Compare(object o)
        {
            if (o == null)
            {
                return this._atomsLength == 0;
            }

            string s = o.ToString();

            if (this._caseInsensitive)
            {
                s = s.ToUpper();
            }

            return this.CompareAt(s, 0, 0, s.Length);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Given a set of ranges in an array list, we simplify it down into the smallest number of ranges and single character references.
        /// </summary>
        /// <param name="ranges">
        /// </param>
        /// <param name="matchCharacters">
        /// </param>
        /// <param name="matchRanges">
        /// </param>
        protected static void SimplifyRanges(ArrayList ranges, out char[] matchCharacters, out char[][] matchRanges)
        {
            for (int i = 0; i < ranges.Count; i++)
            {
                for (int j = 0; j < ranges.Count; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }

                    var charI = (char[])ranges[i];
                    var charJ = (char[])ranges[j];
                    if ((charJ[0] < charI[0]) && (charJ[1] >= (charI[0] - 1)))
                    {
                        charI[0] = charJ[0];
                    }

                    if ((charJ[1] > charI[1]) && (charJ[0] <= (charI[1] + 1)))
                    {
                        charI[1] = charJ[1];
                    }

                    ranges[i] = charI;
                }
            }

            var newRanges = new ArrayList(ranges.Count);
            string newCharacters = string.Empty;

            for (int i = 0; i < ranges.Count; i++)
            {
                var charI = (char[])ranges[i];
                if (charI[0] == charI[1])
                {
                    if (newCharacters.IndexOf(charI[0]) < 0)
                    {
                        newCharacters += charI[0];
                    }
                }
                else
                {
                    bool exists = false;
                    for (int j = 0; j < newRanges.Count; j++)
                    {
                        var charJ = (char[])newRanges[j];
                        if ((charI[0] == charJ[0]) && (charI[1] == charJ[0]))
                        {
                            exists = true;
                            break;
                        }
                    }

                    if (!exists)
                    {
                        newRanges.Add(ranges[i]);
                    }
                }
            }

            matchCharacters = newCharacters.ToCharArray();
            matchRanges = (char[][])newRanges.ToArray(typeof(char[]));
        }

        /// <summary>
        /// CompareAt - used internally, invoking this may incur recursion when walking an "akward" like statement ie.
        /// "welcome%bob" where any number of characters could fall between welcome and bob.  If I stuck my thinking cap
        /// on I could probably come up with a better way of handling this situation that doesn't incur recursion, but for now
        /// I cant see this being too risky as like statements are generally simplistic expressions.
        /// </summary>
        /// <param name="testValue">
        /// the string to test matching for
        /// </param>
        /// <param name="atomIndex">
        /// The atom index to begin testing at
        /// </param>
        /// <param name="testIndex">
        /// The index to begin test the string at
        /// </param>
        /// <param name="testValueLength">
        /// The length of the test string
        /// </param>
        /// <returns>
        /// True if our comparison succeeded
        /// </returns>
        private bool CompareAt(string testValue, int atomIndex, int testIndex, int testValueLength)
        {
            LikeAtom currentAtom;

            for (; atomIndex < this._atomsLength; atomIndex++)
            {
                char matchChar;

                switch (this._atoms[atomIndex].AtomType)
                {
                    case LikeExpressionType.SingleCharacter:
                        if ((testIndex >= testValueLength)
                            || (testValue[testIndex++] != this._atoms[atomIndex].MatchCharacters[0]))
                        {
                            return false;
                        }

                        break;

                    case LikeExpressionType.NotInCharacterSet:
                        currentAtom = this._atoms[atomIndex];
                        matchChar = testValue[testIndex++];
                        for (int i = currentAtom.MatchCharacters.Length - 1; i >= 0; i--)
                        {
                            if (matchChar == currentAtom.MatchCharacters[i])
                            {
                                return false;
                            }
                        }

                        for (int i = currentAtom.RangeCharacters.Length - 1; i >= 0; i--)
                        {
                            if ((matchChar >= currentAtom.RangeCharacters[i][0])
                                && (matchChar <= currentAtom.RangeCharacters[i][1]))
                            {
                                return false;
                            }
                        }

                        break;

                    case LikeExpressionType.CharacterSet:
                        currentAtom = this._atoms[atomIndex];
                        matchChar = testValue[testIndex++];
                        bool matchFound = false;
                        for (int i = currentAtom.MatchCharacters.Length - 1; i >= 0; i--)
                        {
                            if (matchChar == currentAtom.MatchCharacters[i])
                            {
                                matchFound = true;
                                break;
                            }
                        }

                        if (!matchFound)
                        {
                            for (int i = currentAtom.RangeCharacters.Length - 1; i >= 0; i--)
                            {
                                if ((matchChar >= currentAtom.RangeCharacters[i][0])
                                    && (matchChar <= currentAtom.RangeCharacters[i][1]))
                                {
                                    matchFound = true;
                                    break;
                                }
                            }
                        }

                        if (!matchFound)
                        {
                            return false;
                        }

                        break;

                    case LikeExpressionType.IgnoreCharacter:

                        if (testIndex++ >= testValueLength)
                        {
                            return false;
                        }

                        break;

                    case LikeExpressionType.WildCard:

                        if (++atomIndex >= this._atomsLength)
                        {
                            return true;
                        }

                        while (testIndex < testValueLength)
                        {
                            if ((this._atoms[atomIndex].MatchCharacters[0] == testValue[testIndex])
                                && this.CompareAt(testValue, atomIndex, testIndex, testValueLength))
                            {
                                return true;
                            }

                            testIndex++;
                        }

                        return false;
                }
            }

            if (testIndex != testValueLength)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Once the private variables are initialised from the constructor, this method should be invoked to
        /// populate the _atoms and _atomsLength private members, which are used when evaluating matches with 
        /// the CompareXXX methods.
        /// </summary>
        private void Initialise()
        {
            this.ParseIntoAtoms();
            this.NormaliseWildcards();
        }

        /// <summary>
        /// If we have an expression that has a wildcard before an ignore character, we re-shuffle them so the single character
        /// comes first, otherwise we'll get a false positive when evaluating the wild card in the CompareAt method.
        /// </summary>
        private void NormaliseWildcards()
        {
            for (int atomIndex = 0; atomIndex < (this._atomsLength - 1); atomIndex++)
            {
                if ((this._atoms[atomIndex].AtomType == LikeExpressionType.WildCard)
                    && (this._atoms[atomIndex + 1].AtomType == LikeExpressionType.IgnoreCharacter))
                {
                    LikeAtom oldAtom = this._atoms[atomIndex];
                    this._atoms[atomIndex] = this._atoms[atomIndex + 1];
                    this._atoms[atomIndex + 1] = oldAtom;
                }
            }
        }

        /// <summary>
        /// Parses the supplied like string into a number of LikeAtom instances, which we use when parsing
        /// candidate strings to establish a match.  Obviously the overhead for this is potentially "high" 
        /// but it's intended that you would re-use the same like instance for comparing numerous strings
        /// which this class should be adept at.
        /// </summary>		
        private void ParseIntoAtoms()
        {
            bool escaping = false;
            bool charSet = false;
            bool range = false;
            bool notInSet = false;

            int matchExpressionLength = this._matchExpression.Length;
            this._atoms = new LikeAtom[matchExpressionLength];
            this._atomsLength = 0;

            var ranges = new ArrayList();

            for (int matchExpCharIndex = 0; matchExpCharIndex < matchExpressionLength; matchExpCharIndex++)
            {
                char currentChar = this._matchExpression[matchExpCharIndex];

                // character set specification
                if (charSet)
                {
                    if ((currentChar == '^') && (ranges.Count == 0))
                    {
                        if (!notInSet)
                        {
                            notInSet = true;
                        }
                        else
                        {
                            ranges.Add(new[] { currentChar, currentChar });
                        }
                    }
                    else if (currentChar == ']')
                    {
                        // if "-" was the last character in the sequence, then it wasn't a range of characters...
                        if (range)
                        {
                            ranges.Add(new[] { '-', '-' });
                        }

                        char[] matchCharacters;
                        char[][] matchRanges;
                        SimplifyRanges(ranges, out matchCharacters, out matchRanges);

                        if (notInSet)
                        {
                            this._atoms[this._atomsLength++] = new LikeAtom(
                                LikeExpressionType.NotInCharacterSet, 
                                matchCharacters, 
                                matchRanges);
                        }
                        else
                        {
                            this._atoms[this._atomsLength++] = new LikeAtom(
                                LikeExpressionType.CharacterSet, 
                                matchCharacters, 
                                matchRanges);
                        }

                        charSet = false;
                        range = false;
                        notInSet = false;
                    }
                    else if (currentChar == '-')
                    {
                        if (!range)
                        {
                            if (ranges.Count > 0)
                            {
                                range = true;
                            }
                        }
                        else
                        {
                            ranges.Add(new[] { '-', '-' });
                        }
                    }
                    else
                    {
                        if (range)
                        {
                            char cStart = this._matchExpression[matchExpCharIndex - 2];
                            char cEnd = currentChar;
                            if (cStart > cEnd)
                            {
                                // swap them round, the optimiser will most likely eliminate the tempStart
                                // for a register or something similar. I'll never test the hypothesis ;o)								
                                char tempStart = cStart;
                                cStart = cEnd;
                                cEnd = tempStart;
                            }

                            ranges.Add(new[] { cStart, cEnd });

                            // toggle range parsing off again
                            range = false;
                        }
                        else
                        {
                            ranges.Add(new[] { currentChar, currentChar });
                        }
                    }
                }
                else if (escaping == false)
                {
                    if (this._applyEscapeCharacter && (currentChar == this._escapeCharacter))
                    {
                        // escape the next character
                        escaping = true;
                        continue;
                    }
                    else if (currentChar == '_')
                    {
                        this._atoms[this._atomsLength++] = new LikeAtom(LikeExpressionType.IgnoreCharacter);
                    }
                    else if (currentChar == '%')
                    {
                        this._atoms[this._atomsLength++] = new LikeAtom(LikeExpressionType.WildCard);
                    }
                    else if (currentChar == '[')
                    {
                        charSet = true;
                        ranges.Clear();
                    }
                    else
                    {
                        this._atoms[this._atomsLength++] = new LikeAtom(LikeExpressionType.SingleCharacter, currentChar);
                    }
                }
                else
                {
                    // regardless of the character, we escape its contents
                    this._atoms[this._atomsLength++] = new LikeAtom(LikeExpressionType.SingleCharacter, currentChar);

                    // and turn off escaping, to allow the correct parsing of the rest of the string.
                    escaping = false;
                }
            }
        }

        #endregion

        /// <summary>
        /// A single "atom" of the like expression.  The idea is that this structure is lightweight and used to
        /// make the implementation of the LikeComparison class easier to understand.
        /// </summary>
        private struct LikeAtom
        {
            #region Fields

            /// <summary>
            /// The type of atom this is
            /// </summary>
            public readonly LikeExpressionType AtomType;

            /// <summary>
            /// The single character matches (if any) associated with this atom
            /// </summary>
            public readonly char[] MatchCharacters;

            /// <summary>
            /// A 2 character jagged array ie. char[][] = new char[][] {new char[]{'a','z'}}; which 
            /// represents the sets of ranges we need to test characters over.
            /// </summary>
            public readonly char[][] RangeCharacters;

            #endregion

            #region Constructors and Destructors

            /// <summary>
            /// Initializes a new instance of the <see cref="LikeAtom"/> struct. 
            /// Constructor - constructs a LikeAtom off the supplied type, both arrays will be initialised
            /// to null.
            /// </summary>
            /// <param name="atomType">
            /// The type for this LikeAtom
            /// </param>
            public LikeAtom(LikeExpressionType atomType)
            {
                this.AtomType = atomType;
                this.MatchCharacters = null;
                this.RangeCharacters = null;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="LikeAtom"/> struct. 
            /// Constructor - constructs a like atom of the supplied type, for the supplied single matchCharacter.
            /// </summary>
            /// <param name="atomType">
            /// The type for this LikeAtom
            /// </param>
            /// <param name="matchCharacter">
            /// This character will placed as a single character array in the MatchCharacters construct.
            /// </param>
            public LikeAtom(LikeExpressionType atomType, char matchCharacter)
            {
                this.AtomType = atomType;
                this.MatchCharacters = new[] { matchCharacter };
                this.RangeCharacters = null;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="LikeAtom"/> struct. 
            /// Constructor - constructs a like atom of the supplied type, initialising both arrays with the characters supplied.
            /// </summary>
            /// <param name="atomType">
            /// </param>
            /// <param name="matchCharacters">
            /// </param>
            /// <param name="rangeCharacters">
            /// </param>
            public LikeAtom(LikeExpressionType atomType, char[] matchCharacters, char[][] rangeCharacters)
            {
                this.AtomType = atomType;
                this.MatchCharacters = matchCharacters;
                this.RangeCharacters = rangeCharacters;
            }

            #endregion
        }
    }
}
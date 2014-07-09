// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DSCProtocol.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   Class to facilitate the interaction with DSC scan protocol - assembling protocol, reading it etc.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Hardware.YanusIV
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Class to facilitate the interaction with DSC scan protocol - assembling protocol, reading it etc.
    /// </summary>
    public class DSCProtocol
    {
        #region Fields

        /// <summary>
        /// The m_l cmd list.
        /// </summary>
        private List<DSCCommand> m_lCmdList; // list that contains the DSC protocol commands

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Prevents a default instance of the <see cref="DSCProtocol"/> class from being created.  Empty class constructor. 
        /// </summary>
        private DSCProtocol()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DSCProtocol"/> class. 
        /// Default class constructor. 
        /// </summary>
        /// <param name="__lDSCCommandList">
        /// The __l DSC Command List.
        /// </param>
        private DSCProtocol(List<DSCCommand> __lDSCCommandList)
        {
            this.m_lCmdList = __lDSCCommandList;
        }

        #endregion

        #region Public Properties

        /// <summary> Returns the (X,Y) coordinates pairs from a DSC protocol. </summary>
        public double[,] DSPCoordinates
        {
            get
            {
                return this.DSPCoordFromProtocol();
            }
        }

        /// <summary> Property that returns a DSP string from a given DSCCommand list representation. </summary>
        public string DSPString
        {
            get
            {
                StringBuilder _sbCmdString = new StringBuilder();

                // Format a DSCComand command as a string so that it is suitable to send to the YanusIV galvo scanner
                foreach (DSCCommand _dscCmd in this.m_lCmdList)
                {
                    _sbCmdString.Append("A ");
                    _sbCmdString.Append((char)_dscCmd.ScanCmd + ",");
                    _sbCmdString.Append(_dscCmd.Cycle.ToString() + ",");
                    _sbCmdString.Append(_dscCmd.Channel.ToString() + ",");
                    _sbCmdString.Append(_dscCmd.Value.ToString());
                    _sbCmdString.Append("\r\n");
                }

                return _sbCmdString.ToString();
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Returns 
        /// </summary>
        /// <param name="__sProtocol">
        /// The __s Protocol.
        /// </param>
        /// <returns>
        /// The <see cref="DSCProtocol"/>.
        /// </returns>
        public static DSCProtocol FromString(string __sProtocol)
        {
            List<DSCCommand> _lCmdList = new List<DSCCommand>();

            // The regex we will use to scan commands is the following:
            // A\s+[0,V,R,I,J,S,E,U,D],\d+,[0,3,4,5,7],\-?\d+
            // It reads:
            // An "A" followed by any number of spaces followed by either "0", "V", "R", ..., "D" followed by ","
            // followed by any number of digits, followed by either "0", "3", ..., "7" followed by ","
            // followed by either a positive or negative integer number.

            // Create the regex
            Regex _rexPattern = new Regex(@"A\s+[0,V,R,I,J,S,E,U,D],\d+,[0-9],\-?\d+");

            // Get matches
            MatchCollection _mtchCommandStrings = _rexPattern.Matches(__sProtocol);

            // Iterate and process matches
            foreach (Capture c in _mtchCommandStrings)
            {
                string _sCurent = c.Value;
                DSCCommand _dspcNew = new DSCCommand();

                switch (_sCurent.Substring(c.Value.IndexOf(" ") + 1, 1))
                {
                    case "0":
                        _dspcNew.ScanCmd = ScanCommand.scan_cmd_do_nothing;
                        break;
                    case "V":
                        _dspcNew.ScanCmd = ScanCommand.scan_cmd_set_value;
                        break;
                    case "R":
                        _dspcNew.ScanCmd = ScanCommand.scan_cmd_set_value_relative;
                        break;
                    case "I":
                        _dspcNew.ScanCmd = ScanCommand.scan_cmd_set_increment_1;
                        break;
                    case "J":
                        _dspcNew.ScanCmd = ScanCommand.scan_cmd_set_increment_2;
                        break;
                    case "S":
                        _dspcNew.ScanCmd = ScanCommand.scan_cmd_loop_start;
                        break;
                    case "E":
                        _dspcNew.ScanCmd = ScanCommand.scan_cmd_loop_end;
                        break;
                    case "U":
                        _dspcNew.ScanCmd = ScanCommand.scan_cmd_wait_trig_rising;
                        break;
                    case "D":
                        _dspcNew.ScanCmd = ScanCommand.scan_cmd_wait_trig_falling;
                        break;
                    default:
                        _dspcNew.ScanCmd = ScanCommand.scan_cmd_do_nothing;
                        break;
                }

                _sCurent = _sCurent.Substring(c.Value.IndexOf(" ") + 1);
                string[] _sParts = _sCurent.Split(new[] { "," }, StringSplitOptions.None);
                _dspcNew.Cycle = Convert.ToUInt64(_sParts[1]);
                _dspcNew.Channel = (DSCChannel)Convert.ToInt16(_sParts[2]);
                _dspcNew.Value = Convert.ToInt64(_sParts[3]);
                _lCmdList.Add(_dspcNew);

                // c.Value; // write the value to the console "pattern"
            }

            DSCProtocol _dscpNewProtocol = new DSCProtocol(_lCmdList);
            return _dscpNewProtocol;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Generate the protocol coordinates from the protocol DSCComand list. 
        /// </summary>
        /// <returns>
        /// The <see cref="double[,]"/>.
        /// </returns>
        private double[,] DSPCoordFromProtocol()
        {
            ulong _ui64PointsGenerated = 0;

            ulong _ui64PrevCmdCycle = 0;
            int _iCmdIndex = 0;
            Stack<LoopTracker> _stLoopStack = new Stack<LoopTracker>();
            List<long> _li64CoordsX = new List<long>();
            List<long> _li64CoordsY = new List<long>();

            // Initially, position, speed and acceleration are zero!
            long _i64CurrPosX = 0;
            long _i64CurrPosY = 0;
            long _i64CurrIncX = 0;
            long _i64CurrIncY = 0;
            long _i64CurrIncIncX = 0;
            long _i64CurrIncIncY = 0;

            double[,] coord;

            // We need to generate all points in the protocol so we check the number of cycles mentioned in the last protocol command.
            ulong _ui64PointsToGenerate = this.m_lCmdList.Last().Cycle;

            // We need to generate coordinates for as many cycles as we have just determined.
            while (_ui64PointsGenerated < _ui64PointsToGenerate)
            {
                // Get the command to process...
                DSCCommand _dscCmd = this.m_lCmdList[_iCmdIndex];

                // If we processed all commands in the same cycle, we can get to updating values...
                if (_dscCmd.Cycle > _ui64PrevCmdCycle)
                {
                    for (ulong _iI = 0; _iI < _dscCmd.Cycle - _ui64PrevCmdCycle; _iI++)
                    {
                        // Update positions first!
                        _li64CoordsX.Add(_i64CurrPosX);
                        _li64CoordsY.Add(_i64CurrPosY);

                        // Apply increment next!
                        _i64CurrPosX += _i64CurrIncX;
                        _i64CurrPosY += _i64CurrIncY;

                        // Finally apply incinc!
                        _i64CurrIncX += _i64CurrIncIncX;
                        _i64CurrIncY += _i64CurrIncIncY;

                        // Increment points generated.
                        _ui64PointsGenerated += 1;
                    }
                }

                // Did we process the previous instruction completely? I.e. did we let it generate all points?
                // If yes, then it is time to update pos, inc and incinc!
                switch (_dscCmd.ScanCmd)
                {
                    case ScanCommand.scan_cmd_set_value:
                        if (_dscCmd.Channel == DSCChannel.X)
                        {
                            _i64CurrPosX = _dscCmd.Value;
                        }
                        else
                        {
                            _i64CurrPosY = _dscCmd.Value;
                        }

                        // We are done with the current command, store its cycle!
                        _ui64PrevCmdCycle = _dscCmd.Cycle;

                        break;

                    case ScanCommand.scan_cmd_set_value_relative:
                        if (_dscCmd.Channel == DSCChannel.X)
                        {
                            _i64CurrPosX += _dscCmd.Value;
                        }
                        else
                        {
                            _i64CurrPosY += _dscCmd.Value;
                        }

                        // We are done with the current command, store its cycle!
                        _ui64PrevCmdCycle = _dscCmd.Cycle;

                        break;

                    case ScanCommand.scan_cmd_set_increment_1:
                        if (_dscCmd.Channel == DSCChannel.X)
                        {
                            _i64CurrIncX = _dscCmd.Value;
                        }
                        else
                        {
                            _i64CurrIncY = _dscCmd.Value;
                        }

                        // We are done with the current command, store its cycle!
                        _ui64PrevCmdCycle = _dscCmd.Cycle;

                        break;

                    case ScanCommand.scan_cmd_set_increment_2:
                        if (_dscCmd.Channel == DSCChannel.X)
                        {
                            _i64CurrIncIncX = _dscCmd.Value;
                        }
                        else
                        {
                            _i64CurrIncIncY = _dscCmd.Value;
                        }

                        // We are done with the current command, store its cycle!
                        _ui64PrevCmdCycle = _dscCmd.Cycle;

                        break;

                    case ScanCommand.scan_cmd_loop_start:

                        // Push the current index of the loopstart command onto the stack, along with the number of necessary no. of iterations.
                        _stLoopStack.Push(new LoopTracker(_iCmdIndex, _dscCmd.Cycle, 0, 0, _dscCmd.Value));
                        break;

                    case ScanCommand.scan_cmd_loop_end:
                        LoopTracker _ltCurrLoop = _stLoopStack.Pop();

                        // Subtract 1 from the number of iterations to generate!
                        _ltCurrLoop.CurrentIteration += 1;

                        // Very important! Save the cycle of the End command! This allows us to calculate length of the loop in cycles!
                        _ltCurrLoop.EndCommandCycle = _dscCmd.Cycle;

                        if ((long)_ltCurrLoop.CurrentIteration < _ltCurrLoop.Iterations)
                        {
                            // Go back to the loop start.
                            _iCmdIndex = _ltCurrLoop.StartCommandIndex;

                            // Push the modified LoopTracker
                            _stLoopStack.Push(_ltCurrLoop);

                            // We are done with the current command, store its cycle!
                            _ui64PrevCmdCycle = _ltCurrLoop.StartCommandCycle;
                        }
                        else
                        {
                            // When the final iteration of the loop is finished, the DSC will have generated Looplength * iterations of cycles and hence,
                            // The actual command cycle needs to be calculated for correct processing in relation to the next command!
                            _ui64PrevCmdCycle = (_ltCurrLoop.EndCommandCycle - _ltCurrLoop.StartCommandCycle)
                                                * (ulong)_ltCurrLoop.Iterations;
                        }

                        break;

                    default:
                        break;
                }

                // Increment to the next command!
                _iCmdIndex += 1;
            }

            // Convert the coordinate lists for X and Y to simple arrays...
            coord = new double[2, _li64CoordsX.Count];

            for (int _iI = 0; _iI < _li64CoordsX.Count; _iI++)
            {
                coord[0, _iI] = _li64CoordsX[_iI];
                coord[1, _iI] = _li64CoordsY[_iI];
            }

            return coord;
        }

        #endregion

        /// <summary> Simple struct to help unroll DSCProtocols containing loops. </summary>        
        private struct LoopTracker
        {
            #region Fields

            /// <summary>
            /// The current iteration.
            /// </summary>
            public long CurrentIteration;

            /// <summary>
            /// The end command cycle.
            /// </summary>
            public ulong EndCommandCycle;

            /// <summary>
            /// The iterations.
            /// </summary>
            public long Iterations;

            /// <summary>
            /// The start command cycle.
            /// </summary>
            public ulong StartCommandCycle;

            /// <summary>
            /// The start command index.
            /// </summary>
            public int StartCommandIndex;

            #endregion

            #region Constructors and Destructors

            /// <summary>
            /// Initializes a new instance of the <see cref="LoopTracker"/> struct.
            /// </summary>
            /// <param name="__iSCI">
            /// The __i sci.
            /// </param>
            /// <param name="__SCC">
            /// The __ scc.
            /// </param>
            /// <param name="__ECC">
            /// The __ ecc.
            /// </param>
            /// <param name="__iI">
            /// The __i i.
            /// </param>
            /// <param name="__iIs">
            /// The __i is.
            /// </param>
            public LoopTracker(int __iSCI, ulong __SCC, ulong __ECC, long __iI, long __iIs)
            {
                this.StartCommandIndex = __iSCI;
                this.StartCommandCycle = __SCC;
                this.EndCommandCycle = __ECC;
                this.CurrentIteration = __iI;
                this.Iterations = __iIs;
            }

            #endregion
        }
    }
}
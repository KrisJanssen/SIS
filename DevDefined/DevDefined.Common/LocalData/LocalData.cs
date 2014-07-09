// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LocalData.cs" company="">
//   
// </copyright>
// <summary>
//   The local.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region license

// Copyright (c) 2005 - 2007 Ayende Rahien (ayende@ayende.com)
// All rights reserved.
// Redistribution and use in source and binary forms, with or without modification,
// are permitted provided that the following conditions are met:
//     * Redistributions of source code must retain the above copyright notice,
//     this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright notice,
//     this list of conditions and the following disclaimer in the documentation
//     and/or other materials provided with the distribution.
//     * Neither the name of Ayende Rahien nor the names of its
//     contributors may be used to endorse or promote products derived from this
//     software without specific prior written permission.
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE
// FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
// DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
// SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
// CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
// OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
// THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
#endregion

namespace DevDefined.Common.LocalData
{
    using System;
    using System.Collections;
    using System.Web;

    /// <summary>
    /// The local.
    /// </summary>
    public class Local
    {
        #region Static Fields

        /// <summary>
        /// The local data hashtable key.
        /// </summary>
        private static readonly object LocalDataHashtableKey = new object();

        /// <summary>
        /// The current.
        /// </summary>
        private static readonly ILocalData current = new LocalData();

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the data.
        /// </summary>
        public static ILocalData Data
        {
            get
            {
                return current;
            }
        }

        #endregion

        /// <summary>
        /// The local data.
        /// </summary>
        private class LocalData : ILocalData
        {
            #region Static Fields

            /// <summary>
            /// The thread_hashtable.
            /// </summary>
            [ThreadStatic]
            private static Hashtable thread_hashtable;

            #endregion

            #region Properties

            /// <summary>
            /// Gets the local_ hashtable.
            /// </summary>
            private static Hashtable Local_Hashtable
            {
                get
                {
                    if (HttpContext.Current == null)
                    {
                        return thread_hashtable ?? (thread_hashtable = new Hashtable());
                    }

                    var web_hashtable = HttpContext.Current.Items[LocalDataHashtableKey] as Hashtable;
                    if (web_hashtable == null)
                    {
                        HttpContext.Current.Items[LocalDataHashtableKey] = web_hashtable = new Hashtable();
                    }

                    return web_hashtable;
                }
            }

            #endregion

            #region Public Indexers

            /// <summary>
            /// The this.
            /// </summary>
            /// <param name="key">
            /// The key.
            /// </param>
            /// <returns>
            /// The <see cref="object"/>.
            /// </returns>
            public object this[object key]
            {
                get
                {
                    return Local_Hashtable[key];
                }

                set
                {
                    Local_Hashtable[key] = value;
                }
            }

            #endregion

            #region Public Methods and Operators

            /// <summary>
            /// The clear.
            /// </summary>
            public void Clear()
            {
                Local_Hashtable.Clear();
            }

            #endregion
        }
    }
}
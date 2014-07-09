/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) dotPDN LLC, Rick Brewster, Tom Jackson, and contributors.     //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

namespace SIS.Base
{
    using System;

    public class EventArgs<T>
        : EventArgs
    {
        private T data;
        public T Data
        {
            get
            {
                return this.data;
            }
        }

        public EventArgs(T data)
        {
            this.data = data;
        }
    }
}

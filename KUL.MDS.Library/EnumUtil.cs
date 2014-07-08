using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KUL.MDS.Library
{
    public static class EnumUtil
    {
        /// <summary>
        /// This method can be used to run through the 'elements' of an Enum.
        /// 
        /// E.g. var values = EnumUtil.GetValues<Foos>();
        /// </summary>
        /// <typeparam name="T">The enum type</typeparam>
        /// <returns>An array of the values of the constants of the enum.</returns>
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}

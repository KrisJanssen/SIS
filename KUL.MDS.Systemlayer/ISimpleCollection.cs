using System;
using System.Collections.Generic;
using System.Text;

namespace KUL.MDS.SystemLayer
{
    public interface ISimpleCollection<K, V>
    {
        V Get(K key);
        void Set(K key, V value);
    }
}
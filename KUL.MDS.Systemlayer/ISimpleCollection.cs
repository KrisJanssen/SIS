using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.SystemLayer
{
    public interface ISimpleCollection<K, V>
    {
        V Get(K key);
        void Set(K key, V value);
    }
}
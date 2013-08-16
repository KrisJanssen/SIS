using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace DevDefined.Common.Collections
{
  public class RingBuffer<T> : IEnumerable<T>
  {
    readonly int _size;
    T[] _buffer;
    int _end;
    int _start;

    public RingBuffer(int size)
    {
      _size = size;
      Clear();
    }

    public bool IsEmpty
    {
      get { return (_start == _end); }
    }

    public int Count
    {
      get { return IsEmpty ? 0 : ((_start > _end) ? ((_buffer.Length - _start) + _end) : (_end - _start)); }
    }

    public bool IsFull
    {
      get
      {
        return (Count == _size);
      }
    }

    public IEnumerator<T> GetEnumerator()
    {
      if (_end == _start) yield break;
      if (_end < _start)
      {
        for (int i = _start; i < _buffer.Length; i++)
        {
          yield return _buffer[i];
        }
        for (int i = 0; i < _end; i++)
        {
          yield return _buffer[i];
        }
      }
      else
      {
        for (int i = _start; i < _end; i++)
        {
          yield return _buffer[i];
        }
      }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    public void Add(T item)
    {
      bool full = IsFull;

      if (full)
      {
        _start = (_start + 1)%_buffer.Length;
      }

      _buffer[_end] = item;

      _end = (_end + 1)%_buffer.Length;

      if (full) Debug.Assert(Count == _size, "Added item to full buffer, Count should be equal to size");

      Debug.Assert(Count <= _size, "Count should be less than or equal to size");
    }

    public void Clear()
    {
      _start = 0;
      _end = 0;
      _buffer = new T[_size + 1];
    }
  }
}
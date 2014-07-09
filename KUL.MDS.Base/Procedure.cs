using System;

namespace SIS.Base
{
    public delegate void Procedure();
    public delegate void Procedure<T>(T t);
    public delegate void Procedure<T, U>(T t, U u);
    public delegate void Procedure<T, U, V>(T t, U u, V v);
    public delegate void Procedure<T, U, V, W>(T t, U u, V v, W w);
}

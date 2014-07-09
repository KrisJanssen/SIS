using System;

namespace SIS.Base
{
    public delegate R Function<R>();
    public delegate R Function<R, T>(T t);
    public delegate R Function<R, T, U>(T t, U u);
}

using System;

namespace SIS.Base
{
    public static class Do
    {
        public static void Test<T>(
            T value,
            Function<bool, T> testFn,
            Procedure<T> ifTrueFn,
            Procedure<T> ifFalseFn)
        {
            (testFn(value) ? ifTrueFn : ifFalseFn)(value);
        }

        public static void GenerateTest<T>(
            Function<T> generate,
            Function<bool, T> test,
            Procedure<T> ifTrue,
            Procedure<T> ifFalse)
        {
            Test(generate(), test, ifTrue, ifFalse);
        }

        public static bool TryBool(Procedure actionProcedure)
        {
            try
            {
                actionProcedure();
                return true;
            }

            catch (Exception)
            {
                return false;
            }
        }

        public static T TryCatch<T>(
            Function<T> actionFunction,
            Function<T, Exception> catchClause)
        {
            T returnVal;

            try
            {
                returnVal = actionFunction();
            }

            catch (Exception ex)
            {
                returnVal = catchClause(ex);
            }

            return returnVal;
        }
    }
}

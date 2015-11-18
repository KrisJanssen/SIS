namespace SIS.ScanModes
{
    public struct Trigger
    {
        public readonly bool Active;
        public readonly TriggerType Type;
        public readonly int Start, End;

        public Trigger(bool a, TriggerType t, int s, int e)
        {
            Active = a;
            Type = t;
            Start = s;
            End = e;
        }
    }
}

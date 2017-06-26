namespace Slang
{
    public struct SlangEntryPoint
    {
        public SlangEntryPoint(string name, int opIndex)
        {
            this.Name = name;
            this.OpIndex = opIndex;
        }

        public string Name
        {
            get;
        }

        public int OpIndex
        {
            get;
        }
    }
}

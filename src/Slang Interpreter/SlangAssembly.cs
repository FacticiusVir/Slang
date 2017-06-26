namespace Slang
{
    public class SlangAssembly
    {
        public SlangAssembly(Op[] program, SlangEntryPoint[] entryPoints)
        {
            this.Program = program;
            this.EntryPoints = entryPoints;
        }

        public Op[] Program
        {
            get;
        }

        public SlangEntryPoint[] EntryPoints
        {
            get;
        }
    }
}

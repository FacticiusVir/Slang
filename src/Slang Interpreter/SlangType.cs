namespace Slang
{
    public class SlangType
    {
        private readonly string name;

        public SlangType(string name)
        {
            this.name = name;
        }

        public override string ToString()
        {
            return this.name;
        }

        public readonly static SlangType TypeDeclaration = new SlangType("TypeDeclaration");

        public readonly static SlangType ExpressionTree = new SlangType("ExpressionTree");

        public readonly static SlangType Assembly = new SlangType("Assembly");
    }
}
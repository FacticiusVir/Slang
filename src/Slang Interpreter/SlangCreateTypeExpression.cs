namespace Slang
{
    public class SlangCreateTypeExpression
        : SlangExpression
    {
        public SlangCreateTypeExpression(SlangType type)
        {
            this.Type = type;
        }

        public SlangType Type
        {
            get;
        }
    }
}
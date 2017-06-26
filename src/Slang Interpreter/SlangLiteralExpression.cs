namespace Slang
{
    public class SlangLiteralExpression
        : SlangExpression
    {
        public SlangLiteralExpression(SlangExpression typeExpression, object value)
        {
            this.TypeExpression = typeExpression;
            this.Value = value;
        }

        public SlangExpression TypeExpression
        {
            get;
        }

        public object Value
        {
            get;
        }

        public override string ToString()
        {
            return this.Value.ToString();
        }
    }
}
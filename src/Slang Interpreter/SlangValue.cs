namespace Slang
{
    public struct SlangValue
    {
        public SlangType Type;
        public object Value;

        public override string ToString()
        {
            return $"Type: {this.Type}, Value: {this.Value}";
        }
    }
}

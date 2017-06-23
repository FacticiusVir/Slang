namespace Slang
{
    public class PrimitiveType
        : SlangType
    {
        public PrimitiveType()
            : base(null)
        {
        }

        public PrimitiveFormat Format;
        public int Size;

        public override string ToString()
        {
            return $"{this.Format}/{this.Size}";
        }
    }

    public enum PrimitiveFormat
    {
        UnsignedInteger,
        SignedInteger,
        FloatingPoint
    }
}

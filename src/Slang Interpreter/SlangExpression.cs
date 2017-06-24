namespace Slang
{
    public class SlangExpression
    {
        public SlangExpression(Op[] program, int entryPoint)
        {
            this.Program = program;
            this.EntryPoint = entryPoint;
        }

        public Op[] Program
        {
            get;
        }

        public int EntryPoint
        {
            get;
        }

        public override string ToString()
        {
            return $"Expression{{{ToString(this.Program, this.EntryPoint)}}}";
        }

        private static string ToString(Op[] program, int entryPoint)
        {
            var op = program[entryPoint];

            switch (op.Type)
            {
                case OpType.Add:
                    return $"{ToString(program, op.Operands[0])} + {ToString(program, op.Operands[1])}";
                case OpType.Literal:
                    return op.Operands[1].ToString();
            }

            return $"{op.Type}/[{string.Join(", ", op.Operands)}]";
        }
    }
}

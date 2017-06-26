namespace Slang
{
    public class ExpressionBuilder
    {
        public SlangExpression Build(SlangAssembly assembly, int entryPoint)
        {
            var op = assembly.Program[entryPoint];

            switch (op.Type)
            {
                case OpType.Add:
                    return BuildBinaryExpression(assembly, op);
                case OpType.Type:
                    return SlangExpression.CreateType(new PrimitiveType
                    {
                        Format = PrimitiveFormat.SignedInteger,
                        Size = 4
                    });
                case OpType.Literal:
                    return SlangExpression.Literal(this.Build(assembly, op.Operands[0]), op.Operands[1]);
                default:
                    throw new System.NotImplementedException();
            }
        }

        private SlangExpression BuildBinaryExpression(SlangAssembly assembly, Op op)
        {
            var left = this.Build(assembly, op.Operands[0]);
            var right = this.Build(assembly, op.Operands[1]);

            switch(op.Type)
            {
                case OpType.Add:
                    return SlangExpression.Add(left, right);
            }

            throw new System.NotImplementedException();
        }
    }
}

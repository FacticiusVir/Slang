using System;

namespace Slang
{
    class Program
    {
        static void Main(string[] args)
        {
            var program = new Op[]
            {
                new Op
                {
                    Type = OpType.Type,
                    Operands = new[] { 0 }
                },
                new Op
                {
                    Type = OpType.Literal,
                    Operands = new[] { 0, 1234 }
                },
                new Op
                {
                    Type = OpType.Add,
                    Operands = new[] { 1, 1 }
                },
                new Op
                {
                    Type = OpType.MakeExpression,
                    Operands = new[] { 2 }
                }
            };

            Console.WriteLine(Evaluate(program, 0));
            Console.WriteLine(Evaluate(program, 1));
            Console.WriteLine(Evaluate(program, 2));
            Console.WriteLine(Evaluate(program, 3));

            Console.ReadLine();
        }

        private static SlangValue Evaluate(Op[] program, int entryPoint)
        {
            var op = program[entryPoint];

            switch (op.Type)
            {
                case OpType.Type:
                    return new SlangValue
                    {
                        Type = SlangType.TypeDeclaration,
                        Value = new PrimitiveType
                        {
                            Format = PrimitiveFormat.SignedInteger,
                            Size = 4
                        }
                    };
                case OpType.Literal:
                    return new SlangValue
                    {
                        Type = (SlangType)Evaluate(program, op.Operands[0]).Value,
                        Value = op.Operands[1]
                    };
                case OpType.MakeExpression:
                    return new SlangValue
                    {
                        Type = SlangType.ExpressionTree,
                        Value = Tuple.Create(program, op.Operands[0])
                    };
                case OpType.Add:
                    var left = Evaluate(program, op.Operands[0]);
                    var right = Evaluate(program, op.Operands[1]);

                    return new SlangValue
                    {
                        Type = left.Type,
                        Value = (int)left.Value + (int)right.Value
                    };
            }

            throw new NotSupportedException();
        }
    }
}
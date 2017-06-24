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
                },
                new Op
                {
                    Type = OpType.InvokeExternal,
                    Operands = new[] { 0, 3 }
                },
                new Op
                {
                    Type = OpType.InvokeExternal,
                    Operands = new[] { 1, 3 }
                }
            };
            
            Console.WriteLine(Evaluate(program, 5));

            Console.ReadLine();
        }

        private static SlangType Int32 = new PrimitiveType
        {
            Format = PrimitiveFormat.SignedInteger,
            Size = 4
        };

        private static SlangValue Evaluate(Op[] program, int entryPoint)
        {
            var op = program[entryPoint];

            switch (op.Type)
            {
                case OpType.Type:
                    return new SlangValue
                    {
                        Type = SlangType.TypeDeclaration,
                        Value = Int32
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
                        Value = new SlangExpression(program, op.Operands[0])
                    };
                case OpType.Add:
                    var left = Evaluate(program, op.Operands[0]);
                    var right = Evaluate(program, op.Operands[1]);

                    return new SlangValue
                    {
                        Type = left.Type,
                        Value = (int)left.Value + (int)right.Value
                    };
                case OpType.InvokeExternal:
                    switch(op.Operands[0])
                    {
                        case 0:
                            var expression = (SlangExpression)Evaluate(program, op.Operands[1]).Value;
                            return Evaluate(expression.Program, expression.EntryPoint);
                        case 1:
                            Console.WriteLine(" > " + Evaluate(program, op.Operands[1]).Value);
                            break;
                        default:
                            return new SlangValue
                            {
                                Type = Int32,
                                Value = 1
                            };
                    }

                    return new SlangValue
                    {
                        Type = Int32,
                        Value = 0
                    };
            }

            throw new NotSupportedException();
        }
    }
}
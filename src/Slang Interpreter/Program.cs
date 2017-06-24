using System;
using System.Diagnostics;
using System.IO;

namespace Slang
{
    class Program
    {
        static void Main(string[] args)
        {
            var program = new Op[]
            {
                // 0
                new Op
                {
                    Type = OpType.Type,
                    Operands = new[] { 0 }
                },
                // 1
                new Op
                {
                    Type = OpType.Literal,
                    Operands = new[] { 0, 1234 }
                },
                // 2
                new Op
                {
                    Type = OpType.Add,
                    Operands = new[] { 1, 1 }
                },
                // 3
                new Op
                {
                    Type = OpType.MakeExpression,
                    Operands = new[] { 2 }
                },
                // 4
                new Op
                {
                    Type = OpType.InvokeExternal,
                    Operands = new[] { 0, 3 }
                },
                // 5
                new Op
                {
                    Type = OpType.InvokeExternal,
                    Operands = new[] { 1, 3 }
                },
                // 6
                new Op
                {
                    Type = OpType.InvokeExternal,
                    Operands = new[] { 2, 3 }
                },
                // 7
                new Op
                {
                    Type = OpType.InvokeExternal,
                    Operands = new[] { 3 }
                },
                // 8
                new Op
                {
                    Type = OpType.InvokeExternal,
                    Operands = new[] { 1, 7 }
                },
                // 9
                new Op
                {
                    Type = OpType.InvokeExternal,
                    Operands = new[] { 0, 7 }
                },
            };

            Console.WriteLine(Evaluate(program, 6));
            Console.WriteLine(Evaluate(program, 7));
            Console.WriteLine(Evaluate(program, 8));
            Console.WriteLine(Evaluate(program, 9));

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
                    int result = 0;

                    switch (op.Operands[0])
                    {
                        case 0:
                            var expression = (SlangExpression)Evaluate(program, op.Operands[1]).Value;
                            return Evaluate(expression.Program, expression.EntryPoint);
                        case 1:
                            WriteToConsole(program, op.Operands[1]);
                            break;
                        case 2:
                            Serialise(program, op.Operands[1]);
                            break;
                        case 3:
                            return new SlangValue
                            {
                                Type = SlangType.ExpressionTree,
                                Value = Deserialise()
                            };
                        default:
                            result = 1;
                            break;
                    }

                    return new SlangValue
                    {
                        Type = Int32,
                        Value = result
                    };
            }

            throw new NotSupportedException();
        }

        private static void WriteToConsole(Op[] program, int entryPoint)
        {
            Console.WriteLine(" > " + Evaluate(program, entryPoint).Value);
        }

        private static void Serialise(Op[] program, int entrypoint)
        {
            var expression = (SlangExpression)Evaluate(program, entrypoint).Value;

            using (var file = File.Create(".\\output.slbin"))
            {
                using (var writer = new BinaryWriter(file))
                {
                    writer.Write(1);
                    writer.Write(expression.EntryPoint);
                    writer.Write(expression.Program.Length);

                    foreach (var op in expression.Program)
                    {
                        writer.Write((int)op.Type);
                        writer.Write(op.Operands.Length);

                        foreach (var operand in op.Operands)
                        {
                            writer.Write(operand);
                        }
                    }
                }
            }
        }

        private static SlangExpression Deserialise()
        {
            using (var file = File.OpenRead(".\\output.slbin"))
            {
                using (var reader = new BinaryReader(file))
                {
                    Debug.Assert(reader.ReadInt32() == 1);
                    int entryPoint = reader.ReadInt32();
                    int opCount = reader.ReadInt32();

                    var program = new Op[opCount];

                    for (int opIndex = 0; opIndex < opCount; opIndex++)
                    {
                        OpType type = (OpType)reader.ReadInt32();

                        int operandCount = reader.ReadInt32();

                        var operands = new int[operandCount];

                        for (int operandIndex = 0; operandIndex < operandCount; operandIndex++)
                        {
                            operands[operandIndex] = reader.ReadInt32();
                        }

                        program[opIndex] = new Op
                        {
                            Type = type,
                            Operands = operands
                        };
                    }

                    return new SlangExpression(program, entryPoint);
                }
            }
        }
    }
}
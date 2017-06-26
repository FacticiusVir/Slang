using System;

namespace Slang
{
    public abstract class SlangExpression
    {
        public static SlangBinaryOperatorExpression Add(SlangExpression left, SlangExpression right)
        {
            return new SlangBinaryOperatorExpression(SlangOperator.Add, left, right);
        }

        public static SlangBinaryOperatorExpression Subtract(SlangExpression left, SlangExpression right)
        {
            return new SlangBinaryOperatorExpression(SlangOperator.Subtract, left, right);
        }

        public static SlangBinaryOperatorExpression Multiply(SlangExpression left, SlangExpression right)
        {
            return new SlangBinaryOperatorExpression(SlangOperator.Multiply, left, right);
        }

        public static SlangBinaryOperatorExpression Divide(SlangExpression left, SlangExpression right)
        {
            return new SlangBinaryOperatorExpression(SlangOperator.Divide, left, right);
        }

        public static SlangExpression CreateType(SlangType type)
        {
            return new SlangCreateTypeExpression(type);
        }

        public static SlangExpression Literal(SlangExpression typeExpression, object value)
        {
            return new SlangLiteralExpression(typeExpression, value);
        }
    }
}

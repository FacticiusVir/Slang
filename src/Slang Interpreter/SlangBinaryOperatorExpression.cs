namespace Slang
{
    public class SlangBinaryOperatorExpression
        : SlangExpression
    {
        public SlangBinaryOperatorExpression(SlangOperator op, SlangExpression left, SlangExpression right)
        {
            this.Op = op;
            this.Left = left;
            this.Right = right;
        }

        public SlangOperator Op
        {
            get;
        }

        public SlangExpression Left
        {
            get;
        }

        public SlangExpression Right
        {
            get;
        }

        public override string ToString()
        {
            string operatorString;

            switch(this.Op)
            {
                case SlangOperator.Add:
                    operatorString = "+";
                    break;
                default:
                    operatorString = this.Op.ToString();
                    break;
            }

            return $"{this.Left} {operatorString} {this.Right}";
        }
    }
}

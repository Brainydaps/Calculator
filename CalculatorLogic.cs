namespace Calculator
{
    public class CalculatorLogic
    {
        private double _number1;
        private double _number2;
        private string _operation = "";

        public double Number1 
        { 
            get => _number1; 
            set => _number1 = value; 
        }

        public double Number2 
        { 
            get => _number2; 
            set => _number2 = value; 
        }

        public string Operation 
        { 
            get => _operation; 
            set => _operation = value; 
        }

        public double PerformBasicOperation(double num1, string op, double num2)
        {
            return op switch
            {
                "÷" => num1 / num2,
                "×" => num1 * num2,
                "−" => num1 - num2,
                "+" => num1 + num2,
                "xʸ" => Math.Pow(num1, num2),
                "ʸ√x" => Math.Pow(num2, 1 / num1),
                _ => throw new ArgumentException($"Unknown operation: {op}")
            };
        }

        public double PerformTrigonometricOperation(double degrees, string trigOperation)
        {
            double radians = degrees * Math.PI / 180;
            return trigOperation switch
            {
                "cos" => Math.Cos(radians),
                "sin" => Math.Sin(radians),
                "tan" => Math.Tan(radians),
                _ => throw new ArgumentException($"Unknown trigonometric operation: {trigOperation}")
            };
        }

        public double CalculateSquare(double number)
        {
            return number * number;
        }

        public double CalculateSquareRoot(double number)
        {
            return Math.Sqrt(number);
        }

        public double CalculatePower(double baseNumber, double exponent)
        {
            return Math.Pow(baseNumber, exponent);
        }

        public double CalculateNthRoot(double rootDegree, double number)
        {
            return Math.Pow(number, 1 / rootDegree);
        }

        public void Clear()
        {
            _number1 = 0;
            _number2 = 0;
            _operation = "";
        }

        public double ExecuteStoredOperation()
        {
            if (string.IsNullOrEmpty(_operation))
                throw new InvalidOperationException("No operation is set");

            var result = PerformBasicOperation(_number1, _operation, _number2);
            _number1 = result; // Store result for consecutive operations
            _operation = "";
            return result;
        }

        public void SetOperation(double firstNumber, string op)
        {
            _number1 = firstNumber;
            _operation = op;
        }
    }
}

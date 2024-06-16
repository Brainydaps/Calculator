using System;
using Microsoft.Maui.Controls;

namespace Calculator
{
    public partial class MainPage : ContentPage
    {
        string displayText = "";
        string tempDisplay = "";
        double number1 = 0;
        double number2 = 0;
        string operation = "";

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        void Button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string text = button.Text;

            switch (text)
            {
                case "=":
                    CalculateResult();
                    break;
                case "/":
                case "x":
                case "-":
                case "+":
                case "x^y":
                case "y√x":
                    SetOperation(text);
                    break;
                case "cos":
                case "sin":
                case "tan":
                    PerformTrigonometricOperation(text);
                    break;
                case "x²":
                    CalculateSquare();
                    break;
                case "√":
                    CalculateSquareRoot();
                    break;
                case "C":
                    ClearDisplay();
                    break;
                default:
                    AppendToDisplay(text);
                    break;
            }
        }

        void SetOperation(string op)
        {
            if (double.TryParse(displayText, out number1))
            {
                operation = op;
                tempDisplay = displayText + " " + op + " ";
                UpdateDisplay();
                displayText = "";
            }
        }

        void PerformTrigonometricOperation(string op)
        {
            if (double.TryParse(displayText, out number1))
            {
                double result = 0;

                switch (op)
                {
                    case "cos":
                        result = Math.Cos(number1 * Math.PI / 180); // Assuming input in degrees
                        break;
                    case "sin":
                        result = Math.Sin(number1 * Math.PI / 180); // Assuming input in degrees
                        break;
                    case "tan":
                        result = Math.Tan(number1 * Math.PI / 180); // Assuming input in degrees
                        break;
                }

                displayText = result.ToString();
                UpdateDisplay();
                operation = ""; // Clear operation after performing trigonometric calculation
                tempDisplay = "";
            }
        }

        void CalculateSquare()
        {
            if (double.TryParse(displayText, out number1))
            {
                displayText = (number1 * number1).ToString();
                UpdateDisplay();
                tempDisplay = "";
            }
        }

        void CalculateSquareRoot()
        {
            if (double.TryParse(displayText, out number1))
            {
                displayText = Math.Sqrt(number1).ToString();
                UpdateDisplay();
                tempDisplay = "";
            }
        }

        void ClearDisplay()
        {
            displayText = "";
            tempDisplay = "";
            UpdateDisplay();
            number1 = 0;
            number2 = 0;
            operation = "";
        }

        void AppendToDisplay(string text)
        {
            displayText += text;
            UpdateDisplay();
        }

        void UpdateDisplay()
        {
            tempDisplayLabel.Text = tempDisplay;
            displayTextLabel.Text = displayText;
        }

        void CalculateResult()
        {
            if (double.TryParse(displayText, out number2))
            {
                double result = 0;

                switch (operation)
                {
                    case "/":
                        result = number1 / number2;
                        break;
                    case "x":
                        result = number1 * number2;
                        break;
                    case "-":
                        result = number1 - number2;
                        break;
                    case "+":
                        result = number1 + number2;
                        break;
                    case "x^y":
                        result = Math.Pow(number1, number2);
                        break;
                    case "y√x":
                        result = Math.Pow(number2, 1 / number1);
                        break;
                }

                displayText = result.ToString();
                UpdateDisplay();
                number1 = result; // Store result for consecutive operations
                displayText = "";
                operation = "";
                tempDisplay = "";
            }
        }
    }
}

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
        bool _caretBlinking;

        public string DisplayText
        {
            get { return displayText; }
            set
            {
                displayText = value;
                OnPropertyChanged(nameof(DisplayText));
            }
        }

        public string TempDisplay
        {
            get { return tempDisplay; }
            set
            {
                tempDisplay = value;
                OnPropertyChanged(nameof(TempDisplay));
            }
        }

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            StartCaretBlink();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _caretBlinking = false;
            if (caretLabel != null)
            {
                caretLabel.IsVisible = true;
                caretLabel.Opacity = 1;
            }
        }

        void StartCaretBlink()
        {
            _caretBlinking = true;
            // Use a lightweight timer to toggle visibility every 1800ms
            Device.StartTimer(TimeSpan.FromMilliseconds(1800), () =>
            {
                if (!_caretBlinking || caretLabel == null)
                    return false;

                caretLabel.IsVisible = !caretLabel.IsVisible;
                return true; // keep running
            });
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
                case "÷":
                case "×":
                case "−":
                case "+":
                case "xʸ":
                case "ʸ√x":
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
            if (double.TryParse(DisplayText, out number1))
            {
                operation = op;
                TempDisplay = DisplayText + " " + op + " ";
                DisplayText = "";
            }
        }

        void PerformTrigonometricOperation(string op)
        {
            if (double.TryParse(DisplayText, out number1))
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

                DisplayText = result.ToString();
                TempDisplay = "";
            }
        }

        void CalculateSquare()
        {
            if (double.TryParse(DisplayText, out number1))
            {
                DisplayText = (number1 * number1).ToString();
                TempDisplay = "";
            }
        }

        void CalculateSquareRoot()
        {
            if (double.TryParse(DisplayText, out number1))
            {
                DisplayText = Math.Sqrt(number1).ToString();
                TempDisplay = "";
            }
        }

        void ClearDisplay()
        {
            DisplayText = "";
            TempDisplay = "";
            number1 = 0;
            number2 = 0;
            operation = "";
        }

        void AppendToDisplay(string text)
        {
            DisplayText += text;
        }

        void CalculateResult()
        {
            if (double.TryParse(DisplayText, out number2))
            {
                double result = 0;

                switch (operation)
                {
                    case "÷":
                        result = number1 / number2;
                        break;
                    case "×":
                        result = number1 * number2;
                        break;
                    case "−":
                        result = number1 - number2;
                        break;
                    case "+":
                        result = number1 + number2;
                        break;
                    case "xʸ":
                        result = Math.Pow(number1, number2);
                        break;
                    case "ʸ√x":
                        result = Math.Pow(number2, 1 / number1);
                        break;
                }

                DisplayText = result.ToString();
                number1 = result; // Store result for consecutive operations
                TempDisplay = "";
                operation = "";
            }
        }
    }
}

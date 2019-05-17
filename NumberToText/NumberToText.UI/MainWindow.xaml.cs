using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NumberToText.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string decimalSeparator = System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;
        const string InvalidFromatMessage = "შემოყვანილი ტექსი არასწორ ფრომატშია";
        const string NumberOutOfRangeMessage = "თქვენ მიერ შემოყვანილი რიცხვი ზედმეტად დიდია";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text == decimalSeparator && InputNumber.Text.Contains(decimalSeparator))
                e.Handled = true;
            else if (e.Text != decimalSeparator && !e.Text.All(char.IsDigit))
                e.Handled = true;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(InputNumber.Text))
                {
                    var number = double.Parse(InputNumber.Text);
                    var (wholePart, decimalPart) = NumberToTextConverter.Convert(number);
                    OutputNumber.Text = $"{wholePart} ლარი და {decimalPart} თეთრი";
                }
            }
            catch (FormatException)
            {
                OutputNumber.Text = InvalidFromatMessage;
            }
            catch (ArgumentOutOfRangeException)
            {
                OutputNumber.Text = NumberOutOfRangeMessage;
            }
        }
    }
}

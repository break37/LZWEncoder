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

namespace LZWEncoder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Properties

        private string ToEncode { get { return tboxToEncode.Text; } }

        private string P {
            set { tblockP.Text = $"P = {value}"; }
        }

        private string C {
            set { tblockC.Text = $"C = {value}"; }
        }

        private string PpC {
            set { tblockPPlusC.Text = $"P + C = {value}"; }
        }

        private string Code {
            get { return tboxCode.Text; }
            set { tboxCode.Text = value; }
        }

        #endregion

        List<string> dictionary;
        string p, c;

        public MainWindow()
        {
            InitializeComponent();
        }


        private void BtnEncode_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();

            CreateDictionary();

            foreach (var item in ToEncode)
            {
                c = item.ToString();
                PpC = $"{p}{c}";

                if (dictionary.Contains($"{p}{c}"))
                {
                    p = $"{p}{c}";
                }
                else
                {
                    dictionary.Add($"{p}{c}");
                    Code += $"{(dictionary.IndexOf(p) + 1).ToString()}, ";
                    p = c;
                }

                P = p;
                C = c;
                PpC = $"{p}{c}";
                RefreshDictionary(dictionary);
                System.Threading.Thread.Sleep(100);
            }

            Code += (dictionary.IndexOf(p) + 1).ToString();
        }

        private void CreateDictionary()
        {
            //search for all characters occuring in given text
            foreach (var item in ToEncode)
            {
                if (!dictionary.Contains(item.ToString()))
                {
                    dictionary.Add(item.ToString());
                }

                dictionary.Sort();
            }
        }

        private void RefreshDictionary(List<string> dataSource)
        {
            lboxDictionary.ItemsSource = null;
            lboxDictionary.ItemsSource = dataSource;
        }

        private void ClearForm()
        {
            dictionary = new List<string>();
            p = c = Code = "";
            lboxDictionary.ItemsSource = null;
        }
    }
}

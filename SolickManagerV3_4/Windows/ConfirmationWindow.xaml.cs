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
using System.Windows.Shapes;

namespace SolickManagerV3_4.Windows
{
    /// <summary>
    /// Логика взаимодействия для ConfirmationWindow.xaml
    /// </summary>
    public partial class ConfirmationWindow : Window
    {
        public string MessageText { get; set; }

        public ConfirmationWindow(string messageText)
        {
            InitializeComponent();
            MessageText = messageText;

            DataContext = this;
        }

        private void YesButton(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void NoButton(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

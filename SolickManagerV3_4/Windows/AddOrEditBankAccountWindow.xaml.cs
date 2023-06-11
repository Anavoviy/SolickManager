using SolickManagerV3_4.DTO;
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
    /// Логика взаимодействия для AddOrEditBankAccountWindow.xaml
    /// </summary>
    public partial class AddOrEditBankAccountWindow : Window
    {
        private string addBalance = "";

        public string AddTitle { get; set; } = "";
        public string AddBalance { get => addBalance; set { addBalance = value; ValidBalance(); } }
        public AddOrEditBankAccountWindow()
        {
            InitializeComponent();

            DataContext = this;
        }

        private void ValidBalance()
        {
            decimal balance;
            if (decimal.TryParse(AddBalance, out balance))
            {
                BalanceTextBox.Foreground = new SolidColorBrush(Colors.Black);
                SaveButton.IsEnabled = true;
            }
            else
            {
                BalanceTextBox.Foreground = new SolidColorBrush(Colors.Red);
                SaveButton.IsEnabled = false;
            }
        }

        private void SaveBankAccount(object sender, RoutedEventArgs e)
        {
            if(AddTitle.Length > 0) 
            {
                DB.Instance.Bankaccounts.Add(new Bankaccount()
                {
                    Title = AddTitle,
                    Balance = decimal.Parse(AddBalance)
                });
                DB.Instance.SaveChanges();

                MessageBox.Show("Успешно добавлен новый счёт!");

                this.Close();
            }
        }
    }
}

using SolickManagerV3_4.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Логика взаимодействия для AddOrEditOperationWindow.xaml
    /// </summary>
    public partial class AddOrEditOperationWindow : Window, INotifyPropertyChanged
    {
        private string dataOperation = "";
        private string amountOperation = "";

        public event PropertyChangedEventHandler? PropertyChanged;
        void Signal([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        // Основная информация
        public string DataOperation { get => dataOperation; set { dataOperation = value; ValidData(); Signal(); } }
        public string AmountOperation { get => amountOperation; set { amountOperation = value; ValidAmount(); Signal(); } }
        public string ObjectOperation { get; set; } = "";

        public List<Bankaccount> BankAccounts { get; set; }
        public Bankaccount DebetBankAccount { get; set; }
        public Bankaccount CreditBankAccount { get; set; }

        public AddOrEditOperationWindow()
        {
            InitializeComponent();

            BankAccounts = DB.Instance.Bankaccounts.Where(s => s.Deleted == false).OrderBy(s => s.Title).ToList();

            DataContext = this;
        }

        private void ValidData()
        {
            DateOnly data;
            if (DateOnly.TryParse(DataOperation, out data))
            {
                DataTextBox.Foreground = new SolidColorBrush(Colors.Black);
                SaveButton.IsEnabled = true;
            }
            else
            {
                DataTextBox.Foreground = new SolidColorBrush(Colors.Red);
                SaveButton.IsEnabled = false;
            }
        }
        private void ValidAmount()
        {
            decimal amount;
            if (decimal.TryParse(AmountOperation, out amount))
            {
                AmountTextBox.Foreground = new SolidColorBrush(Colors.Black);
                SaveButton.IsEnabled = true;
            }
            else
            {
                AmountTextBox.Foreground = new SolidColorBrush(Colors.Red);
                SaveButton.IsEnabled = false;
            }
        }

        private void SaveOperation(object sender, RoutedEventArgs e)
        {
            if((DebetBankAccount != null || CreditBankAccount != null) && DataOperation != "" && AmountOperation != "")
            {
                Operation operation = new Operation();

                if (CreditBankAccount != null)
                    operation.Credit = CreditBankAccount.Id;
                if (DebetBankAccount != null)
                    operation.Debet = DebetBankAccount.Id;

                operation.Dataopen = DateOnly.Parse(DataOperation);
                operation.Amount = decimal.Parse(AmountOperation);
                operation.Object = ObjectOperation;
                operation.Status = "Создана";

                DB.Instance.Operations.Add(operation);
                DB.Instance.SaveChanges();

                MessageBox.Show("Операция успешно добавлена!");

                this.Close();
            }
        }

        private void SetToday(object sender, RoutedEventArgs e)
        {
            DataOperation = OtherFunctons.Instance.DateOnlyNow().ToString();
            Signal(nameof(DataOperation));
        }
    }
}

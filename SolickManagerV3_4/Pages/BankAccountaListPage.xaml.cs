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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SolickManagerV3_4.Windows;

namespace SolickManagerV3_4.Pages
{
    /// <summary>
    /// Логика взаимодействия для BankAccountaListPage.xaml
    /// </summary>
    public partial class BankAccountaListPage : Page, INotifyPropertyChanged
    {
        private Bankaccount selectedBankAccount = new Bankaccount();
        private string searchTitle = "";

        public event PropertyChangedEventHandler? PropertyChanged;
        void Signal([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        // Работник за приложением
        public Worker Worker { get; set; }

        // Основные данные
        public List<Bankaccount> Bankaccounts { get; set; }
        public Bankaccount SelectedBankAccount { get => selectedBankAccount; set { selectedBankAccount = value; Signal(); } }
        // Поиск
        public string SearchTitle { get => searchTitle; set { searchTitle = value; Search(); } }
        public BankAccountaListPage(Worker worker)
        {
            InitializeComponent();

            Worker = worker;

            Search();

            DataContext = this;
        }

        public void Search()
        {
            var result = DB.Instance.Bankaccounts.Where(s => (SearchTitle == "" || s.Title.ToLower().Contains(SearchTitle.ToLower())) && s.Deleted == false);

            Bankaccounts = result.OrderBy(s => s.Id).ToList();

            if (SelectedBankAccount != null)
                SelectedBankAccount = Bankaccounts.FirstOrDefault(s => s.Id == SelectedBankAccount.Id);

            Signal(nameof(SelectedBankAccount));
            Signal(nameof(Bankaccounts));
        }

        private void AddNewBankAccount(object sender, RoutedEventArgs e)
        {
            new AddOrEditBankAccountWindow().ShowDialog();

            Search();
        }

        private void DeleteSelectedBankAccount(object sender, RoutedEventArgs e)
        {
            if (SelectedBankAccount != null)
            {
                SelectedBankAccount.Deleted = true;

                DB.Instance.Bankaccounts.Update(SelectedBankAccount);
                DB.Instance.SaveChanges();

                Search();
            }
        }

        private void SaveEditSelectedBancAccount(object sender, RoutedEventArgs e)
        {
            if (SelectedBankAccount != null)
            {
                DB.Instance.Bankaccounts.Update(SelectedBankAccount);
                DB.Instance.SaveChanges();

                MessageBox.Show("Выбранный счёт успешно изменён!");

                Search();
            }
        }
    }
}

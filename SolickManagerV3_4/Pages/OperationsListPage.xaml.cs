using Microsoft.EntityFrameworkCore;
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
    /// Логика взаимодействия для OperationsListPage.xaml
    /// </summary>
    public partial class OperationsListPage : Page, INotifyPropertyChanged
    {
        private Bankaccount searchDebet;
        private Bankaccount searchCredit;
        private string searchDataStart = "";
        private string searchDataEnd = "";
        private string searchObject = "";
        private Operation selectedOperation;
        private string dataOperation = "";
        private string editAmount = "";
        private string editStatus;

        public event PropertyChangedEventHandler? PropertyChanged;
        void Signal([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        // Работник за приложением
        public Worker Worker { get; set; }

        // Основные данные
        public List<Operation> Operations { get; set; }
        public Operation SelectedOperation
        {
            get => selectedOperation;
            set
            {
                selectedOperation = value;

                if (SelectedOperation != null)
                {
                    DataOperation = SelectedOperation.Dataclose.ToString();
                    EditAmount = SelectedOperation.Amount.ToString();
                    EditStatus = SelectedOperation.Status;

                    if (SelectedOperation.Status == "Завершена")
                    {
                        DataTextBox.IsEnabled = false;
                        CostTextBox.IsEnabled = false;
                        StatusesComboBox.IsEnabled = false;
                        ObjectTextBox.IsEnabled = false;
                        SaveButton.IsEnabled = false;
                    }
                    else
                    {
                        DataTextBox.IsEnabled = true;
                        CostTextBox.IsEnabled = true;
                        StatusesComboBox.IsEnabled = true;
                        ObjectTextBox.IsEnabled = true;
                        SaveButton.IsEnabled = true;
                    }
                }
                else
                {
                    DataOperation = "";
                    EditAmount = "";
                }
            }
        }

        // Поиск
        public string SearchDataStart { get => searchDataStart; set { searchDataStart = value; Search(); } }
        public string SearchDataEnd { get => searchDataEnd; set { searchDataEnd = value; Search(); } }
        public string SearchObject { get => searchObject; set { searchObject = value; Search(); } }
        public List<Bankaccount> Bankaccounts { get; set; }
        public Bankaccount SearchDebet { get => searchDebet; set { searchDebet = value; Search(); } }
        public Bankaccount SearchCredit { get => searchCredit; set { searchCredit = value; Search(); } }

        public List<string> Statuses { get; set; } = new List<string>() { "Все статусы", "Не завершённые", "Создана", "Установлена", "Частично оплачена", "Просрочена", "Завершена" };
        public string SearchStatus { get; set; } = "Все статусы";

        // Быстрое редактирование
        public List<string> EditStatuses { get; set; } = new List<string>() { "Создана", "Установлена", "Частично оплачена", "Просрочена", "Завершена" };
        public string EditStatus { get => editStatus; set { editStatus = value; Signal(); Signal(nameof(EditStatuses)); } }
        public string DataOperation { get => dataOperation; set { dataOperation = value; ValidData(); Signal();  } }
        public string EditAmount { get => editAmount; set { editAmount = value;  ValidAmount(); Signal(); } }

        public OperationsListPage(Worker worker)
        {
            InitializeComponent();

            Worker = worker;

            Bankaccounts = DB.Instance.Bankaccounts.Where(s => s.Deleted == false).OrderBy(s => s.Title).ToList();

            Search();

            DataContext = this;
        }

        private void Search()
        {
            var result = DB.Instance.Operations.Include(s => s.DebetNavigation).Include(s => s.CreditNavigation).Where(s => (SearchDebet == null || s.Debet == SearchDebet.Id) &&
                                                                                                                            (SearchCredit == null || s.Credit == SearchCredit.Id) &&
                                                                                                                            (Statuses.IndexOf(SearchStatus) == 0 ||
                                                                                                                            (Statuses.IndexOf(SearchStatus) == 1 && s.Status != "Завершена") ||
                                                                                                                            (Statuses.IndexOf(SearchStatus) > 1 && s.Status == SearchStatus)) &&
                                                                                                                            s.Deleted == false);
            DateOnly dataStart;
            DateOnly dataEnd;

            if (DateOnly.TryParse(SearchDataStart, out dataStart) && !DateOnly.TryParse(SearchDataEnd, out dataEnd))
                Operations = result.Where(s => s.Dataopen > dataStart).OrderBy(s => s.Id).ToList();
            else if (!DateOnly.TryParse(SearchDataStart, out dataStart) && DateOnly.TryParse(SearchDataEnd, out dataEnd))
                Operations = result.Where(s => s.Dataopen < dataEnd).OrderBy(s => s.Id).ToList();
            else if (DateOnly.TryParse(SearchDataStart, out dataStart) && DateOnly.TryParse(SearchDataEnd, out dataEnd))
                Operations = result.Where(s => s.Dataopen < dataEnd && s.Dataopen > dataStart).OrderBy(s => s.Id).ToList();
            else
                Operations = result.OrderBy(s => s.Id).ToList();

            if (SelectedOperation != null)
                SelectedOperation = Operations.FirstOrDefault(s => s.Id == SelectedOperation.Id);

            Signal(nameof(Operations));
            Signal(nameof(SelectedOperation));
        }

        private void ValidAmount()
        {
            decimal amount;
            if (decimal.TryParse(EditAmount, out amount) && EditAmount != null)
            {
                CostTextBox.Foreground = new SolidColorBrush(Colors.Black);
                SaveButton.IsEnabled = true;
            }
            else
            {
                CostTextBox.Foreground = new SolidColorBrush(Colors.Red);
                SaveButton.IsEnabled = false;
            }
        }
        private void ValidData()
        {
            DateOnly data;
            if (DateOnly.TryParse(DataOperation, out data) && DataOperation != null)
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

        private void AddNewOperation(object sender, RoutedEventArgs e)
        {
            new AddOrEditOperationWindow().ShowDialog();

            Search();
        }

        private void DeleteSelectedOperation(object sender, RoutedEventArgs e)
        {
            if (SelectedOperation != null)
            {
                SelectedOperation.Deleted = true;

                DB.Instance.Operations.Update(SelectedOperation);
                DB.Instance.SaveChanges();

                MessageBox.Show("Операция успешно удалена!");

                Search();
            }
        }

        private void SaveEditSelectedOperation(object sender, RoutedEventArgs e)
        {
            if (SelectedOperation != null)
            {
                SelectedOperation.Dataclose = DateOnly.Parse(DataOperation);
                SelectedOperation.Amount = decimal.Parse(EditAmount);
                SelectedOperation.Status = EditStatus;

                DB.Instance.Operations.Update(SelectedOperation);

                if (SelectedOperation.Status == "Завершена")
                {
                    if (SelectedOperation.Credit != 0 && SelectedOperation.Credit != null)
                    {
                        Bankaccount credit = DB.Instance.Bankaccounts.FirstOrDefault(s => s.Id == SelectedOperation.Credit);
                        credit.Balance -= SelectedOperation.Amount;
                        DB.Instance.Bankaccounts.Update(credit);
                    }
                    if (SelectedOperation.Debet != 0 && SelectedOperation.Debet != null)
                    {
                        Bankaccount debet = DB.Instance.Bankaccounts.FirstOrDefault(s => s.Id == SelectedOperation.Debet);
                        debet.Balance += SelectedOperation.Amount;
                        DB.Instance.Bankaccounts.Update(debet);
                    }
                }

                DB.Instance.SaveChanges();

                Search();
            }
        }
    }
}

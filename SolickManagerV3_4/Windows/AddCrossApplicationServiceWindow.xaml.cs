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
using Microsoft.EntityFrameworkCore;
using SolickManagerV3_4.DTO;

namespace SolickManagerV3_4.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddCrossApplicationServiceWindow.xaml
    /// </summary>
    public partial class AddCrossApplicationServiceWindow : Window, INotifyPropertyChanged
    {
        private string searchTitle = "";
        private string descripton = "";
        private decimal cost = 0;
        private string secondName = "";
        private string firstName = "";
        private string patronymic = "";

        public event PropertyChangedEventHandler? PropertyChanged;
        void Signal([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        // Данные новой услуги
        public string EditTitle { get => searchTitle; set { searchTitle = value; FilterService(); } }
        public string Description { get => descripton; set { descripton = value; FilterService(); } }
        public decimal Cost { get => cost; set { cost = value; FilterService(); ValidCost(); } }

        // Данные поиска сотрудника
        public string SecondName { get => secondName; set { secondName = value; FilterWorker(); } }
        public string FirstName { get => firstName; set { firstName = value; FilterWorker(); } }
        public string Patronymic { get => patronymic; set { patronymic = value; FilterWorker(); } }

        // Данные списка
        public List<Service> ServicesList { get; set; } = new List<Service>();
        public Service SelectedService { get; set; }

        public List<Worker> Workers { get; set; } = new List<Worker>();
        public Worker SelectedWorker { get; set; }

        public DTO.Application Application { get; set; }

        public AddCrossApplicationServiceWindow(DTO.Application application)
        {
            InitializeComponent();

            Application = application;

            FilterService();
            FilterWorker();

            DataContext = this;
        }

        private void FilterService()
        {
            var result = DB.Instance.Services.Where(s => (s.Title.ToLower().Contains(EditTitle.ToLower()) || EditTitle == "") &&
                                                         (Description == "" || s.Description.ToLower().Contains(Description.ToLower())) 
                                                         && s.Deleted == false);

            ServicesList = result.ToList();

            Signal(nameof(ServicesList));
        }
        private void FilterWorker()
        {
            var result = DB.Instance.Workers.Where(s => ((this.SecondName == "" || s.Surname.ToLower().Contains(this.SecondName.ToLower())) &&
                                                        (this.FirstName == "" || s.Firstname.ToLower().Contains(this.FirstName.ToLower())) &&
                                                        (this.Patronymic == "" || s.Patronymic.ToLower().Contains(this.Patronymic.ToLower())))
                                                        && s.Deleted == false);

            Workers = result.ToList();

            Signal(nameof(Workers));
        }

        private void ValidCost()
        {
            if(this.Cost < 0)
            {
                CostTextBox.Foreground = new SolidColorBrush(Colors.Red);
                SaveButton.IsEnabled = false;
            }
            else
            {
                CostTextBox.Foreground = new SolidColorBrush(Colors.Black);
                SaveButton.IsEnabled = true;
            }
        }

        private void VisibillityService(object sender, RoutedEventArgs e)
        {
            if (ServiceBorder.Visibility == Visibility.Collapsed)
                ServiceBorder.Visibility = Visibility.Visible;
            else
            {
                ServiceBorder.Visibility = Visibility.Collapsed;
            }
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            int ServiceId = 0;
            int WorkerId = 0;

            if (this.SelectedService == null)
            {
                
                    Service newService = new Service();
                    newService.Title = this.EditTitle;
                    newService.Description = this.Description;
                    newService.Cost = this.Cost;

                    DB.Instance.Services.Add(newService);

                    DB.Instance.SaveChanges();

                    ServiceId = DB.Instance.Services.OrderBy(s => s).Last().Id;
                    WorkerId = this.SelectedWorker.Id;
                
            }
            else
                ServiceId = this.SelectedService.Id;

            if (SelectedWorker != null)
                WorkerId = this.SelectedWorker.Id;
            else
                MessageBox.Show("Не выбран сотрудник!");

            if (ServiceId != 0 && WorkerId != 0)
            {
                Applicationservice newApplicationService = new Applicationservice();
                newApplicationService.Idapplication = this.Application.Id;
                newApplicationService.Idservice = ServiceId;
                newApplicationService.Idworker = WorkerId; ;

                DB.Instance.Applicationservices.Add(newApplicationService);

                DB.Instance.SaveChanges();

                MessageBox.Show("Добавлена новая услуга!");

                this.Close();
            }
        }

        private void VisibillityWorker(object sender, RoutedEventArgs e)
        {
            if (WorkerBorder.Visibility == Visibility.Collapsed)
                WorkerBorder.Visibility = Visibility.Visible;
            else
            {
                WorkerBorder.Visibility = Visibility.Collapsed;
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;
using SolickManagerV3_4.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
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
    /// Логика взаимодействия для EditOrAddApplication.xaml
    /// </summary>
    public partial class EditOrAddApplication : Window, INotifyPropertyChanged
    {
        private string firstName = "";
        private string secondName = "";
        private string patronymic = "";
        private string data = "";
        private string phone = "";
        private string model = "";
        private string description = "";
        private decimal cost = 0;
        private Client selectedClient;

        public event PropertyChangedEventHandler? PropertyChanged;
        void Signal([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }


        public string Text { get; set; }

        // Переключатели видимости
        public Visibility VisibilityClient { get; set; } = Visibility.Collapsed;
        public Visibility VisibilityDevice { get; set; } = Visibility.Collapsed;

        // Данные 
        public string DataApplication { get; set; }
        //Клиент
        public string FirstName { get => firstName; set { firstName = value; FilterClients(); } }
        public string SecondName { get => secondName; set { secondName = value; FilterClients(); } }
        public string Patronymic { get => patronymic; set { patronymic = value; FilterClients(); } }
        public string Data { get => data; set { data = value; FilterClients(); ValidBirthday(); } }
        public string Phone { get => phone; set { phone = value; FilterClients(); } }
        //Устройство
        public string Model { get => model; set { model = value; FilterDevice(); } }
        public string Description { get => description; set { description = value; FilterDevice(); } }
        public decimal Cost { get => cost; set { cost = value; FilterDevice(); ValidCost(); } }

        //Остальное на окне
        public Client Client { get; set; } = new Client();
        public Clientsdevice Device { get; set; }
        public string Problem { get; set; }

        //Списки и данные
        public List<Client> Clients { get; set; } = new List<Client>();
        public Client SelectedClient { get => selectedClient; set { selectedClient = value; FilterDevice(); } }

        public List<Clientsdevice> Clientsdevices { get; set; }
        public Clientsdevice SelectedDevice { get; set; }

        public Worker Worker { get; set; }

        private DTO.Application Application = new DTO.Application();

        public EditOrAddApplication(Worker worker)
        {
            InitializeComponent();

            Worker = worker;
            FilterClients();

            DataContext = this;
        }
        public EditOrAddApplication(Worker worker, DTO.Application application)
        {
            InitializeComponent();

            Worker = worker;
            FilterClients();

            //this.DataApplication = application.DateView;
            this.SelectedClient = application.IdclientNavigation;
            this.SelectedDevice = application.IddeviceNavigation;
            this.Problem = application.Problem;

            this.Application = application;

            DataContext = this;
        }

        // Фильтрация 
        public void FilterClients()
        {
            var rezultClient = DB.Instance.Clients.Where(s => ((s.Firstname.ToLower().Contains(this.FirstName.ToLower()) || this.FirstName == "")
                                                   && (s.Secondname.ToLower().Contains(this.SecondName.ToLower()) || this.SecondName == "")
                                                   && (s.Patronymic.ToLower().Contains(this.Patronymic.ToLower()) || this.Patronymic == ""))
                                                   && s.Deleted == false);

            this.Clients = rezultClient.ToList();

            Signal(nameof(Clients));
        }
        public void FilterDevice()
        {
            var rezultDevice = DB.Instance.Clientsdevices.Where(s => ((this.SelectedClient == null || s.Idclient == this.SelectedClient.Id)
                                                                && (this.Model == "" || s.Model.ToLower().Contains(this.Model.ToLower()))
                                                                && (this.Description == "" || s.Description.ToLower().Contains(this.Description.ToLower()))
                                                                && (this.Cost == 0 || s.Cost == this.Cost))
                                                                && s.Deleted == false);

            this.Clientsdevices = rezultDevice.ToList();

            Signal(nameof(Clientsdevices));

        }

        public void ValidCost()
        {
            if(Cost < 0) 
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
        public void ValidBirthday()
        {
            DateOnly data;
            if(DateOnly.TryParse(Data, out data) || Data == "")
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

        // Переключение видимости площадей добавленеия клиенат и утройства
        private void VisibillityClient(object sender, RoutedEventArgs e)
        {
            if (ClientBorder.Visibility == Visibility.Collapsed)
                ClientBorder.Visibility = Visibility.Visible;
            else
            {
                FirstName = "";
                SecondName = "";
                Patronymic = "";
                Data = "";
                Phone = "";

                Signal(nameof(FirstName));
                Signal(nameof(SecondName));
                Signal(nameof(Patronymic));
                Signal(nameof(Data));
                Signal(nameof(Phone));

                ClientBorder.Visibility = Visibility.Collapsed;
            }
        }
        private void VisibillityDevice(object sender, RoutedEventArgs e)
        {
            if (DeviceBorder.Visibility == Visibility.Collapsed)
                DeviceBorder.Visibility = Visibility.Visible;
            else
            {
                Model = "";
                Description = "";
                Cost = 0;

                Signal(nameof(Model));
                Signal(nameof(Description));
                Signal(nameof(Cost));

                DeviceBorder.Visibility = Visibility.Collapsed;
            }
        }

        //Кнопочка "сегодня" (заполняет дату сегодняшней)
        private void SetToday(object sender, RoutedEventArgs e)
        {
            DataApplication = OtherFunctons.Instance.DateOnlyNow().ToString("d");
            Signal(nameof(DataApplication));
        }

        private void SaveApplication(object sender, RoutedEventArgs e)
        {
            int ClientId = 0;
            int DeviceId = 0;

            if (SelectedClient == null)
            {
                Client newClient = new Client();

                newClient.Firstname = this.FirstName;
                newClient.Secondname = this.SecondName;
                newClient.Patronymic = this.Patronymic;
                newClient.Birthday = DateOnly.Parse(this.Data);
                newClient.Phone = this.Phone;

                DB.Instance.Clients.Add(newClient);
                DB.Instance.SaveChanges();

                ClientId = DB.Instance.Clients.OrderBy(s => s).Last().Id;
            }
            else
            {
                ClientId = SelectedClient.Id;
            }

            if (SelectedDevice == null && ClientId != 0)
            {
                Clientsdevice newClientDevice = new Clientsdevice();
                newClientDevice.Model = this.Model;
                newClientDevice.Description = this.Description;
                newClientDevice.Cost = this.Cost;
                newClientDevice.Idclient = ClientId;

                DB.Instance.Clientsdevices.Add(newClientDevice);
                DB.Instance.SaveChanges();

                DeviceId = DB.Instance.Clientsdevices.OrderBy(s => s).Last().Id;
            }
            else if (ClientId != 0)
            {
                DeviceId = SelectedDevice.Id;
            }

            if (this.Application.Id == 0)
            {
                if (ClientId != 0 && DeviceId != 0)
                {
                    DTO.Application newApplication = new DTO.Application();

                    DateOnly data;
                    if (DateOnly.TryParse(this.DataApplication, out data))
                    {
                        newApplication.Data = data;
                        newApplication.Idclient = ClientId;
                        newApplication.Iddevice = DeviceId;
                        newApplication.Idmanager = this.Worker.Id;
                        newApplication.Problem = this.Problem;
                        newApplication.Status = "Принята";

                        DB.Instance.Applications.Add(newApplication);
                        DB.Instance.SaveChanges();

                        MessageBox.Show("Добавлена новая заявка!");
                        this.Close();
                    }

                }
            }
            else
            {
                if(ClientId != 0 && DeviceId != 0)
                {
                    DateOnly data;
                    if (DateOnly.TryParse(this.DataApplication, out data))
                    {
                        Application.Data = data;
                        Application.Idclient = ClientId;
                        Application.Iddevice = DeviceId;
                        Application.Problem = this.Problem;

                        DB.Instance.Applications.Update(Application);
                        DB.Instance.SaveChanges();

                        MessageBox.Show("Заявка изменена!");
                        this.Close();
                    }

                }
            }

        }
    }
}

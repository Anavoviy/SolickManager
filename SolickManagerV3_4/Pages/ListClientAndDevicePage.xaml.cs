using Microsoft.EntityFrameworkCore;
using SolickManagerV3_4.DTO;
using SolickManagerV3_4.Windows;
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

namespace SolickManagerV3_4.Pages
{
    /// <summary>
    /// Логика взаимодействия для ListClientAndDevicePage.xaml
    /// </summary>
    public partial class ListClientAndDevicePage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        void Signal([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }


        private Client selectedClient;
        private string secondName = "";
        private string firstName = "";
        private string patronymic = "";
        private string dataBirthday = "";
        private string phone = "";

        // Worker
        public Worker Worker { get; set; }

        // Данные поиска
        public string SecondName { get => secondName; set { secondName = value; Search(); } }
        public string FirstName { get => firstName; set { firstName = value; Search(); } }
        public string Patronymic { get => patronymic; set { patronymic = value; Search(); } }
        public string DataBirthday { get => dataBirthday; set { dataBirthday = value; Search(); } }
        public string Phone { get => phone; set { phone = value; Search(); } }

        // Боковой вывод
        public Client SelectedClient
        {
            get => selectedClient;
            set
            {
                selectedClient = value;
                if(SelectedClient != null)
                    Clientsdevices = SelectedClient.Clientsdevices.Where(s => s.Deleted == false).ToList();

                Signal(nameof(Clientsdevices));
                Signal();
            }
        }

        public List<Clientsdevice> Clientsdevices { get; set; } = new List<Clientsdevice>();
        public Clientsdevice SelectedDeviceInSelectedClient { get; set; }

        // Основной list
        public List<Client> Clients { get; set; } = new List<Client>();

        public ListClientAndDevicePage(Worker worker)
        {
            InitializeComponent();

            Search();

            DataContext = this;
        }
        private void Search()
        {
            DateOnly data;
            var result = DB.Instance.Clients.Include(s => s.Clientsdevices).Include(s => s.Applications).Where(s => ((this.FirstName == "" || s.Firstname.ToLower().Contains(this.FirstName.ToLower())) &&
                                                        (this.SecondName == "" || s.Secondname.ToLower().Contains(this.SecondName.ToLower())) &&
                                                        (this.Patronymic == "" || s.Patronymic.ToLower().Contains(this.Patronymic.ToLower())) &&
                                                        (this.DataBirthday == "" || (DateOnly.TryParse(this.DataBirthday, out data) && s.Birthday == data)) &&
                                                        (this.Phone == "" || s.Phone.Contains(this.Phone))) && s.Deleted == false);

            Clients = result.OrderBy(s => s.Id).ToList();

            Signal(nameof(Clients));
        }


        private void AddNewClient(object sender, RoutedEventArgs e)
        {
            new AddOrEditClient().ShowDialog();
            Search();
        }

        private void EditSelectedClient(object sender, RoutedEventArgs e)
        {
            if (this.SelectedClient != null)
                new AddOrEditClient(this.SelectedClient).ShowDialog();
            else
                MessageBox.Show("Не выбран клиент!");

            Search();
        }

        private void DeleteSelectedClient(object sender, RoutedEventArgs e)
        {
            if(this.SelectedClient != null)
            {
                if ((bool)new ConfirmationWindow("Вы хотите удалить клиента и все связанные с ним данные (устройства, заявки)?").ShowDialog())
                {
                    for (int i = 0; i < SelectedClient.Clientsdevices.Count; i++)
                    {
                        Clientsdevice clientDevice = SelectedClient.Clientsdevices.ToList()[i];
                        clientDevice.Deleted = true;
                        DB.Instance.Clientsdevices.Update(clientDevice);
                    }
                    for (int i = 0; i < SelectedClient.Applications.Count; i++)
                    {
                        DTO.Application application = SelectedClient.Applications.ToList()[i];
                        application.Deleted = true;
                        DB.Instance.Applications.Update(application);
                    }

                    SelectedClient.Deleted = true;
                    DB.Instance.Clients.Update(SelectedClient);

                    DB.Instance.SaveChanges();
                }
            }
            else
                MessageBox.Show("Не выбран клиент!");

            Search();
        }

        private void SaveEditSelectedClient(object sender, RoutedEventArgs e)
        {
            if(this.SelectedClient != null)
            {
                DB.Instance.Clients.Update(this.SelectedClient);
                DB.Instance.SaveChanges();

                Search();
            }
        }

        private void AddDevice(object sender, RoutedEventArgs e)
        {
            if (SelectedClient != null)
                new AddClient_sDeviceWindow(SelectedClient).ShowDialog();

            Search();
            SelectedClient = Clients.FirstOrDefault(s => s.Id == this.SelectedClient.Id);
            Signal(nameof(SelectedClient));

        }

        private void DeleteDevice(object sender, RoutedEventArgs e)
        {
            if(SelectedClient != null && SelectedDeviceInSelectedClient != null)
            {
                SelectedDeviceInSelectedClient.Deleted = true;
                DB.Instance.Clientsdevices.Update(SelectedDeviceInSelectedClient);
                DB.Instance.SaveChanges();

                Search();
                SelectedClient = Clients.FirstOrDefault(s => s.Id == this.SelectedClient.Id);
                Signal(nameof(SelectedClient));
            }
        }
    }
}

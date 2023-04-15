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
    /// Логика взаимодействия для AddOrEditClient.xaml
    /// </summary>
    public partial class AddOrEditClient : Window
    {
        private string dataBirthday = "";

        // Данные
        public string FirstName { get; set; } = "";
        public string SecondName { get; set; } = "";
        public string Patronymic { get; set; } = "";
        public string DataBirthday { get => dataBirthday; set { dataBirthday = value; DataValid(); } }
        public string Phone { get; set; } = "";
        public string Passport { get; set; } = "";
        public string Email { get; set; } = "";
        public string Notes { get; set; } = "";

        public bool Edit = false;

        // Изменяемый клиент
        public Client Client { get; set; }



        public AddOrEditClient()
        {
            InitializeComponent();

            DataContext = this;
        }
        public AddOrEditClient(Client client)
        {
            InitializeComponent();

            Client = client;

            this.FirstName = Client.Firstname;
            this.SecondName = Client.Secondname;
            this.DataBirthday = Client.DateView;
            
            if(Client.Phone != null) 
            this.Phone = Client.Phone;
            if (Client.Patronymic != null)
                this.Patronymic = Client.Patronymic;
    
            if (Client.Passport != null)
                this.Passport = Client.Passport;
            if(Client.Email != null)
                this.Email = Client.Email;
            
            if(Client.Notes != null) 
                this.Notes = Client.Notes;

            Edit = true;

            DataContext = this;
        }

        private void DataValid()
        {
            DateOnly data;
            if(DateOnly.TryParse(DataBirthday, out data) || DataBirthday == "")
            {
                SaveButton.IsEnabled = true;
                BirthdayTextBox.Foreground = new SolidColorBrush(Colors.Black);
            }
            else
            {
                SaveButton.IsEnabled = false;
                BirthdayTextBox.Foreground = new SolidColorBrush(Colors.Red);
            }
        }

        private void SaveApplication(object sender, RoutedEventArgs e)
        {
            if(Client == null)
                Client = new Client();
            
            Client.Firstname = this.FirstName;
            Client.Secondname = this.SecondName;
            Client.Patronymic = this.Patronymic;
            Client.Birthday = DateOnly.Parse(DataBirthday);
            Client.Phone = this.Phone;
            Client.Email = this.Email;
            Client.Passport = this.Passport;
            Client.Notes = this.Notes;

            if (Edit)
            {
                DB.Instance.Clients.Update(Client);
                MessageBox.Show("Клиент изменён!");
            }
            else
            {
                DB.Instance.Add(Client);
                MessageBox.Show("Клиент добавлен!");
            }

            DB.Instance.SaveChanges();

            this.Close();
        }
    }
}

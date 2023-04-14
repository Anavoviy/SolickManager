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
        // Данные
        public string FirstName { get; set; } = "";
        public string SecondName { get; set; } = "";
        public string Patronymic { get; set; } = "";
        public string DataBirthday { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Passport { get; set; } = "";
        public string Email { get; set; } = "";
        public string Notes { get; set; } = "";

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

            DataContext = this;
        }

    }
}

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
    /// Логика взаимодействия для AddClient_sDeviceWindow.xaml
    /// </summary>
    public partial class AddClient_sDeviceWindow : Window
    {
        private decimal cost = 0;

        // Данные 
        public string Model { get; set; } = "";
        public decimal Cost { get => cost; set { cost = value; CostValid(); } }
        public string Description { get; set; } = "";

        private Client Client { get; set; } 


        public AddClient_sDeviceWindow(Client client)
        {
            InitializeComponent();

            this.Client = client;

            DataContext = this;
        }

        private void CostValid()
        {
            if(Cost < 0)
            {
                SaveButton.IsEnabled = false;
                CostTextBox.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                SaveButton.IsEnabled = true;
                CostTextBox.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void SaveDevice(object sender, RoutedEventArgs e)
        {
            Clientsdevice clientdevice = new Clientsdevice();

            clientdevice.Model = Model;
            clientdevice.Cost = Cost;
            clientdevice.Description = Description;
            clientdevice.Idclient = this.Client.Id;

            DB.Instance.Clientsdevices.Add(clientdevice);
            DB.Instance.SaveChanges();

            MessageBox.Show("Добавлено новое устройство!");

            this.Close();
        }
    }
}

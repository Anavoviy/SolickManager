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
    /// Логика взаимодействия для AddOrEditServiceWindow.xaml
    /// </summary>
    public partial class AddOrEditServiceWindow : Window
    {
        private decimal cost = 0;

        // Данные 
        public string EditTitle { get; set; } = "";
        public decimal Cost { get => cost; set { cost = value; CostValid(); } }
        public string Description { get; set; } = "";

        private bool EditYes { get; set; } = false; 
        private Service EditService { get; set; }

        public AddOrEditServiceWindow()
        {
            InitializeComponent();

            DataContext = this;
        }
        public AddOrEditServiceWindow(Service service)
        {
            InitializeComponent();

            this.EditService = service;
            this.EditYes = true;

            EditTitle = EditService.Title;
            Cost = EditService.Cost;
            Description = EditService.Description;

            DataContext = this;
        }

        private void CostValid()
        {
            if (Cost < 0)
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
            if (EditYes) 
            { 
                EditService.Title = EditTitle;
                EditService.Cost = Cost;
                EditService.Description = Description;

                DB.Instance.Update(EditService);
                DB.Instance.SaveChanges();

                MessageBox.Show("Услуга успешно изменена!");
            }
            else
            {
                Service service = new Service();

                service.Title = this.EditTitle;
                service.Description = this.Description;
                service.Cost = this.Cost;

                DB.Instance.Services.Add(service);
                DB.Instance.SaveChanges();

                MessageBox.Show("Добавлено новое устройство!");
            }

            this.Close();
        }
    }
}

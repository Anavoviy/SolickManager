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
using SolickManagerV3_4.DTO;

namespace SolickManagerV3_4.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddOrEditCharacteristicWindow.xaml
    /// </summary>
    public partial class AddOrEditCharacteristicWindow : Window
    {
        public Characteristic EditCharacteristic { get; set; } = new Characteristic();
        private bool IsEdit = false;

        public AddOrEditCharacteristicWindow()
        {
            InitializeComponent();
            DataContext = this;
        }
        public AddOrEditCharacteristicWindow(Characteristic characteritic)
        {
            InitializeComponent();
            IsEdit = true;
            EditCharacteristic = characteritic; 
            DataContext = this;
        }

        private void SaveCharacteristic(object sender, RoutedEventArgs e)
        {
            if(EditCharacteristic != null)
            {
                if (!IsEdit)
                {
                    DB.Instance.Characteristics.Add(EditCharacteristic);
                    MessageBox.Show("Успешно добавлена новая характеристика!");
                }
                else
                {
                    DB.Instance.Characteristics.Update(EditCharacteristic);
                    MessageBox.Show("Характеристика успешно изменена!");
                }
                DB.Instance.SaveChanges();

                this.Close();
            }
        }
    }
}

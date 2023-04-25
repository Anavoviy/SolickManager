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

namespace SolickManagerV3_4.Windows.Images
{
    /// <summary>
    /// Логика взаимодействия для AddOrEditProviderWindow.xaml
    /// </summary>
    public partial class AddOrEditProviderWindow : Window
    {

        // Основные данные
        public Provider EditProvider { get; set; } = new Provider();

        public AddOrEditProviderWindow()
        {
            InitializeComponent();

            DataContext = this;
        }
        public AddOrEditProviderWindow(Provider provider)
        {
            InitializeComponent();

            EditProvider = provider;

            DataContext = this;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            if(EditProvider.Id == 0)
            {
                if (EditProvider.Title == null)
                    EditProvider.Title = "";
                if (EditProvider.DirectorManager == null)
                    EditProvider.DirectorManager = "";

                DB.Instance.Providers.Add(EditProvider);
                DB.Instance.SaveChanges();

                MessageBox.Show("Успешно добавлен новый поставщик!");

                this.Close();
            }
            else
            {
                if (EditProvider.Title == null)
                    EditProvider.Title = "";
                if (EditProvider.DirectorManager == null)
                    EditProvider.DirectorManager = "";

                DB.Instance.Providers.Update(EditProvider);
                DB.Instance.SaveChanges();

                MessageBox.Show("Успешно изменён поставщик!");

                this.Close();
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;
using SolickManagerV3_4.DTO;
using SolickManagerV3_4.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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
    /// Логика взаимодействия для ListShipmentsPage.xaml
    /// </summary>
    public partial class ListShipmentsPage : Page, INotifyPropertyChanged
    {
        private Shipment selectedShipment;
        private string dataStart = "";
        private string dataEnd = "";
        private string searchTitleProvider = "";

        public event PropertyChangedEventHandler? PropertyChanged;
        private void Signal([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Работник за приложением
        public Worker Worker { get; set; }

        // Основные данные 
        public List<Shipment> Shipments { get; set; }
        public Shipment SelectedShipment { get => selectedShipment; set { selectedShipment = value; Signal(); } }

        // Поиск и фильтрация
        public string DataStart { get => dataStart; set { dataStart = value; Search(); } }
        public string DataEnd { get => dataEnd; set { dataEnd = value; Search(); } }
        public string SearchTitleProvider { get => searchTitleProvider; set { searchTitleProvider = value; Search(); } }

        public ListShipmentsPage(Worker worker)
        {
            InitializeComponent();

            Search();

            Worker = worker;

            DataContext = this;
        }

        private void Search()
        {
            var result = DB.Instance.Shipments.Include(s => s.Products).Include(s => s.IdproviderNavigation).Where(s => (SearchTitleProvider == "" || s.IdproviderNavigation.Title.ToLower().Contains(SearchTitleProvider.ToLower()))
                                                                                                                   && s.Deleted == false);
            Shipments = result.ToList();

            DateOnly startDate;
            DateOnly endData;
            if (DateOnly.TryParse(DataStart, out startDate))
                Shipments = Shipments.Where(s => s.Data > startDate).OrderBy(s => s.Id).ToList();
            if (DateOnly.TryParse(DataEnd, out endData))
                Shipments = Shipments.Where(s => s.Data < endData).OrderBy(s => s.Id).ToList();

            Signal(nameof(Shipments));

            if (SelectedShipment != null)
                SelectedShipment = Shipments.FirstOrDefault(s => s.Id == SelectedShipment.Id);
        }

        private void AddNewShipment(object sender, RoutedEventArgs e)
        {
            new AddOrEditShipmentWindow().ShowDialog();

            Search();
        }
        private void DeleteSelectedShipment(object sender, RoutedEventArgs e)
        {
            if(SelectedShipment != null)
            {
                if ((bool)new ConfirmationWindow("Вы хотите удалить поставку и все связанные с ней товары?").ShowDialog())
                {
                    List<Product> products = DB.Instance.Products.Where(s => s.Idshipment == SelectedShipment.Id).ToList();



                    if (products != null)
                    {
                        foreach(var prod in products)
                        {
                            var prodChars = DB.Instance.Productcharacteristics.Where(s => s.Idproduct == prod.Id).ToList();
                            if(prodChars != null)
                                DB.Instance.Productcharacteristics.RemoveRange(prodChars);

                            var prodPriceChanges = DB.Instance.Productpricechanges.Where(s => s.Idproduct == prod.Id).ToList();
                            if (prodPriceChanges != null)
                                DB.Instance.Productpricechanges.RemoveRange(prodPriceChanges);
                        }

                        DB.Instance.SaveChanges();

                        DB.Instance.Products.RemoveRange(products);
                    }

                    DB.Instance.SaveChanges();

                    DB.Instance.Shipments.Remove(SelectedShipment);

                    DB.Instance.SaveChanges();

                    MessageBox.Show("Поставка успешно удалена!");

                    Search();
                }
            }
        }
    }
}

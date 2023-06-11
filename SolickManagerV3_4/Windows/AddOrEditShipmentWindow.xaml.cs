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
using System.Windows.Shapes;
using SolickManagerV3_4.DTO;

namespace SolickManagerV3_4.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddOrEditShipmentWindow.xaml
    /// </summary>
    public partial class AddOrEditShipmentWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        void Signal([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        // Привязанные товары
        public OtherFunctons OtherFunctons { get; set; } = OtherFunctons.Instance;
        public Product SelectedProduct { get; set; }

        // Верхняя шапка
        public List<Provider> Providers { get; set; }
        public Provider SelectedProvider { get; set; }

        //Объект
        public Shipment EditShipment { get; set; } = new Shipment();
        public string EditData { get; set; }

        public AddOrEditShipmentWindow()
        {
            InitializeComponent();

            
            OtherFunctons.Products.Clear();
            OtherFunctons.Productcharacteristics.Clear();

            Providers = DB.Instance.Providers.Where(s => s.Deleted == false).OrderBy(s => s.Id).ToList();
            

            DataContext = this;
        }

        private void SetToday(object sender, RoutedEventArgs e)
        {
            EditData = OtherFunctons.Instance.DateOnlyNow().ToString();
            Signal(nameof(EditData));

        }
        private void AddProduct(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedProvider != null)
                {
                    new AddOrEditProductWindow(SelectedProvider).ShowDialog();

                    ProductsListView.Items.Refresh();
                }
                else
                    MessageBox.Show("Не выбран поставщик!");
            } catch(Exception ex)
            { MessageBox.Show("Упс, возможно не нужно добавлять, пока вы ещё редактируете!"); }
        }
        private void DeleteProduct(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedProduct != null)
                {
                    OtherFunctons.Instance.RemoveProduct(SelectedProduct);

                    ProductsListView.Items.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Упс, возможно не нужно удалять то, что вы ещё редактируете!");
            }
        }
     
        
        private void Save(object sender, RoutedEventArgs e)
        {
            List<Product> NullProduct = OtherFunctons.Products.Where(s => s.Id == 0).ToList();
            if (NullProduct.Count > 0)
                foreach (var product in NullProduct)
                    OtherFunctons.RemoveProduct(product);

            if (SelectedProvider != null && OtherFunctons.Products.Count > 0) 
            {
                EditShipment.Data = DateOnly.Parse(EditData);
                EditShipment.Idprovider = SelectedProvider.Id;

                int countProduct = 0;
                foreach (var product in OtherFunctons.Products)
                {
                    countProduct += product.Amount;
                }
                EditShipment.Numberproducts = countProduct;

                decimal allCost = 0;
                foreach(var product in OtherFunctons.Products)
                {
                    allCost += product.CostAll;
                }
                EditShipment.Amount = allCost;

                DB.Instance.Shipments.Add(EditShipment);
                DB.Instance.SaveChanges();

                int idShipment = DB.Instance.Shipments.OrderBy(s => s.Id).Last().Id;

                foreach(var product in OtherFunctons.Products)
                    product.Idshipment = idShipment;

                DB.Instance.Products.AddRange(OtherFunctons.Products);
                DB.Instance.Productcharacteristics.AddRange(OtherFunctons.Productcharacteristics);

                DB.Instance.SaveChanges();

                OtherFunctons.Productcharacteristics.Clear();
                OtherFunctons.Products.Clear();

                MessageBox.Show("Успешно добавлена новая поставка");

                this.Close();
            }
        }
    }
}

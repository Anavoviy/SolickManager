using SolickManagerV3_4.DTO;
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
using System.Windows.Shapes;

namespace SolickManagerV3_4.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddOrEditSaleWindow.xaml
    /// </summary>
    public partial class AddOrEditSaleWindow : Window, INotifyPropertyChanged
    {
        private string data = "";

        public event PropertyChangedEventHandler? PropertyChanged;
        void Signal([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        // Основные данные
        public OtherFunctons OtherFunctons { get; set; } = OtherFunctons.Instance;
        public string Data { get => data; set { data = value; DataValid(); } }
        public List<Client> Clients { get; set; }
        public Client SelectedClient { get; set; }
        private Worker Worker { get; set; }

        // Поиск/Новый клиент
        public string FirstName { get; set; } = "";
        public string SecondName { get; set; } = "";
        public string Patronymic { get; set; } = "";
        public string Birthday { get; set; } = "";
        public string Phone { get; set; } = "";

        // Товары и сборки
        public Product SelectedProduct { get; set; }
        public Assembly SelectedAssembly { get; set; }

        public AddOrEditSaleWindow(Worker worker)
        {
            InitializeComponent();

            Worker = worker;

            Clients = DB.Instance.Clients.Where(s => s.Deleted == false).OrderBy(s => s.Secondname).ThenBy(s => s.Firstname).ThenBy(s => s.Patronymic).ToList();

            DataContext = this;
        }

        private void DataValid()
        {

        }
        private void SetToday(object sender, RoutedEventArgs e)
        {
            Data = OtherFunctons.DateOnlyNow().ToString("d");
            Signal(nameof(Data));
        }
        private void VisibillityClient(object sender, RoutedEventArgs e)
        {
            if (ClientBorder.Visibility == Visibility.Visible)
                ClientBorder.Visibility = Visibility.Collapsed;
            else
                ClientBorder.Visibility = Visibility.Visible;
        }

        // Методы действий со товарами
        private void AddProduct(object sender, RoutedEventArgs e)
        {
            new ListAssembliesWindow(false, false).ShowDialog();
            ProductsListView.Items.Refresh();
        }
        private void DeleteProduct(object sender, RoutedEventArgs e)
        {
            if (SelectedProduct != null)
                OtherFunctons.Products.Remove(SelectedProduct);
            ProductsListView.Items.Refresh();
        }

        //Методы действий со сборками
        private void AddAssemblies(object sender, RoutedEventArgs e)
        {
            new ListAddAssemblyWindow().ShowDialog();
            AssembliesListView.Items.Refresh();
        }
        private void DeleteAssemblies(object sender, RoutedEventArgs e)
        {
            if (SelectedAssembly != null)
                OtherFunctons.Assemblies.Remove(SelectedAssembly);
            AssembliesListView.Items.Refresh();
        }



        private void Save(object sender, RoutedEventArgs e)
        {
            if ((SelectedClient != null || (FirstName != "" && SecondName != "" && Birthday != "" && Phone != "")) && (OtherFunctons.Products.Count() > 0 || OtherFunctons.Assemblies.Count() > 0) && Data != "")
            {
                DTO.Application sale = new DTO.Application();

                if (SelectedClient != null)
                    sale.Idclient = SelectedClient.Id;
                else
                {
                    DateOnly birthday;
                    if (!DateOnly.TryParse(Birthday, out birthday))
                    {
                        MessageBox.Show("Неправильно введена дата рождения!");
                        return;
                    }
                    else
                    {
                        DB.Instance.Clients.Add(new Client() { Firstname = FirstName, Secondname = SecondName, Patronymic = Patronymic, Phone = Phone, Birthday = birthday });
                        DB.Instance.SaveChanges();

                        sale.Idclient = DB.Instance.Clients.OrderBy(s => s.Id).Last().Id;
                    }
                }

                sale.Idmanager = Worker.Id;

                sale.Data = DateOnly.Parse(Data);

                sale.Status = "Создана";

                DB.Instance.Applications.Add(sale);
                DB.Instance.SaveChanges();

                int IdSale = DB.Instance.Applications.OrderBy(s => s.Id).Last().Id;

                if (OtherFunctons.Products.Count() > 0)
                    foreach (var product in OtherFunctons.Products)
                    {
                        DB.Instance.Applicationproducts.Add(new Applicationproduct() { Idapplication = IdSale, Idproduct = product.Id });

                        product.Amount -= OtherFunctons.Products.Where(s => s.Id == product.Id).Count();
                        DB.Instance.Products.Update(product);
                    }


                if (OtherFunctons.Assemblies.Count() > 0)
                    foreach (var assembly in OtherFunctons.Assemblies)
                    {
                        DB.Instance.Applicationassemblies.Add(new Applicationassembly() { Idapplication = IdSale, Idassemby = assembly.Id });

                        assembly.Deleted = true;
                        DB.Instance.Assemblies.Update(assembly);    
                    }

                DB.Instance.SaveChanges();

                MessageBox.Show("Продажа успешно добавлена!");

                this.Close();
            }
        }
    }
}

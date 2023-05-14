using SolickManagerV3_4.DTO;
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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SolickManagerV3_4.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddOrEditAssemblyWindow.xaml
    /// </summary>
    public partial class AddOrEditAssemblyWindow : Window, INotifyPropertyChanged
    {
        private string editCost = "";
        private string editData = "";

        public event PropertyChangedEventHandler? PropertyChanged;
        void Signal([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        // Основные данные
        public OtherFunctons OtherFunctons { get; set; } = OtherFunctons.Instance;
        public Assemblyproduct SelectedAssemblyProduct { get; set; }

        // Дополнительные данные
        public string EditData { get => editData; set { editData = value; ValidData(); } }
        public string EditCost { get => editCost; set { editCost = value; ValidCost(); } }
        public string EditTitle { get; set; } = "";

        public List<Worker> Workers { get; set; }
        public Worker SelectedConfigureWorker { get; set; }
        public Worker SelectedAssemblyWorker { get; set; }

        public AddOrEditAssemblyWindow()
        {
            InitializeComponent();

            Workers = DB.Instance.Workers.Where(s => s.Deleted == false).OrderBy(s => s.Surname).ThenBy(s => s.Firstname).ThenBy(s => s.Patronymic).ToList();

            OtherFunctons.Products.Clear();
            OtherFunctons.Productcharacteristics.Clear();

            DataContext = this;
        }
        public void ValidCost()
        {
            decimal cost = 0;
            if (decimal.TryParse(EditCost, out cost) && cost < 0)
            {
                CostTextBox.Foreground = new SolidColorBrush(Colors.Red);
                SaveButton.IsEnabled = false;
            }
            else
            {
                CostTextBox.Foreground = new SolidColorBrush(Colors.Black);
                SaveButton.IsEnabled = true;
            }
        }
        public void ValidData()
        {
            DateOnly data;
            if (DateOnly.TryParse(EditData, out data) || EditData == "")
            {
                DataTextBox.Foreground = new SolidColorBrush(Colors.Black);
                SaveButton.IsEnabled = true;
            }
            else
            {
                DataTextBox.Foreground = new SolidColorBrush(Colors.Red);
                SaveButton.IsEnabled = false;
            }
        }

        private void AddProductInGroup(object sender, RoutedEventArgs e)
        {
            new ListAssembliesWindow(true, true).ShowDialog();
            try
            {
                ProductsListView.Items.Refresh();
            }catch(Exception ex)
            {
                MessageBox.Show("Ошибка добавления товара!");
            }
        }

        private void DeleteProductFromGroup(object sender, RoutedEventArgs e)
        {
            if (SelectedAssemblyProduct != null)
                OtherFunctons.AssemblyProducts.Remove(SelectedAssemblyProduct);
            ProductsListView.Items.Refresh();
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            if(EditData != "" && EditCost != "" && EditTitle != "" && SelectedAssemblyWorker != null && SelectedConfigureWorker != null && OtherFunctons.AssemblyProducts.Count() > 0)
            {
                DB.Instance.Assemblies.Add(new Assembly()
                {
                    Title = EditTitle,
                    Data = DateOnly.Parse(EditData),
                    Cost = decimal.Parse(EditCost),
                    Idmasterassembler = SelectedAssemblyWorker.Id,
                    Idmasterconfiguration = SelectedConfigureWorker.Id,
                });

                DB.Instance.SaveChanges();

                int AssemblyId = DB.Instance.Assemblies.OrderBy(s => s.Id).Last().Id;

                foreach (var AssProd in OtherFunctons.AssemblyProducts)
                {
                    AssProd.Idassembly = AssemblyId;
                    AssProd.Count = AssProd.AddCount;

                    Product product = DB.Instance.Products.FirstOrDefault(s => s.Id == AssProd.Idproduct);
                    product.Amount -= AssProd.Count;
                    DB.Instance.Products.Update(product);
                }

                DB.Instance.Assemblyproducts.AddRange(OtherFunctons.AssemblyProducts);
                DB.Instance.SaveChanges();

                MessageBox.Show("Успешно добавлена новая группа товаров (сборка)!");
                this.Close();
            }
        }

        private void SetToday(object sender, RoutedEventArgs e)
        {
            EditData = OtherFunctons.Instance.DateOnlyNow().ToString("d");
            Signal(nameof(EditData));
        }
    }
}

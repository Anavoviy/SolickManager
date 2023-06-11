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
using SolickManagerV3_4.DTO;

namespace SolickManagerV3_4.Windows
{
    /// <summary>
    /// Логика взаимодействия для ListAddAssemblyWindow.xaml
    /// </summary>
    public partial class ListAddAssemblyWindow : Window, INotifyPropertyChanged
    {
        private string searchText = "";

        public event PropertyChangedEventHandler? PropertyChanged;
        private void Signal([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Основные данные
        public List<Assembly> Assemblies { get; set; }
        public Assembly SelectedAssembly { get; set; }
        public string SearchText { get => searchText; set { searchText = value; Search(); } }
        public ListAddAssemblyWindow()
        {
            InitializeComponent();

            Search();

            DataContext = this;
        }

        public void Search()
        {
            var result = DB.Instance.Assemblies.Where(s => (SearchText == "" || s.Title.ToLower().Contains(SearchText.ToLower())) && s.Deleted == false);

            Assemblies = result.OrderBy(s => s.Id).ToList();

            if (SelectedAssembly != null)
                SelectedAssembly = Assemblies.FirstOrDefault(s => s.Id == SelectedAssembly.Id);

            Signal(nameof(Assemblies));
            Signal(nameof(SelectedAssembly));
        }

        private void AddAssembly(object sender, RoutedEventArgs e)
        {
            if(SelectedAssembly != null)
            {
                OtherFunctons.Assemblies.Add(SelectedAssembly);
                this.Close();
            }
        }
    }
}

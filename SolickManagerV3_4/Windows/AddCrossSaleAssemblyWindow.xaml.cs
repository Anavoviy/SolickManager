using Microsoft.EntityFrameworkCore;
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
    /// Логика взаимодействия для AddCrossSaleAssemblyWindow.xaml
    /// </summary>
    public partial class AddCrossSaleAssemblyWindow : Window
    {
        public List<Assembly> Assemblies { get ; set; }
        public Assembly SelectedAssembly { get; set; }

        public AddCrossSaleAssemblyWindow(List<Assembly> assemblies)
        {
            InitializeComponent();

            Assemblies = DB.Instance.Assemblies.Include(s => s.IdmasterconfigurationNavigation).Include(s => s.IdmasterassemblerNavigation).Where(s => s.Deleted == false).ToList();

            foreach(var assembly in assemblies)
                Assemblies.Remove(assembly);

            DataContext = this;
        }

        private void AddAssemblyInSale(object sender, RoutedEventArgs e)
        {
            if(SelectedAssembly != null)
            {
                OtherFunctons.Assemblies.Add(SelectedAssembly);
                this.Close();
            }
        }
    }
}

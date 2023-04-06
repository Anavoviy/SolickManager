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
    /// Логика взаимодействия для EditOrAddApplication.xaml
    /// </summary>
    public partial class EditOrAddApplication : Window
    {
        public string Text { get; set; }


        // Данные 
        public string Data { get; set; }
        public Client Client { get; set; }
        public Clientsdevice Device { get; set; }
        public string Problem { get; set; }
        public string Notes { get; set; }


        public EditOrAddApplication()
        {
            InitializeComponent();
        }
    }
}

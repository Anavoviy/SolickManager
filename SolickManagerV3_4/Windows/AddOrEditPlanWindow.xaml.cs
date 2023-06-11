using SolickManagerV3_4.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace SolickManagerV3_4.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddOrEditPlanWindow.xaml
    /// </summary>
    public partial class AddOrEditPlanWindow : Window
    {
        private string editCostofone;

        public List<Howpay> HowPays { get; set; }
        public Howpay SelectedHowPay { get; set; }
        public string NewHowPayTitle { get; set; }

        public Plan EditPlan { get; set; } = new Plan();
        public string EditCostofone { get => editCostofone; set { editCostofone = value; ValidCost(); } }


        public AddOrEditPlanWindow()
        {
            InitializeComponent();

            HowPays = DB.Instance.Howpays.Where(s => s.Deleted == false).OrderBy(s => s.Id).ToList();
            SelectedHowPay = HowPays[0];

            DataContext = this;
        }

        private void ValidCost()
        {
            decimal cost;
            if(decimal.TryParse(EditCostofone, out cost))
            {
                CostTextBox.Foreground = new SolidColorBrush(Colors.Black);
                SaveButton.IsEnabled = true;
            }
            else
            {
                CostTextBox.Foreground = new SolidColorBrush(Colors.Red);
                SaveButton.IsEnabled = false;
            }
        }
        private void VisibillityHowPay(object sender, RoutedEventArgs e)
        {
            if (HowPayBorder.Visibility == Visibility.Visible)
                HowPayBorder.Visibility = Visibility.Collapsed;
            else
                HowPayBorder.Visibility = Visibility.Visible;
        }

        public void Save(object sender, RoutedEventArgs e)
        {
            if (EditCostofone != "" && (SelectedHowPay != null || NewHowPayTitle != ""))
            {
                if (HowPayBorder.Visibility == Visibility.Visible && NewHowPayTitle != "")
                {
                    DB.Instance.Howpays.Add(new Howpay() { Title = NewHowPayTitle });
                    DB.Instance.SaveChanges();

                    EditPlan.Idhowpay = DB.Instance.Howpays.OrderBy(s => s.Id).Last().Id;
                }
                else
                    EditPlan.Idhowpay = SelectedHowPay.Id;

                EditPlan.Costofone = decimal.Parse(EditCostofone);

                DB.Instance.Plans.Add(EditPlan);
                DB.Instance.SaveChanges();

                this.Close();
            }
        }
    }
}

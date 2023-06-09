﻿using SolickManagerV3_4.DTO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SolickManagerV3_4.Pages
{
    /// <summary>
    /// Логика взаимодействия для AccountingPage.xaml
    /// </summary>
    public partial class AccountingPage : Page
    {
        //Работник за приложением
        public Worker Worker { get; set; }

        public AccountingPage(DTO.Worker worker)
        {
            InitializeComponent();
        
            Worker = worker;

            DataContext = this;
        }

        private void ListOperations(object sender, MouseButtonEventArgs e)
        {
            Navigation.GetInstance().CurrentPage = new OperationsListPage(Worker);
        }

        private void ListBankAccounts(object sender, MouseButtonEventArgs e)
        {
            Navigation.GetInstance().CurrentPage = new BankAccountaListPage(Worker);
        }
    }
}

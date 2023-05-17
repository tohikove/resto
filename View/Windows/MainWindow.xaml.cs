using RestoApp_Churgel_Gavrilin.Model;
using RestoApp_Churgel_Gavrilin.View.Pages;
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

namespace RestoApp_Churgel_Gavrilin
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(Waiter waiter)
        {
            App.enteredWaiter = waiter;

            InitializeComponent();
            WaiterTbl.Text = waiter.Name;
            MainFrm.Navigate(new ChequePage());
        }

        private void GobackBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrm.CanGoBack)
            {
                MainFrm.GoBack();
            }
        }
    }
}

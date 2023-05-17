using RestoApp_Churgel_Gavrilin.Model;
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
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        private void PincodeBtn_Click(object sender, RoutedEventArgs e)
        {
            PincodePb.Password += (sender as Button).Content.ToString();
            if (PincodePb.Password.Length == 4)
            {
                //Процесс авторизации 
                Waiter waiter = App.context.Waiter.FirstOrDefault(w => w.Pincode == PincodePb.Password);  
                if (waiter != null)
                {
                    MainWindow mainWindow = new MainWindow(waiter);
                    mainWindow.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("Неправильный PIN-код");
                    PincodePb.Clear();
                }
            }
        }

        private void PincodeDeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if(PincodePb.Password.Length > 0)
            {
            PincodePb.Password = PincodePb.Password.Remove(PincodePb.Password.Length - 1);
            }
        }
    }
}

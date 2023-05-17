using RestoApp_Churgel_Gavrilin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RestoApp_Churgel_Gavrilin.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для ChequePage.xaml
    /// </summary>
    public partial class ChequePage : Page
    {
        public ChequePage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            TablesLB.ItemsSource = App.context.Table.ToList();
            OpenChequeLb.ItemsSource = App.context.Cheque.Where(c => c.IsClosed == false).ToList();
        }

        private void TablesLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            App.selectedTable = TablesLB.SelectedItem as Table;
            NavigationService.Navigate(new CreateChequePage());
        }

        private void OpenChequeLb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            App.selectedCheque = OpenChequeLb.SelectedItem as Cheque;
            if (App.enteredWaiter.WaiterId == App.enteredWaiter.WaiterId)
            {
                NavigationService.Navigate(new EditChequePage());
            }
            else
            {
                MessageBox.Show("Вы можете редактировать только свои чеки!");
            }
        }
    }
}
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
using System.Windows.Shapes;

namespace RestoApp_Churgel_Gavrilin.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для PaymentWindow.xaml
    /// </summary>
    public partial class PaymentWindow : Window
    {
       
        public PaymentWindow()
        {
            
            InitializeComponent();

            ToPayTbl.Text = $"К оплате: {App.selectedCheque.TotalCost} ₽";

        }

        private void Pay_Click(object sender, RoutedEventArgs e)
        {
            App.selectedCheque.IsClosed = true;
            App.selectedCheque.Table.IsFree = true;

            App.context.SaveChanges();

            DialogResult = true;

            MessageBox.Show("Заказ успешно оплачен!");

        }

        private void BankCardTbl_LostFocus(object sender, RoutedEventArgs e)
        {
            //  BankCardTb.Text = string.Empty;
            BankCardTb.Clear();
        }

        private void BankCardTbl_GotFocus(object sender, RoutedEventArgs e)
        {
            BankCardTb.Text = App.selectedCheque.TotalCost.ToString();
        }
        private string GetChange()
        {
            if (CashTb.Text != string.Empty)
            {
                decimal chequeCash = App.selectedCheque.TotalCost;
                decimal clientCash = decimal.Parse(CashTb.Text);
                return $"Сдача:  + { clientCash - chequeCash} ₽";
            }
            return "0";
        }

        private void CashTbl_TextChanged(object sender, TextChangedEventArgs e)
        {
            ChangeTbl.Text = GetChange();
        }
    }
}

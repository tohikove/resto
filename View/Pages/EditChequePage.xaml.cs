using RestoApp_Churgel_Gavrilin.Model;
using RestoApp_Churgel_Gavrilin.View.Windows;
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

namespace RestoApp_Churgel_Gavrilin.View.Pages
{
    // < summary >
    // Логика взаимодействия для EditChequePage.xaml
    // </summary>
    public partial class EditChequePage : Page
    {
        //Итоговая цена 
        private decimal totalCost;
        //Выбранная позиция 
        private Position _selectedPosition;
        private List<Category> categories;
        private List<Position> positions;

        public EditChequePage()
        {
            InitializeComponent();
            totalCost = App.selectedCheque.TotalCost;
            TotalCostTbl.Text = GetTotalCost(PositionsLv);
            //Добавление информации о столе и времени 
            TableTbl.Text = App.selectedCheque.Table.Title;
            DateTbl.Text = $"Открыт: {App.selectedCheque.OpeningDate}";
            //Добавление позиций в ListBox 
            EarlierPositionLv.ItemsSource = App.context.ChequePosition.Where(cp => cp.ChequeId == App.selectedCheque.ChequeId).ToList();
            //Добавление категорий в ComboBox 
            categories = App.context.Category.ToList();
            categories.Insert(0, new Category { Title = "Все категории" });
            CategoryCmb.ItemsSource = categories;
            CategoryCmb.SelectedValuePath = "Categoryid";
            CategoryCmb.DisplayMemberPath = "Title";
            CategoryCmb.SelectedIndex = 0;

        }

        private string GetTotalCost(ListView positionsLv)
        {
            totalCost = App.selectedCheque.TotalCost;
            foreach (Position position in positionsLv.Items)
            {
                totalCost += position.Cost;
            }
            return $"К оплате: {totalCost} ₽";
        }

        private void EditChequeBtn_Click(object sender, RoutedEventArgs e)
        {
            App.selectedCheque.TotalCost = totalCost;
            foreach (Position position in PositionsLv.Items)
            {
                ChequePosition chequePosition = new ChequePosition
                {
                    ChequeId = App.selectedCheque.ChequeId,
                    PositionId = position.PositionId,
                };
                App.context.ChequePosition.Add(chequePosition);
                App.context.SaveChanges();
                MessageBox.Show("Новые позиции добавлены в чек!");
            }
        }

        private void PayBtn_Click(object sender, RoutedEventArgs e)
        {
            PaymentWindow paymentWindow = new PaymentWindow();
            paymentWindow.ShowDialog();

            if (paymentWindow.DialogResult == true)
            {
                NavigationService.Navigate(new ChequePage());

            }
        }

        private void CategoryCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            positions = App.context.Position.ToList();
            if (CategoryCmb.SelectedIndex != 0)
            {
                positions = positions.Where(p => p.Category.CategoryId == CategoryCmb.SelectedIndex).ToList();
                PositionsLb.ItemsSource = positions;
            }
            else
            {
                PositionsLb.ItemsSource = positions;
            }
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            // positions = App.GetContext().Position.ToList(); 
            if (SearchTb.Text != string.Empty)
            {
                if (SearchTb.Text != "Поиск по названию")
                {
                    positions = positions.Where(p => p.Title.ToLower().Contains(SearchTb.Text.ToLower())).ToList();
                    PositionsLb.ItemsSource = positions;
                }
            }
            else
            {
                PositionsLb.ItemsSource = positions;
            }
        }

        private void PositionsLb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedPosition = PositionsLb.SelectedItem as Position;
            if (_selectedPosition != null)
            {
                PositionsLv.Items.Add(_selectedPosition);
                TotalCostTbl.Text = GetTotalCost(PositionsLv);
            }
            PositionsLb.SelectedIndex = -1;
        }

        private void PositionsLv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PositionsLv.Items.Remove((sender as ListView).SelectedItem as Position);
            TotalCostTbl.Text = GetTotalCost(PositionsLv);
        }
    }
}
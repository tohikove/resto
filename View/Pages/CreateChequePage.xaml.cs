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

using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RestoApp_Churgel_Gavrilin.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для CreateChequePage.xaml
    /// </summary>
    public partial class CreateChequePage : Page
    {
        decimal totalCost;
        List<Category> categories = new List<Category>();
        public CreateChequePage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Указываем контекст данных (Один объект) для элемента
            TableTbl.DataContext = App.selectedTable;

            // Если забыли как работать с привязками
            // TableTbl.Text = App.selectedTable.Title;

            // Указываем текущую дату и время
            DateTbl.Text = "Открыт: " + DateTime.Now.ToString();

            PositionLB.ItemsSource = App.context.Position.ToList();
            categories = App.context.Category.ToList();
            categories.Insert(0, new Category() { Title = "Все категории" });
            CategoryCmb.ItemsSource = categories;
        }

        private void PositionLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            App.selectedPosition = PositionLB.SelectedItem as Position;
            if (PositionLB.SelectedItem as Position != null)
            {
                PositionLV.Items.Add(PositionLB.SelectedItem as Position);
                TotalCostTbl.Text = GetTotalCost();
            }
            PositionLB.SelectedIndex = -1;
        }

        private void CategoryCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CategoryCmb.SelectedIndex != 0)
            {
                PositionLB.ItemsSource = App.context.Position.Where(p => p.Category.CategoryId == CategoryCmb.SelectedIndex).ToList();
            }
            else
            {
                PositionLB.ItemsSource = App.context.Position.ToList();
            }
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchTb.Text != string.Empty)
            {
                PositionLB.ItemsSource = App.context.Position.Where(p => p.Title.Contains(SearchTb.Text)).ToList();
            }
            else
            {
                PositionLB.ItemsSource = App.context.Position.ToList();
            }
        }

        private void PositionsLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PositionLV.Items.Remove(PositionLV.SelectedItem as Position);
            TotalCostTbl.Text = GetTotalCost();
        }

        private void CreateChequeBtn_Click(object sender, RoutedEventArgs e)
        {
            Cheque newCheque = new Cheque
            {
                Title = GenerateChequeTitle(),
                TotalCost = totalCost,
                IsClosed = false,
                OpeningDate = DateTime.Now,
                WaiterId = App.enteredWaiter.WaiterId,
                TableId = App.selectedTable.TableId,
            };


            if (App.selectedTable.IsFree)
            {
                // Изменение статуса стола
                foreach (Table table in App.context.Table.ToList())
                {
                    if (table.Equals(App.selectedTable))
                    {
                        table.IsFree = false;
                    }
                }
                App.context.Cheque.Add(newCheque);
                App.context.SaveChanges();
                MessageBox.Show("Чек успешно добавлен");
            }
            else
            {
                MessageBox.Show("Этот стол уже занят! Чек создан.");
            }

            foreach (Position position in PositionLV.Items)
            {
                ChequePosition chequePosition = new ChequePosition
                {
                    ChequeId = newCheque.ChequeId,
                    PositionId = position.PositionId,
                };
                App.context.ChequePosition.Add(chequePosition);
                App.context.SaveChanges();
            }
        }
        private string GetTotalCost()
        {
            totalCost = 0;
            foreach (Position position in PositionLV.Items)
            {
                totalCost += position.Cost;
            }
            return "К оплате: " + totalCost + " ₽";
        }

        private string GenerateChequeTitle()
        {
            DateTime currentDateTime = DateTime.Now;
            return $"Чек №{currentDateTime.Day}{currentDateTime.Month}{currentDateTime.Year}-{currentDateTime.Hour}{currentDateTime.Minute}";

            // Вывод:
            // Чек №0642023-1331, где
            // 06 - текущий день
            // 4 - текущий номер месяца
            // 2023 - текущий год
            // 1331 - текущtt время (часы, минуты)
        }
    }
}


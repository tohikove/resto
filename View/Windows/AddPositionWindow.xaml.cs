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
    /// Логика взаимодействия для AddPositionWindow.xaml
    /// </summary>
    ///  // В: В какую таблицу нужно добавить запись?
    //// О: Открыть модель> найти название нужной таблицы

    //// в: Какие свойства из таблицы нужно заполнить?
    //// о: провериякм каждое свойство по логике и исходя из задач

    //// 1. Создаем объект
    //Cheque newCheque = new Cheque

    //// 2. С помощью иниализаторов заполнить свойства объекта
    //{
    //    Title = "Cheque title",
    //    TotalCost = totalCost,
    //    IsClosed = false,
    //    OpeningDate = DateTime.Now,
    //    WaiterId = App.enteredWaiter.WaiterId,
    //    TableId = App.selectedTable.TableId,
    //};

    //// 3. Добавить объект в контекстную таблицу
    //App.context.Cheque.Add(newCheque);

    //        // 4. Сохранить изменения (запись добавляется в БД)
    //        App.context.SaveChanges();

    //        MessageBox.Show("Чек успешно добавлен!");
    public partial class AddPositionWindow : Window
    {
        List<Category> categories = new List<Category>();
        public AddPositionWindow()
        {
            InitializeComponent();
        }

        private void AddPosition_Click(object sender, RoutedEventArgs e)
        {
            Position newPosition = new Position
            {
                Title = NameTb.Text,
                Cost = Convert.ToInt32( CostTb.Text),
                

            };
            App.context.Position.Add(newPosition);
            App.context.SaveChanges();
            MessageBox.Show("Добавлен!");

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            categories = App.context.Category.ToList();
            PositionCb.ItemsSource = categories;

        }
    }
    }


using RestoApp_Churgel_Gavrilin.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace RestoApp_Churgel_Gavrilin
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        //Контекст данных
        public static RestoDb_Churgel_GavrilinEntities context = new RestoDb_Churgel_GavrilinEntities();

        // Выбранная позиция
        public static Position selectedPosition;

        // Выбранный стол
        public static Table selectedTable;

        // Вышедший официант
        public static Waiter enteredWaiter;

        //Выбранный чек
        public static Cheque selectedCheque;

    }
}

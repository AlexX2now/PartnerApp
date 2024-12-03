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

namespace MasirPal
{
    /// <summary>
    /// Логика взаимодействия для Hostory.xaml
    /// </summary>
    public partial class Hostory : Window
    {
        upeshkaEntities db = new upeshkaEntities();
        public Hostory()
        {
            InitializeComponent();

            List<Реализация_продукции_> all = db.Реализация_продукции_.ToList();
            List<HistoryToShow> toshow = new List<HistoryToShow>();

            foreach (var item in all)
            {
                toshow.Add(new HistoryToShow(item.Продукция_.Наименование.TrimEnd(),
                    item.Количество_продукции.Value, (item.Дата_продажи.Value).ToShortDateString()));
            }
            hlist.ItemsSource = toshow;

            List<Партнеры_> tocb = db.Партнеры_.ToList();
            foreach(var item in tocb)
            {
                filtrpart.Items.Add(item.Наименование_партнера.TrimEnd());
            }
        }

        private void addp_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void filtrbtn_Click(object sender, RoutedEventArgs e)
        {
            //Фильрует
            if (filtrpart.SelectedIndex != -1)
            {
                hlist.ItemsSource = "";

                List<Реализация_продукции_> all = db.Реализация_продукции_.ToList();
                List<Реализация_продукции_> whatneed = all.Where(x=>x.Код_партнера == filtrpart.SelectedIndex + 1).ToList();
                List<HistoryToShow> toshow = new List<HistoryToShow>();

                foreach (var item in whatneed)
                {
                    toshow.Add(new HistoryToShow(item.Продукция_.Наименование.TrimEnd(),
                        item.Количество_продукции.Value, (item.Дата_продажи.Value).ToShortDateString()));
                }
                hlist.ItemsSource = toshow;
            }
            else
            {
                MessageBox.Show("Выберите партнера, что бы посмотреть его реализованную продукцию");
            }
        }
        public class HistoryToShow
        {
            public string NameProduct { get; set; }
            public int Quantity { get; set; }
            public string DateProd { get; set; }

            public HistoryToShow(string nameProduct, int quantity, string dateProd)
            {
                NameProduct = nameProduct;
                Quantity = quantity;
                DateProd = dateProd;
            }
        }
    }
}

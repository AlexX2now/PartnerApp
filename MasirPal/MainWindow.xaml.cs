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

namespace MasirPal
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        upeshkaEntities db = new upeshkaEntities();
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                List<Партнеры_> parttoshow = db.Партнеры_.ToList();
                int numpart, quanprod, sale;

                for (int i = 0; i < parttoshow.Count; i++)
                {
                    numpart = i + 1;
                    quanprod = 0;
                    sale = 0;

                    StackPanel mainsp = new StackPanel();
                    mainsp.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F4E8D3"));
                    mainsp.Width = 750;
                    mainsp.Height = 200;
                    mainsp.Margin = new Thickness(0, 0, 0, 20);
                    string mainspname = "partner" + numpart;
                    mainsp.Name = mainspname;
                    mainsp.MouseUp += prtns_MouseUp;

                    StackPanel notmainsp = new StackPanel();
                    notmainsp.Orientation = Orientation.Horizontal;

                    //Партнер
                    TextBlock tbpartner = new TextBlock();
                    tbpartner.FontSize = 25;
                    tbpartner.Width = 600;
                    tbpartner.Margin = new Thickness(20);
                    string totbp = parttoshow[i].C_Тип_партнера__.Наименование.TrimEnd() + " | " + parttoshow[i].Наименование_партнера.TrimEnd();
                    tbpartner.Text = totbp;
                    notmainsp.Children.Add(tbpartner);

                    //Скидка
                    TextBlock tbsale = new TextBlock();
                    tbsale.FontSize = 25;
                    tbsale.Margin = new Thickness(20);
                    List<Реализация_продукции_> tocount = db.Реализация_продукции_.Where(x => x.Код_партнера == numpart).ToList();
                    foreach (var item in tocount)
                    {
                        quanprod += item.Количество_продукции.Value;
                    }
                    if (quanprod > 300000)
                    {
                        sale = 15;
                    }
                    else if (quanprod > 50000)
                    {
                        sale = 10;
                    }
                    else if (quanprod > 10000)
                    {
                        sale = 5;
                    }
                    else
                    {
                        sale = 0;
                    }
                    string totbs = sale + " %";
                    tbsale.Text = totbs;
                    notmainsp.Children.Add(tbsale);
                    mainsp.Children.Add(notmainsp);

                    //Директор
                    TextBlock tbdir = new TextBlock();
                    tbdir.FontSize = 20;
                    string totbd = parttoshow[i].Фамилия_директора.TrimEnd() + " " +
                        parttoshow[i].Имя_директора.TrimEnd() + " " +
                        parttoshow[i].Отчество_директора.TrimEnd();
                    tbdir.Text = totbd;
                    tbdir.Margin = new Thickness(20, 0, 0, 5);
                    mainsp.Children.Add(tbdir);

                    //Телефон
                    TextBlock tbphone = new TextBlock();
                    tbphone.FontSize = 20;
                    string totbph = "+7 " + parttoshow[i].Телефон_партнера.TrimEnd();
                    tbphone.Text = totbph;
                    tbphone.Margin = new Thickness(20, 0, 0, 5);
                    mainsp.Children.Add(tbphone);

                    //Рейтинг
                    TextBlock tbrating = new TextBlock();
                    tbrating.FontSize = 20;
                    var toshowrt = db.C_Рейтинг_партнеров__.FirstOrDefault(x => x.Код_партнера == numpart);
                    string totbr = "Рейтинг: " + toshowrt.Место_в_рейтинге;
                    tbrating.Text = totbr;
                    tbrating.Margin = new Thickness(20, 0, 0, 5);
                    mainsp.Children.Add(tbrating);

                    prtns.Items.Add(mainsp);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            
        }

        //Переход к добавлению партнера
        private void addp_Click(object sender, RoutedEventArgs e)
        {
            AddP ap = new AddP(0);
            ap.Show();
            this.Close();
        }

        //Переход к истории реализации
        private void hist_Click(object sender, RoutedEventArgs e)
        {
            Hostory h = new Hostory();
            h.Show();
            this.Close();
        }

        //Переход к расчету материавов
        private void rasch_Click(object sender, RoutedEventArgs e)
        {
            Materials m = new Materials();
            m.Show();
            this.Close();
        }

        private void prtns_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                StackPanel thissp = sender as StackPanel;

                string stridp = thissp.Name.Substring(7);
                int intidp = Convert.ToInt32(stridp);

                AddP ap = new AddP(intidp);
                ap.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

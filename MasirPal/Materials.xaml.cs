using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
    /// Логика взаимодействия для Materials.xaml
    /// </summary>
    public partial class Materials : Window
    {
        upeshkaEntities db = new upeshkaEntities();
        public Materials()
        {
            InitializeComponent();

            List<C_Тип_материала__> tocbm = db.C_Тип_материала__.ToList();
            List<C_Тип_продукции__> tocbp = db.C_Тип_продукции__.ToList();

            foreach (var item in tocbm)
            {
                tm.Items.Add(item.Наименование.TrimEnd());
            }

            foreach (var item in tocbp)
            {
                tp.Items.Add(item.Название.TrimEnd());
            }
        }

        private void addp_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void tometh_Click(object sender, RoutedEventArgs e)
        {
            if (tm.SelectedIndex != -1 && tp.SelectedIndex != -1 && qu.Text != " " && p1.Text != " " && p2.Text != " ")
            {
                if (IsDouble(p1.Text) && IsDouble(p2.Text) && IsInt(qu.Text))
                {
                    int quantity = Convert.ToInt32(qu.Text);
                    double par1 = Double.Parse(p1.Text);
                    double par2 = Double.Parse(p2.Text);

                    double ditog = GetMatCount(tp.SelectedIndex + 1, tm.SelectedIndex + 1
                        , quantity, par1, par2);
                    itog.Text = "Необходимо материалов: " + ditog; 
                        }
                else
                {
                    MessageBox.Show("Поля записаны в неверном формате");
                }
            }
            else
            {
                MessageBox.Show("Поля не должны быть пустыми");
            }
        }
        public double GetMatCount(int idtypr, int idtyma, int quantity, double par1, double par2)
        {
            var typr = db.C_Тип_продукции__.FirstOrDefault(x=>x.Код == idtypr);
            var tyma = db.C_Тип_материала__.FirstOrDefault(x=>x.Код == idtyma);

            if (tyma != null && typr != null)
            {
                double firststep = par1 * par2 * typr.Коээфицент_типа_продукции.Value;
                double secondstep = firststep * quantity;
                double thirdstep = secondstep * tyma.Коэффицент_типа_материала.Value;
                return Math.Round(thirdstep);
            }
            else
            {
                return -1;
            }
        }

        public static bool IsDouble(string text)
        {
            return double.TryParse(text, out _);
        }
        public static bool IsInt(string text)
        {
            return int.TryParse(text, out _);
        }
    }
}

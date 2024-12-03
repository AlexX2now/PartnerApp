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
    /// Логика взаимодействия для AddP.xaml
    /// </summary>
    public partial class AddP : Window
    {
        public int pid;
        upeshkaEntities db = new upeshkaEntities();
        public AddP(int idp)
        {
            InitializeComponent();

            try
            {
                List<C_Тип_партнера__> tocb = db.C_Тип_партнера__.ToList();
                foreach (var item in tocb)
                {
                    comptype.Items.Add(item.Наименование.TrimEnd());
                }

                //Улыбнись если хош отчислится
                pid = idp;

                switch (pid)
                {
                    case 0:
                        //добавление
                        mtb.Text = "Добавление партнеров";
                        redbtn.Visibility = Visibility.Hidden;
                        break;
                    default:
                        //редактирован
                        mtb.Text = "Редактирование партнеров";
                        addbtn.Visibility = Visibility.Hidden;

                        var currentp = db.Партнеры_.FirstOrDefault(x => x.Код == pid);
                        var currentpr = db.C_Рейтинг_партнеров__.FirstOrDefault(x => x.Код_партнера == pid);
                        dirname.Text = currentp.Имя_директора.TrimEnd();
                        dirsecname.Text = currentp.Фамилия_директора.TrimEnd();
                        dirthrname.Text = currentp.Отчество_директора.TrimEnd();
                        comprat.Text = currentpr.Место_в_рейтинге + "";
                        compname.Text = currentp.Наименование_партнера.TrimEnd();
                        compemail.Text = currentp.Электронная_почта_партнера.TrimEnd();
                        compadrs.Text = currentp.Юридический_адрес_партнера.TrimEnd();
                        compphon.Text = currentp.Телефон_партнера.TrimEnd();
                        comptype.Text = currentp.C_Тип_партнера__.Наименование.TrimEnd();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void addp_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void addbtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dirname.Text != "" &&
            dirsecname.Text != "" &&
            dirthrname.Text != "" &&
            comprat.Text != "" &&
            compname.Text != "" &&
            compemail.Text != "" &&
            compadrs.Text != "" &&
            compphon.Text != "" &&
            comptype.Text != "")
                {
                    if (IsNumber(comprat.Text))
                    {
                        var lastpart = db.Партнеры_.OrderByDescending(x => x.Код).FirstOrDefault();
                        int idlastpart = lastpart.Код;

                        using (var context = new upeshkaEntities())
                        {
                            var nwepart = new Партнеры_
                            {
                                Наименование_партнера = compname.Text,
                                Имя_директора = dirname.Text,
                                Электронная_почта_партнера = compemail.Text,
                                Фамилия_директора = dirsecname.Text,
                                Отчество_директора = dirthrname.Text,
                                Юридический_адрес_партнера = compadrs.Text,
                                Телефон_партнера = compphon.Text,
                                Тип_партнера = comptype.SelectedIndex + 1
                            };
                            context.Партнеры_.Add(nwepart);
                            context.SaveChanges();

                            var newrating = new C_Рейтинг_партнеров__
                            {
                                Место_в_рейтинге = int.Parse(comprat.Text),
                                Дата_обновления = DateTime.Now,
                                Код_партнера = idlastpart + 1
                            };
                            context.C_Рейтинг_партнеров__.Add(newrating);
                            context.SaveChanges();
                        }
                        MessageBox.Show("Партнер добавлен");
                    }
                    else
                    {
                        MessageBox.Show("Рейтинг записан некорректно");
                    }
                }
                else
                {
                    MessageBox.Show("Поля пустыми не должны быть");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void redbtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dirname.Text != "" &&
                            dirsecname.Text != "" &&
                            dirthrname.Text != "" &&
                            comprat.Text != "" &&
                            compname.Text != "" &&
                            compemail.Text != "" &&
                            compadrs.Text != "" &&
                            compphon.Text != "" &&
                            comptype.Text != "")
                {
                    if (IsNumber(comprat.Text))
                    {
                        var currentp = db.Партнеры_.FirstOrDefault(x => x.Код == pid);
                        var currentpr = db.C_Рейтинг_партнеров__.FirstOrDefault(x => x.Код_партнера == pid);

                        currentpr.Дата_обновления = DateTime.Now;
                        currentpr.Место_в_рейтинге = int.Parse(comprat.Text);

                        currentp.Наименование_партнера = compname.Text;
                        currentp.Имя_директора = dirname.Text;
                        currentp.Электронная_почта_партнера = compemail.Text;
                        currentp.Фамилия_директора = dirsecname.Text;
                        currentp.Отчество_директора = dirthrname.Text;
                        currentp.Юридический_адрес_партнера = compadrs.Text;
                        currentp.Телефон_партнера = compphon.Text;
                        currentp.Тип_партнера = comptype.SelectedIndex + 1;
                        db.SaveChanges();

                        MessageBox.Show("Партнер отредактирован");
                    }
                    else
                    {
                        MessageBox.Show("Рейтинг записан некорректно");
                    }
                }
                else
                {
                    MessageBox.Show("Поля пустыми не должны быть");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 
        }

        public static bool IsNumber(string text)
        {
            return double.TryParse(text, out _);
        }
    }
}

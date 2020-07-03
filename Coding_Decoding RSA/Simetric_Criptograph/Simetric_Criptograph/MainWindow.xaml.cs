using System.Windows;

namespace Simetric_Criptograph
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Mails mails = new Mails();        
        public MainWindow()
        {
            InitializeComponent();  
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {    
            string maill = mails.SendEmail(email.Text, coding.Text);
            if (maill != "Интернет отсутствует!!!" && maill != "Error")
            {
                texts_coding.Text = maill;
                maila.Content = "Сообщение закодировано и отправлено";
            }
            else { maila.Content = maill; }           

        }
    }   
}


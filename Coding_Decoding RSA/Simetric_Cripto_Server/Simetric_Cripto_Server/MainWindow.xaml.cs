using OpenPop.Mime;
using OpenPop.Pop3;
using System;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using Message = OpenPop.Mime.Message;
namespace Simetric_Cripto_Server
{
   
    public partial class MainWindow : Window
    {
        RSA_Criptograph_Class criptograph = new RSA_Criptograph_Class();
        
        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 5);
            timer.Start();
            
        }

        private void timerTick(object sender, EventArgs e)
        {
            Get_Decoding();
        }

        IPStatus status;
        public void Get_Decoding()
        {
            Ping p = new Ping();
            PingReply pr = p.Send("mail.ru");
            status = pr.Status;
            if (status == IPStatus.Success)//Проверка интернет соединения
            {

                // using чтобы соединение автоматически закрывалось
                using (Pop3Client client = new Pop3Client())
                {
                    client.Connect("pop.mail.ru", 995, true);// Подключение к серверу                    
                    client.Authenticate("velo.nsk2020@mail.ru", "Vadim762905", AuthenticationMethod.UsernameAndPassword);// Аутентификация (проверка логина и пароля)

                    if (client.Connected)
                    {
                        // Перебор всех сообщений
                        for (int i = client.GetMessageCount(); i > 0; i--)
                        {
                            Message message = client.GetMessage(i);
                            string subject = message.Headers.Subject;
                            string from = message.Headers.From.Address.ToString();
                            string body = "";
                            if (from == "ms.pischalova@mail.ru" && subject == "RSA")
                            {
                                MessagePart mpPlain = message.FindFirstPlainTextVersion();// ищем сообщениие

                                if (mpPlain != null)//проветка сообщения на пустоту
                                {
                                    Encoding enc = mpPlain.BodyEncoding;
                                    body = enc.GetString(mpPlain.Body); //получаем текст сообщения
                                    texts_codings.Text = body;
                                    texts_decoding.Text = criptograph.Decryption(body);
                                    email.Text = from;
                                    maila.Content = "Сообщение найдено и декодировано";
                                    break;
                                }
                                maila.Content = "К сожалению сообщение не найдено";
                            }
                        }
                    }
                }

            }
            else { maila.Content = "Интернет соединение отсутствует !!!"; }
        }
      
    } 
}  


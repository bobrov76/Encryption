using System;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;

namespace Simetric_Criptograph
{
    class Mails
    {
        IPStatus status;
        RSA_Criptograph_Class criptographs = new RSA_Criptograph_Class();
        public string SendEmail(string mail, string sms)
        {
            string email = "ms.pischalova@mail.ru";
                    
                try
                {
                    Ping p = new Ping();
                    PingReply pr = p.Send("yandex.ru");
                    status = pr.Status;
                    if (status == IPStatus.Success)
                    {
                        // отправитель - устанавливаем адрес и отображаемое в письме имя
                        MailAddress from = new MailAddress(email);
                        // кому отправляем
                        MailAddress to = new MailAddress(mail);
                        // создаем объект сообщения
                        MailMessage m = new MailMessage(from, to);
                        // текст письма                   
                        m.Body = criptographs.Encryption(sms);
                        // письмо представляет код html
                        m.Subject = "RSA";    
                        m.IsBodyHtml = false;
                        // адрес smtp-сервера и порт, с которого будем отправлять письмо
                        SmtpClient smtp = new SmtpClient("smtp.mail.ru", 25);
                        // логин и пароль
                        smtp.Credentials = new NetworkCredential(email, "sasha151199");
                        smtp.EnableSsl = true;
                        smtp.Send(m);
                        Console.Read();
                        return m.Body;
                    }
                }
                catch { return "Интернет отсутствует !!!"; }
                return "Error";            
        }        
    }
}

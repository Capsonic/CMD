using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CMDEntities.Reusable.Email
{
    class Email
    {
        public SmtpClient server = new SmtpClient("smtp.emailsrvr.com", 587);
        public string EmailAddress = "rfqm@capsonic.com";
        public string Password = "request1324";

        public Email()
        {
            /*
             * Autenticacion en el Servidor
             * Utilizaremos nuestra cuenta de correo
             *
             * Direccion de Correo (Gmail o Hotmail)
             * y Contrasena correspondiente
             */
            //capsonic.apps@gmail.com

            server.EnableSsl = false;
            server.DeliveryMethod = SmtpDeliveryMethod.Network;
            server.UseDefaultCredentials = false;
            server.Credentials = new System.Net.NetworkCredential(EmailAddress, Password);
        }

        public void SendMail(MailMessage Message)
        {
            server.Send(Message);
        }
    }
}

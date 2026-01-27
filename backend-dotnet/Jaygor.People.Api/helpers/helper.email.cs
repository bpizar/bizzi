using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Configuration;
using System;

using System.Collections.Generic;
using System.Collections;

/*
TESTING OK
noreply.jaygor
jaygor1818
*/


namespace JayGor.People.Api.helpers
{
    public static class HelperEmail
    {

        static string fromName;
        static string fromAddress;
        static string fromPassword;
        static string stmp;
        static int Port;
        static IEnumerable<KeyValuePair<string,string>> RemoveProtocols;

        public static void Config()
        {
			var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
			var configuration = builder.Build();


			var emailSection = configuration.GetSection("MailConfig");
			fromName = emailSection.GetSection("From").GetValue<string>("Name");
			fromAddress = emailSection.GetSection("From").GetValue<string>("Address");
			fromPassword = emailSection.GetSection("From").GetValue<string>("Password");
			stmp = emailSection.GetValue<string>("Smtp");
			Port = emailSection.GetValue<int>("Port");
            RemoveProtocols = emailSection.GetSection("RemoveProtocols").AsEnumerable();
        }

        //public async Task SendEmailAsync(string emailTo, string subject, string messageTo)
        public static void SendEmailAsync(string emailName,string emailTo, string subject, string messageTo)
        {
            var message = new MimeMessage();
                message.From.Add(new MailboxAddress(fromName, fromAddress));
                message.To.Add(new MailboxAddress(emailName, emailTo));
                message.Subject = subject;
                message.Body = new TextPart("plain")
                {
                    Text = messageTo
                };

            using (var client = new SmtpClient())
            {                
                client.Connect(stmp, Port, SecureSocketOptions.SslOnConnect); // SecureSocketOptions.sslonconnect


                foreach(var rp in RemoveProtocols)
                {
                    if(rp.Value!=null)
                    {
                        client.AuthenticationMechanisms.Remove(rp.Value);
                    }
                }               

                client.Authenticate(fromAddress, fromPassword);
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}





//var message = new MimeMessage();
//message.From.Add(new MailboxAddress("noreply.jaygor", "noreply.jaygor@gmail.com"));
//    message.To.Add(new MailboxAddress(emailName, emailTo));
//    message.Subject = subject;
//    message.Body = new TextPart("plain")
//{
//Text = messageTo

//};

//using (var client = new SmtpClient())
//{                
//    client.Connect("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect); // SecureSocketOptions.sslonconnect
//    client.AuthenticationMechanisms.Remove("XOAUTH2");
//    client.Authenticate("noreply.jaygor@gmail.com", "jaygor1818");
//    client.Send(message);
//    client.Disconnect(true);
//}
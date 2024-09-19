using HyggyBackend.BLL.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MimeKit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Printing;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace HyggyBackend.BLL.Services.EmailService
{
	public class EmailSender : IEmailSender
	{
		private readonly EmailConfiguration _emailConfig;

		public EmailSender(EmailConfiguration emailConfig)
		{
			_emailConfig = emailConfig;
		}

		public void SendEmail(Message message)
		{
			var emailMessage = CreateEmailMessage(message);

			Send(emailMessage);
		}

		private void Send(MimeMessage emailMessage)
		{
			using(var client = new SmtpClient())
			{
				try
				{
					client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
					client.AuthenticationMechanisms.Remove("XOAUTH2");
					client.Authenticate(_emailConfig.Username, _emailConfig.Password);

					client.Send(emailMessage);
				}
				catch 
				{
					throw;
				}
				finally
				{
					client.Disconnect(true);
					client.Dispose();
				}
			}
		}

		private MimeMessage CreateEmailMessage(Message message)
		{
			var bodyBuilder = new BodyBuilder();
			bodyBuilder.HtmlBody = message.Content;

			var emailMessage = new MimeMessage();
			emailMessage.From.Add(new MailboxAddress("Hyggy", _emailConfig.From));
			emailMessage.To.AddRange(message.To);
			emailMessage.Subject = message.Subject;
			//emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
			//{ Text = message.Content! };
			emailMessage.Body = bodyBuilder.ToMessageBody();
			return emailMessage;
		}
		private String RegistrationEmailTemplate(string content)
		{
			return
				"<!DOCTYPE html>\r\n<html lang=\"en\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <!-- <link rel=\"stylesheet\" href=\"/style.css\"/> -->\r\n    <title>Document</title>\r\n</head>\r\n<style>\r\n    html{\r\n    width: 800px;\r\n}\r\nbody{\r\n    background-color: #F8F8F8;\r\n}\r\n.container{\r\n    margin: 60px;\r\n    margin-top: 40px;\r\n}\r\n.innercontainer{\r\n    background-color: white;\r\n}\r\n.maincontainer{\r\n    padding: 10px 20px;\r\n}\r\nheader{\r\n    display: flex;\r\n    position: relative;\r\n    justify-content: space-between;\r\n}\r\nheader>h1{\r\n    background-color: #143C8A;\r\n    color: white;\r\n    padding: 2px;\r\n}\r\nheader>h3{\r\n    position: absolute;\r\n    right: 0;\r\n    bottom: 0;\r\n    color:gray;\r\n    font-weight: 100;\r\n}\r\n.reference{\r\n    display: flex;\r\n    justify-content: center;\r\n    border:1px gray solid;\r\n    height: 50px;\r\n}\r\n.reference>a{\r\n    font-size: large;\r\n    font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif\r\n}\r\n.title{\r\n    display: flex;\r\n    flex-direction: column;\r\n    text-align: end;\r\n}\r\n.title>h2{\r\n    margin-top: 50px;\r\n    margin-bottom: 0;\r\n}\r\n.title>h4{\r\n    margin-top: 0;\r\n    font-weight: 100;\r\n}\r\np{\r\n    font-size: 18px;\r\n    font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;\r\n    font-weight:400;\r\n}\r\nbutton{\r\n    font-size: medium;\r\n    background-color: #143C8A;\r\n    color: white;\r\n    height: 50px;\r\n    margin-left: 20px;\r\n    padding-left: 20px;\r\n    padding-right: 20px;\r\n}\r\nbutton>p{\r\n    margin: auto;    \r\n    font-weight:bolder;\r\n}\r\nfooter{\r\n    margin-top: 100px;\r\n    padding: 10px 20px;\r\n    font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;\r\n}\r\n</style>\r\n<body>\r\n    <div class=\"container\">\r\n    <header>\r\n        <h1>Hyggy</h1>\r\n        <h3>Відділ по роботі з клієнтами</h3>\r\n    </header>\r\n    <div class=\"innercontainer\">\r\n    <main>\r\n        <div class=\"reference\">\r\n            <a href=\"#\">Перейти на сайт Hyggy.ua</a>\r\n        </div>\r\n        <div class=\"maincontainer\">\r\n            <div class=\"title\">\r\n                <h2>Ласкаво просимо на Hyggy.ua</h2>\r\n                <h4>06.вересня 2024</h4>\r\n            </div>\r\n            <div>\r\n                <p>Вітаємо Євген<br/><br/>Ми раді вітати вас на JYSK.ua. Все що вам потрібно - це активувати свій обліковий запис!<br/><br/>Зверніть увагу, що посилання активне лише 48 годин.<br/><br/><button><p>Активувати обліковий запис</p></button><br/><br/>Для того, щоб зробити свої покупки максимально приємними просимо заповнити особисті дані у своєму обліковому записі.</p>\r\n    \r\n            </div>\r\n        </div>\r\n\r\n        </main>\r\n        <footer>\r\n            <h3>ВІДДІЛ ПО РОБОТІ З КЛІЄНТАМИ</h3>\r\n            <p>\r\n                У Вас виникли запитання чи потрібна допомога? <a href=\"#\">Зверніться до Відділу по роботі з клієнтами</a>\r\n            </p>    \r\n            <a href=\"#\">\r\n                Адреса та години роботи магазину\r\n            </a>\r\n            <p>З повагою,<br/><span style=\"color: #143C8A;font-weight: bolder;\">Hyggy</span></p>\r\n        </footer>\r\n        \r\n    </div>\r\n    </div>\r\n</body>\r\n</html>";
		}
	}
}

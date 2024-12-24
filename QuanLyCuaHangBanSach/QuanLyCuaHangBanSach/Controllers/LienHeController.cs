using System;
using System.Collections.Generic;
using System.Linq;
//using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLyCuaHangBanSach.Models;

using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace QuanLyCuaHangBanSach.Controllers
{
    public class LienHeController : Controller
    {
        // GET: LienHe
        [HttpGet]
        public ActionResult LienHe()
        {

            return View();
        }


        [HttpPost]
        public ActionResult LienHe(EmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    SendEmail(model.Name, model.Email, model.Message);
                    ViewBag.Message = "Email đã được gửi thành công!";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Có lỗi xảy ra: " + ex.Message;
                }
            }
            return View(model);
        }

        private void SendEmail(string name, string email, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(name, email)); // Người gửi
            emailMessage.To.Add(new MailboxAddress("khiêm tốn khanh", "nguyenvukhanh392@gmail.com")); // Người nhận
            emailMessage.Subject = "Liên hệ từ " + name;

            var bodyBuilder = new BodyBuilder
            {
                TextBody = message // Nội dung email
            };

            emailMessage.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                // Kết nối đến SMTP server (Gmail trong trường hợp này)
                client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);

                // Đăng nhập vào tài khoản email của bạn
                client.Authenticate("nguyenvukhanh09112004@gmail.com", "gjuq ntkw xqbg ybed");

                // Gửi email
                client.Send(emailMessage);

                // Ngắt kết nối
                client.Disconnect(true);
            }
        }
    }
}
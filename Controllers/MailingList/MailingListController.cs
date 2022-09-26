using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tor.Data;
using Tor.Models;

namespace Tor.Controllers.MailingList
{
    public class MailingListController : Controller
    {
        private readonly ApplicationDbContext _db;

        public MailingListController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult MailingListIndex()
        {
            //string Id_user = clsCommon.GetUserId(this.User);

            return View();
        }

        public IActionResult MailingListSuccess()
        {
            return View();
        }

        public IActionResult MailingListExp()
        {
            return View();
        }

        [HttpPost]
        [ActionName("MailingListIndex")]
        public async Task<IActionResult> MailingListIndexInsert(Models.MailingList mailingList)
        {
            var remainderPromo = await _db.MailingList.AsNoTracking().Where(u => u.Email == mailingList.Email).Select(u => u.Email).SingleOrDefaultAsync();
            if(remainderPromo == null)
            {
                await _db.MailingList.AddAsync(mailingList);

                var PathToTemplate = @"C:\Users\volko\source\repos\Tor\Template\NewPromo.cshtml";
                var subject = "Оформление нового заказа";

                string HtmlBody;

                using (StreamReader sr = System.IO.File.OpenText(PathToTemplate))
                {
                    HtmlBody = sr.ReadToEnd();
                }

                var promo = CreatePromo();

                string messageBody = string.Format(HtmlBody,
                promo);

                var emailService = new EmailService();

                await emailService.SendEmailAsync(mailingList.Email, subject, messageBody);

                var prom = new PromoСode
                {
                    ValuePromo = 1,
                    TypePromo = Models.Type.Percent,
                    Dimension = 10,
                    Name = promo
                };

                await _db.PromoCode.AddAsync(prom);
                await _db.SaveChangesAsync();
            }
            else
            {
                return RedirectToAction("MailingListExp");
            }
            return RedirectToAction("MailingListSuccess");
        }

        public string CreatePromo()
        {
            Random rnd = new();
            int randValue;
            string str = "";
            char letter;
            for (int i = 0; i < 15; i++)
            {
                randValue = rnd.Next(0, 26);

                letter = Convert.ToChar(randValue + 65);

                str = str + letter;
            }
            return str;
        }


    }
}

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HejCamping.Application.Interfaces;
using HejCamping.Web.Models;

namespace HejCamping.Web.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(ContactFormModel contactForm)
        {
            if (ModelState.IsValid)
            {
                var result = _contactService.SendContactEmailAsync(contactForm.FromEmail, contactForm.OrderNumber, contactForm.Subject, contactForm.Message);
                if (result.IsCanceled || result.IsFaulted)
                {
                    ViewBag.Message = "An error occurred while sending your message. Please try again later.";
                    return View();
                }
                else {
                    ViewBag.Message = "Your message has been sent successfully!";
                }

                return View();
            }

            return View(contactForm);
        }
    }
}
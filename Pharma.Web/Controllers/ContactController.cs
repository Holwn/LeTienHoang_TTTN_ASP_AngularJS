using Pharma.Service;
using Pharma.Web.Mappings;
using Pharma.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pharma.Web.Controllers
{
    public class ContactController : Controller
    {
        IContactDetailService _contactDetailService;
        public ContactController(IContactDetailService contactDetailService)
        {
            _contactDetailService = contactDetailService;
        }

        public ActionResult Index()
        {
            var model = _contactDetailService.GetDefaultContact();
            var contactViewModel = AutoMapperConfiguration.InitializeAutomapper().Map<ContactDetailViewModel>(model);

            return View(contactViewModel);
        }
    }
}
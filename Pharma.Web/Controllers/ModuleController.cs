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
    public class ModuleController : Controller
    {
        ISlideService _slideService;
        IProductService _productService;
        public ModuleController(ISlideService slideService, IProductService productService)
        {
            _slideService = slideService;
            _productService = productService;
        }
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Slide()
        {
            var model = _slideService.GetAll();
            var listSlideViewModel = AutoMapperConfiguration.InitializeAutomapper().Map<IEnumerable<SlideViewModel>>(model);

            return View("Slide", listSlideViewModel);
        }

        public ActionResult ProductHot()
        {
            var model = _productService.GetProductHot();
            var listProductHotViewModel = AutoMapperConfiguration.InitializeAutomapper().Map<IEnumerable<ProductViewModel>>(model);

            return View("ProductHot", listProductHotViewModel);
        }
    }
}
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
    public class HomeController : Controller
    {
        IProductCategoryService _productCategoryService;
        IProductService _productService;
        IPostService _postService;
        IFooterService _footerService;
        
        public HomeController(IProductCategoryService productCategoryService, IPostService postService, IProductService productService, IFooterService footerService)
        {
            _productCategoryService = productCategoryService;
            _postService = postService;
            _productService = productService;
            _footerService = footerService;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [ChildActionOnly]
        public ActionResult Header()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult Footer()
        {
            var model = _footerService.GetAll();
            var ListFooterViewModel= AutoMapperConfiguration.InitializeAutomapper().Map<IEnumerable<FooterViewModel>>(model);

            return PartialView(ListFooterViewModel);
        }

        public ActionResult Category()
        {
            var model = _productCategoryService.GetAll();
            var listProductCategoryViewModel= AutoMapperConfiguration.InitializeAutomapper().Map<IEnumerable<ProductCategoryViewModel>>(model);

            return PartialView(listProductCategoryViewModel);
        }

        public ActionResult PostStories()
        {
            var model = _postService.GetAll();
            var listPostViewModel = AutoMapperConfiguration.InitializeAutomapper().Map<IEnumerable<PostViewModel>>(model);

            return PartialView(listPostViewModel);
        }

        public ActionResult HomeProduct()
        {
            var model = _productService.GetAll().OrderByDescending(p=>p.UpdatedDate).Take(6);
            var listProductViewModel = AutoMapperConfiguration.InitializeAutomapper().Map<IEnumerable<ProductViewModel>>(model);

            return PartialView(listProductViewModel);
        }
    }
}
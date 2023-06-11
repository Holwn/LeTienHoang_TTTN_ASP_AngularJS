using Pharma.Common;
using Pharma.Service;
using Pharma.Web.Infrastructure.Core;
using Pharma.Web.Mappings;
using Pharma.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Pharma.Web.Controllers
{
    public class ProductController : Controller
    {
        IProductCategoryService _productCategoryService;
        IProductService _productService;
        IUnitService _unitService;
        public ProductController(IProductCategoryService productCategoryService, IProductService productService, IUnitService unitService)
        {
            _productCategoryService = productCategoryService;
            _productService = productService;
            _unitService = unitService;
        }
        public ActionResult Detail(int id)
        {
            var model = _productService.GetById(id);
            var productViewModel = AutoMapperConfiguration.InitializeAutomapper().Map<ProductViewModel>(model);
            var unit = _unitService.GetById(productViewModel.BatchUnitID);
            ViewBag.Unit = AutoMapperConfiguration.InitializeAutomapper().Map<UnitViewModel>(unit);

            List<string> listImages = new JavaScriptSerializer().Deserialize<List<string>>(productViewModel.MoreImage);
            listImages.Insert(0, productViewModel.Image);
            ViewBag.MoreImages = listImages;

            var relatedProducts = _productService.GetRatedProducts(productViewModel.ID, 6);
            ViewBag.RelatedProducts = AutoMapperConfiguration.InitializeAutomapper().Map<IEnumerable<ProductViewModel>>(relatedProducts);

            var tags = _productService.GetListTagByProductId(productViewModel.ID);
            ViewBag.Tags = AutoMapperConfiguration.InitializeAutomapper().Map<IEnumerable<TagViewModel>>(tags);

            return View("Detail", productViewModel);
        }
        public ActionResult ListByTag(string tagId, int page=1)
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var model = _productService.GetListProductByTag(tagId, page, pageSize, out totalRow);

            var listProductViewModel = AutoMapperConfiguration.InitializeAutomapper().Map<IEnumerable<ProductViewModel>>(model);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);

            var tag = _productService.GetTag(tagId);
            ViewBag.Tag = AutoMapperConfiguration.InitializeAutomapper().Map<TagViewModel>(tag);

            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = listProductViewModel,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };
            return View(paginationSet);
        }
        public ActionResult Category(int id, int page = 1)
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var model = _productService.GetListProductByCategoryIdPaging(id, page, pageSize,out totalRow);
            
            var listProductViewModel = AutoMapperConfiguration.InitializeAutomapper().Map<IEnumerable<ProductViewModel>>(model);
            int totalPage =(int) Math.Ceiling((double) totalRow/pageSize);

            var category = _productCategoryService.GetById(id);
            ViewBag.Category = AutoMapperConfiguration.InitializeAutomapper().Map<ProductCategoryViewModel>(category);

            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = listProductViewModel,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };

            return View(paginationSet);
        }

        public ActionResult Search(string keyword, int page = 1)
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var model = _productService.Search(keyword, page, pageSize, out totalRow);

            var listProductViewModel = AutoMapperConfiguration.InitializeAutomapper().Map<IEnumerable<ProductViewModel>>(model);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);

            ViewBag.Keyword = keyword;

            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = listProductViewModel,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };

            return View(paginationSet);
        }

        public JsonResult GetListProductByName(string keyword)
        {
            var model = _productService.GetListProductByName(keyword).Take(5);

            return Json(new { data = model }, JsonRequestBehavior.AllowGet);
        }
    }
}
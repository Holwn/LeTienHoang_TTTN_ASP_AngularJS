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
    public class PageController : Controller
    {
        IPageService _pageService;
        IPostService _postService;
        IPostCategoryService _postCategoryService;
        public PageController(IPageService pageService,IPostService postService, IPostCategoryService postCategoryService)
        {
            _pageService = pageService;
            _postService = postService;
            _postCategoryService = postCategoryService;
        }
        public ActionResult Index(string alias)
        {
            var model = _pageService.GetByAlias(alias);
            var page= AutoMapperConfiguration.InitializeAutomapper().Map<PageViewModel>(model);

            return View(page);
        }

        public ActionResult Post(string alias)
        {
            var model = _postService.GetByAlias(alias);
            var post = AutoMapperConfiguration.InitializeAutomapper().Map<PostViewModel>(model);

            return View(post);
        }

        public ActionResult Category()
        {
            var model = _postService.GetAll();
            var listPostViewModel = AutoMapperConfiguration.InitializeAutomapper().Map<IEnumerable<PostViewModel>>(model);

            var modelCategory = _postCategoryService.GetAll();
            ViewBag.PostCategories = AutoMapperConfiguration.InitializeAutomapper().Map<IEnumerable<PostCategoryViewModel>>(modelCategory);

            return PartialView(listPostViewModel);
        }

        public ActionResult PostList()
        {
            var model = _postService.GetAll();
            var listPostViewModel = AutoMapperConfiguration.InitializeAutomapper().Map<IEnumerable<PostViewModel>>(model);

            return PartialView(listPostViewModel);
        }
    }
}
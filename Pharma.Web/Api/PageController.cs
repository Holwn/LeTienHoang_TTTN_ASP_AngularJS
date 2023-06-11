using Pharma.Model.Models;
using Pharma.Service;
using Pharma.Web.Infrastructure.Core;
using Pharma.Web.Infrastructure.Extensions;
using Pharma.Web.Mappings;
using Pharma.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace Pharma.Web.Api
{
    [RoutePrefix("api/page")]
    [Authorize]
    public class PageController : ApiControllerBase
    {
        #region Initializeprivate
        IPageService _PageService;
        public PageController(IErrorService errorService, IPageService PageService)
            : base(errorService)
        {
            this._PageService = PageService;
        }
        #endregion

        [Route("getallparents")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpReponse(request, () =>
            {
                var list = _PageService.GetAll();

                var listPageVm = AutoMapperConfiguration.InitializeAutomapper().Map<List<PageViewModel>>(list);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listPageVm);

                return response;
            });
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpReponse(request, () =>
            {
                var list = _PageService.GetById(id);

                var listPageVm = AutoMapperConfiguration.InitializeAutomapper().Map<PageViewModel>(list);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listPageVm);

                return response;
            });
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request, string keyword, int page, int pageSize = 10)
        {
            return CreateHttpReponse(request, () =>
            {
                int totalRow = 0;
                var list = _PageService.GetAll(keyword);

                totalRow = list.Count();
                var query = list.OrderByDescending(x => x.ID).Skip(page * pageSize).Take(pageSize);

                var listPageVm = AutoMapperConfiguration.InitializeAutomapper().Map<IEnumerable<PageViewModel>>(query);

                var paginationSet = new PaginationSet<PageViewModel>()
                {
                    Items = listPageVm,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, paginationSet);

                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Create(HttpRequestMessage request, PageViewModel PageVm)
        {
            return CreateHttpReponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var newPage = new Page();
                    newPage.UpdatePage(PageVm);
                    newPage.CreatedDate = DateTime.Now;
                    newPage.UpdatedBy = User.Identity.Name;

                    _PageService.Add(newPage);
                    _PageService.Save();

                    var responseDate = AutoMapperConfiguration.InitializeAutomapper().Map<PageViewModel>(newPage);
                    response = request.CreateResponse(HttpStatusCode.Created, responseDate);
                }
                return response;
            });
        }


        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, PageViewModel PageVm)
        {
            return CreateHttpReponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var dbPage = _PageService.GetById(PageVm.ID);
                    dbPage.UpdatePage(PageVm);
                    dbPage.UpdatedDate = DateTime.Now;
                    dbPage.UpdatedBy = User.Identity.Name;

                    _PageService.Update(dbPage);
                    _PageService.Save();

                    var responseDate = AutoMapperConfiguration.InitializeAutomapper().Map<PageViewModel>(dbPage);
                    response = request.CreateResponse(HttpStatusCode.Created, responseDate);
                }
                return response;
            });
        }

        [Route("delete")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpReponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var oldPage = _PageService.Delete(id);
                    _PageService.Save();

                    var responseDate = AutoMapperConfiguration.InitializeAutomapper().Map<PageViewModel>(oldPage);
                    response = request.CreateResponse(HttpStatusCode.Created, responseDate);
                }
                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedPages)
        {
            return CreateHttpReponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var listPage = new JavaScriptSerializer().Deserialize<List<int>>(checkedPages);
                    foreach (var item in listPage)
                    {
                        _PageService.Delete(item);
                    }
                    _PageService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, listPage.Count);
                }
                return response;
            });
        }

    }
}

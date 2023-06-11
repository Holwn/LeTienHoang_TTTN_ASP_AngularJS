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
    [RoutePrefix("api/footer")]
    [Authorize]
    public class FooterController : ApiControllerBase
    {
        #region Initializeprivate
        IFooterService _footerService;
        public FooterController(IErrorService errorService, IFooterService footerService)
            : base(errorService)
        {
            this._footerService = footerService;
        }
        [Route("getallparents")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpReponse(request, () =>
            {
                var list = _footerService.GetAll();

                var listPageVm = AutoMapperConfiguration.InitializeAutomapper().Map<List<FooterViewModel>>(list);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listPageVm);

                return response;
            });
        }
        #endregion
        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpReponse(request, () =>
            {
                var list = _footerService.GetById(id);

                var listfooterVm = AutoMapperConfiguration.InitializeAutomapper().Map<FooterViewModel>(list);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listfooterVm);

                return response;
            });
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpReponse(request, () =>
            {
                int totalRow = 0;
                var list = _footerService.GetAll(keyword);

                totalRow = list.Count();
                var query = list.OrderByDescending(x => x.ID).Skip(page * pageSize).Take(pageSize);

                var listfooterVm = AutoMapperConfiguration.InitializeAutomapper().Map<IEnumerable<FooterViewModel>>(query);

                var paginationSet = new PaginationSet<FooterViewModel>()
                {
                    Items = listfooterVm,
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
        public HttpResponseMessage Create(HttpRequestMessage request, FooterViewModel footerVm)
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
                    var newFooter = new Footer();
                    newFooter.UpdateFooter(footerVm);

                    _footerService.Add(newFooter);
                    _footerService.Save();

                    var responseDate = AutoMapperConfiguration.InitializeAutomapper().Map<FooterViewModel>(newFooter);
                    response = request.CreateResponse(HttpStatusCode.Created, responseDate);
                }
                return response;
            });
        }


        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, FooterViewModel footerVm)
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
                    var dbFooter = _footerService.GetById(footerVm.ID);
                    dbFooter.UpdateFooter(footerVm);

                    _footerService.Update(dbFooter);
                    _footerService.Save();

                    var responseDate = AutoMapperConfiguration.InitializeAutomapper().Map<FooterViewModel>(dbFooter);
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
                    var oldFooter = _footerService.Delete(id);
                    _footerService.Save();

                    var responseDate = AutoMapperConfiguration.InitializeAutomapper().Map<FooterViewModel>(oldFooter);
                    response = request.CreateResponse(HttpStatusCode.OK, responseDate);
                }
                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedFooters)
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
                    var listFooter = new JavaScriptSerializer().Deserialize<List<int>>(checkedFooters);
                    foreach (var item in listFooter)
                    {
                        _footerService.Delete(item);
                    }
                    _footerService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, listFooter.Count);
                }
                return response;
            });
        }
    }
}

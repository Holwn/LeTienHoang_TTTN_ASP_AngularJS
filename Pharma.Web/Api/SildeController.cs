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
    [RoutePrefix("api/slide")]
    [Authorize]
    public class SlideController : ApiControllerBase
    {
        #region Initializeprivate
        ISlideService _slideService;
        public SlideController(IErrorService errorService, ISlideService slideService)
            : base(errorService)
        {
            this._slideService = slideService;
        }
        #endregion

        [Route("getallparents")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpReponse(request, () =>
            {
                var list = _slideService.GetAll();

                var listSlideVm = AutoMapperConfiguration.InitializeAutomapper().Map<List<SlideViewModel>>(list);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listSlideVm);

                return response;
            });
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpReponse(request, () =>
            {
                var list = _slideService.GetById(id);

                var listSlideVm = AutoMapperConfiguration.InitializeAutomapper().Map<SlideViewModel>(list);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listSlideVm);

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
                var list = _slideService.GetAll(keyword);

                totalRow = list.Count();
                var query = list.OrderByDescending(x => x.ID).Skip(page * pageSize).Take(pageSize);

                var listSlideVm = AutoMapperConfiguration.InitializeAutomapper().Map<IEnumerable<SlideViewModel>>(query);

                var paginationSet = new PaginationSet<SlideViewModel>()
                {
                    Items = listSlideVm,
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
        public HttpResponseMessage Create(HttpRequestMessage request, SlideViewModel SlideVm)
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
                    var newSlide = new Slide();
                    newSlide.UpdateSlide(SlideVm);

                    _slideService.Add(newSlide);
                    _slideService.Save();

                    var responseDate = AutoMapperConfiguration.InitializeAutomapper().Map<SlideViewModel>(newSlide);
                    response = request.CreateResponse(HttpStatusCode.Created, responseDate);
                }
                return response;
            });
        }


        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, SlideViewModel SlideVm)
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
                    var dbSlide = _slideService.GetById(SlideVm.ID);
                    dbSlide.UpdateSlide(SlideVm);

                    _slideService.Update(dbSlide);
                    _slideService.Save();

                    var responseDate = AutoMapperConfiguration.InitializeAutomapper().Map<SlideViewModel>(dbSlide);
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
                    var oldSlide = _slideService.Delete(id);
                    _slideService.Save();

                    var responseDate = AutoMapperConfiguration.InitializeAutomapper().Map<SlideViewModel>(oldSlide);
                    response = request.CreateResponse(HttpStatusCode.Created, responseDate);
                }
                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedSlides)
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
                    var listSlide = new JavaScriptSerializer().Deserialize<List<int>>(checkedSlides);
                    foreach (var item in listSlide)
                    {
                        _slideService.Delete(item);
                    }
                    _slideService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, listSlide.Count);
                }
                return response;
            });
        }

    }
}

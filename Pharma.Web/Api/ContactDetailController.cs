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
    [RoutePrefix("api/contactdetail")]
    [Authorize]
    public class ContactDetailController : ApiControllerBase
    {
        #region Initializeprivate
        IContactDetailService _contactDetailService;
        public ContactDetailController(IErrorService errorService, IContactDetailService ContactDetailService)
            : base(errorService)
        {
            this._contactDetailService = ContactDetailService;
        }
        #endregion

        [Route("getallparents")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpReponse(request, () =>
            {
                var list = _contactDetailService.GetAll();

                var listContactDetailVm = AutoMapperConfiguration.InitializeAutomapper().Map<List<ContactDetailViewModel>>(list);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listContactDetailVm);

                return response;
            });
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpReponse(request, () =>
            {
                var list = _contactDetailService.GetById(id);

                var listContactDetailVm = AutoMapperConfiguration.InitializeAutomapper().Map<ContactDetailViewModel>(list);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listContactDetailVm);

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
                var list = _contactDetailService.GetAll(keyword);

                totalRow = list.Count();
                var query = list.OrderByDescending(x => x.ID).Skip(page * pageSize).Take(pageSize);

                var listContactDetailVm = AutoMapperConfiguration.InitializeAutomapper().Map<IEnumerable<ContactDetailViewModel>>(query);

                var paginationSet = new PaginationSet<ContactDetailViewModel>()
                {
                    Items = listContactDetailVm,
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
        public HttpResponseMessage Create(HttpRequestMessage request, ContactDetailViewModel ContactDetailVm)
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
                    var newContactDetail = new ContactDetail();
                    newContactDetail.UpdateContactDetail(ContactDetailVm);

                    _contactDetailService.Add(newContactDetail);
                    _contactDetailService.Save();

                    var responseDate = AutoMapperConfiguration.InitializeAutomapper().Map<ContactDetailViewModel>(newContactDetail);
                    response = request.CreateResponse(HttpStatusCode.Created, responseDate);
                }
                return response;
            });
        }


        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, ContactDetailViewModel ContactDetailVm)
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
                    var dbContactDetail = _contactDetailService.GetById(ContactDetailVm.ID);
                    dbContactDetail.UpdateContactDetail(ContactDetailVm);

                    _contactDetailService.Update(dbContactDetail);
                    _contactDetailService.Save();

                    var responseDate = AutoMapperConfiguration.InitializeAutomapper().Map<ContactDetailViewModel>(dbContactDetail);
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
                    var oldContactDetail = _contactDetailService.Delete(id);
                    _contactDetailService.Save();

                    var responseDate = AutoMapperConfiguration.InitializeAutomapper().Map<ContactDetailViewModel>(oldContactDetail);
                    response = request.CreateResponse(HttpStatusCode.Created, responseDate);
                }
                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedContactdetails)
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
                    var listContactDetail = new JavaScriptSerializer().Deserialize<List<int>>(checkedContactdetails);
                    foreach (var item in listContactDetail)
                    {
                        _contactDetailService.Delete(item);
                    }
                    _contactDetailService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, listContactDetail.Count);
                }
                return response;
            });
        }

    }
}

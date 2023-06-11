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
    [RoutePrefix("api/deliverynote")]
    [Authorize]
    public class DeliveryNoteController : ApiControllerBase
    {
        #region Initializeprivate
        IDeliveryNoteService _deliveryNoteService;
        public DeliveryNoteController(IErrorService errorService, IDeliveryNoteService deliveryNoteService)
            : base(errorService)
        {
            this._deliveryNoteService = deliveryNoteService;
        }
        #endregion

        [Route("getallparents")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpReponse(request, () =>
            {
                var list = _deliveryNoteService.GetAll();

                var listDeliveryNoteVm = AutoMapperConfiguration.InitializeAutomapper().Map<List<DeliveryNoteViewModel>>(list);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listDeliveryNoteVm);

                return response;
            });
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpReponse(request, () =>
            {
                var list = _deliveryNoteService.GetById(id);

                var listDeliveryNoteVm = AutoMapperConfiguration.InitializeAutomapper().Map<DeliveryNoteViewModel>(list);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listDeliveryNoteVm);

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
                var list = _deliveryNoteService.GetAll(keyword);

                totalRow = list.Count();
                var query = list.OrderByDescending(x => x.ID).Skip(page * pageSize).Take(pageSize);

                var listDeliveryNoteVm = AutoMapperConfiguration.InitializeAutomapper().Map<IEnumerable<DeliveryNoteViewModel>>(query);

                var paginationSet = new PaginationSet<DeliveryNoteViewModel>()
                {
                    Items = listDeliveryNoteVm,
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
        public HttpResponseMessage Create(HttpRequestMessage request, DeliveryNoteViewModel DeliveryNoteVm)
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
                    var newDeliveryNote = new DeliveryNote();
                    newDeliveryNote.UpdateDeliveryNote(DeliveryNoteVm);
                    newDeliveryNote.CreatedDate = DateTime.Now;
                    newDeliveryNote.UpdatedBy = User.Identity.Name;

                    _deliveryNoteService.Add(newDeliveryNote);
                    _deliveryNoteService.Save();

                    var responseDate = AutoMapperConfiguration.InitializeAutomapper().Map<DeliveryNoteViewModel>(newDeliveryNote);
                    response = request.CreateResponse(HttpStatusCode.Created, responseDate);
                }
                return response;
            });
        }


        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, DeliveryNoteViewModel DeliveryNoteVm)
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
                    var dbDeliveryNote = _deliveryNoteService.GetById(DeliveryNoteVm.ID);
                    dbDeliveryNote.UpdateDeliveryNote(DeliveryNoteVm);
                    dbDeliveryNote.UpdatedDate = DateTime.Now;
                    dbDeliveryNote.UpdatedBy = User.Identity.Name;

                    _deliveryNoteService.Update(dbDeliveryNote);
                    _deliveryNoteService.Save();

                    var responseDate = AutoMapperConfiguration.InitializeAutomapper().Map<DeliveryNoteViewModel>(dbDeliveryNote);
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
                    var oldDeliveryNote = _deliveryNoteService.Delete(id);
                    _deliveryNoteService.Save();

                    var responseDate = AutoMapperConfiguration.InitializeAutomapper().Map<DeliveryNoteViewModel>(oldDeliveryNote);
                    response = request.CreateResponse(HttpStatusCode.Created, responseDate);
                }
                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedDeliveryNotes)
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
                    var listDeliveryNote = new JavaScriptSerializer().Deserialize<List<int>>(checkedDeliveryNotes);
                    foreach (var item in listDeliveryNote)
                    {
                        _deliveryNoteService.Delete(item);
                    }
                    _deliveryNoteService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, listDeliveryNote.Count);
                }
                return response;
            });
        }

    }
}

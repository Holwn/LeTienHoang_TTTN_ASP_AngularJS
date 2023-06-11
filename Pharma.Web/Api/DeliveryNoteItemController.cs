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
    [RoutePrefix("api/deliverynoteitem")]
    [Authorize]
    public class DeliveryNoteItemController : ApiControllerBase
    {
        #region Initializeprivate
        IDeliveryNoteItemService _deliveryNoteItemService;
        public DeliveryNoteItemController(IErrorService errorService, IDeliveryNoteItemService DeliveryNoteItemService)
            : base(errorService)
        {
            this._deliveryNoteItemService = DeliveryNoteItemService;
        }
        #endregion

        [Route("getallparents")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpReponse(request, () =>
            {
                var list = _deliveryNoteItemService.GetAll();

                var listDeliveryNoteItemVm = AutoMapperConfiguration.InitializeAutomapper().Map<List<DeliveryNoteItemViewModel>>(list);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listDeliveryNoteItemVm);

                return response;
            });
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpReponse(request, () =>
            {
                var list = _deliveryNoteItemService.GetById(id);

                var listDeliveryNoteItemVm = AutoMapperConfiguration.InitializeAutomapper().Map<DeliveryNoteItemViewModel>(list);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listDeliveryNoteItemVm);

                return response;
            });
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request, int noteId)
        {
            return CreateHttpReponse(request, () =>
            {
                var list = _deliveryNoteItemService.GetAllByNoteId(noteId);

                var listDeliveryNoteItemVm = AutoMapperConfiguration.InitializeAutomapper().Map<IEnumerable<DeliveryNoteItemViewModel>>(list);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listDeliveryNoteItemVm);

                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Create(HttpRequestMessage request, DeliveryNoteItemViewModel DeliveryNoteItemVm)
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
                    var newDeliveryNoteItem = new DeliveryNoteItem();
                    newDeliveryNoteItem.UpdateDeliveryNoteItem(DeliveryNoteItemVm);

                    _deliveryNoteItemService.Add(newDeliveryNoteItem);
                    _deliveryNoteItemService.Save();

                    var responseDate = AutoMapperConfiguration.InitializeAutomapper().Map<DeliveryNoteItemViewModel>(newDeliveryNoteItem);
                    response = request.CreateResponse(HttpStatusCode.Created, responseDate);
                }
                return response;
            });
        }


        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, DeliveryNoteItemViewModel DeliveryNoteItemVm)
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
                    var dbDeliveryNoteItem = _deliveryNoteItemService.GetByNoteId(DeliveryNoteItemVm.NoteID, DeliveryNoteItemVm.ProductID);
                    dbDeliveryNoteItem.UpdateDeliveryNoteItem(DeliveryNoteItemVm);

                    _deliveryNoteItemService.Update(dbDeliveryNoteItem);
                    _deliveryNoteItemService.Save();

                    var responseDate = AutoMapperConfiguration.InitializeAutomapper().Map<DeliveryNoteItemViewModel>(dbDeliveryNoteItem);
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
                    var oldDeliveryNoteItem = _deliveryNoteItemService.Delete(id);
                    _deliveryNoteItemService.Save();

                    var responseDate = AutoMapperConfiguration.InitializeAutomapper().Map<DeliveryNoteItemViewModel>(oldDeliveryNoteItem);
                    response = request.CreateResponse(HttpStatusCode.Created, responseDate);
                }
                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedDeliveryNoteItems)
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
                    var listDeliveryNoteItem = new JavaScriptSerializer().Deserialize<List<int>>(checkedDeliveryNoteItems);
                    foreach (var item in listDeliveryNoteItem)
                    {
                        _deliveryNoteItemService.Delete(item);
                    }
                    _deliveryNoteItemService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, listDeliveryNoteItem.Count);
                }
                return response;
            });
        }

    }
}

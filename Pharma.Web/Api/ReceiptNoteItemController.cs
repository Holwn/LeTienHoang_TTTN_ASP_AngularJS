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
    [RoutePrefix("api/receiptnoteitem")]
    [Authorize]
    public class ReceiptNoteItemController : ApiControllerBase
    {
        #region Initializeprivate
        IReceiptNoteItemService _receiptNoteItemService;
        public ReceiptNoteItemController(IErrorService errorService, IReceiptNoteItemService receiptNoteItemService)
            : base(errorService)
        {
            this._receiptNoteItemService = receiptNoteItemService;
        }
        #endregion

        [Route("getallparents")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpReponse(request, () =>
            {
                var list = _receiptNoteItemService.GetAll();

                var listReceiptNoteItemVm = AutoMapperConfiguration.InitializeAutomapper().Map<List<ReceiptNoteItemViewModel>>(list);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listReceiptNoteItemVm);

                return response;
            });
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpReponse(request, () =>
            {
                var list = _receiptNoteItemService.GetById(id);

                var listReceiptNoteItemVm = AutoMapperConfiguration.InitializeAutomapper().Map<ReceiptNoteItemViewModel>(list);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listReceiptNoteItemVm);

                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Create(HttpRequestMessage request, ReceiptNoteItemViewModel ReceiptNoteItemVm)
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
                    var newReceiptNoteItem = new ReceiptNoteItem();
                    newReceiptNoteItem.UpdateReceiptNoteItem(ReceiptNoteItemVm);

                    _receiptNoteItemService.Add(newReceiptNoteItem);
                    _receiptNoteItemService.Save();

                    var responseDate = AutoMapperConfiguration.InitializeAutomapper().Map<ReceiptNoteItemViewModel>(newReceiptNoteItem);
                    response = request.CreateResponse(HttpStatusCode.Created, responseDate);
                }
                return response;
            });
        }


        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, ReceiptNoteItemViewModel ReceiptNoteItemVm)
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
                    var dbReceiptNoteItem = _receiptNoteItemService.GetById(ReceiptNoteItemVm.NoteID);
                    dbReceiptNoteItem.UpdateReceiptNoteItem(ReceiptNoteItemVm);

                    _receiptNoteItemService.Update(dbReceiptNoteItem);
                    _receiptNoteItemService.Save();

                    var responseDate = AutoMapperConfiguration.InitializeAutomapper().Map<ReceiptNoteItemViewModel>(dbReceiptNoteItem);
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
                    var oldReceiptNoteItem = _receiptNoteItemService.Delete(id);
                    _receiptNoteItemService.Save();

                    var responseDate = AutoMapperConfiguration.InitializeAutomapper().Map<ReceiptNoteItemViewModel>(oldReceiptNoteItem);
                    response = request.CreateResponse(HttpStatusCode.Created, responseDate);
                }
                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedReceiptNoteItems)
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
                    var listReceiptNoteItem = new JavaScriptSerializer().Deserialize<List<int>>(checkedReceiptNoteItems);
                    foreach (var item in listReceiptNoteItem)
                    {
                        _receiptNoteItemService.Delete(item);
                    }
                    _receiptNoteItemService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, listReceiptNoteItem.Count);
                }
                return response;
            });
        }

    }
}

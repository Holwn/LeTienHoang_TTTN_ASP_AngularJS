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
    [RoutePrefix("api/receiptnote")]
    [Authorize]
    public class ReceiptNoteController : ApiControllerBase
    {
        #region Initializeprivate
        IReceiptNoteService _receiptNoteService;
        public ReceiptNoteController(IErrorService errorService, IReceiptNoteService receiptNoteService)
            : base(errorService)
        {
            this._receiptNoteService = receiptNoteService;
        }
        #endregion

        [Route("getallparents")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpReponse(request, () =>
            {
                var list = _receiptNoteService.GetAll();

                var listReceiptNoteVm = AutoMapperConfiguration.InitializeAutomapper().Map<List<ReceiptNoteViewModel>>(list);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listReceiptNoteVm);

                return response;
            });
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpReponse(request, () =>
            {
                var list = _receiptNoteService.GetById(id);

                var listReceiptNoteVm = AutoMapperConfiguration.InitializeAutomapper().Map<ReceiptNoteViewModel>(list);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listReceiptNoteVm);

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
                var list = _receiptNoteService.GetAll(keyword);

                totalRow = list.Count();
                var query = list.OrderByDescending(x => x.ID).Skip(page * pageSize).Take(pageSize);

                var listReceiptNoteVm = AutoMapperConfiguration.InitializeAutomapper().Map<IEnumerable<ReceiptNoteViewModel>>(query);

                var paginationSet = new PaginationSet<ReceiptNoteViewModel>()
                {
                    Items = listReceiptNoteVm,
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
        public HttpResponseMessage Create(HttpRequestMessage request, ReceiptNoteViewModel ReceiptNoteVm)
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
                    var newReceiptNote = new ReceiptNote();
                    newReceiptNote.UpdateReceiptNote(ReceiptNoteVm);
                    newReceiptNote.CreatedDate = DateTime.Now;
                    newReceiptNote.UpdatedBy = User.Identity.Name;

                    _receiptNoteService.Add(newReceiptNote);
                    _receiptNoteService.Save();

                    var responseDate = AutoMapperConfiguration.InitializeAutomapper().Map<ReceiptNoteViewModel>(newReceiptNote);
                    response = request.CreateResponse(HttpStatusCode.Created, responseDate);
                }
                return response;
            });
        }


        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, ReceiptNoteViewModel ReceiptNoteVm)
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
                    var dbReceiptNote = _receiptNoteService.GetById(ReceiptNoteVm.ID);
                    dbReceiptNote.UpdateReceiptNote(ReceiptNoteVm);
                    dbReceiptNote.UpdatedDate = DateTime.Now;
                    dbReceiptNote.UpdatedBy = User.Identity.Name;

                    _receiptNoteService.Update(dbReceiptNote);
                    _receiptNoteService.Save();

                    var responseDate = AutoMapperConfiguration.InitializeAutomapper().Map<ReceiptNoteViewModel>(dbReceiptNote);
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
                    var oldReceiptNote = _receiptNoteService.Delete(id);
                    _receiptNoteService.Save();

                    var responseDate = AutoMapperConfiguration.InitializeAutomapper().Map<ReceiptNoteViewModel>(oldReceiptNote);
                    response = request.CreateResponse(HttpStatusCode.Created, responseDate);
                }
                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedReceiptNotes)
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
                    var listReceiptNote = new JavaScriptSerializer().Deserialize<List<int>>(checkedReceiptNotes);
                    foreach (var item in listReceiptNote)
                    {
                        _receiptNoteService.Delete(item);
                    }
                    _receiptNoteService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, listReceiptNote.Count);
                }
                return response;
            });
        }

    }
}

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
    [RoutePrefix("api/notebook")]
    [Authorize]
    public class NoteBookController : ApiControllerBase
    {
        #region Initializeprivate
        INoteBookService _NoteBookService;
        public NoteBookController(IErrorService errorService, INoteBookService NoteBookService)
            : base(errorService)
        {
            this._NoteBookService = NoteBookService;
        }
        #endregion

        [Route("getallparents")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpReponse(request, () =>
            {
                var list = _NoteBookService.GetAll();

                var listNoteBookVm = AutoMapperConfiguration.InitializeAutomapper().Map<List<NoteBookViewModel>>(list);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listNoteBookVm);

                return response;
            });
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpReponse(request, () =>
            {
                var list = _NoteBookService.GetById(id);

                var listNoteBookVm = AutoMapperConfiguration.InitializeAutomapper().Map<NoteBookViewModel>(list);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listNoteBookVm);

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
                var list = _NoteBookService.GetAll(keyword);

                totalRow = list.Count();
                var query = list.OrderByDescending(x => x.ID).Skip(page * pageSize).Take(pageSize);

                var listNoteBookVm = AutoMapperConfiguration.InitializeAutomapper().Map<IEnumerable<NoteBookViewModel>>(query);

                var paginationSet = new PaginationSet<NoteBookViewModel>()
                {
                    Items = listNoteBookVm,
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
        public HttpResponseMessage Create(HttpRequestMessage request, NoteBookViewModel NoteBookVm)
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
                    var newNoteBook = new NoteBook();
                    newNoteBook.UpdateNoteBook(NoteBookVm);
                    newNoteBook.CreatedDate = DateTime.Now;
                    newNoteBook.UpdatedBy = User.Identity.Name;

                    _NoteBookService.Add(newNoteBook);
                    _NoteBookService.Save();

                    var responseDate = AutoMapperConfiguration.InitializeAutomapper().Map<NoteBookViewModel>(newNoteBook);
                    response = request.CreateResponse(HttpStatusCode.Created, responseDate);
                }
                return response;
            });
        }


        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, NoteBookViewModel NoteBookVm)
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
                    var dbNoteBook = _NoteBookService.GetById(NoteBookVm.ID);
                    dbNoteBook.UpdateNoteBook(NoteBookVm);
                    dbNoteBook.UpdatedDate = DateTime.Now;
                    dbNoteBook.UpdatedBy = User.Identity.Name;

                    _NoteBookService.Update(dbNoteBook);
                    _NoteBookService.Save();

                    var responseDate = AutoMapperConfiguration.InitializeAutomapper().Map<NoteBookViewModel>(dbNoteBook);
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
                    var oldNoteBook = _NoteBookService.Delete(id);
                    _NoteBookService.Save();

                    var responseDate = AutoMapperConfiguration.InitializeAutomapper().Map<NoteBookViewModel>(oldNoteBook);
                    response = request.CreateResponse(HttpStatusCode.Created, responseDate);
                }
                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedNoteBooks)
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
                    var listNoteBook = new JavaScriptSerializer().Deserialize<List<int>>(checkedNoteBooks);
                    foreach (var item in listNoteBook)
                    {
                        _NoteBookService.Delete(item);
                    }
                    _NoteBookService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, listNoteBook.Count);
                }
                return response;
            });
        }

    }
}

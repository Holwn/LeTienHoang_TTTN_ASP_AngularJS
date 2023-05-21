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
    [RoutePrefix("api/subject")]
    [Authorize]
    public class SubjectController : ApiControllerBase
    {
        #region Initializeprivate
        ISubjectService _subjectService;
        public SubjectController(IErrorService errorService, ISubjectService subjectService)
            : base(errorService)
        {
            this._subjectService = subjectService;
        }
        #endregion

        [Route("getallparents")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpReponse(request, () =>
            {
                var list = _subjectService.GetAll();

                var listSubjectVm = AutoMapperConfiguration.InitializeAutomapper().Map<List<SubjectViewModel>>(list);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listSubjectVm);

                return response;
            });
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpReponse(request, () =>
            {
                var list = _subjectService.GetById(id);

                var listSubjectVm = AutoMapperConfiguration.InitializeAutomapper().Map<SubjectViewModel>(list);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listSubjectVm);

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
                var list = _subjectService.GetAll(keyword);

                totalRow = list.Count();
                var query = list.OrderByDescending(x => x.ID).Skip(page * pageSize).Take(pageSize);

                var listSubjectVm = AutoMapperConfiguration.InitializeAutomapper().Map<IEnumerable<SubjectViewModel>>(query);

                var paginationSet = new PaginationSet<SubjectViewModel>()
                {
                    Items = listSubjectVm,
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
        public HttpResponseMessage Create(HttpRequestMessage request, SubjectViewModel SubjectVm)
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
                    var newSubject = new Subject();
                    newSubject.UpdateSubject(SubjectVm);
                    newSubject.CreatedDate = DateTime.Now;
                    newSubject.UpdatedBy = User.Identity.Name;

                    _subjectService.Add(newSubject);
                    _subjectService.Save();

                    var responseDate = AutoMapperConfiguration.InitializeAutomapper().Map<SubjectViewModel>(newSubject);
                    response = request.CreateResponse(HttpStatusCode.Created, responseDate);
                }
                return response;
            });
        }


        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, SubjectViewModel SubjectVm)
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
                    var dbSubject = _subjectService.GetById(SubjectVm.ID);
                    dbSubject.UpdateSubject(SubjectVm);
                    dbSubject.UpdatedDate = DateTime.Now;
                    dbSubject.UpdatedBy = User.Identity.Name;

                    _subjectService.Update(dbSubject);
                    _subjectService.Save();

                    var responseDate = AutoMapperConfiguration.InitializeAutomapper().Map<SubjectViewModel>(dbSubject);
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
                    var oldSubject = _subjectService.Delete(id);
                    _subjectService.Save();

                    var responseDate = AutoMapperConfiguration.InitializeAutomapper().Map<SubjectViewModel>(oldSubject);
                    response = request.CreateResponse(HttpStatusCode.Created, responseDate);
                }
                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedSubjects)
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
                    var listSubject = new JavaScriptSerializer().Deserialize<List<int>>(checkedSubjects);
                    foreach (var item in listSubject)
                    {
                        _subjectService.Delete(item);
                    }
                    _subjectService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, listSubject.Count);
                }
                return response;
            });
        }

    }
}

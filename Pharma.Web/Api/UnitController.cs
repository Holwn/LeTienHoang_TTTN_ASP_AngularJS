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
    [RoutePrefix("api/unit")]
    public class UnitController : ApiControllerBase
    {
        #region Initializeprivate
        IUnitService _unitService;
        public UnitController(IErrorService errorService, IUnitService unitService)
            : base(errorService)
        {
            this._unitService = unitService;
        }
        #endregion

        [Route("getallparents")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpReponse(request, () =>
            {
                var list = _unitService.GetAll();

                var listUnitVm = AutoMapperConfiguration.InitializeAutomapper().Map<List<UnitViewModel>>(list);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listUnitVm);

                return response;
            });
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpReponse(request, () =>
            {
                var list = _unitService.GetById(id);

                var listUnitVm = AutoMapperConfiguration.InitializeAutomapper().Map<UnitViewModel>(list);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listUnitVm);

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
                var list = _unitService.GetAll(keyword);

                totalRow = list.Count();
                var query = list.OrderByDescending(x => x.ID).Skip(page * pageSize).Take(pageSize);

                var listUnitVm = AutoMapperConfiguration.InitializeAutomapper().Map<IEnumerable<UnitViewModel>>(query);

                var paginationSet = new PaginationSet<UnitViewModel>()
                {
                    Items = listUnitVm,
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
        public HttpResponseMessage Create(HttpRequestMessage request, UnitViewModel unitVm)
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
                    var newUnit = new Unit();
                    newUnit.UpdateUnit(unitVm);

                    _unitService.Add(newUnit);
                    _unitService.Save();

                    var responseDate = AutoMapperConfiguration.InitializeAutomapper().Map<UnitViewModel>(newUnit);
                    response = request.CreateResponse(HttpStatusCode.Created, responseDate);
                }
                return response;
            });
        }


        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, UnitViewModel unitVm)
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
                    var dbUnit = _unitService.GetById(unitVm.ID);
                    dbUnit.UpdateUnit(unitVm);

                    _unitService.Update(dbUnit);
                    _unitService.Save();

                    var responseDate = AutoMapperConfiguration.InitializeAutomapper().Map<UnitViewModel>(dbUnit);
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
                    var oldUnit = _unitService.Delete(id);
                    _unitService.Save();

                    var responseDate = AutoMapperConfiguration.InitializeAutomapper().Map<UnitViewModel>(oldUnit);
                    response = request.CreateResponse(HttpStatusCode.Created, responseDate);
                }
                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedUnits)
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
                    var listUnit = new JavaScriptSerializer().Deserialize<List<int>>(checkedUnits);
                    foreach (var item in listUnit)
                    {
                        _unitService.Delete(item);
                    }
                    _unitService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, listUnit.Count);
                }
                return response;
            });
        }

    }
}

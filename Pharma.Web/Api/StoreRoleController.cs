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
    [RoutePrefix("api/storeRole")]
    [Authorize]
    public class StoreRoleController : ApiControllerBase
    {
        #region Initializeprivate
        IStoreRoleService _storeRoleService;
        public StoreRoleController(IErrorService errorService, IStoreRoleService storeRoleService)
            : base(errorService)
        {
            this._storeRoleService = storeRoleService;
        }
        #endregion

        [Route("getallparents")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpReponse(request, () =>
            {
                var list = _storeRoleService.GetAll();

                var listStoreRoleVm = AutoMapperConfiguration.InitializeAutomapper().Map<List<StoreRoleViewModel>>(list);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listStoreRoleVm);

                return response;
            });
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpReponse(request, () =>
            {
                var list = _storeRoleService.GetById(id);

                var listStoreRoleVm = AutoMapperConfiguration.InitializeAutomapper().Map<StoreRoleViewModel>(list);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listStoreRoleVm);

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
                var list = _storeRoleService.GetAll(keyword);

                totalRow = list.Count();
                var query = list.OrderByDescending(x => x.ID).Skip(page * pageSize).Take(pageSize);

                var listStoreRoleVm = AutoMapperConfiguration.InitializeAutomapper().Map<IEnumerable<StoreRoleViewModel>>(query);

                var paginationSet = new PaginationSet<StoreRoleViewModel>()
                {
                    Items = listStoreRoleVm,
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
        public HttpResponseMessage Create(HttpRequestMessage request, StoreRoleViewModel StoreRoleVm)
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
                    var newStoreRole = new StoreRole();
                    newStoreRole.UpdateStoreRole(StoreRoleVm);

                    _storeRoleService.Add(newStoreRole);
                    _storeRoleService.Save();

                    var responseDate = AutoMapperConfiguration.InitializeAutomapper().Map<StoreRoleViewModel>(newStoreRole);
                    response = request.CreateResponse(HttpStatusCode.Created, responseDate);
                }
                return response;
            });
        }


        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, StoreRoleViewModel StoreRoleVm)
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
                    var dbStoreRole = _storeRoleService.GetById(StoreRoleVm.ID);
                    dbStoreRole.UpdateStoreRole(StoreRoleVm);

                    _storeRoleService.Update(dbStoreRole);
                    _storeRoleService.Save();

                    var responseDate = AutoMapperConfiguration.InitializeAutomapper().Map<StoreRoleViewModel>(dbStoreRole);
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
                    var oldStoreRole = _storeRoleService.Delete(id);
                    _storeRoleService.Save();

                    var responseDate = AutoMapperConfiguration.InitializeAutomapper().Map<StoreRoleViewModel>(oldStoreRole);
                    response = request.CreateResponse(HttpStatusCode.Created, responseDate);
                }
                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedStoreRoles)
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
                    var listStoreRole = new JavaScriptSerializer().Deserialize<List<int>>(checkedStoreRoles);
                    foreach (var item in listStoreRole)
                    {
                        _storeRoleService.Delete(item);
                    }
                    _storeRoleService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, listStoreRole.Count);
                }
                return response;
            });
        }

    }
}

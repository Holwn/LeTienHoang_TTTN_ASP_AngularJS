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
    [RoutePrefix("api/store")]
    public class StoreController : ApiControllerBase
    {
        #region Initializeprivate
        IStoreService _storeService;
        public StoreController(IErrorService errorService, IStoreService storeService)
            : base(errorService)
        {
            this._storeService = storeService;
        }
        #endregion

        [Route("getallparents")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpReponse(request, () =>
            {
                var list = _storeService.GetAll();

                var listStoreVm = AutoMapperConfiguration.InitializeAutomapper().Map<List<StoreViewModel>>(list);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listStoreVm);

                return response;
            });
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpReponse(request, () =>
            {
                var list = _storeService.GetById(id);

                var listStoreVm = AutoMapperConfiguration.InitializeAutomapper().Map<StoreViewModel>(list);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listStoreVm);

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
                var list = _storeService.GetAll(keyword);

                totalRow = list.Count();
                var query = list.OrderByDescending(x => x.ID).Skip(page * pageSize).Take(pageSize);

                var listStoreVm = AutoMapperConfiguration.InitializeAutomapper().Map<IEnumerable<StoreViewModel>>(query);

                var paginationSet = new PaginationSet<StoreViewModel>()
                {
                    Items = listStoreVm,
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
        public HttpResponseMessage Create(HttpRequestMessage request, StoreViewModel storeVm)
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
                    var newStore = new Store();
                    newStore.UpdateStore(storeVm);

                    _storeService.Add(newStore);
                    _storeService.Save();

                    var responseDate = AutoMapperConfiguration.InitializeAutomapper().Map<StoreViewModel>(newStore);
                    response = request.CreateResponse(HttpStatusCode.Created, responseDate);
                }
                return response;
            });
        }


        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, StoreViewModel storeVm)
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
                    var dbStore = _storeService.GetById(storeVm.ID);
                    dbStore.UpdateStore(storeVm);

                    _storeService.Update(dbStore);
                    _storeService.Save();

                    var responseDate = AutoMapperConfiguration.InitializeAutomapper().Map<StoreViewModel>(dbStore);
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
                    var oldStore = _storeService.Delete(id);
                    _storeService.Save();

                    var responseDate = AutoMapperConfiguration.InitializeAutomapper().Map<StoreViewModel>(oldStore);
                    response = request.CreateResponse(HttpStatusCode.Created, responseDate);
                }
                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedstores)
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
                    var listStore = new JavaScriptSerializer().Deserialize<List<int>>(checkedstores);
                    foreach (var item in listStore)
                    {
                        _storeService.Delete(item);
                    }
                    _storeService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, listStore.Count);
                }
                return response;
            });
        }

    }
}

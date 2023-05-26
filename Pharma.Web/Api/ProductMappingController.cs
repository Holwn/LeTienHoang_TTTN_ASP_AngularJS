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
    [RoutePrefix("api/productmapping")]
    [Authorize]
    public class ProductMappingController : ApiControllerBase
    {
        #region Initializeprivate
        IProductMappingService _productMappingService;
        public ProductMappingController(IErrorService errorService, IProductMappingService ProductMappingService)
            : base(errorService)
        {
            this._productMappingService = ProductMappingService;
        }
        #endregion

        [Route("getallparents")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpReponse(request, () =>
            {
                var list = _productMappingService.GetAll();

                var listProductMappingVm = AutoMapperConfiguration.InitializeAutomapper().Map<List<ProductMappingViewModel>>(list);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listProductMappingVm);

                return response;
            });
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpReponse(request, () =>
            {
                var list = _productMappingService.GetById(id);

                var listProductMappingVm = AutoMapperConfiguration.InitializeAutomapper().Map<ProductMappingViewModel>(list);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listProductMappingVm);

                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Create(HttpRequestMessage request, ProductMappingViewModel ProductMappingVm)
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
                    var newProductMapping = new ProductMapping();
                    newProductMapping.UpdateProductMapping(ProductMappingVm);

                    _productMappingService.Add(newProductMapping);
                    _productMappingService.Save();

                    var responseDate = AutoMapperConfiguration.InitializeAutomapper().Map<ProductMappingViewModel>(newProductMapping);
                    response = request.CreateResponse(HttpStatusCode.Created, responseDate);
                }
                return response;
            });
        }


        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, ProductMappingViewModel ProductMappingVm)
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
                    var dbProductMapping = _productMappingService.GetById(ProductMappingVm.ID);
                    dbProductMapping.UpdateProductMapping(ProductMappingVm);

                    _productMappingService.Update(dbProductMapping);
                    _productMappingService.Save();

                    var responseDate = AutoMapperConfiguration.InitializeAutomapper().Map<ProductMappingViewModel>(dbProductMapping);
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
                    var oldProductMapping = _productMappingService.Delete(id);
                    _productMappingService.Save();

                    var responseDate = AutoMapperConfiguration.InitializeAutomapper().Map<ProductMappingViewModel>(oldProductMapping);
                    response = request.CreateResponse(HttpStatusCode.Created, responseDate);
                }
                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedProductMappings)
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
                    var listProductMapping = new JavaScriptSerializer().Deserialize<List<int>>(checkedProductMappings);
                    foreach (var item in listProductMapping)
                    {
                        _productMappingService.Delete(item);
                    }
                    _productMappingService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, listProductMapping.Count);
                }
                return response;
            });
        }

    }
}

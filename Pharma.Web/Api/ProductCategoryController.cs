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

namespace Pharma.Web.Api
{
    [RoutePrefix("api/productcategory")]
    public class ProductCategoryController : ApiControllerBase
    {
        IProductCategoryService _productCategoryService;
        public ProductCategoryController(IErrorService errorService,IProductCategoryService productCategoryService) 
            : base(errorService)
        {
            this._productCategoryService = productCategoryService;
        }

        [Route("getallparents")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage requestMessage)
        {
            return CreateHttpReponse(requestMessage, () =>
            {
                var listCategory = _productCategoryService.GetAll();

                var listproductCategoryVm = AutoMapperConfiguration.InitializeAutomapper().Map<List<ProductCategoryViewModel>>(listCategory);
                
                HttpResponseMessage responseMessage = requestMessage.CreateResponse(HttpStatusCode.OK, listproductCategoryVm);

                return responseMessage;
            });
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage requestMessage,string keyword,int page,int pageSize = 20)
        {
            return CreateHttpReponse(requestMessage, () =>
            {
                int totalRow = 0;
                var listCategory = _productCategoryService.GetAll(keyword);

                totalRow = listCategory.Count();
                var query = listCategory.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);

                var listproductCategoryVm = AutoMapperConfiguration.InitializeAutomapper().Map<List<ProductCategoryViewModel>>(query);

                var paginationSet = new PaginationSet<ProductCategoryViewModel>()
                {
                    Items = listproductCategoryVm,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };

                HttpResponseMessage responseMessage = requestMessage.CreateResponse(HttpStatusCode.OK, paginationSet);

                return responseMessage;
            });
        }

        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Create(HttpRequestMessage request,ProductCategoryViewModel productCategoryVm)
        {
            return CreateHttpReponse(request, () =>
            {
                HttpResponseMessage response = null;
                if(!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }    
                else
                {
                    var newProductCategory = new ProductCategory();
                    newProductCategory.UpdateProductCategory(productCategoryVm);

                    _productCategoryService.Add(newProductCategory);
                    _productCategoryService.Save();

                    var responseDate = AutoMapperConfiguration.InitializeAutomapper().Map<ProductCategoryViewModel>(newProductCategory);
                    response = request.CreateResponse(HttpStatusCode.Created, responseDate);
                }
                return response;
            });
        }

    }
}

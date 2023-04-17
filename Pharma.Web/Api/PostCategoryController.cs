using Pharma.Model.Models;
using Pharma.Service;
using Pharma.Web.Infrastructure.Core;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Pharma.Web.Api
{
    [RoutePrefix("api/postcategory")]
    public class PostCategoryController : ApiControllerBase
    {
        IPostCategoryService _postCategoryService;
        public PostCategoryController(IErrorService errorService,IPostCategoryService postCategoryService) : base(errorService)
        {
            this._postCategoryService = postCategoryService;
        }
        [Route("getall")]
        public HttpResponseMessage Get(HttpRequestMessage requestMessage, PostCategory postCategory)
        {
            return CreateHttpReponse(requestMessage, () =>
            {
                HttpResponseMessage responseMessage = null;
                if (ModelState.IsValid)
                {
                    requestMessage.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var listCategory = _postCategoryService.GetAll();

                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.OK, listCategory);
                }
                return responseMessage;
            });
        }
        public HttpResponseMessage Post(HttpRequestMessage requestMessage,PostCategory postCategory)
        {
            return CreateHttpReponse(requestMessage, () =>
             {
                 HttpResponseMessage responseMessage = null;
                 if(ModelState.IsValid)
                 {
                     requestMessage.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                 }
                 else
                 {
                     var category = _postCategoryService.Add(postCategory);
                     _postCategoryService.Save();

                     responseMessage = requestMessage.CreateResponse(HttpStatusCode.Created, category);
                 }
                 return responseMessage;
             });
        }
        public HttpResponseMessage Put(HttpRequestMessage requestMessage, PostCategory postCategory)
        {
            return CreateHttpReponse(requestMessage, () =>
            {
                HttpResponseMessage responseMessage = null;
                if (ModelState.IsValid)
                {
                    requestMessage.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    _postCategoryService.Update(postCategory);
                    _postCategoryService.Save();

                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.OK);
                }
                return responseMessage;
            });
        }
        public HttpResponseMessage Delete(HttpRequestMessage requestMessage, int id)
        {
            return CreateHttpReponse(requestMessage, () =>
            {
                HttpResponseMessage responseMessage = null;
                if (ModelState.IsValid)
                {
                    requestMessage.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    _postCategoryService.Delete(id);
                    _postCategoryService.Save();

                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.OK);
                }
                return responseMessage;
            });
        }
    }
}
using AutoMapper;
using Pharma.Model.Models;
using Pharma.Service;
using Pharma.Web.Infrastructure.Core;
using Pharma.Web.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Pharma.Web.Infrastructure.Extensions;
using Pharma.Web.Mappings;

namespace Pharma.Web.Api
{
    [RoutePrefix("api/postcategory")]
    public class PostCategoryController : ApiControllerBase
    {
        IPostCategoryService _postCategoryService;
        public PostCategoryController(IErrorService errorService, IPostCategoryService postCategoryService) : base(errorService)
        {
            this._postCategoryService = postCategoryService;
        }
        [Route("getall")]
        public HttpResponseMessage Get(HttpRequestMessage requestMessage)
        {
            return CreateHttpReponse(requestMessage, () =>
            {
                var listCategory = _postCategoryService.GetAll();
                var listPostCategoryVm = AutoMapperConfiguration.InitializeAutomapper().Map<List<PostCategoryViewModel>>(listCategory);
                HttpResponseMessage responseMessage = requestMessage.CreateResponse(HttpStatusCode.OK, listPostCategoryVm);

                return responseMessage;
            });
        }
        [Route("add")]
        public HttpResponseMessage Post(HttpRequestMessage requestMessage, PostCategoryViewModel postCategoryVm)
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
                     PostCategory newPostCategory = new PostCategory();
                     newPostCategory.UpdatePostCategory(postCategoryVm);

                     var category = _postCategoryService.Add(newPostCategory);
                     _postCategoryService.Save();

                     responseMessage = requestMessage.CreateResponse(HttpStatusCode.Created, category);
                 }
                 return responseMessage;
             });
        }
        [Route("update")]
        public HttpResponseMessage Put(HttpRequestMessage requestMessage, PostCategoryViewModel postCategoryVm)
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
                    var postCategoryDb = _postCategoryService.GetById(postCategoryVm.ID);
                    postCategoryDb.UpdatePostCategory(postCategoryVm);

                    _postCategoryService.Update(postCategoryDb);
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
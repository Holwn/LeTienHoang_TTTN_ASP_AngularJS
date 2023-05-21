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
    [RoutePrefix("api/post")]
    [Authorize]
    public class PostController : ApiControllerBase
    {
        #region Initializeprivate
        IPostService _postService;
        public PostController(IErrorService errorService, IPostService postService)
            : base(errorService)
        {
            this._postService = postService;
        }
        #endregion

        [Route("getallparents")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpReponse(request, () =>
            {
                var list = _postService.GetAll();

                var listPostVm = AutoMapperConfiguration.InitializeAutomapper().Map<List<PostViewModel>>(list);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listPostVm);

                return response;
            });
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpReponse(request, () =>
            {
                var list = _postService.GetById(id);

                var listPostVm = AutoMapperConfiguration.InitializeAutomapper().Map<PostViewModel>(list);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listPostVm);

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
                var list = _postService.GetAll(keyword);

                totalRow = list.Count();
                var query = list.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);

                var listPostVm = AutoMapperConfiguration.InitializeAutomapper().Map<IEnumerable<PostViewModel>>(query);

                var paginationSet = new PaginationSet<PostViewModel>()
                {
                    Items = listPostVm,
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
        public HttpResponseMessage Create(HttpRequestMessage request, PostViewModel PostVm)
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
                    var newPost = new Post();
                    newPost.UpdatePost(PostVm);
                    newPost.CreatedDate = DateTime.Now;
                    newPost.CreatedBy = User.Identity.Name;

                    _postService.Add(newPost);
                    _postService.Save();

                    var responseDate = AutoMapperConfiguration.InitializeAutomapper().Map<PostViewModel>(newPost);
                    response = request.CreateResponse(HttpStatusCode.Created, responseDate);
                }
                return response;
            });
        }


        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, PostViewModel PostVm)
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
                    var dbPost = _postService.GetById(PostVm.ID);
                    dbPost.UpdatePost(PostVm);
                    dbPost.UpdatedDate = DateTime.Now;
                    dbPost.UpdatedBy = User.Identity.Name;

                    _postService.Update(dbPost);
                    _postService.Save();

                    var responseDate = AutoMapperConfiguration.InitializeAutomapper().Map<PostViewModel>(dbPost);
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
                    var oldPost = _postService.Delete(id);
                    _postService.Save();

                    var responseDate = AutoMapperConfiguration.InitializeAutomapper().Map<PostViewModel>(oldPost);
                    response = request.CreateResponse(HttpStatusCode.Created, responseDate);
                }
                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedPosts)
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
                    var listPost = new JavaScriptSerializer().Deserialize<List<int>>(checkedPosts);
                    foreach (var item in listPost)
                    {
                        _postService.Delete(item);
                    }
                    _postService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, listPost.Count);
                }
                return response;
            });
        }

    }
}

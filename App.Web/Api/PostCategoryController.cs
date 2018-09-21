using App.Model.Models;
using App.Service;
using App.Web.Infrastructure.Core;
using App.Web.Infrastructure.Extensions;
using App.Web.Models;
using AutoMapper;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace App.Web.Api
{
    [RoutePrefix("api/postcategory")]
    public class PostCategoryController : ApiControllerBase
    {
        private IPostCategoryService _postCategoryService;

        public PostCategoryController(IErrorService errorService, IPostCategoryService postCategoryService) : base(errorService)
        {
            this._postCategoryService = postCategoryService;
        }

        [Route("getall")]
        public HttpResponseMessage Get(HttpRequestMessage requestMessage)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                var listCategory = _postCategoryService.GetAll();

                var listPostCategoryViewModel = Mapper.Map<List<PostCategoryViewModel>>(listCategory);

                HttpResponseMessage responseMessage = requestMessage.CreateResponse(HttpStatusCode.OK, listCategory);
                return responseMessage;
            });
        }

        [Route("add")]
        public HttpResponseMessage Post(HttpRequestMessage requestMessage, PostCategoryViewModel postCategoryVM)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                HttpResponseMessage responseMessage = null;
                if (ModelState.IsValid)
                {
                    responseMessage = requestMessage.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    PostCategory newPostCategory = new PostCategory();
                    newPostCategory.UpdatePostCategory(postCategoryVM);
                    var category = _postCategoryService.Add(newPostCategory);
                    _postCategoryService.Save();

                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.Created, category);
                }
                return responseMessage;
            });
        }

        [Route("update")]
        public HttpResponseMessage Put(HttpRequestMessage requestMessage, PostCategoryViewModel postCategoryVM)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                HttpResponseMessage responseMessage = null;
                if (ModelState.IsValid)
                {
                    responseMessage = requestMessage.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var postCategoryDB = _postCategoryService.GetById(postCategoryVM.ID);
                    postCategoryDB.UpdatePostCategory(postCategoryVM);
                    _postCategoryService.Update(postCategoryDB);
                    _postCategoryService.Save();

                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.OK);
                }
                return responseMessage;
            });
        }

        public HttpResponseMessage Delete(HttpRequestMessage requestMessage, int id)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                HttpResponseMessage responseMessage = null;
                if (ModelState.IsValid)
                {
                    responseMessage = requestMessage.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
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
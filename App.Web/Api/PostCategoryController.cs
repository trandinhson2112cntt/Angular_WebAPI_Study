﻿using App.Model.Models;
using App.Service;
using App.Web.Infrastructure.Core;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace App.Web.Api
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
        public HttpResponseMessage Get(HttpRequestMessage requestMessage)
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
                    var listCategory = _postCategoryService.GetAll();

                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.OK,listCategory);
                }
                return responseMessage;
            });
        }

        public HttpResponseMessage Post(HttpRequestMessage requestMessage,PostCategory postCategory)
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
                    var category = _postCategoryService.Add(postCategory);
                    _postCategoryService.Save();

                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.Created, category);
                }
                return responseMessage;
            });
        }

        public HttpResponseMessage Put(HttpRequestMessage requestMessage, PostCategory postCategory)
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
                    _postCategoryService.Update(postCategory);
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
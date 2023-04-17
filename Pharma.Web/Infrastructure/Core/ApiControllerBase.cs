using Pharma.Model.Models;
using Pharma.Service;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Pharma.Web.Infrastructure.Core
{
    public class ApiControllerBase : ApiController
    {
        private IErrorService _errorService;
        public ApiControllerBase(IErrorService errorService)
        {
            this._errorService = errorService;
        }
        protected HttpResponseMessage CreateHttpReponse(HttpRequestMessage requestMessage,Func<HttpResponseMessage> func)
        {
            HttpResponseMessage response = null;
            try
            {
                response = func.Invoke();
            }
            catch(DbEntityValidationException ex)
            {
                foreach(var eve in ex.EntityValidationErrors)
                {
                    Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
                    foreach(var ve in eve.ValidationErrors)
                    {
                        Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                    }    
                }
                LogError(ex);
                response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ex.InnerException.Message);
            }
            catch(DbUpdateException dbEx)
            {
                LogError(dbEx);
                response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, dbEx.InnerException.Message);
            }
            catch(Exception ex)
            {
                LogError(ex);
                response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }
        private void LogError(Exception ex)
        {
            try
            {
                Error error = new Error();
                error.CreateDate = DateTime.Now;
                error.Message = ex.Message;
                error.StackTrace = ex.StackTrace;
                _errorService.Create(error);
                _errorService.Save();
            }
            catch
            {

            }
        }
    }
}

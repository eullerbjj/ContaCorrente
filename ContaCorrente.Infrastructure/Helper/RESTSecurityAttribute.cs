using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContaCorrente.Infrastructure.Helper
{
    public class RESTSecurityAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                if (context.HttpContext.Request.Headers["token"].Count <= 0 ||
                    context.HttpContext.Request.Headers["token"] == "")
                {
                    context.Result = new JsonResult("Informe o 'token' no header da requisição.") { StatusCode = 400 }; ;
                    return;
                }

                string token = context.HttpContext.Request.Headers["token"];

                if (!Authorize(token))
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Validar o token. Por ora, o token está estático, mas deverá ser gerado dinamicamente com autenticação
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private bool Authorize(string token)
        {
            try
            {
                if (token == "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NjU3ODkifQ.rqspsTJ79_Oz_FKfOvOw9ForZTsK5HOHxjNL6ctngr8")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

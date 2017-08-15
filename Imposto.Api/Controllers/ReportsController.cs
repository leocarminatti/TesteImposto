using Imposto.Core.Data;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Imposto.Api.Controllers
{
    [RoutePrefix("reports")]
    public class ReportsController : ApiController
    {
        private readonly NotaFiscalRepository _rep;

        public ReportsController()
        {
            _rep = new NotaFiscalRepository();
        }

        [Route("cfop")]
        [HttpGet]
        public IHttpActionResult GetCfop()
        {
            try
            {
                return Ok(_rep.ProcCfop());
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message));
            }
        }
    }
}

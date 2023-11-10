using App.Domain.DTOs;
using App.Domain.Interfaces.Application;
using Autenticador.API.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace App.Api.Controllers
{
    [ApiController]
    [Route("codigoAcessoController")]
    public class CodigoAcessoController : Controller
    {
        private ICodigoAcessoService _service;
        private readonly JwtIssuerOptions _jwtOptions;

        public CodigoAcessoController(IOptions<JwtIssuerOptions> jwtOptions, ICodigoAcessoService service)
        {
            _service = service;
            _jwtOptions = jwtOptions.Value;
        }

        [HttpGet("gerar")]
        public JsonResult Gerar()
        {
            try
            {
                var token = Request.Headers["Authorization"];
                var auth = _jwtOptions.GetAuthData(token);
                var obj = _service.Gerar(auth);
                return Json(RetornoApi.Sucesso(obj));
            }
            catch (Exception ex)
            {
                return Json(RetornoApi.Erro(ex.Message));
            }
        }
    }
}

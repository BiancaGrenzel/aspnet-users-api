using App.Domain.DTO;
using App.Domain.DTOs;
using App.Domain.Entities;
using App.Domain.Interfaces.Application;
using Autenticador.API.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace App.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IndexController : Controller
    {
        private readonly JwtIssuerOptions _jwtOptions;
        private IIndexService _service;

        public IndexController(IOptions<JwtIssuerOptions> jwtOptions, IIndexService service)
        {
            _service = service;
            _jwtOptions = jwtOptions.Value;
        }

        [HttpPost("Logar")]
        [AllowAnonymous]
        public JsonResult Logar([FromBody] LoginDTO login)
        {
            try
            {
                var auth = _service.Logar(login);
                var token = _jwtOptions.Token(auth).Result;
                var obj = _service.Autenticado(auth);
                return Json(RetornoApi.Sucesso(obj, token));
            }
            catch (Exception e)
            {
                return Json(new { status = "error", message = e.Message });
            }
        }

        [HttpGet("Autenticado")]
        [AllowAnonymous]
        public JsonResult Autenticado()
        {
            try
            {
                var token = Request.Headers["Authorization"];
                var auth = _jwtOptions.GetAuthData(token);
                var obj = _service.Autenticado(auth);
                return Json(RetornoApi.Sucesso(obj, token));
            }
            catch (Exception ex)
            {
                return Json(RetornoApi.Erro(ex.Message));
            }
        }
    }
}

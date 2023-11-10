using App.Domain.DTO;
using App.Domain.DTOs;
using App.Domain.Interfaces.Application;
using Autenticador.API.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace App.Api.Controllers
{
    [ApiController]
    [Route("login")]
    public class LoginController : Controller
    {
        private readonly JwtIssuerOptions _jwtOptions;
        private ILoginService _service;

        public LoginController(IOptions<JwtIssuerOptions> jwtOptions, ILoginService service)
        {
            _service = service;
            _jwtOptions = jwtOptions.Value;
        }

        [HttpPost("entrar")]
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

        [HttpGet("autenticado")]
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

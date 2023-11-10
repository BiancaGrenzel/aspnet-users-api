using App.Api.Auth;
using App.Domain.DTO.Auth;
using App.Domain.DTOs;
using App.Domain.Entities;
using App.Domain.Interfaces.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace App.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("[controller]")]

    public class PessoaController : Controller
    {
        private readonly JwtIssuerOptions _jwtOptions;
        private IPessoaService _pessoaService;

        public PessoaController(IOptions<JwtIssuerOptions> jwtOptions, IPessoaService service)
        {
            _pessoaService = service;
            _jwtOptions = jwtOptions.Value;
        }

        [HttpPost("Autenticar")]
        [AllowAnonymous]
        public virtual async Task<JsonResult> Autenticar([FromBody] AuthRequest data)
        {
            try
            {
                return await Task.Run(() =>
                {
                    var obj = _pessoaService.Autenticar(data);
                    var token = _jwtOptions.Token(obj).Result;
                    return Json(RetornoApi.Sucesso(obj, token));
                });
            }
            catch (Exception e)
            {
                return Json(RetornoApi.Erro(e.Message));
            }
        }

        [HttpGet("Autenticado")]
        public async Task<JsonResult> Autenticado()
        {
            try
            {
                return await Task.Run(() =>
                {
                    var auth = _jwtOptions.GetAuthData(Request.Headers["Authorization"]);
                    var obj = _pessoaService.UsuarioAutenticado(auth);
                    return Json(RetornoApi.Sucesso(obj, _jwtOptions.Renew(Request.Headers["Authorization"]).Result));
                });
            }
            catch (Exception e)
            {
                return Json(RetornoApi.Erro(e.Message));
            }
        }

        [HttpPost("criar")]
        public IActionResult Criar([FromBody] Pessoa pessoa)
        {
            try
            {
                var auth = _jwtOptions.GetAuthData(Request.Headers["Authorization"]);
                _pessoaService.Criar(auth, pessoa);

                return Json(RetornoApi.Sucesso("Pessoa editada com sucesso!", _jwtOptions.Renew(Request.Headers["Authorization"]).Result));
            }
            catch (Exception ex)
            {
                return Json(RetornoApi.Erro(ex.Message));
            }
        }

        [HttpPut("editar")]
        public IActionResult Editar([FromBody] Pessoa pessoa)
        {
            try
            {
                var auth = _jwtOptions.GetAuthData(Request.Headers["Authorization"]);
                _pessoaService.Editar(auth, pessoa);

                return Json(RetornoApi.Sucesso("Pessoa editada com sucesso!", _jwtOptions.Renew(Request.Headers["Authorization"]).Result));

            }
            catch (Exception ex)
            {
                return Json(RetornoApi.Erro(ex.Message));
            }
        }

        [HttpDelete("deletar")]
        public IActionResult Deletar([FromHeader] int id) {
            try
            {
                var auth = _jwtOptions.GetAuthData(Request.Headers["Authorization"]);
                _pessoaService.Deletar(auth, id);
                
                return Json(RetornoApi.Sucesso("Pessoa deletada com sucesso!", _jwtOptions.Renew(Request.Headers["Authorization"]).Result));
            }
            catch (Exception ex)
            {
                return Json(RetornoApi.Erro(ex.Message));
            }
        }

        [HttpGet("buscarPorId")]
        public IActionResult BuscarPorId([FromHeader] int id)
        {
            try
            {
                var auth = _jwtOptions.GetAuthData(Request.Headers["Authorization"]);
                var pessoa = _pessoaService.BuscarPorId(auth, id);
                return Json(RetornoApi.Sucesso(pessoa, _jwtOptions.Renew(Request.Headers["Authorization"]).Result));
            }
            catch (Exception ex)
            {
                return Json(RetornoApi.Erro(ex.Message));
            }
        }

        [HttpGet("buscarLista")]
        public IActionResult BuscarLista()
        {
            try
            {
                var auth = _jwtOptions.GetAuthData(Request.Headers["Authorization"]);
                var listaPessoas = _pessoaService.BuscarLista(auth);
                return Json(RetornoApi.Sucesso(listaPessoas, _jwtOptions.Renew(Request.Headers["Authorization"]).Result));
            }
            catch (Exception ex)
            {
                return Json(RetornoApi.Erro(ex.Message));
            }
        }


    }
}

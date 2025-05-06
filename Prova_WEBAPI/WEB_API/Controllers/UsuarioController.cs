using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WEB_API.Data;
using WEB_API.Models;

namespace WEB_API.Controllers;

//A Partir daqui se não der certo apagar

[Route("api/usuario")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IConfiguration _configuration;
    public UsuarioController(IUsuarioRepository usuarioRepository, IConfiguration configuration)
    {
        _usuarioRepository = usuarioRepository;
        _configuration = configuration;
    }

    [HttpPost("cadastrar")]
    public IActionResult Cadastrar([FromBody] Usuario usuario)
    {
        _usuarioRepository.Cadastrar(usuario);        
        return Created("", usuario);
    }

    [HttpGet("listar")]
    [Authorize]
    public IActionResult Listar()
    {
        return Ok(_usuarioRepository.Listar());
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] Usuario usuario)
    {
        Usuario? usuarioExistente = _usuarioRepository.BuscarUsuarioPorEmailSenha(usuario.Email, usuario.Senha);
        if(usuarioExistente == null)
        {
            return Unauthorized(new { mensagem = "Usuário ou senha inválidos!"});
        }            

        string token = GerarToken(usuarioExistente);
        return Ok(token);
    }
    
    
    [ApiExplorerSettings(IgnoreApi = true)]
    public string GerarToken(Usuario usuario)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, usuario.Email)            
        };

        var chave = Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]!);

        var assinatura = new SigningCredentials(
            new SymmetricSecurityKey(chave),
            SecurityAlgorithms.HmacSha256
        );

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddSeconds(120),
            signingCredentials: assinatura
        );

        return new JwtSecurityTokenHandler().WriteToken(token);

    }

}


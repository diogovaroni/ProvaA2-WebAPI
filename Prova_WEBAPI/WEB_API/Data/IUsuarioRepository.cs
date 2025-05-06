using System;
using WEB_API.Models;

namespace WEB_API.Data;

public interface IUsuarioRepository
{
    void Cadastrar(Usuario usuario);
    List<Usuario> Listar();
    Usuario? BuscarUsuarioPorEmailSenha(string email, string senha);
}

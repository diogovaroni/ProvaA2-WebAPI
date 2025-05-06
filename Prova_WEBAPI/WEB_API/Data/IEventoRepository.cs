using System;
using WEB_API.Models;

namespace WEB_API.Data;

public interface IEventoRepository
{
    void Cadastrar(Evento evento);
    List<Evento> Listar();
}

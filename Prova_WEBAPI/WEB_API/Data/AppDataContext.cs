using System;
using Microsoft.EntityFrameworkCore;
using WEB_API.Models;

namespace WEB_API.Data;

public class AppDataContext : DbContext
{
    public AppDataContext(DbContextOptions options) : base(options) {}
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Evento> Eventos { get; set; }
}

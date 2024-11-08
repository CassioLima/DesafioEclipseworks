using Domain;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;


namespace Infra
{
    public partial class DbContext2 : DbContext, IUnitOfWork
    {
        public DbContext2(DbContextOptions<DbContext2> options) : base(options) { }

        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Tarefa> Tarefas { get; set; }
        public virtual DbSet<Projeto> Projetos { get; set; }
        public virtual DbSet<TarefaComentario> TarefaComentarios { get; set; }
        public virtual DbSet<TarefaHistoricoAtualizacao> TarefaHistoricoAtualizacoes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbContext2).Assembly);
        }

        public void SeedData()
        {

            if (!Usuarios.Any())
            {
                var usuarios = new List<Usuario>
                {
                    new() { Id = 1, Nome = "João" },
                    new () { Id = 2, Nome = "Carla" },
                    new () { Id = 3, Nome = "Sebastião", Funcao = Enums.Funcao.AdministradorSistema },
                    new () { Id = 4, Nome = "Chico", Funcao = Enums.Funcao.Gerente },
                };
                Usuarios.AddRange(usuarios);
                SaveChanges();
            }


            if (!Projetos.Any())
            {
                var projetos = new List<Projeto>
                {
                    new()  { Id = 1, Nome = "Projeto 1", UsuarioId = 2 },
                    
                    new () { Id = 2, Nome = "Projeto 2", UsuarioId = 1 },
                    
                    new () { Id = 3, Nome = "Projeto 3", UsuarioId = 3 },
                    
                    new () { Id = 4, Nome = "Projeto 4" , UsuarioId = 1},

                    new () { Id = 5, Nome = "Projeto 5" , UsuarioId = 4},

                };
                Projetos.AddRange(projetos);
                SaveChanges();
            }


            if (!Tarefas.Any())
            {
                var tarefas = new List<Tarefa>
                {
                    new(Enums.Prioridade.Media) {Id = 1, Descricao = "Tarefa 1", ProjetoId = 2, UsuarioId = 1, DataVencimento = DateTime.Now.AddDays(33), Status = Enums.Status.EmAndamento, Titulo = "Tarefa 1",
                              Comentarios= new List<TarefaComentario>{ new() { Comententario= "Comentario Tarefa", TarefaId = 1, UsuarioId=1}}
                          },

                    new(Enums.Prioridade.Media) {Id = 2, Descricao = "Tarefa 2", ProjetoId = 1, UsuarioId = 2, DataVencimento = DateTime.Now.AddDays(13), Status = Enums.Status.Pendente, Titulo = "Tarefa 2",
                              Comentarios= new List<TarefaComentario>{ new() { Comententario= "Comentario Tarefa", TarefaId = 2, UsuarioId=1}}
                          },

                    new(Enums.Prioridade.Media) {Id = 3, Descricao = "Tarefa 3", ProjetoId = 3, UsuarioId = 3, DataVencimento = DateTime.Now.AddDays(45), Status = Enums.Status.EmAndamento, Titulo = "Tarefa 3",
                              Comentarios= new List<TarefaComentario>{ new() { Comententario= "Comentario Tarefa", TarefaId = 3, UsuarioId=1}}
                          },

                    new(Enums.Prioridade.Media) {Id = 4, Descricao = "Tarefa 4", ProjetoId = 4, UsuarioId = 1, DataVencimento = DateTime.Now.AddDays(65), Status = Enums.Status.Pendente, Titulo = "Tarefa 4",
                              Comentarios= new List<TarefaComentario>{ new() { Comententario= "Comentario Tarefa", TarefaId = 4, UsuarioId=1}}
                          },

                    new(Enums.Prioridade.Media) {Id = 5, Descricao = "Tarefa 5", ProjetoId = 5, UsuarioId = 4, DataVencimento = DateTime.Now.AddDays(20), Status = Enums.Status.Concluida, Titulo = "Tarefa 5",
                              Comentarios= new List<TarefaComentario>{ new() { Comententario= "Comentario Tarefa", TarefaId = 5, UsuarioId=1}}
                          },

                    new(Enums.Prioridade.Media) {Id = 6, Descricao = "Tarefa 5", ProjetoId = 5, UsuarioId = 4, DataVencimento = DateTime.Now.AddDays(39), Status = Enums.Status.EmAndamento, Titulo = "Tarefa 5",
                              Comentarios= new List<TarefaComentario>{ new() { Comententario= "Comentario Tarefa", TarefaId = 6, UsuarioId=1}}
                          },

                    new(Enums.Prioridade.Media) {Id = 7, Descricao = "Tarefa 5", ProjetoId = 5, UsuarioId = 4, DataVencimento = DateTime.Now.AddDays(10), Status = Enums.Status.Concluida, Titulo = "Tarefa 5",
                              Comentarios= new List<TarefaComentario>{ new() { Comententario= "Comentario Tarefa", TarefaId = 7, UsuarioId=1}}
                          }
                };
                Tarefas.AddRange(tarefas);
                SaveChanges();
            }


        }
    }
}
using Atividade0705.Data;
using Atividade0705.Models;
using Microsoft.AspNetCore.Mvc;

namespace Atividade0705.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly UsuarioContext _context;

        public TarefaController(UsuarioContext context)
        {
            _context = context;
        }
       
        [HttpPost("criar")]
        public IActionResult CriarPessoas(Tarefa tarefa)
        {
            var usuario = HttpContext.Session.GetString("Email");
            if (usuario == null)
                return Unauthorized("Não autenticado");

            var sessao = Request.Cookies["IdUsado"];
            if (sessao != null)
            {
                tarefa.IdUsuario = int.Parse(sessao);
            }

            _context.Add(tarefa);
            _context.SaveChanges();
            return Created("Teste", tarefa);
        }
       
        

        [HttpGet]
        public IActionResult ReservasCliente()
        {
            var usuario = HttpContext.Session.GetString("Email");
            if (usuario == null)
                return Unauthorized("Não autenticado");

            var idUsuarioLogado = Request.Cookies["IdUsado"];
                if (idUsuarioLogado != null) {
                var resultado = from u in _context.Usuarios
                                join t in _context.Tarefas
                                on u.Id equals t.IdUsuario
                                where u.Id == int.Parse(usuario)
                                select new
                                {
                                    Usuarios = u.Nome,
                                    u.Email,
                                    Tarefas = t.Descricao,
                                    t.Statuss

                                };
                return Ok(resultado.ToList());
            }
            return Unauthorized("Não autenticado");

        }


        [HttpPut("atualizar/{id}")]
        public IActionResult Atualizar(int id, Tarefa tarefa)
        {
            var tarefaDoBanco = _context.Tarefas.Find(id);

            if (tarefaDoBanco == null)
                return NotFound("Tarefa não encontrado");

            tarefaDoBanco.Descricao = tarefa.Descricao;
            tarefaDoBanco.Statuss = tarefa.Statuss;
            _context.SaveChanges();
            return Ok("Atualizado com sucesso!!");
        }


        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var tarefa= _context.Usuarios.Find(id);

            if (tarefa== null)
                return NotFound("Tarefa não encontrado");

            _context.Usuarios.Remove(tarefa);
            _context.SaveChanges();

            return Ok("Deletado com sucesso ");
        }

    }
}

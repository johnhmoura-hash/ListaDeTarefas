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
            _context.Add(tarefa);
            _context.SaveChanges();
            return Created("Teste", tarefa);
        }

        [HttpPut("atualizar/{id}")]
        public IActionResult Atualizar(int id, Tarefa tarefa)
        {
            var tarefaDoBanco = _context.Tarefas.Find(id);

            if (tarefaDoBanco == null)
                return NotFound("Tarefa não encontrado");

            tarefaDoBanco.Descricao = tarefa.Descricao;
            tarefaDoBanco.Statuss = tarefa.Statuss;

            return Ok("Atualizado com sucesso!!");
        }
        [HttpGet("status/{nome}")]
        public IActionResult ConsultaTarefaStatus(string nome)
        {
            var tarefaDoBanco = _context.Tarefas.Where(t => t.Statuss.Contains(nome)).ToList();
            if (!tarefaDoBanco.Any())
                return NotFound("Tarefa não encontrado");
            return Ok(tarefaDoBanco);
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

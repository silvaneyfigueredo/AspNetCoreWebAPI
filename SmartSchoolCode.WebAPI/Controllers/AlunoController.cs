using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchoolCode.WebAPI.Data;
using SmartSchoolCode.WebAPI.Models;

namespace SmartSchoolCode.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
     
         private readonly IRepository _repository;
        public AlunoController(IRepository repository)
        {
            _repository = repository;
        }

        #region Métodos GET 

        [HttpGet]
        public IActionResult Get()
        {
            var alunos = _repository.GetAllAlunos(true);
            return Ok(alunos);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repository.GetAlunoById(id, true);
            if (aluno == null) return BadRequest("Aluno não encontrado!");
            return Ok(aluno);
        }
        /*
        //api/aluno/byid?id=1&nome=Pedro       
        [HttpGet("byid")]
        public IActionResult GetById(int id, string nome)
        {
            var aluno = _context.Alunos.FirstOrDefault(x => x.Id == id && x.Nome == nome);
            if (aluno == null) return BadRequest("Aluno não encontrado!");
            return Ok(aluno);
        }


        //api/aluno/Pedro
        [HttpGet("{nome}")]
        public IActionResult GetByName(string nome)
        {
            var alunos = _context.Alunos.Where(x => x.Nome.Contains(nome));
            if (!alunos.Any()) return BadRequest("Aluno não encontrado!");
            return Ok(alunos);
        }

        //api/aluno/byname?nome=Pedro&sobrenome=Silva
        [HttpGet("byname")]
        public IActionResult GetByName(string nome, string sobrenome)
        {
            var alunos = _context.Alunos.Where(x => x.Nome.Contains(nome) && x.Sobrenome.Contains(sobrenome));
            if (!alunos.Any()) return BadRequest($"Aluno ({nome}, {sobrenome}) não encontrado!");
            return Ok(alunos);
        }*/
        #endregion


        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            _repository.Add(aluno);
            if(_repository.SaveChanges())
                return Ok("Aluno cadastrado com Sucesso!");

            return BadRequest("Erro ao cadastrar o Aluno");
        }

        [HttpPut]
        public IActionResult Put(int id, Aluno aluno)
        {
            var alu = _repository.GetAlunoById(id, false);
            if(alu == null)
                return BadRequest("Aluno não encontrado");

            _repository.Update(aluno);
            if(_repository.SaveChanges())
                return Ok("Aluno alterado com Sucesso!");

            return BadRequest("Houve um erro ao alterar o aluno");
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = _repository.GetAlunoById(id, false);
            if(aluno == null)
                return BadRequest("Aluno não encontrado");

            _repository.Delete(aluno);
            if(_repository.SaveChanges())
                return Ok("Aluno cadastrado com Sucesso!");

            return BadRequest("Houve um erro ao Deletar o aluno");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
            var alu = _repository.GetAlunoById(id, false);
            if(alu == null)
                return BadRequest("Aluno não encontrado");

            _repository.Update(aluno);
            _repository.SaveChanges();
            return Ok();
        }                
    }
}
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SmartSchoolCode.WebAPI.Models;

namespace SmartSchoolCode.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        public List<Aluno> Alunos = new List<Aluno>{
            new Aluno(){ Id = 1, Nome = "Pedro", Sobrenome = "Silva", Telefone = "67 99999 9999"},
            new Aluno(){ Id = 2, Nome = "Ana Maria", Sobrenome ="Larrea", Telefone = "67 99999 9998"},
            new Aluno(){ Id = 3, Nome = "João Paulo", Sobrenome = "Figueredo", Telefone = "67 99999 9997"},
        };
        public AlunoController()
        {
            
        }

        #region Métodos GET 

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Alunos);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var aluno = Alunos.FirstOrDefault(x => x.Id == id);
            if(aluno == null) return BadRequest("Aluno não encontrado!");
            return Ok(aluno);
        }

        //api/aluno/byid?id=1&nome=Pedro       
        [HttpGet("byid")]
        public IActionResult GetById(int id, string nome)
        {
            var aluno = Alunos.FirstOrDefault(x => x.Id == id && x.Nome == nome);
            if(aluno == null) return BadRequest("Aluno não encontrado!");
            return Ok(aluno);
        }


        //api/aluno/Pedro
        [HttpGet("{nome}")]
        public IActionResult GetByName(string nome)
        {
            var alunos = Alunos.Where(x => x.Nome.Contains(nome));
            if(!alunos.Any()) return BadRequest("Aluno não encontrado!");
            return Ok(alunos);
        }

        //api/aluno/byname?nome=Pedro&sobrenome=Silva
        [HttpGet("byname")]
        public IActionResult GetByName(string nome, string sobrenome)
        {
            var alunos = Alunos.Where(x => x.Nome.Contains(nome) && x.Sobrenome.Contains(sobrenome));
            if(!alunos.Any()) return BadRequest($"Aluno ({nome}, {sobrenome}) não encontrado!");
            return Ok(alunos);
        }
    #endregion
       

        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            Alunos.Add(aluno);
            return Ok(aluno);
        }

        [HttpPut]
        public IActionResult Put(int id,  Aluno aluno)
        {
            return Ok(aluno);
        }
        // [HttpDelete("{id}")]
        // public IActionResult Delete(int id)
        // {
        //     return Ok();
        // }

        // [HttpDelete("{id}")]
        // public IActionResult Patch(int id, Aluno aluno)
        // {
        //     return Ok();
        // }
    }
}
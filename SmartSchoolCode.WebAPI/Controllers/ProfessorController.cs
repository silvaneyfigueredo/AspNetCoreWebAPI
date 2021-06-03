using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchoolCode.WebAPI.Data;
using SmartSchoolCode.WebAPI.Models;

namespace SmartSchoolCode.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly IRepository _repository;
        public ProfessorController(IRepository repository)
        {
            _repository = repository;
        }
        //[Route("~/api/Professores/")]
        [HttpGet]
        public IActionResult GetAll()
        {
            var professores = _repository.GetAllProfessores(true);
            return Ok(professores);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var professor = _repository.GetProfessorById(id, true);
            return Ok(professor);
        }

        [HttpGet("PorNome")]
        public IActionResult GetByName(string nome)
        {
            var professores = _repository.GetAllProfessoresByName(nome, true);
            return Ok(professores);
        }

        [HttpPost]
        public IActionResult Post(Professor professor)
        {
            _repository.Add(professor);
            if(_repository.SaveChanges())
                return Ok("Professor cadastrado com Sucesso!");

            return BadRequest("Erro ao cadastrar o Professor");
        }
        
        [HttpPut]
        public IActionResult Put(int id, Professor professor)
        {
            var prof = _repository.GetProfessorById(id, false);
            if(prof == null)
                return BadRequest("Professor não encontrado");

            _repository.Update(professor);
            if(_repository.SaveChanges())
                return Ok("Professor alterado com Sucesso!");

            return BadRequest("Erro ao alterar o Professor");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var prof = _repository.GetProfessorById(id, false);
            if(prof == null)
                return BadRequest("Professor não encontrado");

            _repository.Delete(prof);
            if(_repository.SaveChanges())
                return Ok("Professor excluido com Sucesso!");

            return BadRequest("Erro ao excluir o Professor");
        }
    }
}
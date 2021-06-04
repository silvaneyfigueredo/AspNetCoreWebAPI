using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartSchoolCode.WebAPI.Data;
using SmartSchoolCode.WebAPI.V1.Dtos;
using SmartSchoolCode.WebAPI.Models;
using System.Collections.Generic;

namespace SmartSchoolCode.WebAPI.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        public ProfessorController(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        //[Route("~/api/Professores/")]
        [HttpGet]
        public IActionResult GetAll()
        {
            var professores = _repository.GetAllProfessores(false);

            return Ok(_mapper.Map<IEnumerable<ProfessorDto>>(professores));
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var professor = _repository.GetProfessorById(id, false);
            if(professor == null)
                return BadRequest("Professor não encontrado");
            return Ok(_mapper.Map<ProfessorDto>(professor));
        }

        [HttpGet("PorNome")]
        public IActionResult GetByName(string nome)
        {
            var professores = _repository.GetAllProfessoresByName(nome, true);
            return Ok(_mapper.Map<IEnumerable<ProfessorDto>>(professores));
        }

        [HttpPost]
        public IActionResult Post(ProfessorRegistrarDto professorDto)
        {
            var professor = _mapper.Map<Professor>(professorDto);
            _repository.Add(professor);
            if(_repository.SaveChanges())
                return Created($"/api/aluno/{professor.Id}", _mapper.Map<ProfessorDto>(professor));

            return BadRequest("Erro ao cadastrar o Professor");
        }
        
        [HttpPut]
        public IActionResult Put(int id, ProfessorRegistrarDto professorDto)
        {
            var professor = _repository.GetProfessorById(id, false);
            if(professor == null)
                return BadRequest("Professor não encontrado");

            _mapper.Map(professorDto, professor);   

            _repository.Update(professor);
            if(_repository.SaveChanges())
                return Created($"/api/aluno/{professor.Id}", _mapper.Map<ProfessorDto>(professor));

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
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartSchoolCode.WebAPI.Data;
using SmartSchoolCode.WebAPI.V2.Dtos;
using SmartSchoolCode.WebAPI.Models;
using System.Collections.Generic;

namespace SmartSchoolCode.WebAPI.V2.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public AlunoController(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        #region Métodos GET 
        /// <summary>
        /// Método responsável por retornar todos alunos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            var alunos = _repository.GetAllAlunos(true);

            return Ok(_mapper.Map<IEnumerable<AlunoDto>>(alunos));
        }

        /// <summary>
        /// Método responsável por retornar o aluno por ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repository.GetAlunoById(id, true);
            if (aluno == null) return BadRequest("Aluno não encontrado!");


            return Ok(_mapper.Map<AlunoDto>(aluno));
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

        /// <summary>
        /// Método responsável pela inclusão do aluno
        /// </summary>
        /// <param name="alunoDto"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(AlunoRegistrarDto alunoDto)
        {

            var aluno = _mapper.Map<Aluno>(alunoDto);
            _repository.Add(aluno);
            if (_repository.SaveChanges())
                return Created($"/api/aluno/{aluno.Id}", _mapper.Map<AlunoDto>(aluno));

            return BadRequest("Erro ao cadastrar o Aluno");
        }

        [HttpPut]
        public IActionResult Put(int id, AlunoRegistrarDto alunoDto)
        {

            var aluno = _repository.GetAlunoById(id, false);
            if (aluno == null)
                return BadRequest("Aluno não encontrado");

            _mapper.Map(alunoDto, aluno);

            _repository.Update(aluno);
            if (_repository.SaveChanges())
                return Created($"/api/aluno/{aluno.Id}", _mapper.Map<AlunoDto>(aluno));

            return BadRequest("Houve um erro ao alterar o aluno");
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = _repository.GetAlunoById(id, false);
            if (aluno == null)
                return BadRequest("Aluno não encontrado");

            _repository.Delete(aluno);
            if (_repository.SaveChanges())
                return Ok("Aluno cadastrado com Sucesso!");

            return BadRequest("Houve um erro ao Deletar o aluno");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
            var alu = _repository.GetAlunoById(id, false);
            if (alu == null)
                return BadRequest("Aluno não encontrado");

            _repository.Update(aluno);
            _repository.SaveChanges();
            return Ok();
        }
    }
}
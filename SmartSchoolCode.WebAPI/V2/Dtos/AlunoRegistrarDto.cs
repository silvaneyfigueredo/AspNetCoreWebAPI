using System;

namespace SmartSchoolCode.WebAPI.V2.Dtos
{
    /// <summary>
    /// Modelo utilizado para inclusão de aluno
    /// </summary>
    public class AlunoRegistrarDto
    {
        public int Id { get; set; }
        public int Matricula { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataInicio { get; set; } = DateTime.Now;
        public DateTime ? DataFim { get; set; } = null;
        public bool Ativo { get; set; } = true;
        
    }
}
using SmartSchoolCode.WebAPI.Models;

namespace SmartSchoolCode.WebAPI.Data
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        bool SaveChanges();

        Aluno[] GetAllAlunos(bool incluirDisciplina = false);
        Aluno[] GetAllAlunosByDisciplinaId(int alunoId, bool incluirDisciplina = false);
        Aluno GetAlunoById(int alunoId, bool incluirDisciplina = false);


        Professor[] GetAllProfessores(bool incluirAlunos = false);
        Professor[] GetAllProfessoresByDisciplinaId(int disciplinaId, bool incluirAlunos = false);
        Professor GetProfessorById(int professorId, bool incluirAlunos = false);
        Professor[] GetAllProfessoresByName(string nome, bool incluirAlunos = false);
    }
}
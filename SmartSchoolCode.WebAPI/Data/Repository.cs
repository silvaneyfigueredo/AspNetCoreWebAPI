using System.Linq;
using Microsoft.EntityFrameworkCore;
using SmartSchoolCode.WebAPI.Models;

namespace SmartSchoolCode.WebAPI.Data
{
    public class Repository : IRepository
    {
        private readonly SmartContext _context;
        public Repository(SmartContext contexto)
        {
            _context = contexto;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public bool SaveChanges()
        {
           return (_context.SaveChanges() > 0);
        }

        public Aluno[] GetAllAlunos(bool incluirDisciplina)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if(incluirDisciplina){
                query = query.Include(ad => ad.AlunosDisciplinas)
                             .ThenInclude(d => d.Disciplina)
                             .ThenInclude(p => p.Professor);
            }

            query = query.AsNoTracking().OrderBy(x => x.Id);
            return query.ToArray();
        }

        public Aluno[] GetAllAlunosByDisciplinaId(int disciplinaId, bool incluirDisciplina = false)
        {
           IQueryable<Aluno> query = _context.Alunos;

            if(incluirDisciplina){
                query = query.Include(ad => ad.AlunosDisciplinas)
                             .ThenInclude(d => d.Disciplina)
                             .ThenInclude(p => p.Professor);
            }

            query = query.AsNoTracking().Where(x => x.AlunosDisciplinas.Any(d => d.DisciplinaId == disciplinaId)).OrderBy(x => x.Id);
            return query.ToArray();
        }

        public Aluno GetAlunoById(int alunoId, bool incluirDisciplina = false)
        {
           IQueryable<Aluno> query = _context.Alunos;

            if(incluirDisciplina){
                query = query.Include(ad => ad.AlunosDisciplinas)
                             .ThenInclude(d => d.Disciplina)
                             .ThenInclude(p => p.Professor);
            }

            return query.AsNoTracking().FirstOrDefault(x => x.Id == alunoId);
        }

        public Professor[] GetAllProfessores(bool incluirAlunos = false)
        {
            IQueryable<Professor> query = _context.Professores;
            if(incluirAlunos)
            {
                query = query.Include(d => d.Disciplinas)
                             .ThenInclude(ad => ad.AlunosDisciplinas)
                             .ThenInclude(a => a.Aluno);
            }
            return query.AsNoTracking().OrderBy(x => x.Id).ToArray();
        }

        public Professor[] GetAllProfessoresByDisciplinaId(int disciplinaId, bool incluirAlunos = false)
        {
            IQueryable<Professor> query = _context.Professores;
            if(incluirAlunos)
            {
                query = query.Include(d => d.Disciplinas)
                             .ThenInclude(ad => ad.AlunosDisciplinas)
                             .ThenInclude(a => a.Aluno);
            }
            return query.AsNoTracking().Where(x => x.Disciplinas.Any(d => d.AlunosDisciplinas.Any(y => y.DisciplinaId == disciplinaId)
                        )).OrderBy(x => x.Id).ToArray();
        }

        public Professor GetProfessorById(int professorId, bool incluirAlunos = false)
        {
            IQueryable<Professor> query = _context.Professores;
            if(incluirAlunos)
            {
                query = query.Include(d => d.Disciplinas)
                             .ThenInclude(ad => ad.AlunosDisciplinas)
                             .ThenInclude(a => a.Aluno);
            }
            return query.AsNoTracking().FirstOrDefault(x => x.Id == professorId);
        }

        public Professor[] GetAllProfessoresByName(string nome, bool incluirAlunos = false)
        {
            IQueryable<Professor> query = _context.Professores;
            if(incluirAlunos)
            {
                query = query.Include(d => d.Disciplinas)
                             .ThenInclude(ad => ad.AlunosDisciplinas)
                             .ThenInclude(a => a.Aluno);
            }
            return query.AsNoTracking().Where(x => x.Nome.ToUpper().Contains(nome.ToUpper())).ToArray();
        }
      
    }
}
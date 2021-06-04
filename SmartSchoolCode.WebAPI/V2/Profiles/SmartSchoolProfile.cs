using AutoMapper;
using SmartSchoolCode.WebAPI.V2.Dtos;
using SmartSchoolCode.WebAPI.Models;
using SmartSchoolCode.WebAPI.Helpers;

namespace SmartSchoolCode.WebAPI.V2.Profiles
{
    public class SmartSchoolProfile : Profile
    {
        public SmartSchoolProfile()
        {
            CreateMap<Aluno, AlunoDto>().ForMember(dest => dest.Nome, opt => opt.MapFrom(src => $"{src.Nome} {src.Sobrenome}"))
                                        .ForMember(dest => dest.Idade, opt => opt.MapFrom(src => src.DataNascimento.GetAno()));
            CreateMap<AlunoDto, Aluno>();
            CreateMap<Aluno, AlunoRegistrarDto>().ReverseMap();
            CreateMap<Professor, ProfessorDto>().ForMember(dest => dest.Nome, opt => opt.MapFrom(src => $"{src.Nome} {src.Sobrenome}"));
            CreateMap<ProfessorDto, Professor>();
            CreateMap<Professor, ProfessorRegistrarDto>().ReverseMap();
        }
    }
}
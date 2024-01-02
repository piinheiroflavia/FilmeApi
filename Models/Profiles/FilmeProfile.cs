using FilmeApi.Data.Dtos;
using AutoMapper;


namespace FilmeApi.Models.Profiles;

    public class FilmeProfile : Profile
    {
        public FilmeProfile() 
        {
            CreateMap<CreateFilmeDto, Filme>();
            CreateMap<UpdateFilmeDto, Filme>();
            CreateMap<Filme, ReadFilmeDto>();
        }
    }


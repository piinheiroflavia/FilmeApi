using AutoMapper;
using FilmeApi.Data.Dtos;
using FilmesApi.Models;

namespace FilmeApi.Models.Profiles
{
    public class CinemaProfile : Profile
    {


        public CinemaProfile()
        {
            CreateMap<CreateCinemaDto, Cinema>();

            CreateMap<UpdateCinemaDto, Cinema>();
               
            //retorna o mapeamento do readEndereco dentro do mapeamento de cinema 
            CreateMap<Cinema, ReadCinemaDto>().ForMember(cinemaDto => cinemaDto.Endereco,
                    opt => opt.MapFrom(Cinema => Cinema.Endereco));

        }

    }
}

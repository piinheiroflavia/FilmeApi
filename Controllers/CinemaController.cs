using AutoMapper;
using FilmesApi.Data.Dtos;
using FilmesApi.Data;
using FilmesApi.Models;
using Microsoft.AspNetCore.Mvc;
using FilmeApi.Data.Dtos;

namespace FilmesApi.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class CinemaController : ControllerBase
    {
        private FilmeContext _context;
        private IMapper _mapper;

        public CinemaController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Adiciona um cinema ao banco de dados
        /// </summary>
        /// <param name="cinemaDto">Objeto com os campos necessários para criação de um cinema</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Inserção bem-sucedida</response>
        [HttpPost]
        public IActionResult AdicionaCinema([FromBody] CreateCinemaDto cinemaDto)
        {
            Cinema cinema = _mapper.Map<Cinema>(cinemaDto);
            _context.Cinemas.Add(cinema);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaCinemasPorId), new { Id = cinema.Id }, cinemaDto);
        }

        /// <summary>
        /// Recupera todos os cinema no banco de dados
        /// </summary>
        /// <param name="cinemaDto">Objeto com os campos necessários para criação de um cinema</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso o returno seja feito com sucesso</response>
        [HttpGet]
        public IEnumerable<ReadCinemaDto> RecuperaCinemas()
        {
            //var listaCinemas = _mapper.Map<List<ReadCinemaDto>>(_context.Cinemas.ToList());
            //return listaCinemas;

            return _mapper.Map<List<ReadCinemaDto>>(_context.Cinemas.ToList()); 
        }


        /// <summary>
        /// Recupera um cinema pelo ID no banco de dados.
        /// </summary>
        /// <param name="id">ID do cinema a ser recuperado</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Recuperação bem-sucedida</response>
        /// <response code="404">Cinema não encontrado</response>
        [HttpGet("{id}")]
        public IActionResult RecuperaCinemasPorId(int id)
        {
            Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema != null)
            {
                ReadCinemaDto cinemaDto = _mapper.Map<ReadCinemaDto>(cinema);
                return Ok(cinemaDto);
            }
            return NotFound();
        }

        /// <summary>
        /// Atualiza um cinema no banco de dados pelo ID.
        /// </summary>
        /// <param name="id">ID do cinema a ser atualizado</param>
        /// <param name="cinemaDto">Objeto com os campos a serem atualizados</param>
        /// <returns>IActionResult</returns>
        /// <response code="204">Atualização bem-sucedida</response>
        /// <response code="404">Cinema não encontrado</response>
        [HttpPut("{id}")]
        public IActionResult AtualizaCinema(int id, [FromBody] UpdateCinemaDto cinemaDto)
        {
            Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema == null)
            {
                return NotFound();
            }
            _mapper.Map(cinemaDto, cinema);
            _context.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// Exclui um cinema no banco de dados pelo ID.
        /// </summary>
        /// <param name="id">ID do cinema a ser excluído</param>
        /// <returns>IActionResult</returns>
        /// <response code="204">Exclusão bem-sucedida</response>
        /// <response code="404">Cinema não encontrado</response>
        [HttpDelete("{id}")]
        public IActionResult DeletaCinema(int id)
        {
            Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema == null)
            {
                return NotFound();
            }
            _context.Remove(cinema);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
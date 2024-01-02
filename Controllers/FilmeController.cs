using AutoMapper;
using FilmeApi.Data.Dtos;
using FilmeApi.Models;
using FilmesApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmeApi.Controllers;


[ApiController]
[Route("[controller]")]

public class FilmeController : ControllerBase
{

    private FilmeContext _context;
    private IMapper _mapper;

    public FilmeController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


        /// <summary>
        /// Adiciona um filme ao banco de dados
        /// </summary>
        /// <param name="filmeDto">Objeto com os campos necessários para criação de um filme</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Inserção bem-sucedida</response>
        [HttpPost]
        public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto)
        {
           Filme filme = _mapper.Map<Filme>(filmeDto);
            _context.Filmes.Add(filme);
            _context.SaveChanges();
            // Retorna um resultado 201 (Created) com informações sobre como acessar o novo recurso.
            return CreatedAtAction(nameof(VerificarFilmePorId),
                new { id = filme.Id },
                filme);
        }



    //paginação com os métodos .Skip() e Take()
    //Skip() indica quantos elementos da lista pular
    //Take() define quantos serão selecionados
        /// <summary>
        /// Recupera todos os filmes no banco de dados
        /// </summary>
        /// <param name="filmeDto">Objeto com os campos necessários para criação de um filme</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso o returno seja feito com sucesso</response>
        [HttpGet]
        public IEnumerable<ReadFilmeDto> RecuperaFilmes([FromQuery] int skip = 0,
        [FromQuery] int take = 50)
        {
            return _mapper.Map<List<ReadFilmeDto>>(_context.Filmes.Skip(skip).Take(take));
        }



        /// <summary>
        /// Recupera um filme pelo ID no banco de dados.
        /// </summary>
        /// <param name="id">ID do filme a ser recuperado</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Recuperação bem-sucedida</response>
        /// <response code="404">Filme não encontrado</response>
        [HttpGet("{id}")]
        public IActionResult VerificarFilmePorId(int id)
        {
            var filmeEncontrado = _context.Filmes.FirstOrDefault(f => f.Id == id);


            if (filmeEncontrado == null)
            {
                Console.WriteLine($"Filme com ID {id} não encontrado.");
                return NotFound();
            }
            var filmeDto = _mapper.Map<ReadFilmeDto>(filmeEncontrado);

            return Ok(filmeDto);

        }

        /// <summary>
        /// Atualiza um filme no banco de dados pelo ID.
        /// </summary>
        /// <param name="id">ID do filme a ser atualizado</param>
        /// <param name="filmeDto">Objeto com os campos a serem atualizados</param>
        /// <returns>IActionResult</returns>
        /// <response code="204">Atualização bem-sucedida</response>
        /// <response code="404">Filme não encontrado</response>
        [HttpPut("{id}")]
        public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
        {
            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

            if (filme == null) return NotFound();
            _mapper.Map(filmeDto, filme);
            _context.SaveChanges();
            return NoContent();
        }


    //funciona com envios parciais 
    //[HttpPatch("{id}")]
    //public IActionResult AtualizaFilmeParcial(int id, JSONPatchDocument<UpdateFilmeDto> patch)
    //{
    //    var filme = _context.Filmes.FirstOrDefault(
    //        filme => filme.Id == id);
    //    if (filme == null) return NotFound();
    //    _mapper.Map(filmeDto, filme);
    //    _context.SaveChanges();
    //    return NoContent();
    //}



    /// <summary>
    /// Exclui um filme no banco de dados pelo ID.
    /// </summary>
    /// <param name="id">ID do filme a ser excluído</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Exclusão bem-sucedida</response>
    /// <response code="404">Filme não encontrado</response>
    [HttpDelete("{id}")]
    public IActionResult DeletarFilmes(int id)
    {

            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if(filme == null ) return NotFound();

            _context.Remove(filme);
            _context.SaveChanges();

            return NoContent();
    }

}
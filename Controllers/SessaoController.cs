using AutoMapper;
using FilmesApi.Data.Dtos;
using FilmesApi.Data;
using FilmesApi.Models;
using Microsoft.AspNetCore.Mvc;
using FilmeApi.Data.Dtos;
using FilmeApi.Models;

namespace FilmesApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessaoController : ControllerBase
    {
        private FilmeContext _context;
        private IMapper _mapper;

        public SessaoController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Adiciona uma sessão ao banco de dados
        /// </summary>
        /// <param name="sessaoDto">Objeto com os campos necessários para criação de uma sessão</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Inserção bem-sucedida</response>
        [HttpPost]
        public IActionResult AdicionaSessao([FromBody] CreateSessaoDto sessaoDto)
        {
            Sessao sessao = _mapper.Map<Sessao>(sessaoDto);
            _context.Sessoes.Add(sessao);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaSessoesPorId), new { Id = sessao.Id }, sessao);
        }
        /// <summary>
        /// Recupera todas as sessões no banco de dados
        /// </summary>
        /// <param name="sessaoDto">Objeto com os campos necessários para criação de uma sessão</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso o returno seja feito com sucesso</response>
        [HttpGet]
        public IEnumerable<ReadSessaoDto> RecuperaSessoes()
        {
            return _mapper.Map<List<ReadSessaoDto>>(_context.Sessoes.ToList());
        }

        /// <summary>
        /// Recupera uma sessão pelo ID no banco de dados.
        /// </summary>
        /// <param name="id">ID do sessão a ser recuperado</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Recuperação bem-sucedida</response>
        /// <response code="404">Sessão não encontrado</response>
        [HttpGet("{id}")]
        public IActionResult RecuperaSessoesPorId(int id)
        {
            Sessao sessao = _context.Sessoes.FirstOrDefault(sessao => sessao.Id == id);
            if (sessao != null)
            {
                ReadSessaoDto sessaoDto = _mapper.Map<ReadSessaoDto>(sessao);

                return Ok(sessaoDto);
            }
            return NotFound();
        }   

    }
}
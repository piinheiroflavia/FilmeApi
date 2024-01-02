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
    public class EnderecoController : ControllerBase
    {
        private FilmeContext _context;
        private IMapper _mapper;

        public EnderecoController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Adiciona um endereço ao banco de dados
        /// </summary>
        /// <param name="enderecoDto">Objeto com os campos necessários para criação de um Endereço</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Inserção bem-sucedida</response>
        [HttpPost]
        public IActionResult AdicionaEndereco([FromBody] CreateEnderecoDto enderecoDto)
        {
            Endereco endereco = _mapper.Map<Endereco>(enderecoDto);
            _context.Enderecos.Add(endereco);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaEnderecosPorId), new { Id = endereco.Id }, endereco);
        }
        /// <summary>
        /// Recupera todos os endereço no banco de dados
        /// </summary>
        /// <param name="enderecoDto">Objeto com os campos necessários para criação de um endereço</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso o returno seja feito com sucesso</response>
        [HttpGet]
        public IEnumerable<ReadEnderecoDto> RecuperaEnderecos()
        {
            return _mapper.Map<List<ReadEnderecoDto>>(_context.Enderecos);
        }

        /// <summary>
        /// Recupera um endereço pelo ID no banco de dados.
        /// </summary>
        /// <param name="id">ID do endereço a ser recuperado</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Recuperação bem-sucedida</response>
        /// <response code="404">Endereço não encontrado</response>
        [HttpGet("{id}")]
        public IActionResult RecuperaEnderecosPorId(int id)
        {
            Endereco endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
            if (endereco != null)
            {
                ReadEnderecoDto enderecoDto = _mapper.Map<ReadEnderecoDto>(endereco);

                return Ok(enderecoDto);
            }
            return NotFound();
        }

        /// <summary>
        /// Atualiza um endereço no banco de dados pelo ID.
        /// </summary>
        /// <param name="id">ID do endereço a ser atualizado</param>
        /// <param name="enderecoDto">Objeto com os campos a serem atualizados</param>
        /// <returns>IActionResult</returns>
        /// <response code="204">Atualização bem-sucedida</response>
        /// <response code="404">Endereço não encontrado</response>
        [HttpPut("{id}")]
        public IActionResult AtualizaEndereco(int id, [FromBody] UpdateEnderecoDto enderecoDto)
        {
            Endereco endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
            if (endereco == null)
            {
                return NotFound();
            }
            _mapper.Map(enderecoDto, endereco);
            _context.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// Exclui um endereço no banco de dados pelo ID.
        /// </summary>
        /// <param name="id">ID do endereço vai ser excluído</param>
        /// <returns>IActionResult</returns>
        /// <response code="204">Exclusão bem-sucedida</response>
        /// <response code="404">Endereço não encontrado</response>
        [HttpDelete("{id}")]
        public IActionResult DeletaEndereco(int id)
        {
            Endereco endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
            if (endereco == null)
            {
                return NotFound();
            }
            _context.Remove(endereco);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PARCIAL1A.Models;

namespace PARCIAL1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly PARCIAL1AContext _PARCIAL1AContext;
        public LibrosController(PARCIAL1AContext PARCIAL1AContext)
        {
            _PARCIAL1AContext = PARCIAL1AContext;

        }

        [HttpGet]
        [Route("GetAllLibros")]
        public IActionResult Get()
        {
            try
            {

                List<Libros> listadoLibros = (from a in _PARCIAL1AContext.Libros
                                                select a).ToList();
                if (listadoLibros.Count == 0)
                {
                    return NotFound();
                }
                return Ok(listadoLibros);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetByIdLibro/{id}")]
        public IActionResult Get(int id)
        {
            Libros? Libro = (from e in _PARCIAL1AContext.Libros
                              where e.Id == id
                              select e).FirstOrDefault();
            if (Libro == null)
            {
                return NotFound();
            }
            return Ok(Libro);


        }
        [HttpPost]
        [Route("AddLibro")]
        public IActionResult GuardarLibro([FromBody] Libros Libro)
        {
            try
            {
                _PARCIAL1AContext.Libros.Add(Libro);
                _PARCIAL1AContext.SaveChanges();
                return Ok(Libro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        [Route("actualizarLibros/{id}")]

        public IActionResult ActualizarLibro(int id, [FromBody] Libros LibroM)
        {
            try
            {

                Libros? LActual = (from e in _PARCIAL1AContext.Libros
                                      where e.Id == id
                                      select e).FirstOrDefault();


                if (LActual == null)
                {
                    return NotFound();
                }

                LActual.Titulo = LibroM.Titulo;

                _PARCIAL1AContext.Entry(LActual).State = EntityState.Modified;
                _PARCIAL1AContext.SaveChanges();

                return Ok(LibroM);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }
        [HttpDelete]
        [Route("eliminarAutor/{id}")]

        public IActionResult EliminarAutor(int id)
        {
            try
            {

                Autores? Autor = (from e in _PARCIAL1AContext.Autores
                                  where e.Id == id
                                  select e).FirstOrDefault();

                if (Autor == null)
                {
                    return NotFound();
                }

                _PARCIAL1AContext.Autores.Attach(Autor);
                _PARCIAL1AContext.Autores.Remove(Autor);
                _PARCIAL1AContext.SaveChanges();

                return Ok(Autor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

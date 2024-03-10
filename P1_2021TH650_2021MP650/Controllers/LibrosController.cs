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
        [Route("GetLibro*Autor/{autor}")]

        public IActionResult GetLibroAutor(string autor)
        {
            var listadoLibro = (from l in _PARCIAL1AContext.Libros
                                join al in _PARCIAL1AContext.AutorLibro
                                    on l.Id equals al.LibroId
                                join a in _PARCIAL1AContext.Autores
                                    on al.AutorId equals a.Id
                                 where a.Nombre.Contains(autor)
                select new
                {
                    a.Nombre,
                    l.Titulo,
                    l.Id
                }).ToList();

            if (listadoLibro.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoLibro);
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
        [Route("eliminarLibro/{id}")]

        public IActionResult EliminarLibro(int id)
        {
            try
            {

                Libros? Libro = (from e in _PARCIAL1AContext.Libros
                                  where e.Id == id
                                  select e).FirstOrDefault();

                if (Libro == null)
                {
                    return NotFound();
                }

                _PARCIAL1AContext.Libros.Attach(Libro);
                _PARCIAL1AContext.Libros.Remove(Libro);
                _PARCIAL1AContext.SaveChanges();

                return Ok(Libro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

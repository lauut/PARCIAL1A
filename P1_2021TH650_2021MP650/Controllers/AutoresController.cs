using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PARCIAL1A.Models;


namespace PARCIAL1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        private readonly PARCIAL1AContext _PARCIAL1AContext;

        public AutoresController(PARCIAL1AContext PARCIAL1AContext)
        {
            _PARCIAL1AContext = PARCIAL1AContext;

        }

        [HttpGet]
        [Route("GetAllAutores")]
        public IActionResult Get()
        {
            try
            {

                List<Autores> listadoAutores = (from a in _PARCIAL1AContext.Autores
                                                select a).ToList();
                if (listadoAutores.Count == 0)
                {
                    return NotFound();
                }
                return Ok(listadoAutores);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("GetByIdAutor/{id}")]
        public IActionResult Get(int id)
        {
            Autores? Autor = (from e in _PARCIAL1AContext.Autores
                               where e.Id == id
                               select e).FirstOrDefault();
            if (Autor == null)
            {
                return NotFound();
            }
            return Ok(Autor);


        }

        [HttpPost]
        [Route("AddAutor")]
        public IActionResult GuardarAutor([FromBody] Autores Autor)
        {
            try
            {
                _PARCIAL1AContext.Autores.Add(Autor);
                _PARCIAL1AContext.SaveChanges();
                return Ok(Autor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        [Route("actualizarAutores/{id}")]

        public IActionResult ActualizarAutor(int id, [FromBody] Autores AutorM)
        {
            try
            {

                Autores? AutActual = (from e in _PARCIAL1AContext.Autores
                                         where e.Id == id
                                         select e).FirstOrDefault();

                
                if (AutActual == null)
                {
                    return NotFound();
                }

                AutActual.Nombre = AutorM.Nombre;
        
                _PARCIAL1AContext.Entry(AutActual).State = EntityState.Modified;
                _PARCIAL1AContext.SaveChanges();

                return Ok(AutorM);
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

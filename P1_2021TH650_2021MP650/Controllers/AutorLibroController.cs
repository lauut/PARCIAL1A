using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PARCIAL1A.Models;


namespace PARCIAL1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorLibroController : ControllerBase
    {
        private readonly PARCIAL1AContext _PARCIAL1AContext;

        public AutorLibroController(PARCIAL1AContext PARCIAL1AContext)
        {
            _PARCIAL1AContext = PARCIAL1AContext;
        }

        [HttpGet]
        [Route("GetAllAutorLibro")]
        public IActionResult Get()
        {
            try
            {

                List<AutorLibro> listadoAutL = (from al in _PARCIAL1AContext.AutorLibro
                                              select al).ToList();
                if (listadoAutL.Count == 0)
                {
                    return NotFound();
                }
                return Ok(listadoAutL);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetByIdAutorLibro/{id}")]
        public IActionResult Get(int id)
        {
            AutorLibro? autorLibro = (from at in _PARCIAL1AContext.AutorLibro
                             where at.AutorId == id
                             select at).FirstOrDefault();
            if (autorLibro == null)
            {
                return NotFound();
            }
            return Ok(autorLibro);


        }
        [HttpPost]
        [Route("AddAutor*Libro")]
        public IActionResult Guardar([FromBody] AutorLibro autorLibro)
        {
            try
            {
                _PARCIAL1AContext.AutorLibro.Add(autorLibro);
                _PARCIAL1AContext.SaveChanges();
                return Ok(autorLibro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPut]
        [Route("actualizarAutor*Libro/{id}")]

        public IActionResult Actualizar(int id, [FromBody] AutorLibro AutlM)
        {
            try
            {

                AutorLibro? autorLN = (from e in _PARCIAL1AContext.AutorLibro
                                   where e.AutorId == id
                                   select e).FirstOrDefault();


                if (autorLN == null)
                {
                    return NotFound();
                }

                autorLN.LibroId = AutlM.LibroId;
                autorLN.Orden = AutlM.Orden;


                _PARCIAL1AContext.Entry(autorLN).State = EntityState.Modified;
                _PARCIAL1AContext.SaveChanges();

                return Ok(AutlM);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }
        [HttpDelete]
        [Route("eliminarAutor*Libro/{id}")]

        public IActionResult EliminarLibro(int id)
        {
            try
            {

                AutorLibro? autorLibro = (from e in _PARCIAL1AContext.AutorLibro
                                 where e.AutorId == id
                                 select e).FirstOrDefault();

                if (autorLibro == null)
                {
                    return NotFound();
                }

                _PARCIAL1AContext.AutorLibro.Attach(autorLibro);
                _PARCIAL1AContext.AutorLibro.Remove(autorLibro);
                _PARCIAL1AContext.SaveChanges();

                return Ok(autorLibro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}

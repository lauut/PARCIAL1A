using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PARCIAL1A.Models;

namespace PARCIAL1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly PARCIAL1AContext _PARCIAL1AContext;
        public PostsController(PARCIAL1AContext PARCIAL1AContext)
        {
            _PARCIAL1AContext = PARCIAL1AContext;
        }

        [HttpGet]
        [Route("GetAllPosts")]
        public IActionResult Get()
        {
            try
            {

                List<Posts> listadoPosts = (from p in _PARCIAL1AContext.Posts
                                                select p).ToList();
                if (listadoPosts.Count == 0)
                {
                    return NotFound();
                }
                return Ok(listadoPosts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("GetAllPosts*Libros/{libronombre}")]
        public IActionResult GetJ(string libronombre)

        {
            try
            {

                var listadoPosts = (from p in _PARCIAL1AContext.Posts
                                    join at in _PARCIAL1AContext.Autores
                                    on p.AutorId equals at.Id
                                    join al in _PARCIAL1AContext.AutorLibro 
                                    on at.Id equals al.AutorId
                                    join l in _PARCIAL1AContext.Libros
                                    on al.LibroId equals l.Id
                                    where l.Titulo.Contains(libronombre)

                                    select new
                                    {
                                        p.Id,
                                        p.Titulo,
                                        p.Contenido,
                                        p.FechaPublicacion,
                                        p.AutorId,
                                        at.Nombre,
                                        IdLibro = l.Id
                                    }).ToList();

                if (listadoPosts.Count == 0)
                {
                    return NotFound();
                }
                return Ok(listadoPosts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("GetPost*Autor/{AutorNombre}")]

        public IActionResult GetPostbyAutor(string AutorNombre)
        {
            var listadopost =( from p in _PARCIAL1AContext.Posts
                                join A in _PARCIAL1AContext.Autores
                                on p.AutorId equals A.Id
                                 where A.Nombre.Contains(AutorNombre)
                select new
                {
                    p.Id,
                    p.Titulo,
                    A.Nombre,
                    p.Contenido,
                    p.FechaPublicacion
                    
                }).Take(20).ToList();

            if (listadopost.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadopost);
        }




        [HttpGet]
        [Route("GetByIdPost/{id}")]
        public IActionResult Get(int id)
        {
            Posts? posts = (from p in _PARCIAL1AContext.Posts
                                      where p.Id == id
                                      select p).FirstOrDefault();
            if (posts == null)
            {
                return NotFound();
            }
            return Ok(posts);


        }
        [HttpPost]
        [Route("AddPosts")]
        public IActionResult Guardar([FromBody] Posts posts)
        {
            try
            {
                _PARCIAL1AContext.Posts.Add(posts);
                _PARCIAL1AContext.SaveChanges();
                return Ok(posts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        [Route("actualizarPosts/{id}")]

        public IActionResult Actualizar(int id, [FromBody] Posts postsM)
        {
            try
            {

                Posts? postsA = (from e in _PARCIAL1AContext.Posts
                                       where e.Id == id
                                       select e).FirstOrDefault();


                if (postsA == null)
                {
                    return NotFound();
                }

                postsA.Titulo = postsM.Titulo;
                postsA.Contenido = postsM.Contenido;
                postsA.FechaPublicacion = postsM.FechaPublicacion;
                postsA.AutorId = postsM.AutorId;

                _PARCIAL1AContext.Entry(postsA).State = EntityState.Modified;
                _PARCIAL1AContext.SaveChanges();

                return Ok(postsM);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }
        [HttpDelete]
        [Route("eliminarPosts/{id}")]

        public IActionResult Eliminar(int id)
        {
            try
            {

                Posts? posts = (from e in _PARCIAL1AContext.Posts
                                          where e.Id == id
                                          select e).FirstOrDefault();

                if (posts == null)
                {
                    return NotFound();
                }

                _PARCIAL1AContext.Posts.Attach(posts);
                _PARCIAL1AContext.Posts.Remove(posts);
                _PARCIAL1AContext.SaveChanges();

                return Ok(posts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }


}

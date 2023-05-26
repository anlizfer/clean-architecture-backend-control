using AutoMapper;
using CodeFirst.Core.DTOs.Request.Course.Requests;
using CodeFirst.Core.Interfaces.Services;
using CodeFirst.Domain.QueryFilters;
using CodeFirst.Domain.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace CodeFirst.Web.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/course")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _course;
        private readonly IMapper _mapper;
        public CourseController(
            ICourseService course,
            IMapper mapper
            )
        {
            _course = course;
            _mapper = mapper;
        }
        /// <summary>
        /// Obtiene un listado de Cursos.
        /// </summary>
        /// <remarks>
        /// De acuerdo a los filtros se obtiene un listado de cursos.
        /// </remarks>
        /// <returns>Retorna un listado de cursos solicitado.</returns>
        /// <param name="filters">Diferentes filtros para obtener el listado de cursos.</param>
        /// <response code="500">InternalServerError. Ha ocurrido una exception no controlada.</response>
        /// <response code="200">OK. Devuelve la información solicitada.</response>
        /// <response code="404">NotFound. No se ha encontrado la información solicitada.</response>
        [HttpGet("listado", Name = nameof(GetAllCurso))]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllCurso([FromQuery] CourseQueryFilter filters)
        {
            var courses = _course.GetCourses(filters, Url.RouteUrl(nameof(GetAllCurso)).ToString());
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(courses.Meta));
            return Ok(courses);
        }
        /// <summary>
        /// Obtiene un los datos de un curso.
        /// </summary>
        /// <remarks>
        /// Los datos del curso se obtiene por su Id.
        /// </remarks>
        /// <returns>Retorna los datos del curso solicitado.</returns>
        /// <param name="id">Identificador del objeto curso.</param>
        /// <response code="500">InternalServerError. Ha ocurrido una exception no controlada.</response>
        /// <response code="200">OK. Devuelve la información solicitada.</response>
        /// <response code="404">NotFound. No se ha encontrado la información solicitada.</response>
        [HttpGet("curso/{id:int}", Name = "cursoById")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromRoute] long id)
        {
            return Ok(await _course.GetCourseAsync(id).ConfigureAwait(false));
        }

        /// <summary>
        /// Agrega un nuevo curso.
        /// </summary>
        /// <remarks>
        /// Inserta los datos del curso.
        /// </remarks>
        /// <returns>Retorna los datos del curso agregado.</returns>
        /// <param name="course">El objeto curso.</param>
        /// <response code="500">InternalServerError. Ha ocurrido una exception no controlada.</response>
        /// <response code="200">OK. Devuelve la información solicitada.</response>
        /// <response code="404">NotFound. No se ha encontrado la información solicitada.</response>
        [HttpPost("curso")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Post([FromBody] CourseAddDtoRequest course)
        {
            return Ok(await _course.AddCourseAsync(course));
        }

        /// <summary>
        /// Actualiza el curso.
        /// </summary>
        /// <remarks>
        /// Actualiza los diferentes datos del curso.
        /// </remarks>
        /// <returns>Retorna el curso actualizado</returns>
        /// <param name="id">El identificador del curso</param>
        /// <param name="course">Los datos del curso.</param>
        /// <response code="500">InternalServerError. Ha ocurrido una exception no controlada.</response>
        /// <response code="200">OK. Devuelve la información solicitada.</response>
        /// <response code="404">NotFound. No se ha encontrado la información solicitada.</response>
        [HttpPut("curso/{id:int}")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put([FromRoute] long id, [FromBody] CourseUpdateDtoRequest course)
        {
            return Ok(await _course.UpdateCourseAsync(id, course));
        }

        /// <summary>
        /// Elimina el curso.
        /// </summary>
        /// <remarks>
        /// Elimina los dastos del  curso po su id.
        /// </remarks>
        /// <returns>Retorna el Objeto curso solicitado</returns>
        /// <param name="id">El identificador del curso.</param>
        /// <response code="500">InternalServerError. Ha ocurrido una exception no controlada.</response>
        /// <response code="200">OK. Devuelve la información solicitada.</response>
        /// <response code="404">NotFound. No se ha encontrado la información solicitada.</response>
        [HttpDelete("curso/{id:int}")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            return Ok(await _course.DeleteCourseAsync(id));
        }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace CodeFirst.Core.DTOs.Student.Request
{
    public class StudentAddDtoRequest
    {
        /// <summary>
        /// Nombre del alumno
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Fecha de nacimiento
        /// </summary>
        [Required]
        public DateTime DateOfBirth { get; set; }
    }
}
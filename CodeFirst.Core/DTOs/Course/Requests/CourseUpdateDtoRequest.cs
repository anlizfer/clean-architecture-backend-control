using CodeFirst.Domain.Enums;
using System;

namespace CodeFirst.Core.DTOs.Request.Course.Requests
{
    public class CourseUpdateDtoRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Estado StateId { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
namespace CodeFirst.Core.DTOs.Request.Inscription.Requests
{
    public class InscriptionUpdateDtoRequest
    {
        public long Id { get; set; }
        public long CourseId { get; set; }

        public long StudentId { get; set; }
    }
}
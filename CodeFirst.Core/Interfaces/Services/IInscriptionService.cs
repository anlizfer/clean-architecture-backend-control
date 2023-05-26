using CodeFirst.Core.DTOs.Request.Inscription.Requests;
using CodeFirst.Domain.Entities;
using CodeFirst.Domain.QueryFilters;
using CodeFirst.Domain.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeFirst.Core.Interfaces.Services
{
    public interface IInscriptionService
    {
        PagedListResponse<List<Inscription>> GetInscriptions(InscriptionQueryFilter filters, string actionUrl);

        Task<Response<Inscription>> GetInscriptionAsync(long id);

        Task<Response<Inscription>> AddInscriptionAsync(InscriptionAddDtoRequest inscripcion);

        Task<Response<bool>> UpdateInscriptionAsync(InscriptionUpdateDtoRequest inscripcion);

        Task<Response<bool>> DeleteInscriptionAsync(int id);
    }
}
using AutoMapper;
using CodeFirst.Core.DTOs.DocumentType.Response;
using CodeFirst.Core.Interfaces.Repositories;
using CodeFirst.Core.Interfaces.Services;
using CodeFirst.Domain.Entities;
using CodeFirst.Domain.Exceptions;
using CodeFirst.Domain.Wrappers;
using System.Threading.Tasks;

namespace CodeFirst.Core.Features.DocumentTypeServices
{
    public class DocumentTypeService : IDocumentTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DocumentTypeService(IUnitOfWork UnitOfWork, IMapper Mapper)
        {
            _unitOfWork = UnitOfWork;
            _mapper = Mapper;
        }



        public async Task<Response<DocumentTypeDtoResponse>> GetTypeDocumentAsync(long id)
        {
            DocumentType DocumentSearch = await _unitOfWork.DocumentTypeRepositoryAsync.GetByIdAsync(id).ConfigureAwait(false);
            if (DocumentSearch == null) { throw new CoreException("La información solicitada no exitosa."); }
            DocumentTypeDtoResponse DocumentTypeMap = _mapper.Map<DocumentTypeDtoResponse>(DocumentSearch);
            return new Response<DocumentTypeDtoResponse>(DocumentTypeMap) { Message = "La información solicitada ha sido exitosa." };
        }



    }
}

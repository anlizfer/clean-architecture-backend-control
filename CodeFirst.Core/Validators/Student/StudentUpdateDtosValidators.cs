using CodeFirst.Core.DTOs.Student.Request;
using CodeFirst.Core.Interfaces.Repositories;
using FluentValidation;
using System.Threading.Tasks;

namespace CodeFirst.Core.Validators.Student
{
    public class StudentUpdateDtosValidators : AbstractValidator<StudentUpdateDtoRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentUpdateDtosValidators(
             IUnitOfWork unitOfWork
            )
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x.Name)
                    .NotEmpty().WithMessage("El campo {PropertyName} no puede ser vacío.")
                    .NotNull().WithMessage("El campo {PropertyName}  es requerido.")
                    .MinimumLength(2).WithMessage("El campo {PropertyName} debe  tener minimo 2 caracteres.")
                    .MaximumLength(50).WithMessage("El campo {PropertyName} debe  tener un maximo de caracteres de 50.")
                    .MustAsync(async (x, cancellation) => await NameExistsAsync(x))
                              .WithMessage("El campo {PropertyName} que incluye el valor {PropertyValue} ya existe en el sistemas.")
                    ;
        }

        public async Task<bool> NameExistsAsync(string nombre) => !await _unitOfWork.StudentRepositoryAsync
                               .GetExistsAsync(x => x.Name.Trim().ToUpper()
                                                             .Equals(nombre.Trim().ToUpper())
                                          );
    }
}
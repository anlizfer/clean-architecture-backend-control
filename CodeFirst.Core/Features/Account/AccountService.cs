using AutoMapper;
using CodeFirst.Core.DTOs.Account.Requests;
using CodeFirst.Core.DTOs.Account.Responses;
using CodeFirst.Core.Interfaces.Repositories;
using CodeFirst.Core.Interfaces.Services;
using CodeFirst.Domain.Exceptions;
using CodeFirst.Domain.Helpers;
using CodeFirst.Domain.Interfaces;
using CodeFirst.Domain.QueryFilters;
using CodeFirst.Domain.QueryFilters.Pagination;
using CodeFirst.Domain.Settings;
using CodeFirst.Domain.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst.Core.Features.Account
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IUriService _uriService;
        private readonly PaginationOptionsSetting _paginationOptions;
        private readonly IMapper _mapper;
        public AccountService(
            UserManager<IdentityUser> userManager,
            IUnitOfWork unitOfWork,
            IConfiguration configuration,
            SignInManager<IdentityUser> signInManager,
            IUriService uriService,
            IOptions<PaginationOptionsSetting> options,
            IMapper mapper
            )
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _signInManager = signInManager;
            _uriService = uriService;
            _paginationOptions = options.Value;
            _mapper = mapper;
        }

        public PagedResponse<IEnumerable<UsuarioDtoResponse>> GetUsers(UserQueryFilter filters, string actionUrl)
        {
            PaginationFilter validFilter = new(filters.PageNumber, filters.PageSize, _paginationOptions);
            IEnumerable<IdentityUser> UsersPagedData = _unitOfWork.UserRepositoryAsync
                                                                .GetPagedElementsAsync(
                                                                                        validFilter.PageNumber,
                                                                                        validFilter.PageSize,
                                                                                        x => x.Email,
                                                                                        true).Result;
            var total = _unitOfWork.StudentRepositoryAsync.GetCountAsync().Result;

            PagedResponse<IEnumerable<UsuarioDtoResponse>> response = PaginationHelper.PadageCreateResponse<UsuarioDtoResponse, IdentityUser>(
                                                                    UsersPagedData.ToList(),
                                                                    validFilter,
                                                                    _paginationOptions,
                                                                    total,
                                                                    _uriService,
                                                                    actionUrl,
                                                                    _mapper
                                                               );
            return response;
        }
        public async Task<Response<AuthenticationDtoResponse>> AddAccountAsync(CredentialsUserDtoRequest credentials)
        {
            var usuario = new IdentityUser
            {
                UserName = credentials.Email,
                Email = credentials.Email
            };
            var resultado = await _userManager.CreateAsync(usuario, credentials.Password);

            if (resultado.Succeeded)
            {
                return new Response<AuthenticationDtoResponse>(await ConstruirToken(credentials)) { Message = "La información solicitada ha sido exitosa." };
            }
            else
            {
                List<string> ErrorsValidations = new();

                foreach (IdentityError errorField in resultado.Errors)
                {
                    ErrorsValidations.Add(errorField.Description);
                };

                if (ErrorsValidations.Count != 0)
                {
                    throw new ValidationException(ErrorsValidations);
                }
                throw new ValidationException(ErrorsValidations);
            }
        }
        public async Task<Response<AuthenticationDtoResponse>> GetLoginAsync(CredentialsUserDtoRequest credentials)
        {
            var resultado = await _signInManager.PasswordSignInAsync(
                credentials.Email,
                credentials.Password,
                isPersistent: false,
                lockoutOnFailure: false
                );

            if (resultado.Succeeded)
            {
                return new Response<AuthenticationDtoResponse>(await ConstruirToken(credentials)) { Message = "La información solicitada ha sido exitosa." };
            }
            else
            {
                throw new CoreException("Autenticación no válida.");
            }
        }
        public async Task<Response<AuthenticationDtoResponse>> GetRenovateTokenAsync(HttpContext httpContext)
        {
            var credencialesUsuario = new CredentialsUserDtoRequest()
            {
                Email = httpContext.User.Identity.Name
            };

            return new Response<AuthenticationDtoResponse>(await ConstruirToken(credencialesUsuario)) { Message = "La información solicitada ha sido exitosa." };
        }
        private async Task<AuthenticationDtoResponse> ConstruirToken(CredentialsUserDtoRequest credencialesUsuario)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, credencialesUsuario.Email),
                new Claim(ClaimTypes.Email, credencialesUsuario.Email),
            };

            var identityUser = await _userManager.FindByEmailAsync(credencialesUsuario.Email);

            claims.Add(new Claim(ClaimTypes.NameIdentifier, identityUser.Id));

            var usuario = await _userManager.FindByEmailAsync(credencialesUsuario.Email);
            var claimsDB = await _userManager.GetClaimsAsync(usuario);

            claims.AddRange(claimsDB);

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["KeyJwt"]));
            var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

            var expiracion = DateTime.UtcNow.AddYears(1);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
                expires: expiracion, signingCredentials: creds);

            return new AuthenticationDtoResponse()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiracion = expiracion
            };
        }
        public async Task<Response<bool>> CrearClaimUser(AdminAddDtoRequest AdminDTO)
        {
            var usuario = await _userManager.FindByEmailAsync(AdminDTO.Email);
            await _userManager.AddClaimAsync(usuario, new Claim("esAdmin", "1"));
            return new Response<bool>(true) { Message = $"El registro solicitado ha sido eliminado." };
        }
        public async Task<Response<bool>> RemoverClaimUser(AdminUpdateDtoRequest AdminDTO)
        {
            var usuario = await _userManager.FindByEmailAsync(AdminDTO.Email);
            await _userManager.RemoveClaimAsync(usuario, new Claim("esAdmin", "1"));
            return new Response<bool>(true) { Message = $"El registro solicitado ha sido eliminado." };
        }

    }
}

using NEEFRA.Core.DTO.Account;
using NEEFRA.Core.DTO.Email;
using NEEFRA.Core.DTO.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEEFRA.Core
{
    public interface IAccountService
    {
        Task<ServiceResult<string>> RegisterAsync(RegisterDTO model, string origin);
        Task<ServiceResult<TokenDTO>> LoginAsync(LoginDTO model);
        Task<ServiceResult<string>> SendEmailConfirmationAsync(EmailDTO email, string origin);
        Task<ServiceResult<string>> ConfirmEmailAsync(string userId, string token);
        Task<ServiceResult<string>> ForgetPasswordAsync(EmailDTO model, string origin);
        Task<ServiceResult<string>> ResetPasswordAsync(string token, ResetPasswordDTO model);
        Task<ServiceResult<string>> ChangePasswordAsync(string userId, ChangePasswordDTO model);
        Task<ServiceResult<string>> ChangeEmailAsync(string userId, EmailDTO model);

        Task<ServiceResult<TokenDTO>> GoogleLoginExternalAsync(string email, string name);
        Task<ServiceResult<TokenDTO>> RefreshTokenAsync(TokenDTO tokenDTO);
        Task<ServiceResult<string>> LogoutAsync(TokenDTO tokenDTO);
    }
}

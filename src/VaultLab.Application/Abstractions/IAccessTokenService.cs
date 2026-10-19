using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaultLab.Application.Features.Authentication.Models;

namespace VaultLab.Application.Abstractions
{
    public interface IAccessTokenService
    {
        AccessTokenResult GenerateToken(
            Guid userId,
            string email,
            string name
        );
    }
}
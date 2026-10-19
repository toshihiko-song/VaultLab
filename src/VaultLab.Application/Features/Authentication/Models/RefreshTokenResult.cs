using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaultLab.Application.Features.Authentication.Models
{
    public sealed record RefreshTokenResult(
        string Token,
        DateTime ExpiresAt
    );
}
using System.Collections.Generic;
using System.Security.Claims;

namespace GiamminLib.DomainModels;

public interface IJwtTokenHelper
{
    /// <summary>
    /// genera il token con i claim impostati
    /// </summary>
    /// <param name="claims">elenco dei claims da inserire nel token</param>
    /// <param name="duration">express in seconds</param>
    /// <returns></returns>
    string GetNewToken(IEnumerable<Claim> claims, int duration = 60);
}
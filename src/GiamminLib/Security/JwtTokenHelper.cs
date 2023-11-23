using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using GiamminLib.DomainModels;
using Microsoft.IdentityModel.Tokens;

namespace GiamminLib.Security;

public class JwtTokenHelper : IJwtTokenHelper
{
    public string Audience { get; }
    public string Issuer { get; }
    public string SignatureKey { get; }
    protected readonly JwtHeader Header;
    protected readonly JwtSecurityTokenHandler Handler = new();
    private static readonly object Locker = new();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="audience"> url server api</param>
    /// <param name="issuer">nome client</param>
    /// <param name="signatureKey">chiave privata per firma token</param>
    public JwtTokenHelper(string audience, string issuer, string signatureKey)
    {
        Audience = audience ?? throw new ArgumentNullException(nameof(audience));
        Issuer = issuer ?? throw new ArgumentNullException(nameof(issuer));
        SignatureKey = signatureKey ?? throw new ArgumentNullException(nameof(signatureKey));

        if (signatureKey.Length < 16)
        {
            throw new ArgumentException("signatureKey must be at least 16 char long", nameof(signatureKey));
        }
        Header = GenerateHeader();
    }

    protected JwtHeader GenerateHeader()
    {
        var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(SignatureKey));
        var securityKey = new SymmetricSecurityKey(hmac.Key);
        // length should be >256b
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        return new JwtHeader(credentials);
    }

    /// <summary>
    /// genera il token con i claim impostati
    /// </summary>
    /// <param name="claims">elenco dei claims da inserire nel token</param>
    /// <param name="duration">express in seconds</param>
    /// <returns></returns>
    public string GetNewToken(IEnumerable<Claim> claims, int duration = 60)
    {
        lock (Locker)
        {
            var now = DateTime.Now;

            var jwtSecurityToken = new JwtSecurityToken(
                Header,
                new JwtPayload(Issuer, Audience, claims, now, now.AddSeconds(duration), now));

            return Handler.WriteToken(jwtSecurityToken);
        }
    }
}
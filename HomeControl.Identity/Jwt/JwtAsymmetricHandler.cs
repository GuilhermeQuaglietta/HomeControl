using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Asn1.Sec;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;

namespace HomeControl.Identity.Jwt
{

    public class JwtAsymmetricHandler : IJwtHandler
    {
        public JwtAsymmetricHandler()
        {

        }

        public string GenerateToken(IJwtConfiguration configuration, string userName)
        {
            byte[] byteKey = _fromHexString(configuration.SecretKey);
            ECDsa privateECDsa = _loadPrivateKey(byteKey);
            var credentials = _generateCredentials(privateECDsa);

            var now = DateTime.Now;
            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(userName, "TokenAuth")
            );

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateJwtSecurityToken(new SecurityTokenDescriptor
            {
                Subject = identity,
                Issuer = configuration.Issuer,
                Audience = configuration.Audience,
                Expires = now.AddMinutes(configuration.MinutesToExpire),
                NotBefore = now,
                IssuedAt = now,
                SigningCredentials = credentials,
            });

            return handler.WriteToken(securityToken);
        }

        public JwtValidationResult VerifyToken(IJwtConfiguration configuration, string token)
        {
            byte[] byteKey = _fromHexString(configuration.SecretKey);
            var publicECDsa = _loadPublicKey(byteKey);

            var tokenHandler = new JwtSecurityTokenHandler();
            

            try
            {
                var claimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidIssuer = configuration.Issuer,
                    ValidAudience = configuration.Audience,
                    IssuerSigningKey = new ECDsaSecurityKey(publicECDsa)
                }, out var parsedToken);

                var identity = new JwtIdentity(claimsPrincipal, token, parsedToken);
                return new JwtValidationResult(identity);
            }
            catch (Exception e)
            {
                return new JwtValidationResult(e.Message);
            }
        }

        private byte[] _fromHexString(string hex)
        {
            var numberChars = hex.Length;
            var hexAsBytes = new byte[numberChars / 2];
            for (var i = 0; i < numberChars; i += 2)
                hexAsBytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);

            return hexAsBytes;
        }

        private ECDsa _loadPrivateKey(byte[] key)
        {
            var privKeyInt = new Org.BouncyCastle.Math.BigInteger(+1, key);
            var parameters = SecNamedCurves.GetByName("secp256r1");
            var ecPoint = parameters.G.Multiply(privKeyInt);
            var privKeyX = ecPoint.Normalize().XCoord.ToBigInteger().ToByteArrayUnsigned();
            var privKeyY = ecPoint.Normalize().YCoord.ToBigInteger().ToByteArrayUnsigned();

            return ECDsa.Create(new ECParameters
            {
                Curve = ECCurve.NamedCurves.nistP256,
                D = privKeyInt.ToByteArrayUnsigned(),
                Q = new ECPoint
                {
                    X = privKeyX,
                    Y = privKeyY
                }
            });
        }

        private ECDsa _loadPublicKey(byte[] key)
        {
            var pubKeyX = key.Skip(1).Take(32).ToArray();
            var pubKeyY = key.Skip(33).ToArray();

            return ECDsa.Create(new ECParameters
            {
                Curve = ECCurve.NamedCurves.nistP256,
                Q = new ECPoint
                {
                    X = pubKeyX,
                    Y = pubKeyY
                }
            });
        }

        private SigningCredentials _generateCredentials(ECDsa privateECDsa)
        {
            return new SigningCredentials(new ECDsaSecurityKey(privateECDsa), SecurityAlgorithms.EcdsaSha256);
        }
    }
}

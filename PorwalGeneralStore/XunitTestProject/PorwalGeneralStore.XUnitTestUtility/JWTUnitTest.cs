using PorwalGeneralStore.Utility.JWTTokenGenerator;
using PorwalGeneralStore.Utility.JWTTokenGenerator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Xunit;

namespace PorwalGeneralStore.XUnitTestUtility
{
    public class JwtUnitTest
    {
        private readonly IJwtBuilder jwtBuilder;
        public JwtUnitTest()
        {
            jwtBuilder = new JwtBuilder(new JwtConfiguration() { Issuer = "Issuer", Secret = "Secret123456789123456789" });
        }

        [Theory(DisplayName = "Get Validation Error Response When Claim are null")]
        [MemberData(nameof(ClaimsData))]
        public void UnitTest1(Dictionary<string, string> claims)
        {
            var ActualResult = jwtBuilder.GetJWTToken(claims);
            Assert.NotNull(ActualResult);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.Null(ActualResult.TokenDetail);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.True(ActualResult.ErrorList.Count > 0);
            Assert.True(ActualResult.ErrorList.First().Message == "Claims can't be blank");
        }

        [Theory(DisplayName = "Get Validation Error Response When Audience is null")]
        [InlineData(null)]
        public void UnitTest2(string audience)
        {
            Dictionary<string, string> claimList = new Dictionary<string, string>
            {
                    { "Claims1","Claims1"},
                    { "Claims2","Claims2"},
                    { "Claims3","Claims3"},
            };

            var ActualResult = jwtBuilder.GetJWTToken(claimList, audience);
            Assert.NotNull(ActualResult);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.Null(ActualResult.TokenDetail);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.True(ActualResult.ErrorList.Count > 0);
            Assert.True(ActualResult.ErrorList.First().Message == "audience can't be blank");
        }


        [Theory(DisplayName = "Get Valid JwtTokenResponse With given expires time")]
        [MemberData(nameof(Data))]
        public void UnitTest3(DateTime? expires)
        {
            Dictionary<string, string> claimList = new Dictionary<string, string>
            {
                    { "Claims1","Claims1"},
                    { "Claims2","Claims2"},
                    { "Claims3","Claims3"},
            };
            var ActualResult = jwtBuilder.GetJWTToken(claimList, "Audience", expires);
            Assert.NotNull(ActualResult);
            Assert.Null(ActualResult.ErrorList);
            Assert.NotNull(ActualResult.TokenDetail);
            Assert.True(ActualResult.StatusCode == 200);
            Assert.True(ActualResult.TokenDetail.ExpiredAt == expires);
        }

        [Theory(DisplayName = "Get Claims List")]
        [MemberData(nameof(ClaimsInlineData))]
        public void UnitTest4(Dictionary<string, string> claimList, int claimsCount)
        {
            var ActualResult = new JwtBuilder(new JwtConfiguration() { Issuer = "Issuer", Secret = "Secret" }).GetClaimsList(claimList);
            Assert.NotNull(ActualResult);
            Assert.True(ActualResult?.Count() == claimsCount);
        }

        [Theory(DisplayName = "Get JWT Token")]
        [MemberData(nameof(TokenCreatorData))]
        public void UnitTest5(Dictionary<string, string> claimList, string audience, DateTime? expires)
        {
            var ActualResult = new JwtBuilder(
                new JwtConfiguration()
                {
                    Issuer = "Issuer",
                    Secret = "Secret123456789123456789"
                }).GenerateToken(claimList, audience, expires, DateTime.UtcNow);
            Assert.NotNull(ActualResult);
            Assert.True(!string.IsNullOrWhiteSpace(ActualResult));
        }

        [Theory(DisplayName = "Exception Should be trhown when token is not generate.")]
        [MemberData(nameof(TokenCreatorData))]
        public void UnitTest6(Dictionary<string, string> claimList, string audience, DateTime? expires)
        {
            var exception = Record.Exception(() =>
            {
                new JwtBuilder(
                    new JwtConfiguration()
                    {
                        Issuer = "Issuer",
                        Secret = "Wrong key Size"
                    }).GenerateToken(claimList, audience, expires, DateTime.UtcNow);
            });

            Assert.NotNull(exception);
        }

        [Theory(DisplayName = "Get Validtion Response When Token is not generate")]
        [MemberData(nameof(TokenCreatorData))]
        public void UnitTest7(Dictionary<string, string> claimList, string audience, DateTime? expires)
        {
            var ActualResult = new JwtBuilder(
                new JwtConfiguration()
                {
                    Issuer = "Issuer",
                    Secret = "Wrong key Size"
                }).GetJWTToken(claimList, audience, expires);
            Assert.NotNull(ActualResult);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.Null(ActualResult.TokenDetail);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.True(ActualResult.ErrorList.Count > 0);
        }

        [Theory(DisplayName = "Get Validtion Response When expires time is less then current time")]
        [MemberData(nameof(InvalidExpiredTimeData))]
        public void UnitTest8(DateTime? expires)
        {
            Dictionary<string, string> claimList = new Dictionary<string, string>
            {
                    { "Claims1","Claims1"},
                    { "Claims2","Claims2"},
            };
            var ActualResult = jwtBuilder.GetJWTToken(claimList, "Audience", expires);
            Assert.NotNull(ActualResult);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.Null(ActualResult.TokenDetail);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.True(ActualResult.ErrorList.Count > 0);
            Assert.True(ActualResult.ErrorList.First().Message == "expires time should be greater then current utc time");
        }

        public static IEnumerable<object[]> Data =>
        new List<object[]>
        {
            new object[] { null },
            new object[] { DateTime.Now.AddDays(7) },
            new object[] { DateTime.UtcNow.AddDays(7) },
        };


        public static IEnumerable<object[]> InvalidExpiredTimeData =>
        new List<object[]>
        {
            new object[] { DateTime.Now.AddDays(-7) },
            new object[] { DateTime.UtcNow.AddDays(-7) },
            new object[] { new DateTime() },
        };


        public static IEnumerable<object[]> ClaimsInlineData =>
        new List<object[]>
        {
            new object[] { null,0 },
            new object[] { new Dictionary<string,string>{},0 },
            new object[] {
                new Dictionary<string,string>
                {
                    { "Claims1","Claims1"},
                    { "Claims2","Claims2"},
                    { "Claims3","Claims3"},
                },3
            }
        };

        public static IEnumerable<object[]> ClaimsData =>
                new List<object[]>
                {
                    new object[] { null },
                    new object[] { new Dictionary<string,string>{} },
                };

        public static IEnumerable<object[]> TokenCreatorData =>
        new List<object[]>
        {
            new object[] {
                new Dictionary<string,string>
                {
                    { "Claims1","Claims11"},
                    { "Claims2","Claims222"},
                    { "Claims3","Claims3555"},
                },
                "Audience",
                null
            }
        };
    }
}

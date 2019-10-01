using PorwalGeneralStore.Utility.JWTTokenGenerator;
using PorwalGeneralStore.Utility.JWTTokenGenerator.Model;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Xunit;

namespace PorwalGeneralStore.XUnitTestUtility
{
    public class JwtUnitTest
    {
        private readonly IJwtBuilder jwtBuilder;
        public JwtUnitTest()
        {
            jwtBuilder = new JwtBuilder(new JwtConfiguration() { Issuer = "Issuer", Secret = "Secret" });
        }
        [Fact(DisplayName = "Get Null Response When Claim are null")]
        public void UnitTest1()
        {
            var ActualResult = jwtBuilder.GetJWTToken(null);
            Assert.Null(ActualResult);
        }

        [Theory(DisplayName = "Get Null Response When Issuer is null")]
        [InlineData(null)]
        public void UnitTest2(string issuer)
        {
            Claim[] claimList = new Claim[]{
                new Claim("Claim1","Value1")
            };
            var ActualResult = jwtBuilder.GetJWTToken(claimList, issuer);
            Assert.Null(ActualResult);
        }


        [Theory(DisplayName = "Get Null Response When Issuer is null")]
        [InlineData(null)]
        public void UnitTest3(string audience)
        {
            Claim[] claimList = new Claim[]{
                new Claim("Claim1","Value1")
            };
            var ActualResult = jwtBuilder.GetJWTToken(claimList, "Issuer", audience);
            Assert.Null(ActualResult);
        }


        [Theory(DisplayName = "Get JwtTokenResponse With given expires time")]
        [MemberData(nameof(Data))]
        public void UnitTest4(DateTime? expires)
        {
            Claim[] claimList = new Claim[]{
                new Claim("Claim1","Value1")
            };
            var ActualResult = jwtBuilder.GetJWTToken(claimList, "Issuer", "Audience", expires);
            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.ExpiredAt == expires);
        }

        [Fact(DisplayName = "Get JWT Token")]
        public void UnitTest10()
        {
            Claim[] claimList = new Claim[]{
                new Claim("Claim1","Value1")
            };
            var ActualResult = jwtBuilder.GetJWTToken(claimList);

            Assert.NotNull(ActualResult);
            Assert.True(!string.IsNullOrWhiteSpace(ActualResult.Type));
            Assert.True(!string.IsNullOrWhiteSpace(ActualResult.Value));
            Assert.True(ActualResult.CreatedAt != DateTime.MinValue);
        }

        public static IEnumerable<object[]> Data =>
        new List<object[]>
        {
            new object[] { null },
            new object[] { DateTime.Now },
            new object[] { DateTime.UtcNow },
            new object[] { new DateTime() },
        };
    }
}

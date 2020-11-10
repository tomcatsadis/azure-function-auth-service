using System.Collections.Generic;

namespace AuthService.Domain.UnitTests.Util
{
    public static class ClaimFactory
    {
        public static IEnumerable<object[]> Data()
        {
            return new List<object[]>
            {
                new object[] { "Role", "Admin" },
                new object[] { "Role", "Merchant" },
                new object[] { "Role", "Customer" }
            };
        }
    }
}

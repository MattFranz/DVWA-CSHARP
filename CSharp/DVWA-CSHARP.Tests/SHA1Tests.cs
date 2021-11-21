using Xunit;

namespace DVWA_CSHARP.Tests
{
    public class SHA1Tests
    {
        [Fact]
        public void HashTest()
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes("");
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var hash = sha1.ComputeHash(bytes);
            var hasBase64 = System.Convert.ToBase64String(hash);
            Assert.NotEmpty(hash);
            Assert.NotNull(hash);

        }
    }
}
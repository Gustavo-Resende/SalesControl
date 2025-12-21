using System;
using Xunit;
using SalesControl.Domain.ClientAggregate;

namespace SalesControl.Tests.Domain
{
    public class ClientTests
    {
        [Fact]
        public void Create_Valid()
        {
            var client = new Client("Gustavo Resende", "gustavo.resende@example.com", "123456789");

            Assert.NotEqual(Guid.Empty, client.Id);
            Assert.Equal("Gustavo Resende", client.Name);
            Assert.Equal("gustavo.resende@example.com", client.Email);
            Assert.Equal("123456789", client.Phone);
            Assert.True(client.CreatedAt <= DateTimeOffset.UtcNow);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Create_InvalidName_Throws(string? name)
        {
            Assert.ThrowsAny<ArgumentException>(() => new Client(name!, "gustavo.resende@example.com"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Create_InvalidEmail_Throws(string? email)
        {
            Assert.ThrowsAny<ArgumentException>(() => new Client("Gustavo", email!));
        }

        [Fact]
        public void Update_Contact()
        {
            var client = new Client("Gustavo Resende", "gustavo.resende@example.com", "123");

            client.UpdateContact("Gustavo Resende", "gustavo.resende+alt@example.com", "456");

            Assert.Equal("Gustavo Resende", client.Name);
            Assert.Equal("gustavo.resende+alt@example.com", client.Email);
            Assert.Equal("456", client.Phone);
            Assert.True(client.UpdatedAt >= client.CreatedAt);
        }
    }
}

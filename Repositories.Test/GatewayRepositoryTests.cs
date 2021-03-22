using DataAccess;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace Repositories.Test
{

    public class GatewayRepositoryTests
    {
        DbContextOptionsBuilder<TestDbContext> builder;
        public GatewayRepositoryTests()
        {
            builder = new DbContextOptionsBuilder<TestDbContext>();
        }
        [Fact]
        public async void Get_Gateway_Returns_Element_If_Exists()
        {
            builder.UseInMemoryDatabase("Get_Gateway_Returns_Element_If_Exists");
            var options = builder.Options;
            var gateway = new Domain.Gateway()
            {
                Id = 1,
                SerialNumber = "G1",
                IPV4Address = "12.12.1.2",
                Name = "G1"

            };
            using (var context = new TestDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Add(gateway);
                context.SaveChanges();
            }

            Domain.Gateway existingGateway = null;
            Domain.Gateway nonExistingGateway = null;

            using (var context = new TestDbContext(options))
            {
                var repository = new GatewayRepository(context);
                existingGateway = await repository.Get(1);
                nonExistingGateway = await repository.Get(2);
            }

            Assert.True(existingGateway != null && nonExistingGateway == null);
            Assert.True(existingGateway.Id == gateway.Id
                && existingGateway.IPV4Address == gateway.IPV4Address
                && existingGateway.Name == gateway.Name
                && existingGateway.SerialNumber == gateway.SerialNumber);
        }

        [Fact]
        public async void Insert_Gateway_Inserts_Element()
        {
            builder.UseInMemoryDatabase("Insert_Gateway_Inserts_Element");
            var options = builder.Options;

            Domain.Gateway gateway = new Domain.Gateway();
            using (var context = new TestDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                var repository = new GatewayRepository(context);
                await repository.Insert(new Domain.Gateway()
                {
                    Id = 1,
                    SerialNumber = "G1",
                    IPV4Address = "12.12.1.2",
                    Name = "G1"

                });
                gateway = context.Gateways.Find(1);
            }

            Assert.True(gateway != null && gateway.Id == 1);
        }

        [Fact]
        public async void Insert_Null_Gateway_Throws_Error()
        {
            builder.UseInMemoryDatabase("Insert_Null_Gateway_Throws_Error");
            var options = builder.Options;

            Domain.Gateway gateway = new Domain.Gateway();
            using (var context = new TestDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                var repository = new GatewayRepository(context);

                await Assert.ThrowsAsync<ArgumentNullException>(async () => await repository.Insert(null));

            }
        }

        [Fact]
        public async void Delete_Gateway_Deletes_Element()
        {
            
            builder.UseInMemoryDatabase("Delete_Gateway_Deletes_Element");
            var options = builder.Options;
            var gateway = new Domain.Gateway();
            using (var context = new TestDbContext(options))
            {
                gateway = new Domain.Gateway()
                {
                    Id = 1,
                    SerialNumber = "G1",
                    IPV4Address = "12.12.1.2",
                    Name = "G1"

                };
                context.Add(gateway);
                context.SaveChanges();
            }

            using (var context = new TestDbContext(options))
            {
                var repository = new GatewayRepository(context);
                await repository.Delete(gateway);
                gateway = context.Gateways.Find(1);
            }

            Assert.Null(gateway);
        }

        [Fact]
        public async void Delete_Null_Gateway_Throws_Error()
        {

            builder.UseInMemoryDatabase("Delete_Null_Gateway_Throws_Error");
            var options = builder.Options;

            Domain.Gateway gateway = new Domain.Gateway();
            using (var context = new TestDbContext(options))
            {

                var repository = new GatewayRepository(context);
                gateway = new Domain.Gateway()
                {
                    Id = 1,
                    SerialNumber = "G1",
                    IPV4Address = "12.12.1.2",
                    Name = "G1"

                };
                context.Gateways.Add(gateway);
                context.SaveChanges();
                await Assert.ThrowsAsync<ArgumentNullException>(async () => await repository.Delete(null));

            }
        }
        [Fact]
        public async void Delete_Non_Existing_Gateway_Throws_Error()
        {

            builder.UseInMemoryDatabase("Delete_Non_Existing_Gateway_Throws_Error");
            var options = builder.Options;

            Domain.Gateway gateway = new Domain.Gateway();
            using (var context = new TestDbContext(options))
            {
                var repository = new GatewayRepository(context);
                gateway = new Domain.Gateway()
                {
                    Id = 1,
                    SerialNumber = "G1",
                    IPV4Address = "12.12.1.2",
                    Name = "G1"

                };
                context.Gateways.Add(gateway);
                context.SaveChanges();
                gateway.Id = 2;
                await Assert.ThrowsAsync<InvalidOperationException>(async () => await repository.Delete(gateway));

            }
        }

        [Fact]
        public async void Delete_Non_Existing_Gateway_In_Empty_DB_Throws_Error()
        {
            builder.UseInMemoryDatabase("Delete_Non_Existing_Gateway_In_Empty_DB_Throws_Error");
            var options = builder.Options;
            Domain.Gateway gateway = new Domain.Gateway();
            using (var context = new TestDbContext(options))
            {
                var repository = new GatewayRepository(context);
                gateway = new Domain.Gateway()
                {
                    Id = 1,
                    SerialNumber = "G1",
                    IPV4Address = "12.12.1.2",
                    Name = "G1"

                };
                await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () => await repository.Delete(gateway));
            }
        }
    }
}

using DataAccess;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using System;
using Xunit;

namespace Repositories.Test
{
   
    public class GatewayRepositoryTests
    {
        IGatewayRepository gatewayServiceMock = Substitute.For<IGatewayRepository>();
        public GatewayRepositoryTests()
        {
           
        }
        [Fact]
        public async void Get_Gateway_Returns_Element_If_Exists()
        {
            DbContextOptions<TestDbContext> options;
            var builder = new DbContextOptionsBuilder<TestDbContext>();
            builder.UseInMemoryDatabase("Get_Gateway_Returns_Element_If_Exists");
            options = builder.Options;
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
            DbContextOptions<TestDbContext> options;
            var builder = new DbContextOptionsBuilder<TestDbContext>();
            builder.UseInMemoryDatabase("Insert_Gateway_Inserts_Element");
            options = builder.Options;

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

            Assert.True(gateway != null && gateway.Id==1);
        }

        [Fact]
        public async void Insert_Null_Gateway_Throws_Error()
        {
            DbContextOptions<TestDbContext> options;
            var builder = new DbContextOptionsBuilder<TestDbContext>();
            builder.UseInMemoryDatabase("Insert_Null_Gateway_Throws_Error");
            options = builder.Options;

            Domain.Gateway gateway = new Domain.Gateway();
            using (var context = new TestDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                var repository = new GatewayRepository(context);
               
                await Assert.ThrowsAsync<ArgumentNullException>(async ()=> await repository.Insert(null));
                
            }

           
        }

        [Fact]
        public async void Delete_Gateway_Deletes_Element()
        {
            DbContextOptions<TestDbContext> options;
            var builder = new DbContextOptionsBuilder<TestDbContext>();
            builder.UseInMemoryDatabase("Delete_Gateway_Deletes_Element");
            options = builder.Options;
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
            DbContextOptions<TestDbContext> options;
            var builder = new DbContextOptionsBuilder<TestDbContext>();
            builder.UseInMemoryDatabase("Delete_Null_Gateway_Throws_Error");
            options = builder.Options;

            Domain.Gateway gateway = new Domain.Gateway();
            using (var context = new TestDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
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
    }
}

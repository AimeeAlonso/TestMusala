using DataAccess;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using System;
using Xunit;

namespace Repositories.Test
{
   
    public class DeviceRepositoryTests
    {
        public DeviceRepositoryTests()
        {
           
        }
        [Fact]
        public async void Get_Device_Returns_Element_If_Exists()
        {
            DbContextOptions<TestDbContext> options;
            var builder = new DbContextOptionsBuilder<TestDbContext>();
            builder.UseInMemoryDatabase("Get_Device_Returns_Element_If_Exists");
            options = builder.Options;
            var device = new Domain.Device()
            {
                Id = 1,
                Vendor = "V1",
                Status = true,
                UId = 1,
                DateCreated=DateTime.Today

            };
            using (var context = new TestDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Add(device);
                context.SaveChanges();
            }

            Domain.Device existingDevice = null;
            Domain.Device nonExistingDevice= null;

            using (var context = new TestDbContext(options))
            {
                var repository = new DeviceRepository(context);
                existingDevice = await repository.Get(1);
                nonExistingDevice = await repository.Get(2);
            }

            Assert.True(existingDevice != null && nonExistingDevice == null);
            Assert.True(existingDevice.Id == device.Id
                && existingDevice.Vendor == device.Vendor
                && existingDevice.UId == device.UId
                && existingDevice.Status == device.Status
                && existingDevice.DateCreated == device.DateCreated);
        }

        [Fact]
        public async void Insert_Device_Inserts_Element()
        {
            DbContextOptions<TestDbContext> options;
            var builder = new DbContextOptionsBuilder<TestDbContext>();
            builder.UseInMemoryDatabase("Insert_Device_Inserts_Element");
            options = builder.Options;

            Domain.Device device = new Domain.Device();
            using (var context = new TestDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                var repository = new DeviceRepository(context);
                await repository.Insert(new Domain.Device()
                {
                    Id = 1,
                    Vendor = "V1",
                    Status = true,
                    UId = 1,
                    DateCreated = DateTime.Today

                });
                device = context.Devices.Find(1);
            }

            Assert.True(device != null && device.Id==1);
        }

        [Fact]
        public async void Insert_Null_Device_Throws_Error()
        {
            DbContextOptions<TestDbContext> options;
            var builder = new DbContextOptionsBuilder<TestDbContext>();
            builder.UseInMemoryDatabase("Insert_Null_Device_Throws_Error");
            options = builder.Options;

            Domain.Device device = new Domain.Device();
            using (var context = new TestDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                var repository = new DeviceRepository(context);
               
                await Assert.ThrowsAsync<ArgumentNullException>(async ()=> await repository.Insert(null));
                
            }

           
        }

        [Fact]
        public async void Delete_Device_Deletes_Element()
        {
            DbContextOptions<TestDbContext> options;
            var builder = new DbContextOptionsBuilder<TestDbContext>();
            builder.UseInMemoryDatabase("Delete_Device_Deletes_Element");
            options = builder.Options;
            var device = new Domain.Device();
            using (var context = new TestDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                 device = new Domain.Device()
                {
                     Id = 1,
                     Vendor = "V1",
                     Status = true,
                     UId = 1,
                     DateCreated = DateTime.Today

                 };
                context.Devices.Add(device);
                var repository = new DeviceRepository(context);
                await repository.Delete(device);
                device = context.Devices.Find(1);
            }

            Assert.True(device==null);
        }

        [Fact]
        public async void Delete_Null_Device_Throws_Error()
        {
            DbContextOptions<TestDbContext> options;
            var builder = new DbContextOptionsBuilder<TestDbContext>();
            builder.UseInMemoryDatabase("Delete_Null_Device_Throws_Error");
            options = builder.Options;

            Domain.Device device = new Domain.Device();
            using (var context = new TestDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                var repository = new DeviceRepository(context);
                device = new Domain.Device()
                {
                    Id = 1,
                    Vendor = "V1",
                    Status = true,
                    UId = 1,
                    DateCreated = DateTime.Today

                };
                context.Devices.Add(device);
               
                await Assert.ThrowsAsync<ArgumentNullException>(async () => await repository.Delete(null));

            }


        }
    }
}

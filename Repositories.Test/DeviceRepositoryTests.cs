using DataAccess;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace Repositories.Test
{
   
    public class DeviceRepositoryTests
    {
        DbContextOptionsBuilder<TestDbContext> builder;
        public DeviceRepositoryTests()
        {
            builder = new DbContextOptionsBuilder<TestDbContext>();
        }
        [Fact]
        public async void Get_Device_Returns_Element_If_Exists()
        {
            builder.UseInMemoryDatabase("Get_Device_Returns_Element_If_Exists");
            var options = builder.Options;
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
           
            builder.UseInMemoryDatabase("Insert_Device_Inserts_Element");
            var options = builder.Options;

            Domain.Device device = new Domain.Device();
            using (var context = new TestDbContext(options))
            {
               
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
           
            builder.UseInMemoryDatabase("Insert_Null_Device_Throws_Error");
            var options = builder.Options;

            Domain.Device device = new Domain.Device();
            using (var context = new TestDbContext(options))
            {
                var repository = new DeviceRepository(context);
                await Assert.ThrowsAsync<ArgumentNullException>(async ()=> await repository.Insert(null));
                
            }

           
        }
        [Fact]
        public async void Delete_Device_Deletes_Element()
        {
             builder.UseInMemoryDatabase("Delete_Device_Deletes_Element");
            var options = builder.Options;
            var device = new Domain.Device();
            using (var context = new TestDbContext(options))
            {
                 device = new Domain.Device()
                {
                     Id = 1,
                     Vendor = "V1",
                     Status = true,
                     UId = 1,
                     DateCreated = DateTime.Today

                 };
                context.Devices.Add(device);
                context.SaveChanges();
                var repository = new DeviceRepository(context);
                await repository.Delete(device);
                device = context.Devices.Find(1);
            }

            Assert.True(device==null);
        }

        [Fact]
        public async void Delete_Null_Device_Throws_Error()
        {
          
            builder.UseInMemoryDatabase("Delete_Null_Device_Throws_Error");
            var options = builder.Options;

            Domain.Device device = new Domain.Device();
            using (var context = new TestDbContext(options))
            {
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
                context.SaveChanges();
                await Assert.ThrowsAsync<ArgumentNullException>(async () => await repository.Delete(null));

            }
        }

        [Fact]
        public async void Delete_Non_Existing_Device_Throws_Error()
        {

            builder.UseInMemoryDatabase("Delete_Non_Existing_Device_Throws_Error");
            var options = builder.Options;

            Domain.Device device = new Domain.Device();
            using (var context = new TestDbContext(options))
            {
                var repository = new DeviceRepository(context);
                device = new Domain.Device()
                {
                    Id = 1,
                    Vendor = "V1",
                    Status = true,
                    UId = 1,
                    DateCreated = DateTime.Today

                };
                context.Add(device);
                context.SaveChanges();
                device.Id = 2;
                await Assert.ThrowsAsync<InvalidOperationException>(async () => await repository.Delete(device));

            }
        }

        [Fact]
        public async void Delete_Non_Existing_Device_In_Empty_DB_Throws_Error()
        {

            builder.UseInMemoryDatabase("Delete_Non_Existing_Device_In_Empty_DB_Throws_Error");
            var options = builder.Options;

            Domain.Device device = new Domain.Device();
            using (var context = new TestDbContext(options))
            {
                var repository = new DeviceRepository(context);
                device = new Domain.Device()
                {
                    Id = 1,
                    Vendor = "V1",
                    Status = true,
                    UId = 1,
                    DateCreated = DateTime.Today

                };
                await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () => await repository.Delete(device));
            }
        }
    }
}

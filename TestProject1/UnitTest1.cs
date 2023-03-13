using A3_BusinessGroupProjectApi.Controllers;
using A3_BusinessGroupProjectApi.Data;
using A3_BusinessGroupProjectApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public async Task TestCreateImmunization_Post()
        {
            // Arrange
            var connectionString = "Server=(localdb)\\mssqllocaldb;Database=TestDatabase;Trusted_Connection=True;MultipleActiveResultSets=true";
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            using (var context = new AppDbContext(options))
            {
                // Ensure that the database is created
                context.Database.EnsureCreated();

                var controller = new ImmunizationController(context);
                var immunization = new Immunization
                {
                    Id = Guid.NewGuid(),
                    CreationTime = DateTimeOffset.UtcNow,
                    OfficialName = "Test Immunization",
                    TradeName = "Test Trade Name",
                    LotNumber = "12345",
                    ExpirationDate = DateTimeOffset.UtcNow.AddDays(30)
                };

                // Act
                var result = await controller.CreateImmunization(immunization);

                // Assert
                Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));
                Assert.AreEqual(StatusCodes.Status200OK, ((CreatedAtActionResult)result.Result).StatusCode);
            }
        }

        [TestMethod]
        public async Task TestGetImmunizationById()
        {
            // Arrange
            var connectionString = "Server=(localdb)\\mssqllocaldb;Database=TestDatabase;Trusted_Connection=True;MultipleActiveResultSets=true";
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            using (var context = new AppDbContext(options))
            {
                var controller = new ImmunizationController(context);
                var immunization = new Immunization
                {
                    Id = Guid.NewGuid(),
                    CreationTime = DateTimeOffset.UtcNow,
                    OfficialName = "Test Immunization",
                    TradeName = "Test Trade Name",
                    LotNumber = "12345",
                    ExpirationDate = DateTimeOffset.UtcNow.AddDays(30)
                };
                await context.Immunizations.AddAsync(immunization);
                await context.SaveChangesAsync();

                // Act
                var result = await controller.GetImmunizationById(immunization.Id);

                // Assert
                Assert.IsInstanceOfType(result, typeof(ActionResult<Immunization>));
                var actionResult = (ActionResult<Immunization>)result;
                var okObjectResult = (OkObjectResult)actionResult.Result;
                Assert.AreEqual(StatusCodes.Status200OK, okObjectResult.StatusCode);

            }
        }

        [TestMethod]
        public async Task TestUpdateImmunization()
        {
            // Arrange
            var connectionString = "Server=(localdb)\\mssqllocaldb;Database=TestDatabase;Trusted_Connection=True;MultipleActiveResultSets=true";
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(connectionString)
                .Options;


            using (var context = new AppDbContext(options))
            {
                var controller = new ImmunizationController(context);
                var immunization = new Immunization
                {
                    Id = Guid.NewGuid(),
                    CreationTime = DateTimeOffset.UtcNow,
                    OfficialName = "Test Immunization",
                    TradeName = "Test Trade Name",
                    LotNumber = "12345",
                    ExpirationDate = DateTimeOffset.UtcNow.AddDays(30)
                };
                await context.Immunizations.AddAsync(immunization);
                await context.SaveChangesAsync();

                immunization.TradeName = "Updated Trade Name";

                // Act
                var result = await controller.UpdateImmunization(immunization.Id, immunization);

                // Assert
                Assert.IsInstanceOfType(result, typeof(NoContentResult));
                Assert.AreEqual(StatusCodes.Status204NoContent, ((NoContentResult)result).StatusCode);
            }
        }

        [TestMethod]
        public async Task TestDeleteImmunization()
        {
            // Arrange
            var connectionString = "Server=(localdb)\\mssqllocaldb;Database=TestDatabase;Trusted_Connection=True;MultipleActiveResultSets=true";
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(connectionString)
                .Options;


            using (var context = new AppDbContext(options))
            {
                var controller = new ImmunizationController(context);
                var immunization = new Immunization
                {
                    Id = Guid.NewGuid(),
                    CreationTime = DateTimeOffset.UtcNow,
                    OfficialName = "Test Immunization",
                    TradeName = "Test Trade Name",
                    LotNumber = "12345",
                    ExpirationDate = DateTimeOffset.UtcNow.AddDays(30)
                };
                await context.Immunizations.AddAsync(immunization);
                await context.SaveChangesAsync();

                // Act
                var result = await controller.DeleteImmunization(immunization.Id);

                // Assert
                Assert.IsInstanceOfType(result, typeof(NoContentResult));
                Assert.AreEqual(StatusCodes.Status204NoContent, ((NoContentResult)result).StatusCode);
            }
        }
    }
}

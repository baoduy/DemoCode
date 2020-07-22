using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayerTests.LoadData;
using FluentAssertions;
using GenericServices;
using GenericServices.Setup;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataLayerTests
{
    [TestClass]
    public class UserTests
    {
        private ServiceProvider _service;

        [TestInitialize]
        public void Setup()
        {
            var svColl = new ServiceCollection();

            svColl.AddDbContext<MyContext>(op => op.UseSqliteMemory())
                .ConfigureGenericServicesEntities(typeof(MyContext))
                //More Model here
                .ScanAssemblesForDtos(typeof(MyContext).Assembly)
                .RegisterGenericServices();

            _service = svColl.BuildServiceProvider();

            var db = _service.GetService<MyContext>();
            db.Database.EnsureCreated();
            db.LoadUsers();
        }

        [TestCleanup]
        public void CleanUp() => _service?.Dispose();

        [TestMethod]
        public async Task ReadUsers()
        {
            var sv = _service.GetService<ICrudServicesAsync<MyContext>>();

            var users = await sv.ProjectFromEntityToDto<User, UserDto>(
                q => q.IgnoreQueryFilters().Where(u => u.IsDeleted))
                .FirstOrDefaultAsync();

            users.Should().NotBeNull();
            users.IsDeleted.Should().BeTrue();
        }
    }
}

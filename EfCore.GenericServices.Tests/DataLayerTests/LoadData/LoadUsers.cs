using System.Linq;
using Bogus;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace DataLayerTests.LoadData
{
    internal static class  LoadUsersExtensions
    {
        public static void LoadUsers(this DbContext @this)
        {
            if(EnumerableExtensions.Any(@this.Set<User>()))return;

            var users = new Faker<User>()
                .RuleFor(u => u.Name, op => op.Person.FullName)
                .Generate(100);

            foreach (var user in users.Take(10))
                user.IsDeleted = true;

            @this.Set<User>().AddRange(users);
            @this.SaveChanges();
        }
    }
}

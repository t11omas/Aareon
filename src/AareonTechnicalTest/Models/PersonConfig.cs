using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AareonTechnicalTest.Models
{
    public class PersonConfig : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasData(
                new Person() { Id = 1, Forename = "System", Surname = "Admin", IsAdmin = true },
                new Person() { Id = 2, Forename = "Test", Surname = "User", IsAdmin = false });
        }
    }
}
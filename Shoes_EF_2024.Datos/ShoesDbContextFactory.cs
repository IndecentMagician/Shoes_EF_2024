using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Shoes_EF_2024.Datos
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;
    using System.IO;

    public class ShoesDbContextFactory : IDesignTimeDbContextFactory<ShoesDbContext>
    {
        public ShoesDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<ShoesDbContext>();
            var connectionString = configuration.GetConnectionString("MyConnection");

            builder.UseSqlServer(connectionString);

            return new ShoesDbContext(builder.Options);
        }
    }

}

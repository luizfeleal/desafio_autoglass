using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Configuration
{
    public class ContextBase : DbContext
    {
        public ContextBase(DbContextOptions<ContextBase> options) : base(options)
        {

        }

        public DbSet<Product> Product { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(getStringConection());
                base.OnConfiguring(optionsBuilder);
            }
        }

        public string getStringConection()
        {
            string strconn = "Data Source=DESKTOP-FICCU2T\\SQLEXPRESS;Initial Catalog=desafio_autoglass;Integrated Security=True;Encrypt=False";
            return strconn;
        }
    }
}

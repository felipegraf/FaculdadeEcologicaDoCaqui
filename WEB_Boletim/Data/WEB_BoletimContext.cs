using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WEB_Boletim.Models;

namespace WEB_Boletim.Data
{
    public class WEB_BoletimContext : DbContext
    {
        public WEB_BoletimContext (DbContextOptions<WEB_BoletimContext> options)
            : base(options)
        {
        }

        public DbSet<WEB_Boletim.Models.Aluno> Aluno { get; set; }

        public DbSet<WEB_Boletim.Models.Curso> Curso { get; set; }

        public DbSet<WEB_Boletim.Models.Materia> Materia { get; set; }

        public DbSet<WEB_Boletim.Models.Nota> Nota { get; set; }

        public DbSet<WEB_Boletim.Models.Situacao> Situacao { get; set; }
    }
}

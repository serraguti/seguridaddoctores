using SeguridadDoctores.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeguridadDoctores.Repositories
{
    public class RepositoryDoctores
    {
        DoctoresContext context;

        public RepositoryDoctores(DoctoresContext context)
        {
            this.context = context;
        }
    }
}

using SeguridadDoctores.Data;
using SeguridadDoctores.Models;
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

        public List<Doctor> GetDoctoresEspecialidad(String especialidad)
        {
            return this.context.Doctores
                .Where(x => x.Especialidad == especialidad)
                .ToList();
        }

        public Doctor BuscarDoctor(int iddoctor)
        {
            return this.context.Doctores
                .SingleOrDefault(x => x.IdDoctor == iddoctor);
        }

        public Doctor ExisteDoctor(String apellido, int iddoctor)
        {
            var consulta = from datos in context.Doctores
                           where datos.Apellido == apellido
                           && datos.IdDoctor == iddoctor
                           select datos;
            return consulta.FirstOrDefault();
        }
    }
}

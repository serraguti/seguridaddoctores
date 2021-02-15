using Microsoft.AspNetCore.Mvc;
using SeguridadDoctores.Filters;
using SeguridadDoctores.Models;
using SeguridadDoctores.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SeguridadDoctores.Controllers
{
    [AuthorizeDoctores]
    public class DoctoresController : Controller
    {
        RepositoryDoctores repo;

        public DoctoresController(RepositoryDoctores repo)
        {
            this.repo = repo;
        }

        public IActionResult Perfil()
        {
            String dato =
                User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();
            int iddoctor = int.Parse(dato);
            Doctor doc = this.repo.BuscarDoctor(iddoctor);
            return View(doc);
        }

        public IActionResult DoctoresEspecialidad()
        {
            String dato =
                User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();
            int iddoctor = int.Parse(dato);
            Doctor doc = this.repo.BuscarDoctor(iddoctor);
            List<Doctor> doctores = this.repo.GetDoctoresEspecialidad(doc.Especialidad);
            return View(doctores);
        }
    }
}

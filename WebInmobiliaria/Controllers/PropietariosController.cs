using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebInmobiliaria.Models;

namespace WebInmobiliaria.Controllers
{
    public class PropietariosController : Controller
    {
        private readonly ILogger<PropietariosController> _logger;
        private readonly RepositorioPropietario repository;

        public PropietariosController(ILogger<PropietariosController> logger)
        {
            _logger = logger;
            repository = new RepositorioPropietario();
        }

        public IActionResult Index()
        {
            var lista = repository.ObtenerPropietarios();
            return View(lista);
        }
        public IActionResult Edicion(int id)
        {
            if (id == 0)
            {
                // Caso crear nuevo propietario
                return View(new Propietarios());
            }
            else
            {
                // Caso editar propietario existente
                var propietario = repository.Obtener(id);

                if (propietario == null)
                {
                    return NotFound(); // Si no existe el ID en la BD
                }

                return View(propietario);
            }
        }

[HttpPost]
public IActionResult Guardar(Propietarios propietario)
{
    if (propietario.IdPropietario == 0)
    {
        repository.Alta(propietario);
    }
    else
    {
        repository.Modificar(propietario);
    }
    return RedirectToAction(nameof(Index));
}

public IActionResult Eliminar(int id)
{
        repository.Baja(id); // Elimina el registro
        return RedirectToAction(nameof(Index));
}


    }
}

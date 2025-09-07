using Microsoft.AspNetCore.Mvc;
using WebInmobiliaria.Models;

namespace WebInmobiliaria.Controllers
{
    public class InquilinosController : Controller
    {
        private readonly ILogger<InquilinosController> _logger;
        private readonly RepositorioInquilino repository;

        public InquilinosController(ILogger<InquilinosController> logger)
        {
            _logger = logger;
            repository = new RepositorioInquilino();
        }

        // Listar todos los inquilinos
        public IActionResult Index()
        {
            var lista = repository.ObtenerInquilinos();
            return View(lista);
        }

        // Crear o editar inquilino
        public IActionResult Edicion(int id)
        {
            if (id == 0)
            {
                // Crear nuevo inquilino
                return View(new Inquilinos());
            }
            else
            {
                // Editar inquilino existente
                var inquilino = repository.Obtener(id);

                if (inquilino == null)
                {
                    return NotFound(); // ID no existe
                }

                return View(inquilino);
            }
        }

        [HttpPost]
        public IActionResult Guardar(Inquilinos inquilino)
        {
            if (inquilino.Id_Inquilinos == 0)
            {
                repository.Alta(inquilino); // Crear
            }
            else
            {
                repository.Modificar(inquilino); // Modificar
            }
            return RedirectToAction(nameof(Index));
        }

        // Eliminar inquilino
        public IActionResult Eliminar(int id)
        {
            repository.Baja(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

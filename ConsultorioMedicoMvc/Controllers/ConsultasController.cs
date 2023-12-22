using Microsoft.AspNetCore.Mvc;

namespace ConsultorioMedicoMvc.Controllers
{
    public class ConsultasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using ConsultorioMedicoMvc.Contexts;
using ConsultorioMedicoMvc.Models.Entities;
using ConsultorioMedicoMvc.ViewModels.Medicos;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace ConsultorioMedicoMvc.Controllers
{
    public class MedicoController : Controller
    {
        private readonly SisMedContext _context;
        private readonly IValidator<AdicionarMedicoViewModel> _adicionarMedicoValidator;
        private readonly IValidator<EditarMedicoViewModel> _editarMedicoValidator;
        private const int tamanhoPagina = 4;

        public MedicoController(SisMedContext context,
            IValidator<AdicionarMedicoViewModel> adicionarMedicoValidator,
            IValidator<EditarMedicoViewModel> editarMedicoValidator)
        {
            _context = context;
            _adicionarMedicoValidator = adicionarMedicoValidator;
            _editarMedicoValidator = editarMedicoValidator;
        }

        public IActionResult Index(string filtro, int pagina = 1)
        {
            var medicos = _context.Medicos.Select(x => new ListarMedicoViewModel
            { Id = x.Id, Nome = x.Nome, CRM = x.CRM}) 
            .Where(x => x.Nome.Contains(filtro) || x.CRM.Contains(filtro));

            ViewBag.Filtro = filtro;
            ViewBag.NumeroPagina = pagina;
            ViewBag.TotalPaginas = Math.Ceiling((decimal)medicos.Count() / tamanhoPagina);
            
            return View(medicos.Skip((pagina - 1) * tamanhoPagina).Take(tamanhoPagina));
        }

        #region Cadastrar

        public IActionResult Adicionar()
        {
            return View(); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Adicionar(AdicionarMedicoViewModel dados)
        {
            var validacao =_adicionarMedicoValidator.Validate(dados);
            if(!validacao.IsValid)
            {
                validacao.AddToModelState(ModelState,string.Empty);
                return View(dados);
            }
            var medico = new Medico
            {
                CRM = dados.CRM,
                Nome = dados.Nome
            };

            _context.Medicos.Add(medico);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Editar

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var medico = _context.Medicos.Find(id);

            if(medico != null) 
            {
                return View(new EditarMedicoViewModel
                {
                    CRM = medico.CRM,
                    Id = medico.Id,
                    Nome = medico.Nome

                });
            }
            return NotFound();
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(int id,EditarMedicoViewModel dados)
        {
            var validacao = _editarMedicoValidator.Validate(dados);

            if (!validacao.IsValid)
            {
                validacao.AddToModelState(ModelState, string.Empty);
                return View(dados);
            }

            var medico = _context.Medicos.Find(id);
            if (medico != null)
            {
                medico.CRM = dados.CRM;
                medico.Nome = dados.Nome;

                _context.Medicos.Update(medico);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }
        #endregion


        public IActionResult Excluir(int id) 
        {
            var medico = _context.Medicos.Find(id);

            if (medico != null)
            {
                return View(new ListarMedicoViewModel
                {
                    CRM = medico.CRM,
                    Id = medico.Id,
                    Nome = medico.Nome

                });
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Excluir(int id, EditarMedicoViewModel dados)
        {

            var medico = _context.Medicos.Find(id);
            if (medico != null)
            {
                _context.Medicos.Remove(medico);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }
    }
}

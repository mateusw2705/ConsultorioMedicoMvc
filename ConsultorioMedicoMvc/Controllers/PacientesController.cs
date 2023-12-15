using ConsultorioMedicoMvc.Contexts;
using ConsultorioMedicoMvc.Models.Entities;
using ConsultorioMedicoMvc.Validators.Medicos;
using ConsultorioMedicoMvc.ViewModels.Medicos;
using ConsultorioMedicoMvc.ViewModels.Pacientes;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace ConsultorioMedicoMvc.Controllers
{
    public class PacientesController : Controller
    {
        private readonly SisMedContext _context;
        private readonly IValidator<AdicionarPacienteViewModel> _adicionarPacienteValidator;
        private readonly IValidator<EditarPacienteViewModel> _editarPacienteValidator;

        public PacientesController(SisMedContext context, IValidator<AdicionarPacienteViewModel> adicionarPacienteValidator, IValidator<EditarPacienteViewModel> editarPacienteValidator)
        {
            _context = context;
            _adicionarPacienteValidator = adicionarPacienteValidator;
            _editarPacienteValidator = editarPacienteValidator;
        }

        int tamanhoPagina = 4;
        public IActionResult Index(string filtro, int pagina = 1)
        {
            var medicos = _context.Pacientes.Select(x => new ListarPacienteViewModel
            { Id = x.Id, Nome = x.Nome, CPF = x.CPF })
            .Where(x => x.Nome.Contains(filtro) || x.CPF.Contains(filtro));

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
        public IActionResult Adicionar(AdicionarPacienteViewModel dados)
        {
            var validacao = _adicionarPacienteValidator.Validate(dados);
            if (!validacao.IsValid)
            {
                validacao.AddToModelState(ModelState, string.Empty);
                return View(dados);
            }

            var paciente = new Paciente
            {
                CPF = Regex.Replace(dados.CPF, "[^0-9]", ""),
                Nome = dados.Nome,
                DataNascimento =  dados.DataNascimento
            };

            _context.Pacientes.Add(paciente);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Editar

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var paciente = _context.Pacientes.Find(id);

            if (paciente != null)
            {
                return View(new EditarPacienteViewModel
                {
                    CPF = paciente.CPF,
                    Id = paciente.Id,
                    Nome = paciente.Nome,
                    DataNascimento = paciente.DataNascimento

                });
            }
            return NotFound();

        }

        [HttpPost]
        public IActionResult Editar(EditarPacienteViewModel dados, int id)
        {
            var validacao = _editarPacienteValidator.Validate(dados);

            if (!validacao.IsValid)
            {
                validacao.AddToModelState(ModelState, string.Empty);
                return View(dados);
            }

            var paciente = _context.Pacientes.Find(id);
            if (paciente != null)
            {
                paciente.CPF = Regex.Replace(dados.CPF, "[^0-9 ]", "");
                paciente.Nome = dados.Nome;
                paciente.DataNascimento = dados.DataNascimento;

                _context.Pacientes.Update(paciente);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return NotFound();

        }

        #endregion

        public IActionResult Excluir(int id)
        {
            var paciente = _context.Pacientes.Find(id);

            if (paciente != null)
            {
                return View(new EditarPacienteViewModel
                {
                    CPF = paciente.CPF,
                    Id = paciente.Id,
                    Nome = paciente.Nome,
                    DataNascimento = paciente.DataNascimento

                });
            }
            return NotFound();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Excluir(int id, EditarPacienteViewModel dados)
        {

            var pacientes = _context.Pacientes.Find(id);
            if (pacientes != null)
            {
                _context.Pacientes.Remove(pacientes);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }
    }
}

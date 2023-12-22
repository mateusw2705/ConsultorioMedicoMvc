using ConsultorioMedicoMvc.Contexts;
using ConsultorioMedicoMvc.Models.Entities;
using ConsultorioMedicoMvc.Validators.Medicos;
using ConsultorioMedicoMvc.Validators.MonitoramentoPaciente;
using ConsultorioMedicoMvc.ViewModels.MonitoramentoPaciente;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace ConsultorioMedicoMvc.Controllers
{
    [Route("Monitoramento")]
    public class MonitoramentoPacienteController : Controller
    {
        private readonly IValidator<AdicionarMonitoramentoViewModel> _adicionarMonitoramentoValidator;
        private readonly IValidator<EditarMonitoramentoViewModel> _editarMonitoramentoValidator;
        private readonly SisMedContext _context;

        public MonitoramentoPacienteController(SisMedContext context, IValidator<AdicionarMonitoramentoViewModel> adicionarMonitoramentoValidator, IValidator<EditarMonitoramentoViewModel> editarMonitoramentoValidator)
        {
            _context = context;
            _adicionarMonitoramentoValidator = adicionarMonitoramentoValidator;
            _editarMonitoramentoValidator = editarMonitoramentoValidator;
        }

        public IActionResult Index(int idPaciente)
        {
            ViewBag.IdPaciente = idPaciente;
            var monitoramentos =_context.MonitoramentoPacientes.Where(x=> x.IdPaciente == idPaciente)
                                                               .Select(x=> new ListarMonitoramentoViewModel 
                                                               {
                                                                   Id = x.Id,
                                                                   PressaoArterial = x.PressaoArterial,
                                                                   SaturacaoOxigenio = x.SaturacaoOxigenio,
                                                                   DataAfericao = x.DataAfericao,
                                                                   FrequenciaCardiaca = x.FrequenciaCardiaca,
                                                                   Temperatura = x.Temperatura
                                                               });
            return View(monitoramentos);
        }

        [Route("Adicionar")]
        public IActionResult Adicionar(int idPaciente)
        {
            ViewBag.IdPaciente = idPaciente;
            return View();
        }

        [Route("Adicionar")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Adicionar(int idPaciente, AdicionarMonitoramentoViewModel dados)
        {
            
            ViewBag.IdPaciente = idPaciente;
            var validacao = _adicionarMonitoramentoValidator.Validate(dados);

            if (!validacao.IsValid)
            {
                validacao.AddToModelState(ModelState, string.Empty);
                return View(dados);
            }

            var monitoramento = new MonitoramentoPaciente
            {
                PressaoArterial = dados.PressaoArterial,
                SaturacaoOxigenio = dados.SaturacaoOxigenio,
                Temperatura = dados.Temperatura,
                FrequenciaCardiaca = dados.FrequenciaCardiaca,
                DataAfericao = dados.DataAfericao,
                IdPaciente = 1
            };

            _context.MonitoramentoPacientes.Add(monitoramento);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index), new { idPaciente });
        }

        [Route("Editar/{id}")]
        public IActionResult Editar(int id) 
        {
            var monitoramento = _context.MonitoramentoPacientes.Find(id);

            if (monitoramento != null)
            {
                  return View(new EditarMonitoramentoViewModel 
                  {
                    Id = monitoramento.Id,
                    DataAfericao = monitoramento.DataAfericao,
                    FrequenciaCardiaca = monitoramento.FrequenciaCardiaca,
                    PressaoArterial = monitoramento.PressaoArterial,
                    SaturacaoOxigenio = monitoramento.SaturacaoOxigenio,
                    Temperatura = monitoramento.Temperatura
                  });
            }
            return NotFound();
        }


        [Route("Editar/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(int id, EditarMonitoramentoViewModel dados)
        {
            var validacao = _editarMonitoramentoValidator.Validate(dados);

            if (!validacao.IsValid)
            {
                validacao.AddToModelState(ModelState, string.Empty);
                return View(dados);
            }

            var monitoramento = _context.MonitoramentoPacientes.Find(id);
            if (monitoramento != null)
            {
                monitoramento.DataAfericao = dados.DataAfericao;
                monitoramento.FrequenciaCardiaca = dados.FrequenciaCardiaca;
                monitoramento.SaturacaoOxigenio = dados.SaturacaoOxigenio;
                monitoramento.Temperatura = dados.Temperatura;
                monitoramento.PressaoArterial = dados.PressaoArterial;
                
                    

                _context.MonitoramentoPacientes.Update(monitoramento);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index), new {monitoramento.IdPaciente});
            }
            return NotFound();

        }


        [Route("Excluir/{id}")]
        public IActionResult Excluir(int id)
        {
            var monitoramento = _context.MonitoramentoPacientes.Find(id);

            if (monitoramento != null)
            {
                return View(new ListarMonitoramentoViewModel
                {
                    Id = monitoramento.Id,
                    PressaoArterial = monitoramento.PressaoArterial,
                    SaturacaoOxigenio = monitoramento.SaturacaoOxigenio,
                    Temperatura = monitoramento.Temperatura,
                    FrequenciaCardiaca = monitoramento.FrequenciaCardiaca,
                    DataAfericao = monitoramento.DataAfericao
                });
            }

            return NotFound();
        }

        [Route("Excluir/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Excluir(int id, ListarMonitoramentoViewModel dados)
        {
            var monitoramento = _context.MonitoramentoPacientes.Find(id);

            if (monitoramento != null)
            {
                _context.MonitoramentoPacientes.Remove(monitoramento);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index), new { monitoramento.IdPaciente });
            }

            return NotFound();
        }
    }


}

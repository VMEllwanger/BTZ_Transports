using AutoMapper;
using BTZ_Transports.API.ViewModels;
using BTZ_Transports.Negocio.Interfaces;
using BTZ_Transports.Negocio.Models;
using Microsoft.AspNetCore.Mvc;

namespace BTZ_Transports.API.Controllers
{
    [Route("[controller]")]
    public class RegistroAbastecimentoController : MainController
    {
        private readonly IRegistroAbastecimentoService _registroAbastecimentoService;
        private readonly IMotoristaService _motoristaService;
        private readonly IVeiculoService _veiculoService;
        private readonly IMapper _mapper;
        public RegistroAbastecimentoController(INotificador notificador, IVeiculoService veiculoRepository, IMotoristaService motoristaService, IRegistroAbastecimentoService RegistroAbastecimentoRepository, IMapper mapper) : base(notificador)
        {
            _registroAbastecimentoService = RegistroAbastecimentoRepository;
            _mapper = mapper;
            _motoristaService = motoristaService;
            _veiculoService = veiculoRepository;
        }

        public async Task<IActionResult> Index()
        {
            var motoristas = _mapper.Map<IEnumerable<RegistroAbastecimentoViewModel>>(await _registroAbastecimentoService.ObterRegistroAbastecimentos());
            return motoristas.Any() ?
                          View("Index", motoristas.ToList()) :
                           View("Index", Enumerable.Empty<RegistroAbastecimentoViewModel>());
        }

        [Route("criar-novo")]
        public async Task<IActionResult> Criar()
        {
            ViewBag.Motoristas = _mapper.Map<IEnumerable<MotoristaViewModel>>(await _motoristaService.ObterMotoristas()).ToList();
            ViewBag.Veiculo = _mapper.Map<IEnumerable<VeiculoViewModel>>(await _veiculoService.ObterVeiculos()).ToList();
            return View("Criar");
        }

        [HttpPost("criar-registro-abastecimento")]
        public async Task<IActionResult> Adicionar([FromForm] RegistroAbastecimentoViewModel veiculoViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Criar", veiculoViewModel);
            }
            else
            {
                await _registroAbastecimentoService.Adicionar(_mapper.Map<RegistroAbastecimento>(veiculoViewModel));
                return RedirectToAction(nameof(Index));
            }
        }

        [Route("editar-motorista/{id}")]
        public async Task<ActionResult<MotoristaViewModel>> Editar(Guid id)
        {
            var motoristaViewModel = await _motoristaService.ObterPorId(id);
            if (id == null || motoristaViewModel == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<MotoristaViewModel>(motoristaViewModel));
        }

        [HttpPost("editar-motorista/{id:guid}")]
        public async Task<IActionResult> Editar(Guid id, [FromForm] MotoristaViewModel MotoristaViewModel)
        {
            if (id != MotoristaViewModel.Id)
            {
                NotificarErro("O id informado não corresponde o mesmo que foi passado na query");
                return CustomResponse(MotoristaViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _motoristaService.Atualizar(_mapper.Map<Motorista>(MotoristaViewModel));

            return RedirectToAction(nameof(Index));
        }
    }
}

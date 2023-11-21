using AutoMapper;
using BTZ_Transports.API.ViewModels;
using BTZ_Transports.Negocio.Interfaces;
using BTZ_Transports.Negocio.Models;
using Microsoft.AspNetCore.Mvc;

namespace BTZ_Transports.API.Controllers
{
    [Route("[controller]")]
    public class MotoristasController : MainController
    {
        private readonly IMotoristaService _motoristaService;
        private readonly IMapper _mapper;
        public MotoristasController(INotificador notificador, IMotoristaService motoristaService, IMapper mapper) : base(notificador)
        {
            _motoristaService = motoristaService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var motoristas = _mapper.Map<IEnumerable<MotoristaViewModel>>(await _motoristaService.ObterMotoristas());
            return motoristas.Any() ?
                          View("Index", motoristas.ToList()) :
                           View("Index", Enumerable.Empty<MotoristaViewModel>());
        }

        [Route("criar-novo")]
        public IActionResult Criar()
        {
            return View("Criar");
        }

        [HttpPost("criar-motorista")]
        public async Task<IActionResult> Adicionar([FromForm] MotoristaViewModel veiculoViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Criar", veiculoViewModel);
            }
            else
            {
                await _motoristaService.Adicionar(_mapper.Map<Motorista>(veiculoViewModel));
                return RedirectToAction(nameof(Index));
            };
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

        [Route("excluir/{id}")] 
        public async Task<ActionResult<MotoristaViewModel>> Remover(Guid id)
        {
            var veiculoViewModel = await _motoristaService.ObterPorId(id);
            if (id == null || veiculoViewModel == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<MotoristaViewModel>(veiculoViewModel));
        }

        [HttpPost("{id}"), ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExcluirConfirmado(Guid id)
        {
            var veiculo = await _motoristaService.ObterPorId(id);
            if (veiculo == null) return NotFound();

            await _motoristaService.Remover(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
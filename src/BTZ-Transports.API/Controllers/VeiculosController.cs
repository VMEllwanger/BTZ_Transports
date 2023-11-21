using AutoMapper;
using BTZ_Transports.API.ViewModels;
using BTZ_Transports.Negocio.Interfaces;
using BTZ_Transports.Negocio.Models;
using Microsoft.AspNetCore.Mvc;

namespace BTZ_Transports.API.Controllers
{
    [Route("[controller]")]
    public class VeiculosController : MainController
    {
        private readonly IVeiculoService _veiculoService;
        private readonly IMapper _mapper;
        public VeiculosController(INotificador notificador, IVeiculoService veiculoRepository, IMapper mapper) : base(notificador)
        {
            _veiculoService = veiculoRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var veiculos = _mapper.Map<IEnumerable<VeiculoViewModel>>(await _veiculoService.ObterVeiculos());
            return veiculos.Any() ?
                          View("Index", veiculos.ToList()) :
                           View("Index", Enumerable.Empty<VeiculoViewModel>());
        }
         

        [Route("criar-novo")]
        public IActionResult Criar()
        {
            return View("Criar");
        }

        [HttpPost("criar-veiculo")]
        [HttpPost]
        public async Task<ActionResult<VeiculoViewModel>> Adicionar([FromForm] VeiculoViewModel veiculoViewModel)
        {
            if ( !ModelState.IsValid) return View("Criar", veiculoViewModel);

            await _veiculoService.Adicionar(_mapper.Map<Veiculo>(veiculoViewModel));
             
            return RedirectToAction(nameof(Index));
        }


        [Route("editar-veiculo/{id}")]
        public async Task<ActionResult<VeiculoViewModel>> Editar(Guid id)
        {
            var veiculoViewModel = await ObterPorId(id);
            if (id == null || veiculoViewModel == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<VeiculoViewModel>(veiculoViewModel));

        }

        [HttpPost("editar-veiculo/{id:guid}")]
        public async Task<ActionResult<VeiculoViewModel>> Editar(Guid id, [FromForm] VeiculoViewModel veiculoViewModel)
        {
            if (id != veiculoViewModel.Id)
            {
                NotificarErro("O id informado não corresponde o mesmo que foi passado na query");
                return CustomResponse(veiculoViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _veiculoService.Atualizar(_mapper.Map<Veiculo>(veiculoViewModel));
            return RedirectToAction(nameof(Index));
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<VeiculoViewModel>> Remover(Guid id)
        {
            var veiculo = await _veiculoService.ObterPorId(id);
            if (veiculo == null) return NotFound();

            await _veiculoService.Remover(id);

            return CustomResponse(veiculo);
        }

        private async Task<VeiculoViewModel> ObterPorId(Guid id)
        {
            var veiculoViewModel = await _veiculoService.ObterPorId(id);
            if (veiculoViewModel == null) return null;

            return _mapper.Map<VeiculoViewModel>(veiculoViewModel);
        }
    }
}

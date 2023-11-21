using AutoMapper;
using BTZ_Transports.API.ViewModels;
using BTZ_Transports.Negocio.Models;

namespace BTZ_Transports.API.Configuracoes
{
    public class AutomapperConfiguracoes : Profile
    {
        public AutomapperConfiguracoes()
        {
            CreateMap<Veiculo, VeiculoViewModel>().ReverseMap();
            CreateMap<Motorista, MotoristaViewModel>().ReverseMap();
            CreateMap<RegistroAbastecimento, RegistroAbastecimentoViewModel>().ReverseMap();
        }
    }
}
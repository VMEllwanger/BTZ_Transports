using BTZ_Transports.Negocio.Notificacoes;

namespace BTZ_Transports.Negocio.Interfaces
{
    public interface INotificador 
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}

using RadarGov.Dominio.Notificacoes.Entidades;
using System.Text;

namespace RadarGov.Dominio.Notificacoes.Servicos
{
    /// <summary>
    /// Serviço responsável por gerenciar mensagens de notificação durante a execução da aplicação.
    /// </summary>
    public class MensagemServico
    {
        private readonly List<Mensagem> _mensagens = new();

        /// <summary>
        /// Adiciona uma nova mensagem à lista de notificações.
        /// </summary>
        public void Adicionar(string texto, TipoMensagem tipo = TipoMensagem.Informacao)
        {
            if (!string.IsNullOrWhiteSpace(texto))
                _mensagens.Add(new Mensagem(texto, tipo));
        }

        /// <summary>
        /// Retorna verdadeiro se existir qualquer mensagem registrada.
        /// </summary>
        public bool PossuiMensagens() 
        { 
            return _mensagens.Any(); 
        }

        /// <summary>
        /// Retorna verdadeiro se existir ao menos uma mensagem de erro registrada.
        /// </summary>
        public bool PossuiErros() 
        {
            return _mensagens.Any(m => m.Tipo == TipoMensagem.Erro);
        }

        /// <summary>
        /// Retorna todas as mensagens registradas.
        /// </summary>
        public IReadOnlyCollection<Mensagem> ObterMensagens()
        {
            return _mensagens.AsReadOnly();
        }

        /// <summary>
        /// Retorna apenas as mensagens do tipo erro.
        /// </summary>
        public IEnumerable<Mensagem> ObterErros()
        {
            return _mensagens.Where(m => m.Tipo == TipoMensagem.Erro);
        }

        /// <summary>
        /// Limpa todas as mensagens registradas.
        /// </summary>
        public void Limpar() 
        {
            _mensagens.Clear();
        }

        /// <summary>
        /// Retorna todas as mensagens formatadas em uma única string.
        /// </summary>
        public override string ToString()
        {
            if (!_mensagens.Any())
                return "Nenhuma mensagem registrada.";

            var builder = new StringBuilder();
            foreach (var msg in _mensagens)
                builder.AppendLine($"[{msg.Tipo}] {msg.Texto}");

            return builder.ToString().TrimEnd();
        }
    }
}

namespace GrupoSelect.Web.Views.Shared.Components.Models
{
    public class ChartMoney
    {
        /// <summary>
        /// Id usado para diferencias varios charts na mesma tela.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Título do Chart
        /// </summary>
        public string Titulo { get; set; }

        /// <summary>
        /// Array de valores dos dados
        /// </summary>
        public decimal[] Valores { get; set; }

        /// <summary>
        /// Array de Textos dos dados
        /// </summary>
        public string[] Textos { get; set; }

        /// <summary>
        /// Array de cores dos dados
        /// </summary>
        public string[] Cores { get; set; }

        /// <summary>
        /// Cor de fundo. Por padrão é #FFF.
        /// </summary>
        public string BackgroundColor { get; set; }

        /// <summary>
        /// Boolean que permite ou bloqueia minimizar
        /// </summary>
        public bool PermiteMinimizar { get; set; }

        /// <summary>
        /// Tipo do Chart. Use o UtilWebEnums.TipoChart para escolher.
        /// </summary>
        public string TipoChart { get; set; }

        /// <summary>
        /// Controller usada para atualizar o componente. Caso deixe em branco o componente não será atualizado.
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        /// Action usada para atualizar o componente. Caso deixe em branco o componente não será atualizado.
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// Valor definido em milisegundos. Ex: 6000 = 6 segundos
        /// </summary>
        public int IntervaloAtualizacao { get; set; }
    }    
}
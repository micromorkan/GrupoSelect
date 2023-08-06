namespace GrupoSelect.Web.Views.Shared.Components.Models
{
    public class Tile
    {
        /// <summary>
        /// Id usado para diferencias varios tile na mesma tela.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Título a ser exibido.
        /// </summary>
        public string Titulo { get; set; }

        /// <summary>
        /// Descrição a ser exibida.
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Valor a ser exibido.
        /// </summary>
        public string Valor { get; set; }

        /// <summary>
        /// Cor de fundo. Por padrão é #FFF.
        /// </summary>
        public string BackgroundColor { get; set; }

        /// <summary>
        /// Defina o ícone que sera usado no Tile. Ex: fa-user
        /// </summary>
        public string Icone { get; set; }
        
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


        /// <summary>
        /// Filtro usado para fornecer dados opcionais quando o Tile atualizar com o servidor.
        /// </summary>
        public object Filter { get; set; }
    }
}

using Classifica3._0.Model.Enum;
using System.ComponentModel.DataAnnotations;

namespace Classifica3._0.Model
{
    public class Card
    {
        public Card()
        {
            DataCardCreated = DateTime.Now;
        }
        public int CardId { get; set; }

        [MaxLength(80, ErrorMessage = " Titulo não pode exceder 80 caracteres")]
        public string Titulo { get; set; }

        [MaxLength(120, ErrorMessage = "Descricao não pode exceder 80 caracteres")]
        public string Descricao { get; set; }
        public EnumCard Situacao { get; set; }
        public DateTime DateCardCreated { get; set; }
        public DateTime? DateCardUpdated { get; set; }

        public async void Ativo()
        {
            Situacao = EnumCard.Ativo;    
        }
        public async void Inativo()
        {
            Situacao = EnumCard.Inativo;
        }
        public void DateUpdated()
        {
            DateCardUpdated = DateTime.Now;
        }
    }

}

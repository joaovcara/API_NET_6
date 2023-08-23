using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class Notify
    {
        [NotMapped]
        public string NameProperty { get; set; }

        [NotMapped]
        public string Message { get; set; }

        [NotMapped]
        public List<Notify> Notifies { get; set; }

        /// <summary>
        /// Inicializa classe com uma lista de notificações
        /// </summary>
        public Notify()
        {
            Notifies = new List<Notify>();
        }

        /// <summary>
        /// Valida propriedade string
        /// </summary>
        /// <param name="value">Valor da propriedade</param>
        /// <param name="nameProperty">Nome da propriedade</param>
        /// <returns>True ou false</returns>
        public bool ValidPropertyString(string value, string nameProperty)
        {
            if(string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(nameProperty))
            {
                Notifies.Add(new Notify { NameProperty = nameProperty, Message = "O campo " + nameProperty + " é obrigatório." });
                return false;
            }

            return true;
        }

        /// <summary>
        /// Valida propriedade int
        /// </summary>
        /// <param name="value">Valor da propriedade</param>
        /// <param name="nameProperty">Nome da propriedade</param>
        /// <returns>True ou false</returns>
        public bool ValidPropertyInt(int value, string nameProperty)
        {
            if (value < 1 || string.IsNullOrWhiteSpace(nameProperty))
            {
                Notifies.Add(new Notify { NameProperty = nameProperty, Message = "O campo " + nameProperty + " é obrigatório." });
                return false;
            }

            return true;
        }
    }
}

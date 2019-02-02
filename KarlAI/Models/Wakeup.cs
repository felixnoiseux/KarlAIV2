using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlAI.Models
{
    /// <summary>
    /// Permet de savoir si on appel le robot
    /// </summary>
    public class Wakeup
    {
        string wordBuilder = "";
        public string magicWord { get; set; }

        #region Fonctions
        /// <summary>
        /// Permet de savoir si on demande quelque chose a Karl
        /// </summary>
        /// <param name="content"></param>
        /// <returns>True si on a dit le magicWord</returns>
        public bool Ecouter(string content)
        {

            bool droitEspace = true;
            wordBuilder = "";
            for (int i = 0; i < content.Length; i++)
            {
                if (content[i] == ' ' && droitEspace)
                {
                    wordBuilder += " ";
                    droitEspace = false;
                    continue;
                }
                else if (content[i] == ' ' && !droitEspace)
                    break;


                wordBuilder += content[i];
            }

            if (wordBuilder.ToLower() == magicWord.ToLower())
                return true;
            else
                return false;
        }

        /// <summary>
        /// Date : 2 Fevrier 2019
        /// </summary>
        /// <param name="content">La phrase que l'utilisateur a entrer.</param>
        /// <returns>Les commandes pour que le bot puise les executers</returns>
        public string AvoirCommandes(string content)
        {
            wordBuilder = "";
            for (int i = magicWord.Length +1; i < content.Length; i++)
            {
                wordBuilder += content[i];
            }
            return wordBuilder;
        }
        #endregion


    }
}

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
        string contenu = "";
        public string magicWord { get; set; }

        #region Fonctions
        /// <summary>
        /// Permet de savoir si on demande quelque chose a Karl
        /// </summary>
        /// <param name="content"></param>
        /// <returns>True si on a dit le magicWord</returns>
        public bool Ecouter(string content)
        {
            if (content.Length < 8)
                return false;
            else if (!VerifierHey(content))
                return false;

            bool droitEspace = true;
            contenu = content;
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

            //Refaire une verification pour Hey Carl , Hey Caught , Hey Girl
            string carl = "";
            for (int i = 4; i <= 1000; i++)
            {
                if (contenu[i] == ' ')
                {
                    break;
                }
                carl += contenu[i];
                carl = carl.ToLower();
            }
            string name = "carl";
            int count = 0;
            for (int i = 0; i < 4; i++)
            {
                if (count == 2)
                {
                    wordBuilder = "hey carl";
                    break;
                }
                for (int i2 = 0; i2 < carl.Length ; i2++)
                {
                    if (name[i] == carl[i2])
                    {
                        count++;
                    }
                }
                if (count == 2)
                {
                    wordBuilder = "hey carl";
                    break;
                }
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
            for (int i = magicWord.Length + 1; i < content.Length; i++)
            {
                wordBuilder += content[i];
            }
            return wordBuilder;
        }
        private bool VerifierHey(string content)
        {
            string mot = "";
            for (int i = 0; i < content.Length; i++)
            {
                if (content[i] == ' ')
                    break;

                mot += content[i];
            }

            if (mot.ToLower() == "hey")
                return true;
            else
                return false;
        }
        #endregion


    }
}

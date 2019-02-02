using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using KarlAI.Models;

namespace KarlAI
{
    class Program
    {
        static void Main(string[] args)
        {
            Wakeup wakeup = new Wakeup();
            Bot bot = new Bot();
            string informations = "";
            string commandes = "";
            wakeup.magicWord = "Hey Karl";
           

            //START
            Console.WriteLine("Bonjour , bienvenue dans KarlAI \n" +
                                             "1.|Hey Karl| suivi de l'instruction \n" +
                                             "\n Exemple : Hey Karl Demarre google.com \n" +
                                             "2.|Help| Affiche les commandes que carl peut executer\n" +
                                             "3. |Exit| Quitte l'IA\n");
            //test
    
            while(true)
            {
                informations = Console.ReadLine();

                if (wakeup.Ecouter(informations))
                {
                    commandes = wakeup.AvoirCommandes(informations);
                    bot.GetMessage(commandes);
                    string result = bot.Identifier();
                    bot.Executer(result);
                }

                if (informations.ToLower() == "exit")
                    break;

            }
            Console.WriteLine("Merci d'avoir utiliser Karl AI , A la prochaine");
            Console.ReadLine();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using KarlAI.Models;
using System.Diagnostics;

namespace KarlAI
{
    class Program
    {
        string requete = "";
        public static async Task RecognizeSpeechAsync(Wakeup wakeup , Bot bot)
        {
            string informations = "";
            string commandes = "";
            string resultat = "";
            //wakeup.magicWord = "hey carl";
            // Creates an instance of a speech config with specified subscription key and service region.
            // Replace with your own subscription key and service region (e.g., "westus").
            var config = SpeechConfig.FromSubscription("feac160ac00d466b970fee69c0c513eb", "westus");

            // Creates a speech recognizer.
            using (var recognizer = new SpeechRecognizer(config))
            {
                Console.WriteLine("Say something...");

                // Performs recognition. RecognizeOnceAsync() returns when the first utterance has been recognized,
                // so it is suitable only for single shot recognition like command or query. For long-running
                // recognition, use StartContinuousRecognitionAsync() instead.
                var result = await recognizer.RecognizeOnceAsync();

                // Checks result.
                if (result.Reason == ResultReason.RecognizedSpeech)
                {
                    Console.WriteLine($"We recognized: {result.Text}");
                    informations = result.Text.ToLower();
                    if (wakeup.Ecouter(informations))
                    {
                        commandes = wakeup.AvoirCommandes(informations);
                        bot.GetMessage(commandes);
                         resultat = bot.Identifier();
                        bot.Executer(resultat);
                    }
                }
                else if (result.Reason == ResultReason.NoMatch)
                {
                    Console.WriteLine($"NOMATCH: Speech could not be recognized.");
                }
                else if (result.Reason == ResultReason.Canceled)
                {
                    var cancellation = CancellationDetails.FromResult(result);
                    Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

                    if (cancellation.Reason == CancellationReason.Error)
                    {
                        Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                        Console.WriteLine($"CANCELED: ErrorDetails={cancellation.ErrorDetails}");
                        Console.WriteLine($"CANCELED: Did you update the subscription info?");
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            Wakeup wakeup = new Wakeup();
            Bot bot = new Bot();
            wakeup.magicWord = "hey carl";


            Console.WriteLine("Please press a key to continue.");
            Console.ReadLine();

            ////START
            Console.WriteLine("Bonjour , bienvenue dans KarlAI \n" +
                                             "1.|Hey Karl| suivi de l'instruction \n" +
                                             "\n Exemple : Hey Karl Demarre google.com \n" +
                                             "2.|Help| Affiche les commandes que carl peut executer\n" +
                                             "3. |Exit| Quitte l'IA\n");


            while (true)
            {

                //RecognizeSpeechAsync(wakeup , bot).Wait();
                string informations = Console.ReadLine();
                if (wakeup.Ecouter(informations))
                {
                    string commandes = wakeup.AvoirCommandes(informations);
                    bot.GetMessage(commandes);
                    string resultat = bot.Identifier();
                    bot.Executer(resultat);
                }
                informations = Console.ReadLine();



                //if (informations.ToLower() == "exit")
                //    break;

            }
            Console.WriteLine("Merci d'avoir utiliser Karl AI , A la prochaine");
            Console.ReadLine();
        }
    }
}

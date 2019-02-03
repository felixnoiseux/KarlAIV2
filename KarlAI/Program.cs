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
                        if (commandes == "")
                        {
                            commandes = wakeup.AffirmationPrecedente;
                            wakeup.AffirmationPrecedente = "";
                        }
                        bot.GetMessage(commandes);
                         resultat = bot.Identifier();
                        bot.Executer(resultat, wakeup);
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
            Console.WriteLine("Welcome on KarlAI \n" +
                                             "1.|Hey Karl| followed by the instruction \n" +
                                             "\n\t Exemple : Hey Karl open google.com \n" +
                                             "\t\t : Hey Karl open spotify");


            while (true)
            {
                RecognizeSpeechAsync(wakeup , bot).Wait();
                //Code pour essayer Karl en mode console
                #region CodeConsole
                //string informations = Console.ReadLine();
                //if (wakeup.Ecouter(informations))
                //{
                //    string commandes = wakeup.AvoirCommandes(informations);
                //    if (commandes == "")
                //    {
                //        commandes = wakeup.AffirmationPrecedente;
                //        wakeup.AffirmationPrecedente = "";
                //    }
                //    bot.GetMessage(commandes);
                //    string resultat = bot.Identifier();
                //    bot.Executer(resultat , wakeup);
                //}



                //if (informations.ToLower() == "exit")
                //    break;
                #endregion
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Speech.Synthesis;

namespace KarlAI.Models
{
    /// <summary>
    /// Auteur: David Daoud
    /// Description: Permet de recevoir un message et de retourner les mots importants pour ensuite faire une action.
    /// Date: 2019-02-02
    /// </summary>
    public class Bot
    {
        //
        // Variables
        //
        SpeechSynthesizer synth = new SpeechSynthesizer();

        public string message { get; set; }
        public string Bibliotheque { get; set; }
        private string OrdreChoisi { get; set; }

        List<string> Ordre = new List<string> { "dit", "fait", "peux-tu", "pourrais-tu", "mets", "demarre", "démare", "open", "open,", "start", "search", "install" };
        List<string> useless = new List<string> { "le", "la", "les", "nous", "mon", "mon", "ma", "mes", "nos", "on" };


        //
        // Méthodes
        //
        public void Talk(string tts)
        {
            // Configure the audio output.   
            synth.SetOutputToDefaultAudioDevice();
            // Speak a string.  
            Console.WriteLine(tts);
            synth.Speak(tts);
            Console.WriteLine();
        }
        public void GetMessage(string texte)
        {
            string messageSanspoint = "";
            int count = 0;
            count = texte.Length;
            if (texte[count - 1] == '.')
            {
                for (int i = 0; i < texte.Length - 1; i++)
                {
                    messageSanspoint += texte[i];
                }
                message = messageSanspoint;
            }
            else if (texte[count - 1] == '?')
            {
                Bibliotheque = "Question";
                message = texte;
            }

        }

        public string Identifier()
        {
            string bibliochoisi = "";
            string messagefinal = "";
            bool next = false;
            string[] messagecut = message.Split(' ');
            for (int i = 0; i < messagecut.Length - 1; i++)
            {
                next = false;
                if (bibliochoisi == "")
                {

                    for (int i2 = 0; i2 < Ordre.Count; i2++)
                    {
                        if (messagecut[i].ToLower() == Ordre[i2])
                        {
                            bibliochoisi = "Ordre";
                            OrdreChoisi = Ordre[i2];
                            next = true;
                            continue;
                        }
                    }
                }
                if (Bibliotheque == "Question")
                {
                    bibliochoisi = "Question";
                }
                if (bibliochoisi != "" && next == false)
                {
                    for (int i2 = 0; i2 < useless.Count - 1; i2++)
                    {
                        if (messagecut[i] == useless[i2])
                        {
                            messagecut[i] = "";
                            continue;
                        }
                    }
                }
            }

            for (int i = 0; i <= messagecut.Length - 1; i++)
            {
                if (messagecut[i] != "")
                {
                    messagefinal += messagecut[i] + " ";
                }
            }
            Bibliotheque = bibliochoisi;
            return messagefinal;
        }

        // 
        // Comprendre le string et faire une action 
        //

        public void Executer(string demande, Wakeup wakeup)
        {

            string username = Environment.UserName;
            string[] demandecut = demande.Split(' ');
            if (Bibliotheque == "Ordre")
            {
                for (int i = 0; i <= demandecut.Length - 1; i++)
                {
                    if (demandecut[i].ToLower() == "spotify")
                    {
                        bool installSpotify = false;
                        //Verifie si on repond a la reponse d'installation
                        if (demandecut[i - 1] == "install")
                        {
                            Process.Start("https://www.spotify.com/ca-fr/download/windows/");
                            wakeup.EnAttenteDuneReponse = false;
                            break;
                        }

                        try
                        {
                            Process.Start("C:\\Users\\" + username + "\\AppData\\Roaming\\Spotify\\Spotify.exe");
                        }
                        catch (System.ComponentModel.Win32Exception)
                        {
                            installSpotify = true;
                        }
                        if(installSpotify)
                        {
                            Talk("You don't have spotify. Do you want to install it ? ");
                            wakeup.EnAttenteDuneReponse = true;
                            wakeup.AffirmationPrecedente = "hey carl install spotify.";
                            installSpotify = false;
                            break;
                        }
                    }
                    if (demandecut[i].ToLower() == "google")
                    {
                        //Verifier l'ordre qu'on a donner pour demarrer google.
                        if (OrdreChoisi == "search")
                            if (demandecut[i + 1] == "")
                            {
                                Console.WriteLine("You have to specify what to search.");
                                Talk("You have to specify what to search. ");
                                break;
                            }
                            else
                                Process.Start("https://www.google.com/search?q=" + demandecut[i + 1]);
                        else
                            Process.Start("http://www.google.com");

                        break;
                    }
                    if(demandecut[i].ToLower() =="porn")
                    {
                        Process.Start("http://www.pornhub.com");
                    }
                }
            }
            else if (Bibliotheque == "Question")
            {
                for (int i = 0; i <= demandecut.Length - 1; i++)
                {
                    if (demandecut[i].ToLower() == "temps")
                    {
                        Talk("This is what I found on the net");
                        Process.Start("https://www.theweathernetwork.com");
                    }
                    else if (demandecut[i].ToLower() == "who")
                    {
                        for (int i2 = 0; i2 < demandecut.Length - 1; i2++)
                        {
                            if (demandecut[i2].ToLower() == "are")
                            {
                                for (int i3 = 0; i3 < demandecut.Length; i3++)
                                {
                                    if (demandecut[i3].ToLower() == "you" || demandecut[i3].ToLower() == "you?")
                                    {
                                        Talk("I am Carl, an Artificial Intelligence designed by David Daoud and Félix Noiseux, two students studying in computer science at Saint-Hyacinthe.");
                                    }
                                }
                            }
                        }
                    }
                    else if (true)
                    {

                    }
                }
                Bibliotheque = "";
            }


        }

        private void VeuxTuExecuterSpotify(Wakeup wakeup)
        {
            Talk("You don't have spotify. Do you want to install it ? ");
            wakeup.EnAttenteDuneReponse = true;
            wakeup.AffirmationPrecedente = "hey carl install spotify.";
            return;
        }
    }
}
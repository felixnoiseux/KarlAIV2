using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

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

        public string message { get; set; }
        public string Bibliotheque { get; set; }

        List<string> Ordre = new List<string> { "dit", "fait", "peux-tu", "pourrais-tu", "mets", "demarre", "démare", "open", "open,", "start" };
        List<string> useless = new List<string> { "le", "la", "les", "nous", "mon", "mon", "ma", "mes", "nos" };


        //
        // Méthodes
        //

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

                    for (int i2 = 0; i2 < Ordre.Count - 1; i2++)
                    {
                        if (messagecut[i].ToLower() == Ordre[i2])
                        {
                            bibliochoisi = "Ordre";
                            next = true;
                            continue;
                        }
                    }
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

        public void Executer(string demande)
        {

            string username = Environment.UserName;
            string[] demandecut = demande.Split(' ');
            if (Bibliotheque == "Ordre")
            {
                for (int i = 0; i <= demandecut.Length - 1; i++)
                {
                    if (demandecut[i].ToLower() == "spotify")
                    {

                        try
                        {
                            Process.Start("C:\\Users\\" + username + "\\AppData\\Roaming\\Spotify\\Spotify.exe");
                        }
                        catch (System.ComponentModel.Win32Exception)
                        {
                            Console.WriteLine("Vous n'avez pas Spotify d'installer.");
                        }

                    }
                    if (demandecut[i].ToLower() == "google")
                    {
                        Process.Start("http://www.google.com");
                    }
                }

            }
        }
    }
}

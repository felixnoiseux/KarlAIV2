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

        List<string> Ordre = new List<string> { "dit", "fait", "peux-tu", "pourrais-tu", "mets", "demarre", "démare" };
        List<string> useless = new List<string> { "le", "la", "les", "nous", "mon", "mon", "ma", "mes", "nos" };


        //
        // Méthodes
        //

        public void GetMessage(string texte)
        {
            message = texte;
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

                    for (int i2 = 0; i2 < Ordre.Capacity - 1; i2++)
                    {
                        if (messagecut[i] == Ordre[i2])
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
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.WorkingDirectory = @"%ProgramFiles%";
            string username = Environment.UserName;
            string[] demandecut = demande.Split(' ');
            if (Bibliotheque == "Ordre")
            {
                for (int i = 0; i <= demandecut.Length - 1; i++)
                {
                    if (demandecut[i] == "spotify")
                    {
                        //processStartInfo.WorkingDirectory = @"%C:\Users\"+ username + @"\AppData\Roaming\Spotify%";

                        //processStartInfo.FileName = @"Spotify.exe";
                        Process.Start("C:\\Users\\" + username + "\\AppData\\Roaming\\Spotify\\Spotify.exe");
                    }
                    if (demandecut[i] == "google")
                    {
                        Process.Start("chrome.exe");
                    }
                }

            }
        }
    }
}

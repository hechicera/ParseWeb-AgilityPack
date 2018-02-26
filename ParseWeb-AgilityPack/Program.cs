using HtmlAgilityPack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ParseWeb_AgilityPack
{
    class Program
    {
        static void Main(string[] args)
        { 
            List<String> ListLinks = new List<String>();

            FileStream stream = new FileStream("LinksReady.txt", FileMode.Open);
            StreamReader reader = new StreamReader(stream);
            while (!reader.EndOfStream)
            {
                ListLinks.Add(reader.ReadLine());                
            }
            
            HtmlWeb web = new HtmlWeb();

            List<String> ListSentencesES = new List<String>();
            List<String> ListSentencesPT = new List<String>();
            List<String> AllLines = new List<String>();

            for (int i = 0; i < ListLinks.Count; i++)
            {
                string html = ListLinks[i];
                System.Threading.Thread.Sleep(1000);
                var htmlDoc = web.Load(html);
                var htmlNodes = htmlDoc.DocumentNode.SelectNodes("//span[@lang='es']");
                var htmlNodes2 = htmlDoc.DocumentNode.SelectNodes("//span[@lang='pt']");
                try
                {
                    foreach (var node in htmlNodes)
                    {
                        ListSentencesES.Add(node.InnerText);
                        if (ListSentencesES.Count == 1000)
                        {
                            Console.WriteLine("1000");
                        }                       
                        if (ListSentencesES.Count == 3000)
                        {
                            Console.WriteLine("3000");
                        }                        
                        if (ListSentencesES.Count == 4500)
                        {
                            Console.WriteLine("4500");
                        }
                    }
                }
                catch (NullReferenceException e)
                {
                    Console.WriteLine("An error occurred: '{0}'", e);
                    continue;
                }
                try
                {
                    foreach (var node in htmlNodes2)
                    {
                        ListSentencesPT.Add(node.InnerText);
                    }
                }
                catch (NullReferenceException e)
                {
                    Console.WriteLine("An error occurred: '{0}'", e);
                    continue;
                }
            }
            
            reader.Close();

            FileStream stream2 = new FileStream("SentencesReady.txt", FileMode.OpenOrCreate);
            StreamWriter writer = new StreamWriter(stream2);
                       
            for (int i = 0; i < ListSentencesES.Count; i++)
            {   
                AllLines.Add(ListSentencesES[i] + " " + ">" + " " + ListSentencesPT[i]);                            
                writer.Write(AllLines[i]);
            }
            Console.WriteLine("The End");
            writer.Close();


            Console.ReadKey();
        }
    }
}

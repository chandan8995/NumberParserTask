using Nancy.Json;
using System;
using System.IO;
using System.Text.Json;
using System.Xml;

namespace NumberParser
{
    interface InumParse
    {
        string[] valsortDESC(string y);
        string valParseXML(string[] y);
        string valParseJSON(string[] y);
        string valParseText(string[] y);
        void print(string[] str,string Filetype);
    }
    public class Numparser : InumParse
    {
        public void print(string[] result, string fileType)
        {
            Console.WriteLine("File is Created at directory 'H:\\NumberParser' as output."+fileType);
            Console.WriteLine(File.ReadAllText(@"H:\\NumberParser\output." + fileType));
            
        }

        public string valParseJSON(string[] y)
        {
            string jsondata = new JavaScriptSerializer().Serialize(y);
            File.WriteAllText(@"H:\\NumberParser\output.json", jsondata);
            return "file created";
        }

        public string valParseText(string[] y)
        {           
            using (StreamWriter sw = File.CreateText(@"H:\\NumberParser\output.txt"))
            {
                string str = "";
                for (int i = 0; i < y.Length; i++)
                {
                    if (i == 0)
                        str += y[i].ToString();
                    else
                        str += "," + y[i].ToString();
                }
                sw.WriteLine(str);
                sw.Close();
                File.WriteAllText(@"H:\\NumberParser\output.txt", str);
            }           

            return "file created";
        }

        public string valParseXML(string[] y)
        {
            var sts = new XmlWriterSettings()
            {
                Indent = true,
            };
            using XmlWriter writer = XmlWriter.Create(@"H:\\NumberParser\output.xml", sts);
            writer.WriteStartDocument();
            writer.WriteStartElement("List");
            writer.WriteValue(y);
            writer.WriteEndElement();
            writer.WriteEndDocument();
            //File.WriteAllText(@"H:\\NumberParser\output.xml", Convert.ToString(writer));
            return "file created at directory H";
        }

        public string[] valsortDESC(string x)
        {           
            float[] arr = {};
            string[] intArray = x.Split(',');           
            double temp = 0;
            for (int i = 0; i <= intArray.Length - 1; i++)
            {
                for (int j = i + 1; j < intArray.Length; j++)
                {
                    if (Convert.ToDouble(intArray[i]) < Convert.ToDouble(intArray[j]))
                    {
                        temp = Convert.ToDouble(intArray[i]);
                        intArray[i] = intArray[j];
                        intArray[j] = temp.ToString();
                    }
                }
            }            
            return intArray;
        }
    }
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("NumberParser");
            var v1 = Console.ReadLine();
            string arr = v1.Split(' ')[0];
            string file = v1.Split(' ')[v1.Split(' ').Length - 1];
            InumParse N = new Numparser();
            var result = N.valsortDESC(arr);
            string dir = @"H:\\NumberParser";
            // If directory does not exist, create it
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            if (file.EndsWith("JSON") || file.EndsWith("json"))
            {
                N.valParseJSON(result);
                N.print(result,"json"); 
            }
            else if (file.EndsWith("XML") || file.EndsWith("xml"))
            {
                N.valParseXML(result);
                N.print(result, "xml");
            }
            else
            {
                N.valParseText(result);
                N.print(result, "txt");

            }
            
        }
    }
}

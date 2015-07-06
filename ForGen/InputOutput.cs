using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForGen
{
    class InputOutput
    {
        public static void WriteObject(String arg)
        {
            String folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/FormeleMethode/";
            String date = DateTime.Now.ToString("dd-MM-yyyy_hh:mm:ss");
            String filePath = folder + "/" + date + "ForGenIO.txt";

            if (!Directory.Exists(folder))
            {
                System.IO.Directory.CreateDirectory(folder);
            }
            try
            {
                using (StreamWriter stream = new StreamWriter(filePath))
                {
                    stream.Write(arg.ToString());
                }
            }
            catch (IOException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        public static String ReadObject(String arg)
        {
            String folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/FormeleMethode/";
            String date = DateTime.Now.ToString("dd-MM-yyyy_hh:mm:ss");
            String filePath = folder + "/" + date + "ForGenIO.txt";
            
            String output = "";

            if (!Directory.Exists(folder))
            {
                System.Diagnostics.Debug.WriteLine("There is no file to read.");
                return output + "There is no file or folder to open.";
            }
            try
            {
                using (StreamReader stream = new StreamReader(filePath))
                {
                    String line = "";
                    while ((line = stream.ReadLine()) != null)
                    {
                        output += line;
                    }
                }
            }
            catch (IOException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return output;
        }
    }
}

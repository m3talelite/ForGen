using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ForGen
{
    class InputOutput
    {
        public static void WriteString(String arg)
        {
            String folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/FormeleMethode/";
            String filePath = folder + "/" + "ForGenIOString.txt";

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

        public static String ReadFileAsString()
        {
            String folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/FormeleMethode/";
            String filePath = folder + "/" + "ForGenIOString.txt";
            
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

        public static void WriteSerializedObject(object arg)
        {
            String folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/FormeleMethode/";
            String filePath = folder + "/" + "ForGenIOObject.bin";
			if (!Directory.Exists(folder))
			{
				System.IO.Directory.CreateDirectory(folder);
			}

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, arg);
            stream.Close();
        }

        public static object ReadSerializedObject()
        {
            String folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/FormeleMethode/";
            String filePath = folder + "/" + "ForGenIOObject.bin";

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            object obj = formatter.Deserialize(stream);
            stream.Close();

            return obj;
        }

		public static bool IsRunningOnMac ()
		{
			// Hacky solution to an otherwise unsolvable problem :s
			if (Directory.Exists("/Applications")
				& Directory.Exists("/System")
				& Directory.Exists("/Users")
				& Directory.Exists("/Volumes"))
				return true;
			return false;
		}
    }
}

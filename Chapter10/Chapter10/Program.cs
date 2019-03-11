using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Net;

namespace Chapter10
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Getting drive info*/
            DriveInfo info = new DriveInfo(@"c:\");
            Console.WriteLine("Drive name is: {0}", info.Name);
            Console.WriteLine("Drive type is: {0}", info.DriveType);
            Console.WriteLine($"Drive free space is: {info.TotalFreeSpace}");
            Console.WriteLine($"Drive size is: {info.TotalSize}");
            Console.WriteLine($"Drive available free space is: {info.AvailableFreeSpace}");

            Console.WriteLine("*********************");

            DriveInfo[] driveinfo = DriveInfo.GetDrives();
            foreach(DriveInfo drive in driveinfo)
            {
                Console.WriteLine($"Drive name: {drive.Name}");
            }

            /*Creating directories*/
            /*
            DirectoryInfo directory = Directory.CreateDirectory("tochi/program");
            DirectoryInfo directoryInfo = new DirectoryInfo("chichi/school");
            directoryInfo.Create();
            */

            /*Test if directory exists*/
            if (Directory.Exists("chuks"))
            {
                Console.WriteLine("'Chuks' directory exists");
            }
            DirectoryInfo dirInfo = new DirectoryInfo("chichi/school");
            if (dirInfo.Exists)
            {
                Console.WriteLine("'chichi/school' directory exists");
            }


            /*Moving a directory*/
            /*
            Directory.Move("tochi", "chuks/tochi");           
            DirectoryInfo dirInfo1 = new DirectoryInfo("chichi");             
            dirInfo1.MoveTo("school/chichi");
            */

            /*Listing file names with Directory class*/
            Console.WriteLine("***Listing file names with Directory class***");
            string[] fileNames = Directory.GetFiles("school/chichi");
            foreach(string fileName in fileNames)
            {
                Console.WriteLine($"File Name: {fileName}");
            }

            /*Listing file infos with DirectoryInfo class*/
            Console.WriteLine("***Listing file info with DirectoryInfo class***");
            DirectoryInfo dirInfo2 = new DirectoryInfo("school/chichi");
            FileInfo[] fileInfos = dirInfo2.GetFiles();
            foreach(FileInfo fileinfo in fileInfos)
            {
                Console.WriteLine($"File Name: {fileinfo.Name}");
                Console.WriteLine($"File Length: {fileinfo.Length}");
                Console.WriteLine($"File Creation Time: {fileinfo.CreationTime}");
                Console.WriteLine($"Fle Directory: {fileinfo.Directory}");
                Console.WriteLine($"File Directory Name: {fileinfo.DirectoryName}");
                Console.WriteLine("-----");
            }

            /*Create Write, Read and Copy file*/
            Console.WriteLine("**File Create, Write Read Operation***");
            //FileOperation1();

            /*FileStream write Operation using File (static) class*/
            FileStreamWriteOperation();

            /*Using the FileStream class to write*/
            FileStreamWrite("C Sharp has been a bit of fun to learn");

            /*StringWriter and StringReader class */
            StringWriteRead();

            /*BinaryWriter and BinaryReader*/
            BinaryWriteRead();

            Console.WriteLine("***Communication over a network****");
            /*WebRequest and WebResponse class of System.Net*/
            //WebStream();

            /*File I\O Asynchronous methods*/
            ReadWriteAsync();

            /*StreamReader*/
            streamRead1();
            streamRead2();

            Console.ReadLine();
        }
        static void FileOperation1()
        {
            File.Create("Tochi.log").Close();
            File.WriteAllText("Tochi.log", "Tochukwu is c# Programmer. \n He is an expert.");                       
            File.Copy("Tochi.log", "school/chichi/tochi.tmp");
            string tochi = File.ReadAllText("school/chichi/tochi.tmp");

            Console.WriteLine($"About Tochukz:\n {tochi}");
        }
        static void FileOperationA()
        {
            FileInfo fileinfo = new FileInfo("chuks.log");
            fileinfo.Create();
            FileStream fileHandle  = fileinfo.OpenWrite();
            //TODO

        }
        static void FileStreamWriteOperation()
        {
            FileStream fileStream = File.Create("tochukwu.txt");
            string content = "Tochukwu the great programmer";
            byte[] byteContent = Encoding.UTF8.GetBytes(content);
            fileStream.Write(byteContent, 0, byteContent.Length);
            fileStream.Close();
        }
        static void FileStreamWrite(string content)
        {
            FileStream stream = new FileStream("school/c_sharp.txt", FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write);
            byte[] contentByte = Encoding.UTF8.GetBytes(content);
            stream.Write(contentByte, 0, contentByte.Length);
            stream.Close();
        }
        static void FileStreamRead()
        {
            FileStream stream = new FileStream("school/c_sharp.txt", FileMode.Open, FileAccess.Read, FileShare.Read);

            byte byt;
            for(int x=0; x<stream.Length; x++)
            {
                byt = (byte) stream.ReadByte();
                
                
            }
            //TODO
        }
        static void StringWriteRead()
        {
            StringWriter str = new StringWriter();
            str.WriteLine("My Name is tochukwu");
            str.WriteLine("I Am a C# Developer");

            StringReader read = new StringReader(str.ToString());
            string lines = read.ReadToEnd();
            Console.WriteLine(lines);
            
        }
        static void BinaryWriteRead()
        {
            FileStream streamA = File.Create("sample.dat");
            BinaryWriter writer = new BinaryWriter(streamA);
            writer.Write("I am C# developer");
            writer.Write('C');
            writer.Write(true);
            writer.Write(33000);
            streamA.Close();

            FileStream streamB = File.Open("sample.dat", FileMode.Open);
            BinaryReader reader = new BinaryReader(streamB);
            /* BinaryReader.Read methods must be called orinally otherwise the wrong output may be produced or an exception thrown*/
            Console.WriteLine(reader.ReadString());
            Console.WriteLine(reader.ReadChar());
            Console.WriteLine(reader.ReadBoolean());
            Console.WriteLine(reader.ReadInt32());
            streamB.Close();

            /*Note that of the type to read is not in the binary string then System.IO.EndOfStreamException will be thrown*/
        }
        static void WebStream()
        {
            WebRequest request = WebRequest.Create("http://batto.test/");
            WebResponse response = request.GetResponse();

            StreamReader reader = new StreamReader(response.GetResponseStream());
            string html = reader.ReadToEnd();
            Console.WriteLine(html);
            response.Close();

        }
        static async void ReadWriteAsync()
        {
            FileStream stream = File.Create("mynewfile.dat");
            StreamWriter writer = new StreamWriter(stream);
            StreamReader reader = new StreamReader(stream);
            for(int i=0; i<100; i++)
            {
                await writer.WriteAsync($"{i}");
                string line = await reader.ReadLineAsync();
                Thread.Sleep(1000);
                Console.WriteLine("text");
            }
            //TODO
        }
        static void streamRead1()
        {
            StreamReader reader = new StreamReader("Tochi.log");
            Console.WriteLine(reader.ReadToEnd());
            reader.Close();
        }
        static void streamRead2()
        {
            StreamReader reader;
            using (reader = new StreamReader("Tochi.log"))
            {
                Console.WriteLine(reader.ReadToEnd());
            }
            reader.Close();
        }
    }
}

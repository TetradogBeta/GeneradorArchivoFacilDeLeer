using System.IO;

namespace GeneradorArchivoFacilDeLeer
{
    class Program
    {
        static void Main(string[] args)
        {
            File.WriteAllBytes(args[0] + ".out", StringReader.Create(File.ReadAllLines(args[0])));
        }
    }
}

using Gabriel.Cat.S.Extension;
namespace GeneradorArchivoFacilDeLeer
{
    public class StringReader : Reader<string>
    {
        public const string ENTER = "\n\r";
        public StringReader() : base(null) {
        }
        public static byte[] Create(string texto,string marcaFin = ENTER)
        {
            return Reader<string>.Create(texto.Split(new string[] { marcaFin },System.StringSplitOptions.RemoveEmptyEntries));
        }
    }
}

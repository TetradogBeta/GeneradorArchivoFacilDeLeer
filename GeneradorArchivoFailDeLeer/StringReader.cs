using Gabriel.Cat.S.Binaris;
using Gabriel.Cat.S.Extension;
namespace GeneradorArchivoFacilDeLeer
{
    public class StringReader : Reader<string>
    {
        public const string ENTER = "\n\r";
        public StringReader(byte[] file) : base(file,ElementoBinario.ElementoTipoAceptado(Gabriel.Cat.S.Utilitats.Serializar.TiposAceptados.String)) {
        }
        public static byte[] Create(string texto,string marcaFin = ENTER)
        {
            return Reader<string>.Create(texto.Split(new string[] { marcaFin },System.StringSplitOptions.RemoveEmptyEntries));
        }
    }
}

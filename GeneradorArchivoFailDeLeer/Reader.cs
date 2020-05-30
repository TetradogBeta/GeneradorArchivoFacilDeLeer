using Gabriel.Cat.S.Binaris;
using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;

namespace GeneradorArchivoFacilDeLeer
{
    public class Reader<T>
    {
        private byte[] file;
        int[] Index { get; set; }

        public Reader(byte[] file,ElementoBinario customSerializador=default)
        {
            if (!Equals(customSerializador, default))
                Serializador = ElementoBinario.GetSerializador<T>();
            else Serializador = customSerializador;
            Diccionary = new SortedList<long, T>();

            File = file;
        }
        public byte[] File { get => file; set { file = value; RefreshHeader();Diccionary.Clear(); } }
      

        public ElementoBinario Serializador { get; set; }
        SortedList<long, T> Diccionary { get; set; }

        public T this[long index]
        {
            get
            {
                byte[] data;
                T item;
                if (!Diccionary.ContainsKey(index))
                {
                    if (index < Index.Length - 1)
                    {
                        data = File.SubArray(Index[index], Index[index + 1] - Index[index]);
                    }
                    else data = File.SubArray(Index[index]);

                    item = (T)Serializador.GetObject(data);
                }
                else item = Diccionary[index];
                return item;
            }
        }

        private void RefreshHeader()
        {
            int offset = 0;
            long entradas = BitConverter.ToInt64(File, offset);
            offset += sizeof(long);
            Index = new int[entradas];
            for(long i = 0; i < entradas; i++)
            {
                Index[i]= BitConverter.ToInt32(File, offset);
                offset += sizeof(int);
            }

        }

        public static byte[] Create(IList<T> elements,ElementoBinario serializador = default)
        {
            byte[] dataElement;
            int offset;
            List<byte> data = new List<byte>();
            List<byte> dataAux = new List<byte>();
       
            if (Equals(serializador, default))
            {
                serializador = ElementoBinario.GetSerializador<T>();
            }
            data.AddRange(Serializar.GetBytes(elements.Count));
            offset = data.Count+elements.Count*sizeof(int);//avanzo el header
            for(int i = 0; i < elements.Count; i++)
            {
                dataElement = serializador.GetBytes(elements[i]);
                data.AddRange(Serializar.GetBytes(offset));
                offset += dataElement.Length;
                dataAux.AddRange(dataElement);
            }
            data.AddRange(dataAux);
            return data.ToArray();
        }
        

    }
}

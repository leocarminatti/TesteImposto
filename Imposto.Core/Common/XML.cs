using System.Configuration;
using System.IO;
using System.Xml.Serialization;

namespace Imposto.Core.Common
{
    public class XML 
    {
        private string local = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
        
        public void Save<T>(T obj)
        {
            var pasta = ConfigurationManager.AppSettings["Pasta"];
            var arquivo = ConfigurationManager.AppSettings["Arquivo"];
            var caminho = string.Concat(local, pasta);

            if (!Directory.Exists(caminho))
            {
                Directory.CreateDirectory(caminho);
            }

            using (var ms = new MemoryStream())
            {
                var writer = new StreamWriter(ms);
                var serializer = new XmlSerializer(obj.GetType());
                serializer.Serialize(writer, obj);
                writer.Flush();

                File.WriteAllBytes(string.Concat(caminho, arquivo), ms.ToArray());
            }
        }
    }
}

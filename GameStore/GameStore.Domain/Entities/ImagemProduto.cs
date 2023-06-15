using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Entities
{
    public class ImagemProduto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string UrlBlobStorage { get; set; }
        public byte[] DataStream { get; set; }
        public string Database64Content { get; set; }

        public ImagemProduto() { }

        public ImagemProduto(int id, string url)
            : this(url)
        {
            Id = id;
        }

        public ImagemProduto(string url)
        {
            Url = url;
            DataStream = GetDataStream(url);
            Database64Content = GetDatabase64(DataStream);
        }

        public ImagemProduto(int id, string url, byte[] dataStream, string database64)
        {
            this.Id = id;
            this.Url = url;
            DataStream = dataStream;
            Database64Content = database64;
        }

        private byte[] GetDataStream(string url)
        {
            byte[] dataStream = null;

            if (!string.IsNullOrEmpty(url) && File.Exists(url))
            {
                using (FileStream fileStream = new FileStream(url, FileMode.Open))
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        fileStream.CopyTo(memoryStream);
                        dataStream = memoryStream.ToArray();
                    }
                }
            }

            return dataStream;
        }

        private string GetDatabase64(byte[] content)
        {
            string base64 = null;

            if (content is not null)
                base64 = Convert.ToBase64String(content);

            // TODO: ver algoritmo para reducao de texto
            return base64;
        }

        public void CleanStreamData()
        {
            DataStream = null;
            Database64Content = null;
        }

        public ImagemProduto CreateImageByObjectCopy()
        {
            var result = new ImagemProduto(this.Id, this.Url, this.DataStream, this.Database64Content);
            return result;
        }
    }
}

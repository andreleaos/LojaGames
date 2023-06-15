using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Entities
{
    public class ImagemProduto
    {
        #region Atributos

        private int Id;
        private string Url;
        private string UrlBlobStorage;
        private byte[] DataStream { get; set; }
        private string Database64Content { get; set; }
        #endregion

        #region Propriedades

        public int GetId()
        {
            return Id;
        }
        public void SetId(int id) => this.Id = id;

        public string GetUrl()
        {
            return Url;
        }
        public void SetUrl(string url) => this.Url = url;

        public string GetUrlBlobStorage()
        {
            return UrlBlobStorage;
        }
        public void SetUrlBlobStorage(string url) => this.UrlBlobStorage = url;

        public byte[] GetDataStream()
        {
            return DataStream ?? new byte[0];
        }
        public void SetDataStream(byte[] data) => DataStream = data;

        public string GetDatabase64Content()
        {
            return Database64Content;
        }
        public void SetDatabase64Content(string data) => Database64Content = data;

        #endregion

        #region Construtores
        public ImagemProduto() { }
        public ImagemProduto(int id, string url)
            : this(url)
        {
            Id = id;
        }
        public ImagemProduto(string url)
        {
            Url = url;
            UrlBlobStorage = url;
            DataStream = GetDataStream(url);
            Database64Content = GetDatabase64(DataStream);
        }
        public ImagemProduto(int id, string url, byte[] dataStream, string database64)
        {
            this.Id = id;
            this.Url = url;
            this.UrlBlobStorage = url;
            DataStream = dataStream;
            Database64Content = database64;
        }
        #endregion

        #region Metodos Publicos

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

        #endregion

        #region Metodos Privados

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
        #endregion
    }
}

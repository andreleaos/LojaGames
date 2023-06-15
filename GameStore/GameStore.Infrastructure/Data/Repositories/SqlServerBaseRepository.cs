using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Infrastructure.Data.Repositories
{
    public abstract class SqlServerBaseRepository
    {
        protected string ReadResource(string pastaSql, string name)
        {
            // Determine path
            var assembly = Assembly.GetExecutingAssembly();
            string resourcePath = name;
            var format = $"GameStore.Infrastructure.sql.{pastaSql}.{name}.{("sql")}";
            if (!name.StartsWith(nameof(format)))
            {
                resourcePath = assembly.GetManifestResourceNames()
                    .Single(str => str.EndsWith(format));
            }

            using (
                Stream stream = assembly.GetManifestResourceStream(resourcePath))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
        protected byte[] ReadResourceBytes(string pastaSql, string name)
        {
            var assembly = Assembly.GetExecutingAssembly();
            string resourcePath = name;
            var format = $"GameStore.Infrastructure.sql.{pastaSql}.{name}.{("sql")}";
            if (!name.StartsWith(nameof(format)))
            {
                resourcePath = assembly.GetManifestResourceNames()
                    .Single(str => str.EndsWith(format));
            }

            using (
                Stream stream = assembly.GetManifestResourceStream(resourcePath))
            using (StreamReader reader = new StreamReader(stream))
            {
                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    return memoryStream.ToArray();
                }
            }
        }
    }
}

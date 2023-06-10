using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Infrastructure.Config
{
    public interface IConfigParameters
    {
        void SetGeneralConfig();
        void EnableConnectionLocal(bool enabled);
    }
}

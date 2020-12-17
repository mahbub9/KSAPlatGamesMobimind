using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateGame.BL.IBL
{
    public interface IStatusBL
    {
        Guid GetStatusByCode(string code);
    }
}

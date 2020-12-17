using PlatGame.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateGame.BL.IBL
{
    public interface IClientTokenBL
    {
        bool AddClientToken(ClientToken clientToken);
        bool IsValidTokenExists();
        ClientToken GetToken();
    }
}

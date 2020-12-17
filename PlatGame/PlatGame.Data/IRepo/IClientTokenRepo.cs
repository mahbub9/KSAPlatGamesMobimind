using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatGame.Data.IRepo
{
    public interface IClientTokenRepo
    {      
        bool AddClientToken(ClientToken clientToken);
        bool IsValidTokenExists();
        ClientToken GetToken();
    }
}

using PlatGame.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatGame.Service.Interfaces
{
    public interface IClientAccessTokenService
    {

        TokenModel GenerateToken();
    }
}

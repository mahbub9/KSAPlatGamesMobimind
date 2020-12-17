using PlateGame.BL.IBL;
using PlatGame.Data;
using PlatGame.Data.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateGame.BL.BL
{
    public class ClientTokenBL : IClientTokenBL
    {
        private IClientTokenRepo _clientTokenRepo;
        public ClientTokenBL(IClientTokenRepo clientTokenRepo)
        {
            _clientTokenRepo = clientTokenRepo;
        }
        public bool AddClientToken(ClientToken clientToken)
        {
            return _clientTokenRepo.AddClientToken(clientToken);
        }

        public ClientToken GetToken()
        {

            return _clientTokenRepo.GetToken();
        }

        public bool IsValidTokenExists()
        {
            return _clientTokenRepo.IsValidTokenExists();
        }
    }
}

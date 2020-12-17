using PlateGame.BL.IBL;
using PlatGame.DataAccess.IRepo;
using PlatGame.DataAccess.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateGame.BL.BL
{
    public class StatusBL : IStatusBL
    {
        private IStatusRepo _statusRepo;

        public StatusBL(IStatusRepo statusRepo)
        {
            _statusRepo = statusRepo;
        }

        public Guid GetStatusByCode(string code)
        {
            return _statusRepo.GetStatusByCode(code);
        }
       
    }
}

using PlatGame.Data.IRepo;
using PlatGame.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatGame.Data.Repo
{    public class BaseRepo : IBaseRepo
    {
        private KSAPlatGamesMobimindEntities lContext;
        public KSAPlatGamesMobimindEntities LContext
        {
            get
            {
                if (lContext == null)
                {
                    lContext = new KSAPlatGamesMobimindEntities();
                }
                return lContext;
            }
        }
    }
}

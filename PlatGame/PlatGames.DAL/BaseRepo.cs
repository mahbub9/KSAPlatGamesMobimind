using PlatGames.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatGames.DAL
{
    public class BaseRepo
    {
        private KSAPlatGamesMobimindEntities lContext;
        public KSAPlatGamesMobimindEntities LContext
        {
            get
            {
                if (lContext == null)
                {
                    lContext = new KSAPlatGamesMobimindEntities();
                    lContext.Configuration.LazyLoadingEnabled = false;
                    lContext.Configuration.ProxyCreationEnabled = false;
                }
                return lContext;
            }
        }
    }
}

using ForestInterActive;
using PlatGames.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatGames.PlatGames.DAL.DataRepo
{
    public class AddLandingRepo:BaseRepo
    {
        public void AddLanding()
        {
            try
            {
                LContext.AddLanding();
            }
            catch (Exception ex)
            {
                Logs.Log(ex);
            }
        }
    }
}

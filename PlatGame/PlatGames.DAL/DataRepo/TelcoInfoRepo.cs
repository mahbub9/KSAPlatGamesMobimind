using ForestInterActive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqKit;
using System.Linq.Expressions;

namespace PlatGames.DAL.DataRepo
{
    public class TelcoInfoRepo : BaseRepo
    {
        public static Object locthis = new object();

        
        public IQueryable<TelcoInfo> FindBy(System.Linq.Expressions.Expression<Func<TelcoInfo, bool>> predicate, string include = null)
        {
            try
            {
                if (!string.IsNullOrEmpty(include))
                {
                    return LContext.TelcoInfoes.Include(include).Where(predicate);
                }
                return LContext.TelcoInfoes.Where(predicate);
            }
            catch (Exception ex)
            {
                Logs.Log(ex);
                return null;
            }
        }
    }
}

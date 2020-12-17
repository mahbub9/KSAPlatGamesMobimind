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
    public class LandingClickRepo : BaseRepo
    {
        public static Object locthis = new object();

        public List<LandingClick> GetAll()
        {
            try
            {
                return (from c in LContext.LandingClicks select c).ToList();
            }
            catch (Exception ex)
            {
                Logs.Log(ex);
                return null;
            }
        }
        public LandingClick GetById(Guid Id)
        {
            try
            {
                return (from c in LContext.LandingClicks where c.Id == Id select c).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logs.Log(ex);
                return null;
            }
        }
        public IQueryable<LandingClick> FindBy(System.Linq.Expressions.Expression<Func<LandingClick, bool>> predicate, string include = null)
        {
            try
            {
                if (!string.IsNullOrEmpty(include))
                {
                    return LContext.LandingClicks.Include(include).Where(predicate);
                }
                return LContext.LandingClicks.Where(predicate);
            }
            catch (Exception ex)
            {
                Logs.Log(ex);
                return null;
            }
        }
        public Result Insert(LandingClick Obj)
        {
            try
            {
                lock (locthis)
                {
                    LandingClick chekc = (from c in LContext.LandingClicks where c.Txid == Obj.Txid select c).FirstOrDefault();
                    if (chekc == null)
                    {
                        LContext.LandingClicks.Add(Obj);
                        LContext.SaveChanges();
                        return new Result(ResultState.Success, "Insert Successfully", Obj);
                    }
                    return new Result(ResultState.Fail, "Dublicate", Obj);
                }
            }
            catch (Exception ex)
            {
                Logs.Log(ex);
                return new Result(ResultState.Fail, "Sorry, saving error please try again later", Obj);
            }
        }
        public Result Update(LandingClick Obj)
        {
            try
            {
                LContext.Entry(LContext.Set<LandingClick>().Find(Obj.Id)).State = System.Data.Entity.EntityState.Detached;
                LContext.Entry(Obj).State = System.Data.Entity.EntityState.Modified;
                LContext.SaveChanges();
                return new Result(ResultState.Success, "Updated Successfully", Obj);
            }
            catch (Exception ex)
            {
                Logs.Log(ex);
                return new Result(ResultState.Fail, "Sorry, saving error please try again later", Obj);
            }
        }
        public Result Update(List<LandingClick> Objs)
        {
            try
            {
                foreach (LandingClick Obj in Objs)
                {
                    LContext.Entry(LContext.Set<LandingClick>().Find(Obj.Id)).State = System.Data.Entity.EntityState.Detached;
                    LContext.Entry(Obj).State = System.Data.Entity.EntityState.Modified;
                }
                LContext.SaveChanges();
                return new Result(ResultState.Success, "Updated Successfully", Objs);
            }
            catch (Exception ex)
            {
                Logs.Log(ex);
                return new Result(ResultState.Fail, "Sorry, saving error please try again later", Objs);
            }
        }
        public Result Delete(Guid Id)
        {
            try
            {
                LandingClick Obj = GetById(Id);
                LContext.LandingClicks.Remove(Obj);
                LContext.SaveChanges();
                return new Result(ResultState.Success, "Delete Successfully", Id);
            }
            catch (Exception ex)
            {
                Logs.Log(ex);
                return new Result(ResultState.Fail, "Unable To Delete", Id);
            }
        }

        public System.Linq.Expressions.Expression<Func<LandingClick, bool>> GetWhere(string where)
        {

            Expression<Func<LandingClick, bool>> myFilter = x => 1 == 1;

            if (string.IsNullOrEmpty(where))
            {
                return myFilter;
            }

            List<string> allFilter = where.Split(';').ToList();

            if (allFilter[1].Split(':').ToList().Count == 2)
            {
                DateTime startD = DateTime.Now;
                DateTime endD = DateTime.Now;
                if (DateTime.TryParse(allFilter[1].Split(':')[0], out startD) && DateTime.TryParse(allFilter[1].Split(':')[1], out endD))
                {
                    endD = endD.AddDays(1);
                    //myFilter = myFilter.And(c => c.LandingClickDate >= startD && c.LandingClickDate <= endD);

                }

            }

            List<string> searchDetails = allFilter[0].Split(':').ToList();

            if (searchDetails.Count != 2)
            {
                return myFilter;
            }
            if (string.IsNullOrEmpty(searchDetails[1]))
            {
                return myFilter;

            }

            string val = searchDetails[1].ToLower();

            switch (searchDetails[0])
            {
                //case "Msisdn":
                //    return myFilter.And(c => c.MSISDN.ToLower().Contains(val));
                //case "Keyword":
                //    return myFilter.And(c => c.LandingClickKeywords.Where(t => t.Keyword.Name.
                //    ToLower().Contains(val)).Count() > 0);
                default:
                    return myFilter;
            }
        }
        public List<LandingClick> Get(int skip, int take, string search)
        {
            try
            {
                List<LandingClick> result = (from c in LContext.LandingClicks.Include("Telco").AsExpandable() orderby c.Id select c).Where(GetWhere(search)).Skip(skip).Take(take).ToList();
                return result;
            }
            catch (Exception ex)
            {
                Logs.Log(ex);
                return null;
            }
        }
        public List<LandingClick> GetToExport(string search)
        {
            try
            {
                List<LandingClick> result = (from c in LContext.LandingClicks.AsExpandable() orderby c.Id select c).Where(GetWhere(search)).ToList();
                return result;
            }
            catch (Exception ex)
            {
                Logs.Log(ex);
                return null;
            }
        }
        public int GetLenght(string search)
        {
            try
            {
                return (from c in LContext.LandingClicks.AsExpandable() select c).Where(GetWhere(search)).Count();
            }
            catch (Exception ex)
            {
                Logs.Log(ex);
                return 0;
            }
        }
    }
}

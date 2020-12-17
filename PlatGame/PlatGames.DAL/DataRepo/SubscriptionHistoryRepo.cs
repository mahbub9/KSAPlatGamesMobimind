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
    public class SubscriptionHistoryRepo : BaseRepo
    {
        public static Object locthis = new object();

        public List<SubscriptionHistory> GetAll()
        {
            try
            {
                return (from c in LContext.SubscriptionHistories select c).ToList();
            }
            catch (Exception ex)
            {
                Logs.Log(ex);
                return null;
            }
        }
        public SubscriptionHistory GetById(Guid Id)
        {
            try
            {
                return (from c in LContext.SubscriptionHistories where c.Id == Id select c).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logs.Log(ex);
                return null;
            }
        }
        public IQueryable<SubscriptionHistory> FindBy(System.Linq.Expressions.Expression<Func<SubscriptionHistory, bool>> predicate, string include = null)
        {
            try
            {
                if (!string.IsNullOrEmpty(include))
                {
                    return LContext.SubscriptionHistories.Include(include).Where(predicate);
                }
                return LContext.SubscriptionHistories.Where(predicate);
            }
            catch (Exception ex)
            {
                Logs.Log(ex);
                return null;
            }
        }
        public Result Insert(SubscriptionHistory Obj)
        {
            try
            {
                lock (locthis)
                {
                    LContext.SubscriptionHistories.Add(Obj);
                    LContext.SaveChanges();
                    return new Result(ResultState.Success, "Insert Successfully", Obj);
                }
            }
            catch (Exception ex)
            {
                Logs.Log(ex);
                return new Result(ResultState.Fail, "Sorry, saving error please try again later", Obj);
            }
        }
        public Result Update(SubscriptionHistory Obj)
        {
            try
            {
                LContext.Entry(LContext.Set<SubscriptionHistory>().Find(Obj.Id)).State = System.Data.Entity.EntityState.Detached;
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
        public Result Update(List<SubscriptionHistory> Objs)
        {
            try
            {
                foreach (SubscriptionHistory Obj in Objs)
                {
                    LContext.Entry(LContext.Set<SubscriptionHistory>().Find(Obj.Id)).State = System.Data.Entity.EntityState.Detached;
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
                SubscriptionHistory Obj = GetById(Id);
                LContext.SubscriptionHistories.Remove(Obj);
                LContext.SaveChanges();
                return new Result(ResultState.Success, "Delete Successfully", Id);
            }
            catch (Exception ex)
            {
                Logs.Log(ex);
                return new Result(ResultState.Fail, "Unable To Delete", Id);
            }
        }

        public System.Linq.Expressions.Expression<Func<SubscriptionHistory, bool>> GetWhere(string where)
        {

            Expression<Func<SubscriptionHistory, bool>> myFilter = x => 1 == 1;

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
                    //myFilter = myFilter.And(c => c.SubscriptionHistoryDate >= startD && c.SubscriptionHistoryDate <= endD);

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
                //    return myFilter.And(c => c.SubscriptionHistoryKeywords.Where(t => t.Keyword.Name.
                //    ToLower().Contains(val)).Count() > 0);
                default:
                    return myFilter;
            }
        }
        public List<SubscriptionHistory> Get(int skip, int take, string search)
        {
            try
            {
                List<SubscriptionHistory> result = (from c in LContext.SubscriptionHistories.Include("Telco").AsExpandable() orderby c.Id select c).Where(GetWhere(search)).Skip(skip).Take(take).ToList();
                return result;
            }
            catch (Exception ex)
            {
                Logs.Log(ex);
                return null;
            }
        }
        public List<SubscriptionHistory> GetToExport(string search)
        {
            try
            {
                List<SubscriptionHistory> result = (from c in LContext.SubscriptionHistories.AsExpandable() orderby c.Id select c).Where(GetWhere(search)).ToList();
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
                return (from c in LContext.SubscriptionHistories.AsExpandable() select c).Where(GetWhere(search)).Count();
            }
            catch (Exception ex)
            {
                Logs.Log(ex);
                return 0;
            }
        }
    }
}

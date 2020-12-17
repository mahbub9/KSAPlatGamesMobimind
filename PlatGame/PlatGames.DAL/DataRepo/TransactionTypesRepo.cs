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
    public class TransactionTypesRepo : BaseRepo
    {
        public static Object locthis = new object();

        public List<TransactionType> GetAll()
        {
            try
            {
                return (from c in LContext.TransactionTypes select c).ToList();
            }
            catch (Exception ex)
            {
                Logs.Log(ex);
                return null;
            }
        }
        public TransactionType GetById(int Id)
        {
            try
            {
                return (from c in LContext.TransactionTypes where c.ID == Id select c).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logs.Log(ex);
                return null;
            }
        }
        public IQueryable<TransactionType> FindBy(System.Linq.Expressions.Expression<Func<TransactionType, bool>> predicate, string include = null)
        {
            try
            {
                if (!string.IsNullOrEmpty(include))
                {
                    return LContext.TransactionTypes.Include(include).Where(predicate);
                }
                return LContext.TransactionTypes.Where(predicate);
            }
            catch (Exception ex)
            {
                Logs.Log(ex);
                return null;
            }
        }
        public Result Insert(TransactionType Obj)
        {
            try
            {
                LContext.TransactionTypes.Add(Obj);
                LContext.SaveChanges();
                return new Result(ResultState.Success, "Insert Successfully", Obj);
            }
            catch (Exception ex)
            {
                Logs.Log(ex);
                return new Result(ResultState.Fail, "Sorry, saving error please try again later", Obj);
            }
        }
        public Result Update(TransactionType Obj)
        {
            try
            {
                LContext.Entry(LContext.Set<TransactionType>().Find(Obj.ID)).State = System.Data.Entity.EntityState.Detached;
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
        public Result Update(List<TransactionType> Objs)
        {
            try
            {
                foreach (TransactionType Obj in Objs)
                {
                    LContext.Entry(LContext.Set<TransactionType>().Find(Obj.ID)).State = System.Data.Entity.EntityState.Detached;
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
        public Result Delete(int Id)
        {
            try
            {
                TransactionType Obj = GetById(Id);
                LContext.TransactionTypes.Remove(Obj);
                LContext.SaveChanges();
                return new Result(ResultState.Success, "Delete Successfully", Id);
            }
            catch (Exception ex)
            {
                Logs.Log(ex);
                return new Result(ResultState.Fail, "Unable To Delete", Id);
            }
        }

        public System.Linq.Expressions.Expression<Func<TransactionType, bool>> GetWhere(string where)
        {

            Expression<Func<TransactionType, bool>> myFilter = x => 1 == 1;

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
                    //myFilter = myFilter.And(c => c.TransactionTypesDate >= startD && c.TransactionTypesDate <= endD);

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
                //    return myFilter.And(c => c.TransactionTypesKeywords.Where(t => t.Keyword.Name.
                //    ToLower().Contains(val)).Count() > 0);
                default:
                    return myFilter;
            }
        }
        public List<TransactionType> Get(int skip, int take, string search)
        {
            try
            {
                List<TransactionType> result = (from c in LContext.TransactionTypes.Include("Telco").AsExpandable() orderby c.ID select c).Where(GetWhere(search)).Skip(skip).Take(take).ToList();
                return result;
            }
            catch (Exception ex)
            {
                Logs.Log(ex);
                return null;
            }
        }
        public List<TransactionType> GetToExport(string search)
        {
            try
            {
                List<TransactionType> result = (from c in LContext.TransactionTypes.AsExpandable() orderby c.ID select c).Where(GetWhere(search)).ToList();
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
                return (from c in LContext.TransactionTypes.AsExpandable() select c).Where(GetWhere(search)).Count();
            }
            catch (Exception ex)
            {
                Logs.Log(ex);
                return 0;
            }
        }
    }
}

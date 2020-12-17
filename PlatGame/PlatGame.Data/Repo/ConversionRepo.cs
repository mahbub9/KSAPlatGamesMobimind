using PlatGame.Data.IRepo;
using PlatGame.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatGame.Data.Repo
{
    public class ConversionRepo : IConversionRepo
    {
        private IBaseRepo _db;

        public ConversionRepo(IBaseRepo db)
        {
            _db = db;
        }

        public bool AddConversion(string msisdn, string txid)
        {
            Conversion info = new Conversion
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                Msisdn = msisdn,
                Txid = txid
            };

            _db.LContext.Conversions.Add(info);

            return _db.LContext.SaveChanges() > 0;
        }
    }
}

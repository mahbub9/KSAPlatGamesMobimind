using System;

namespace PlatGame.DataAccess.IRepo
{
    public interface IStatusRepo
    {
        Guid GetStatusByCode(string code);
    }
}
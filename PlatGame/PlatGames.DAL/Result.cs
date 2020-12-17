using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatGames.DAL
{
    public class Result
    {
        public ResultState State { set; get; }
        public string Message { set; get; }
        public object Data { set; get; }
        public Result(ResultState state, string message, object data)
        {
            State = state;
            Message = message;
            Data = data;
        }
        public Result()
        {
            State = ResultState.Fail;
            Message = "";
            Data = null;
        }
    }
    public enum ResultState { Success = 1, Fail = 0 }
}

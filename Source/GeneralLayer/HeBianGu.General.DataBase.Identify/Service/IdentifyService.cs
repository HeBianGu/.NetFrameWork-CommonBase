using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.General.DataBase.Identify
{
   public interface IIdentifyService
    {
        void ActionMessage(string account, string message, string title);
        Task<Tuple<hi_dd_user, string>> Login(string account, string password);
    }

    public class IdentifyService : IIdentifyService
    {
        IUserRespository _user;
        IRoleRespository _role;
        IAuthorRespository _author;
        ILogRespository _log;

        public IdentifyService(IUserRespository user, IRoleRespository role, IAuthorRespository author, ILogRespository log)
        {
            _user = user;
            _role = role;
            _author = author;
            _log = log;
        }

        /// <summary> 登录 </summary>
        public async Task<Tuple<hi_dd_user, string>> Login(string account, string password)
        {
            var find = await _user.GetListAsync(l => l.Account == account);

            if (find == null || find.Count == 0)
            {
                return Tuple.Create<hi_dd_user, string>(null, "用户名不存在");
            }

            find = find.Where(l => l.Password == password)?.ToList();

            if (find == null || find.Count == 0)
            {
                return Tuple.Create<hi_dd_user, string>(null, "密码不正确");
            }

            return Tuple.Create(find.FirstOrDefault(), string.Empty);
        }

        /// <summary> 操作日志 </summary>
        public void ActionMessage(string account, string message, string title)
        {
            hi_dd_log log = new hi_dd_log();

            log.Account = account;

            log.Message = message;

            log.Title = title;

            _log.InsertAsync(log);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.General.DataBase.SysConfig
{
    public interface IConfigService
    {
        Task<string> GetValue(string type, bool userversion = false);
        Task<bool> SetValue(string type, string value);
    }

    public class ConfigService : IConfigService
    {
        IConfigRespository _config;


        public ConfigService(IConfigRespository config)
        { 
            _config = config;
        }

        /// <summary> 根据类型设置值 </summary>
        public async Task<bool> SetValue(string type, string value)
        {
            var finds = await _config.GetListAsync(l => l.Type == type);

            var find = finds?.FirstOrDefault();

            if (find == null)
            {
                hc_dd_config config = new hc_dd_config();

                config.Type = type;
                config.Value = value;

                config.Verson = Assembly.GetEntryAssembly().GetName().Version?.ToString();

                await _config.InsertAsync(config);
            }
            else
            {
                find.Value = value;

                find.Verson = Assembly.GetEntryAssembly().GetName().Version?.ToString();

                await _config.InsertOrUpdateAsync(find);
            }

            return true;
        }

        /// <summary> 根据类型获取值 </summary>
        public async Task<string> GetValue(string type, bool useVerson = false)
        {
            if (useVerson)
            {
                var version = Assembly.GetEntryAssembly().GetName().Version?.ToString();

                var find = await _config.FirstOrDefaultAsync(l => l.Type == type && l.Verson == version);

                return find?.Value;
            }
            else
            {
                var find = await _config.FirstOrDefaultAsync(l => l.Type == type);

                return find?.Value;
            }

        }
    }
}

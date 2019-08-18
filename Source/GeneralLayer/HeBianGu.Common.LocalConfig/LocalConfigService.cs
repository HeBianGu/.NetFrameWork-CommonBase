using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Common.LocalConfig
{
    public class LocalConfigService : ILocalConfigService
    {
        public LocalConfigService()
        {
            Folder = LocalConfigConfig.GetLocalAssemblyPath();
        }

        public string Folder { get; set; }

        public void Init(string folder)
        {
            Folder = folder;

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
        }

        XmlProvider _xmlProvider = new XmlProvider();

        public bool SaveConfig<T>(T t)
        {
            string path = Path.Combine(Folder, typeof(T).Name + ".xml");

            _xmlProvider.Save(path, t);

            return true;
        }

        public T LoadConfig<T>()
        {
            string path = Path.Combine(Folder, typeof(T).Name + ".xml");

            if (!File.Exists(path)) return default(T);

            return _xmlProvider.Load<T>(path);
        }
    }
}

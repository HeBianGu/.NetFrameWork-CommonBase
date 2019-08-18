using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Common.LocalConfig
{
    class LocalConfigConfig
    { 
        public  static string GetLocalAssemblyPath()
        {
            string doc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); 

            if(Assembly.GetEntryAssembly()==null) return Path.Combine(doc, "HeBianGu", "Config");

            return Path.Combine(doc, "HeBianGu", Assembly.GetEntryAssembly()?.GetName()?.Name, "Config");
        }

        public static string GetLocalAssemblyPath<T>()
        {
            string doc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            return Path.Combine(GetLocalAssemblyPath(), typeof(T)+".xml");
        }
    }
}

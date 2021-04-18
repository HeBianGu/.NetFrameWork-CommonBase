using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Common.Registry
{
    public class RightMouseRegisterService
    {
        /// <summary>
        /// 注册右键菜单
        /// </summary>
        /// <param name="name"> 右键菜单名称 </param>
        /// <param name="exe"> 执行exe路径 </param>
        /// <param name="type"> 应用在文件还是文件夹 </param>
        /// <returns></returns>
        public bool Register(string name, string exe, SystemFileType type)
        {
            string param = type == SystemFileType.File ? @"*\shell" : @"directory\shell";

            RegistryKey shellKey1 = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(param,
            RegistryKeyPermissionCheck.ReadWriteSubTree, System.Security.AccessControl.RegistryRights.FullControl);

            if (shellKey1 == null)
                shellKey1 = Microsoft.Win32.Registry.ClassesRoot.CreateSubKey(param);
            //创建项：右键显示的菜单名称
            RegistryKey rightCommondKey1 = shellKey1.CreateSubKey(name);
            RegistryKey associatedProgramKey1 = rightCommondKey1.CreateSubKey("command");

            //创建默认值：关联的程序
            associatedProgramKey1.SetValue(string.Empty, exe + " %1");
            //刷新到磁盘并释放资源
            associatedProgramKey1.Close();
            rightCommondKey1.Close();
            shellKey1.Close();

            return true;

        }

        /// <summary> 注销右键菜单 </summary>
        public bool UnRegister(string name, SystemFileType type)
        {

            string param = type == SystemFileType.File ? @"*\shell" : @"directory\shell";

            RegistryKey shellKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(param,
                RegistryKeyPermissionCheck.ReadWriteSubTree, System.Security.AccessControl.RegistryRights.FullControl);
            if (shellKey == null)
                shellKey = Microsoft.Win32.Registry.ClassesRoot.CreateSubKey(param);

            shellKey.DeleteSubKey(name + "\\command");
            shellKey.DeleteSubKey(name);

            //刷新到磁盘并释放资源
            shellKey.Close();

            return true;
        }

        /// <summary> 注册文件 应用在当前执行的进程中 </summary>
        public bool RegisterFile(string name)
        {
            string exe = Process.GetCurrentProcess().MainModule.FileName;

            return this.Register(name, exe, SystemFileType.File);

        }

        /// <summary> 注册文件家 应用在当前执行的进程中 </summary>
        public bool RegisterFolder(string name)
        {
            string exe = Process.GetCurrentProcess().MainModule.FileName;

            return this.Register(name, exe, SystemFileType.Folder);

        }
    }

    public enum SystemFileType
    {
        File = 0, Folder
    }


}

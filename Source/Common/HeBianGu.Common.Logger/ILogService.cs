using System;

namespace HeBianGu.Common.Logger
{
    public interface ILogService
    {
        void Error(params Exception[] ex);
        void Error(string message, Exception ex);
        void Info(params string[] message);
    }
}
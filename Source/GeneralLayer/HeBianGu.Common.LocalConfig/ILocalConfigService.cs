namespace HeBianGu.Common.LocalConfig
{
    public interface ILocalConfigService
    {
        T LoadConfig<T>();

        bool SaveConfig<T>(T t);

        void Init(string folder);
    }
}
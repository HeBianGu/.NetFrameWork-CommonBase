
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeBianGu.Common.PublicTool
{
    /// <summary> ������������</summary>
    public abstract class BaseFactory<T> : IDisposableBF where T : class,new()
    {

        #region - Start ����ģʽ -

        /// <summary> ����ģʽ </summary>
        private static T t = null;

        /// <summary> ���߳��� </summary>
        private static object localLock = new object();

        /// <summary> ����ָ������ĵ���ʵ�� </summary>
        public static T Instance
        {
            get
            {
                if (t == null)
                {
                    lock (localLock)
                    {
                        if (t == null)
                            return t = new T();
                    }
                }
                return t;
            }
        }

        #endregion - ����ģʽ End -

        #region - Start ����ģʽ -

        /// <summary> ����ģʽ </summary>
        static Dictionary<string, T> cache = null;

        /// <summary> ͨ�����Ƶõ�����ʵ�� </summary>
        public static T InstanceByName(string strKey)
        {
            lock (localLock)
            {
                if (cache == null)
                    cache = new Dictionary<string, T>();

                if (!cache.ContainsKey(strKey))
                    cache.Add(strKey, new T());

                return cache[strKey];
            }

        }

        #endregion - ����ģʽ End -

        /// <summary> �ⲿ�����Թ��� </summary>
        protected BaseFactory()
        { }

        #region - ��Դ���� -

        /// <summary> ������Ա��ʽ���õ�Dispose���� </summary>
        public void Dispose()
        {
            //  ���ô�������Dispose�������ͷ��йܺͷ��й���Դ
            Dispose(true);

            //  �ֶ�������Dispose�ͷ���Դ����ô�����������ǲ���Ҫ���ˣ�������ֹGC������������
            System.GC.SuppressFinalize(this);
        }


        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                ///TODO:�������������"�й���Դ"�Ĵ��룬Ӧ����xxx.Dispose();
                t = null;
            }
            ///TODO:�������������"���й���Դ"�Ĵ���
        }

        /// <summary> ����ָ�����ƵĻ���͵��� </summary>
        public void Dispose(string name)
        {
            Dispose(true);

            if (cache != null && cache.ContainsKey(name))
            {
                cache.Remove(name);
            }
        }

        /// <summary> ����ָ�����ƵĻ���͵��� </summary>
        public static void Dispose(Predicate<KeyValuePair<string, T>> macth)
        {
            foreach (KeyValuePair<string, T> v in cache)
            {
                if (macth(v))
                {
                    cache.Remove(v.Key);
                }
            }
        }

        /// <summary> ��GC���õ��������� </summary>
        ~BaseFactory()
        {
            //  �ͷŷ��й���Դ
            Dispose(false);
        }

        #endregion

    }

    public interface IDisposableBF : IDisposable
    {
        void Dispose(string name);
    }
}

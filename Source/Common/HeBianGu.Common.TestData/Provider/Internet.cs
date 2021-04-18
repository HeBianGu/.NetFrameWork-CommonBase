﻿using System;
using System.Linq;
using System.Text.RegularExpressions; 

namespace HeBianGu.Common.TestData
{

    public partial class Internet
    {

        public string Email => GetEmail();

        public string DisposableEmail => GetDisposableEmail();

        public string FreeEmail => GetFreeEmail();

        public string UserName => GetUserName();

        public string DomainName => GetDomainName();

        public string DomainWord => GetDomainWord();

        public string DomainSuffix => GetDomainSuffix();

        public string Uri => GetUri("http");

        public string HttpUrl => GetHttpUrl();

        public string IP_V4_Address => GetIP_V4_Address();
    }

    public partial class Internet
    {
        static Internet()
        {
            BYTE = 0.To(255).Select(item => item.ToString()).ToArray();
        }

        public static string GetEmail(string name = null)
        {
            return GetUserName(name) + '@' + GetDomainName();
        }

        /// <summary>
        /// Returns an email address of an online disposable email service (like tempinbox.com).
        /// you can really send an email to these addresses an access it by going to the service web pages.
        /// <param name="name">User Name initial value.</param>
        /// </summary>
        public static string GetDisposableEmail(string name = null)
        {
            return GetUserName(name) + '@' + DISPOSABLE_HOSTS.Rand();
        }

        public static string GetFreeEmail(string name = null)
        {
            return GetUserName(name) + "@" + HOSTS.Rand();
        }

        public static string GetUserName(string name = null)
        {
            if (name != null)
            {
                string parts = name.Split(' ').Join(new[] { ".", "_" }.Rand());
                return parts.ToLower();
            }
            else
            {
                switch (FakerRandom.Rand.Next(2))
                {
                    case 0:
                        return new Regex(@"\W").Replace(Name.GetFirstName(), "").ToLower();
                    case 1:
                        var parts = new[] { Name.GetFirstName(), Name.GetLastName() }.Select(n => new Regex(@"\W").Replace(n, ""));
						return parts.Join(new[] { ".", "_" }.Rand()).ToLower();
                    default: throw new ApplicationException();
                }
            }
        }

        public static string GetDomainName()
        {
            return GetDomainWord() + "." + GetDomainSuffix();
        }

        public static string GetDomainWord()
        {
            string dw = Company.GetName().Split(' ').First();
            dw = new Regex(@"\W").Replace(dw, "");
            dw = dw.ToLower();
            return dw;
        }

        public static string GetDomainSuffix()
        {
            return DOMAIN_SUFFIXES.Rand();
        }

        public static string GetUri(string protocol)
        {
            return protocol + "://" + GetDomainName();
        }

        public static string GetHttpUrl()
        {
            return GetUri("http");
        }

        public static string GetIP_V4_Address()
        {
            return BYTE.RandPick(4).Join(".");
        } 

        private static readonly string[] BYTE; //new [] { ((0..255).to_a.map { |n| n.to_s })
        static readonly string[] HOSTS = new[] { "gmail.com", "yahoo.com", "hotmail.com" };
        static readonly string[] DISPOSABLE_HOSTS = new[] { "mailinator.com", "suremail.info", "spamherelots.com", "binkmail.com", "safetymail.info", "tempinbox.com" };
        static readonly string[] DOMAIN_SUFFIXES = new[] { "co.uk", "com", "us", "uk", "ca", "biz", "info", "name" };
    }
}

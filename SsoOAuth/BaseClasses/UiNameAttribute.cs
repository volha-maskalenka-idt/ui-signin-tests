using System;

namespace SsoOAuth.BaseClasses
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class UiNameAttribute : Attribute
    {
        public string Name;
        public UiNameAttribute(string name)
        {
            Name = name;
        }
    }
}
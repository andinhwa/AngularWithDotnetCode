using System;
using System.Reflection;

namespace WebApp.Core.ReflectionLoader.Impl
{
    internal class SystemWebTypeLoader : ISystemWebTypeLoader
    {
        private Type _httpContextType;

        private Type _formsIdentityType;

        public Type GetHttpContextType()
        {
            if (_httpContextType == null)
            {
                var assembly = GetSystemWebAssembly();
                _httpContextType = assembly.GetType("System.Web.HttpContext");
            }

            return _httpContextType;
        }

        public Type GetFormsIdentityType()
        {
            if (_formsIdentityType == null)
            {
                var assembly = GetSystemWebAssembly();
                _formsIdentityType = assembly.GetType("System.Web.Security.FormsIdentity");
            }

            return _formsIdentityType;
        }

        private Assembly GetSystemWebAssembly()
        {
            return Assembly.Load("System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
        }
    }
}

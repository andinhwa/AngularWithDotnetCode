using System;

namespace WebApp.Core.ReflectionLoader
{
    internal interface ISystemWebTypeLoader
    {
        Type GetHttpContextType();

        Type GetFormsIdentityType();
    }
}

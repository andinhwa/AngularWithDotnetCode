using System.Reflection;

namespace WebApp.Core.ReflectionLoader
{
    internal interface IAssemblyLoader
    {
        Assembly GetExecutingAssembly();
    }
}
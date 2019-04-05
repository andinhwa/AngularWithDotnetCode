using System.Reflection;

namespace WebApp.Core.ReflectionLoader.Impl
{
    internal class AssemblyLoader : IAssemblyLoader
    {
        public Assembly GetExecutingAssembly()
        {
            return Assembly.GetExecutingAssembly();
        }
    }
}
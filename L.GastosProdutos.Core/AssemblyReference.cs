using System.Reflection;

namespace L.GastosProdutos.Core
{
    public class AssemblyReference
    {
        public Assembly GetAssembly() =>
            this.GetType().Assembly;
    }
}

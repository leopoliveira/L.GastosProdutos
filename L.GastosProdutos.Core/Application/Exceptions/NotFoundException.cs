namespace L.GastosProdutos.Core.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException() : base("Entity not found")
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string message, Exception innerExeception) : base(message, innerExeception)
        {
        }
    }
}

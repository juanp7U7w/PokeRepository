namespace PokeApi.Helpers
{
    public class ValidacionException
    {
        public object Detalle { get; set; }
        public string Codigo { get; set; }
        //public ValidacionException(string message) : base(message) { }

        //public ValidacionException(string message, object detalle) : base(message)
        //{
        //    Detalle = detalle;
        //}

        //public ValidacionException(string message, object detalle, string codigo) : base(message)
        //{
        //    Detalle = detalle;
        //    Codigo = codigo;
        //}
    }
}

namespace PruebaIngresoBibliotecario.Business.Extensions
{
    using PruebaIngresoBibliotecario.Business.Common.Enums;

    public static class CalculateDateExtension
    {
        public static DateTime CalcularFechaEntrega(int tipoUsuario)
        {
            DayOfWeek[] weekend = new[] { DayOfWeek.Saturday, DayOfWeek.Sunday };
            DateTime fechaDevolucion = DateTime.Now;
            int diasPrestamo = tipoUsuario switch
            {
                (int)TiposUsuarioEnum.Afiliado => 10,
                (int)TiposUsuarioEnum.Empleado => 8,
                (int)TiposUsuarioEnum.Invitado => 7,
                _ => -1,
            };

            for (int i = 0; i < diasPrestamo;)
            {
                fechaDevolucion = fechaDevolucion.AddDays(1);
                i = !weekend.Contains(fechaDevolucion.DayOfWeek) ? ++i : i;
            }

            return fechaDevolucion;
        }
    }
}

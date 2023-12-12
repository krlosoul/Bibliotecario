namespace PruebaIngresoBibliotecario.Core.Dtos
{
    using System;

    public class LoanCreateDto
    {
        public Guid Isbn { get; set; }
        public string? IdentificacionUsuario { get; set; } = null!;
        public int TipoUsuario { get; set; }

    }

    public class LoanCreateResponseDto
    {
        public Guid Id { get; set; }
        public string? FechaMaximaDevolucion { get; set; }
    }

    public class GetLoanDto
    {
        public Guid Id { get; set; }
    }

    public class LoanDto
    {
        public Guid Id { get; set; }
        public Guid Isbn { get; set; }
        public string? IdentificacionUsuario { get; set; }
        public int TipoUsuario { get; set; }
        public DateTime FechaMaximaDevolucion { get; set; }
    }
}

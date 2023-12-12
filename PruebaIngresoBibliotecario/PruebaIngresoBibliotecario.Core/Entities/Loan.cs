namespace PruebaIngresoBibliotecario.Core.Entities
{
    using System;

    public class Loan
    {
        public Guid Id { get; set; }
        public Guid Isbn { get; set; }
        public string? UserId { get; set; } = null!;
        public int UserType { get; set; }
        public DateTime DevolutionDate { get; set; }
    }
}

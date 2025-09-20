namespace MotorRent.Domain.Entities
{
    public class Moto : EntityBase
    {
        public Moto(string identifier, string model, int year, string plate) : base(identifier)
        {
            Model = model;
            Year = year;
            Plate = plate;
        }
        public string Model { get; private set; } = null!;
        public int Year { get; private set; }
        public string Plate { get; private set; } = null!;
        public ICollection<Rental>? Rentals { get; private set; } = [];
    }
}

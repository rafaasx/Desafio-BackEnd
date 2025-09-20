namespace MotorRent.Domain.Entities
{
    public class Rental : EntityBase
    {
        public Rental(string identifier, Guid riderId, DeliveryRider rider, Guid motoId, Moto moto, int planDays, decimal dailyPrice, DateOnly startDate, DateOnly expectedEndDate, DateOnly? returnedAt, decimal? totalPaid) : base(identifier)
        {
            RiderId = riderId;
            Rider = rider;
            MotoId = motoId;
            Moto = moto;
            PlanDays = planDays;
            DailyPrice = dailyPrice;
            StartDate = startDate;
            ExpectedEndDate = expectedEndDate;
            ReturnedAt = returnedAt;
            TotalPaid = totalPaid;
        }

        public Guid RiderId { get; private set; }
        public DeliveryRider Rider { get; private set; } = null!;
        public Guid MotoId { get; private set; }
        public Moto Moto { get; private set; } = null!;
        public int PlanDays { get; private set; }
        public decimal DailyPrice { get; private set; }
        public DateOnly StartDate { get; private set; }
        public DateOnly ExpectedEndDate { get; private set; }
        public DateOnly? ReturnedAt { get; private set; }
        public decimal? TotalPaid { get; private set; }
    }
}

using System;

namespace SC.DevChallenge.DataAccess.Abstractions.Domain
{
    public class Price : EntityBase<int>
    {
        public DateTime Date { get; set; }

        public float Value { get; set; }
    }
}

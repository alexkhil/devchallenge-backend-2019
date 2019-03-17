using System;

namespace SC.DevChallenge.DataAccess.EF.Seeder
{
    internal class DataRow
    {
        public string Portfolio { get; set; }

        public string Owner { get; set; }

        public string Instrument { get; set; }

        public string Date { get; set; }

        public float Price { get; set; }
    }
}

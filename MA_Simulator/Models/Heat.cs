using System;

namespace MA_Simulator.Models
{
    public class Heat : EntityId
    {
        public SteelGrade Grade { get; set; }  // The steel grade used in this heat

        public Heat() { }

        public Heat(int id, SteelGrade grade)
        {
            Id = id;
            Grade = grade;
        }
    }
}

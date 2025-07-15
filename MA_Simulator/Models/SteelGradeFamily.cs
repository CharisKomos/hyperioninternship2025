using System;

namespace MA_Simulator.Models
{
    public class SteelGradeFamily : EntityId
    {
        public string Name { get; set; }    // e.g., "Carbon Steel", "Alloy Steel"

        public SteelGradeFamily() { }

        public SteelGradeFamily(int id, string name)
        {
            Id = id;
            Name = name;
        }
        
    }
}

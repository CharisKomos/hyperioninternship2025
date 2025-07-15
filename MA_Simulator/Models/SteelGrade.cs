using System;

namespace MA_Simulator.Models
{
    public class SteelGrade : EntityId
    {
        public string GradeCode { get; set; }           // e.g., "S235JR"
        public string Description { get; set; } = "";   // Optional description
        public SteelGradeFamily Family { get; set; }    // Link to SteelGradeFamily

        public SteelGrade() { }

        public SteelGrade(int id, string gradeCode, SteelGradeFamily family, string description = "")
        {
            Id = id;
            GradeCode = gradeCode;
            Family = family;
            Description = description;
        }
    }
}



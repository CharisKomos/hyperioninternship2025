namespace MA_Simulator.Models
{
    public class SteelGrade : EntityId
    {
        public string Code { get; set; }           // e.g., "S235JR"
        public string Description { get; set; }   // Optional description
        public SteelGradeFamily Family { get; set; }    // Link to SteelGradeFamily

        public SteelGrade(int id, string gradeCode, SteelGradeFamily family, string description = "")
        {
            Id = id;
            Code = gradeCode;
            Description = description;
            Family = family;
        }
    }
}



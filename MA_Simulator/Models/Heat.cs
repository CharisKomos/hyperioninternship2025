using System;

namespace MA_Simulator.Models
{
    public class Heat : EntityId
    {
        public string Code { get; set; }
        public SteelGrade Grade { get; set; }  // The steel grade used in this heat
        public List<ChemicalComposite> ChemicalComposition { get; set; }

        public Heat(int id, string heatCode, SteelGrade grade, List<ChemicalComposite> chemComp)
        {
            Id = id;
            Code = heatCode;
            Grade = grade;
            ChemicalComposition = chemComp;
        }
    }
}

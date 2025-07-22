using MA_Simulator.Enums;
using MA_Simulator.Models;
using System.Collections.Generic;

namespace MA_Simulator.Schedulers
{
    public class LongProductionScheduler : ProductionSchedulerBase
    {
        public LongProductionScheduler()
        {
            // Create jobs
            ScheduledJobs.AddRange(new List<Job> { 
                // Job
                new Job(
                    id: 1,
                    customer: "CustomerA",
                    // List of heats assigned to the job
                    heatList: new List<Heat>
                                    {
                                        new Heat(
                                                id: 1,
                                                heatCode: "HEAT001",
                                                grade: new SteelGrade(
                                                            id: 1,
                                                            gradeCode: "SG001",
                                                            family: new SteelGradeFamily(1, "SGF001")
                                                            ),
                                                chemComp: new List<ChemicalComposite>()
                                                ),
                                        new Heat(
                                                id: 2,
                                                heatCode: "HEAT002",
                                                grade: new SteelGrade(
                                                            id: 2,
                                                            gradeCode: "SG002",
                                                            family: new SteelGradeFamily(1, "SGF001")
                                                            ),
                                                chemComp: new List<ChemicalComposite>()
                                                ),
                                        new Heat(
                                                id: 3,
                                                heatCode: "HEAT003",
                                                grade: new SteelGrade(
                                                            id: 2,
                                                            gradeCode: "SG003",
                                                            family: new SteelGradeFamily(1, "SGF001")
                                                            ),
                                                chemComp: new List<ChemicalComposite>()
                                                ),
                                        new Heat(
                                                id: 4,
                                                heatCode: "HEAT004",
                                                grade: new SteelGrade(
                                                            id: 2,
                                                            gradeCode: "SG004",
                                                            family: new SteelGradeFamily(1, "SGF001")
                                                            ),
                                                chemComp: new List<ChemicalComposite>()
                                                ),
                                        new Heat(
                                                id: 5,
                                                heatCode: "HEAT005",
                                                grade: new SteelGrade(
                                                            id: 2,
                                                            gradeCode: "SG005",
                                                            family: new SteelGradeFamily(1, "SGF001")
                                                            ),
                                                chemComp: new List<ChemicalComposite>()
                                                ),
                                    },
                    statusChange: DateTime.Now,
                    status: JobStatus.InProcess
                    ),
               new Job(
                    id: 2,
                    customer: "CustomerB",
                    // List of heats assigned to the job
                    heatList: new List<Heat>
                                    {
                                        new Heat(
                                                id: 3,
                                                heatCode: "PFR0003",
                                                grade: new SteelGrade(
                                                            id: 3,
                                                            gradeCode: "SN003",
                                                            family: new SteelGradeFamily(1, "SGF002")
                                                            ),
                                                chemComp: new List<ChemicalComposite>()
                                                ),
                                    },
                    statusChange: DateTime.Now,
                    status: JobStatus.Scheduled
                    ),
               new Job(
                    id: 3,
                    customer: "CustomerC",
                    // List of heats assigned to the job
                    heatList: new List<Heat>
                                    {
                                        new Heat(
                                                id: 4,
                                                heatCode: "PFR0004",
                                                grade: new SteelGrade(
                                                            id: 4,
                                                            gradeCode: "SN004",
                                                            family: new SteelGradeFamily(1, "SGF003")
                                                            ),
                                                chemComp: new List<ChemicalComposite>()
                                                ),
                                    },
                    statusChange: DateTime.Now,
                    status: JobStatus.Scheduled
                    ),
             new Job(
                    id: 4,
                    customer: "CustomerD",
                    // List of heats assigned to the job
                    heatList: new List<Heat>
                                    {
                                        new Heat(
                                                id: 5,
                                                heatCode: "PFR0005",
                                                grade: new SteelGrade(
                                                            id: 5,
                                                            gradeCode: "SN005",
                                                            family: new SteelGradeFamily(1, "SGF004")
                                                            ),
                                                chemComp: new List<ChemicalComposite>()
                                                ),
                                    },
                    statusChange: DateTime.Now,
                    status: JobStatus.Scheduled
                    ),
             new Job(
                    id: 5,
                    customer: "CustomerF",
                    // List of heats assigned to the job
                    heatList: new List<Heat>
                                    {
                                        new Heat(
                                                id: 6,
                                                heatCode: "PFR0006",
                                                grade: new SteelGrade(
                                                            id: 6,
                                                            gradeCode: "SN006",
                                                            family: new SteelGradeFamily(1, "SGF005")
                                                            ),
                                                chemComp: new List<ChemicalComposite>()
                                                ),
                                    },
                    statusChange: DateTime.Now,
                    status: JobStatus.Scheduled
                    )
            });

            // When assigning a heat to job, create a scheduled semiproduct with heat code and job id
            foreach (Job job in ScheduledJobs)
            {
                foreach (Heat heat in job.Heats)
                {
                    ScheduledBillets.Add(
                        new ScheduledBillet(
                                heatCode: heat.Code,
                                jobStatus: job.Status
                            )
                        );
                }
            }
        }
    }
}

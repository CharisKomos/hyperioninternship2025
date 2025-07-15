using MA_Simulator.Enums;
using MA_Simulator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MA_Simulator.Schedulers
{
    public class LongProductionScheduler : ProductionSchedulerBase
    {
        public LongProductionScheduler()
        {
            // Create jobs
            ScheduledJobs.Add(
                // Job
                new Job(
                    id: 1, 
                    customer: "CustomerA", 
                    // List of heats assigned to the job
                    heatList: new List<Heat>
                                    {
                                        new Heat(
                                                id: 1,
                                                heatCode: "PFR0001",
                                                grade: new SteelGrade(
                                                            id: 1,
                                                            gradeCode: "SG001",
                                                            family: new SteelGradeFamily(1, "SGF001")
                                                            ),
                                                chemComp: new List<ChemicalComposite>()
                                                ),
                                    },
                    statusChange: DateTime.Now,
                    status: JobStatus.Scheduled
                    )
                );

            // When assigning a heat to job, create a scheduled semiproduct with heat code and job id
            foreach (Job job in ScheduledJobs)
            {
                foreach (Heat heat in job.Heats)
                {
                    ScheduledBillets.Add(
                        new ScheduledBillet(
                                heatCode: heat.Code
                                )
                        );
                }
            }
        }
    }
}

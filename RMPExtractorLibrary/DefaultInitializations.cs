using RMPExtractorLibrary.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMPExtractorLibrary
{
    internal static class DefaultInitializations
    {
        private static IEnumerable<ProfessorRatingResult> _DefaultProfessorRatings;
        public static IEnumerable<ProfessorRatingResult> DefaultProfessorRatings
        {
            get
            {
                if (DefaultInitializations._DefaultProfessorRatings == null)
                {
                    DefaultInitializations._DefaultProfessorRatings = new List<ProfessorRatingResult>
                    {
                        new ProfessorRatingResult
                        {
                            Label = "Helpfulness",
                            Rating = "N/A"
                        },
                        new ProfessorRatingResult
                        {
                            Label = "Clarity",
                            Rating = "N/A"
                        },
                        new ProfessorRatingResult
                        {
                            Label = "Easiness",
                            Rating = "N/A"
                        }
                    };
                }

                return DefaultInitializations._DefaultProfessorRatings;
            }
        }
               
        private static IEnumerable<ProfessorRatingResult> _DefaultProfessorGrades;
        public static IEnumerable<ProfessorRatingResult> DefaultProfessorGrades
        {
            get
            {
                if (DefaultInitializations._DefaultProfessorGrades == null)
                {
                    List<ProfessorRatingResult> result = new List<ProfessorRatingResult>();

                    for (int gradeCount = 0; gradeCount < 3; gradeCount++)
                    {
                        result.Add(new ProfessorRatingResult
                        {
                            Label = "N/A",
                            Rating = "N/A"
                        });
                    }

                    DefaultInitializations._DefaultProfessorGrades = result;
                }

                return DefaultInitializations._DefaultProfessorGrades;
            }
        }

    }
}

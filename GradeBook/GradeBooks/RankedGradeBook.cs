using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GradeBook.Enums;

namespace GradeBook.GradeBooks
{
    class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name) : base(name)
        {
            Type = GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5)
            {
                throw new InvalidOperationException("Ranked grading requires at least 5 students.");
            }

            var sortedGrades = Students.Select(s => s.AverageGrade).OrderByDescending(g => g).ToList();

            int percentileThreshold = (int)Math.Ceiling(Students.Count * 0.2);

            int rank = sortedGrades.FindIndex(g => g <= averageGrade);

            if (rank == -1)
            {
                return 'F'; // If the grade is lower than all grades, return F
            }

            // Determine the letter grade based on the rank
            if (rank < percentileThreshold)
            {
                return 'A';
            }
            else if (rank < percentileThreshold * 2)
            {
                return 'B';
            }
            else if (rank < percentileThreshold * 3)
            {
                return 'C';
            }
            else if (rank < percentileThreshold * 4)
            {
                return 'D';
            }
            else
            {
                return 'F';
            }
        }

        public override void CalculateStatistics()
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students.");
                return;
            }
            base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students.");
                return;
            }

            base.CalculateStudentStatistics(name);
        }
    }
}

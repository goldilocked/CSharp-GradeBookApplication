using System;
using System.Linq;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name, bool weighted) : base(name, weighted)
        {
            Type = Enums.GradeBookType.Ranked;
        }

        public override void CalculateStatistics()
        {
            if(Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }
            base.CalculateStatistics();

        }
        public override void CalculateStudentStatistics(string name)
        {
            if(Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }
            base.CalculateStudentStatistics(name);
        }
        public override char GetLetterGrade(double averageGrade)
        {
            if(Students.Count < 5)
            {
                throw new InvalidOperationException();
            }

            var threshold = (int)Math.Ceiling(Students.Count * 0.2);
            var grades = Students.OrderByDescending(e => e.AverageGrade).Select(e => e.AverageGrade).ToList();
            
            if(grades[threshold - 1] <= averageGrade)
            {
                return 'A';
            }
            else if(grades[(2*threshold) - 1] <= averageGrade)
            {
                return 'B';
            }
            else if(grades[(3*threshold) - 1] <= averageGrade)
            {
                return 'C';
            }
            else if(grades[(4*threshold) - 1] <= averageGrade)
            {
                return 'D';
            }

            return 'F';

        }
    }
}

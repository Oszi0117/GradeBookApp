using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GradeBook.GradeBooks
{
    class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name) : base(name)
        {
            Type = Enums.GradeBookType.Ranked;
        }
        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count() < 5)
            {
                throw new InvalidOperationException();
            }
            if (AssignGrade(0.2f, averageGrade))
            {
                return 'A';
            }
            else if (AssignGrade(0.4f, averageGrade))
            {
                return 'B';
            }
            else if (AssignGrade(0.6f, averageGrade))
            {
                return 'C';
            }
            else if (AssignGrade(0.8f, averageGrade))
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
            if (Students.Count() < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students.");
            }
            else
            {
                base.CalculateStatistics();
            }
        }
        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count() < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students.");
            }
            else
            {
                base.CalculateStudentStatistics(name);
            }
        }
        bool AssignGrade(float threshold, double grade)
        {
            List<double> StudentsGrades = new List<double>();
            foreach (var student in Students)
            {
                StudentsGrades.Add(student.AverageGrade);
            }
            var StudentsGradesDescending = StudentsGrades.OrderByDescending(g => g);
            var percentage = StudentsGradesDescending.Count() * threshold;
            if ((percentage % 1) != 0)
            {
                var lastStudentsAverageGrade = StudentsGradesDescending.Take(Convert.ToInt32(StudentsGradesDescending.Count() * threshold) + 1).LastOrDefault();
                var secondLastStudentsAverageGrade = StudentsGradesDescending.Take(Convert.ToInt32(StudentsGradesDescending.Count() * threshold)).LastOrDefault();
                if (grade >= (lastStudentsAverageGrade+secondLastStudentsAverageGrade)/2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                var lastStudentsAverageGrade = StudentsGradesDescending.Take((int)percentage).LastOrDefault();
                if (grade >= lastStudentsAverageGrade)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}

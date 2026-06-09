using System;

namespace TmsCore;

public class EnrollmentService
{
    public EnrollmentRecord ProcessRegistration(Student? student, Course? course)
    {
        // TODO1: Guard clauses - fail fast
        if (student is null) 
            throw new ArgumentNullException(nameof(student));
            
        if (course is null) 
            throw new ArgumentNullException(nameof(course));
            
        if (course.EnrolledCount >= course.Capacity)
                  throw new CapacityReachedException(course.Code);

        // TODO2: Switch expression on student.GPA to classify academic standing
        string standing = student.GPA switch
        {
            >= 3.5m => "Honors",
            >= 2.5m => "GoodStanding",
            _ => "AcademicWarning"
        };

        Console.WriteLine($"{student.Name} is in {standing}.");

        // TODO3: Return a new EnrollmentRecord
        return new EnrollmentRecord(student.Id, course.Code, DateTime.UtcNow);
    }
}

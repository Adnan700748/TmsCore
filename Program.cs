// string? region = null;  

// string? upperRegion = region?.ToUpper();
// Console.WriteLine($"region(conditional):{upperRegion}"); 

// string? displayRegion = region??"Unassigned";
// Console.WriteLine($"region(coalesced):{displayRegion}"); 


// region ??= "Addis Ababa"; 
// Console.WriteLine($"Region (assigned): {region}"); 

// string studentName = "Adnan"; 
// string studentId = "STU-001"; 
// int enrollmentCount = 3; 
// decimal grantAmount = 1999.99m;  
// DateTime enrolledAt = DateTime.UtcNow; 
// string? campusRegion = null; 

// Console.WriteLine($"Student: {studentName} ({studentId})"); 
// Console.WriteLine($"Courses: {enrollmentCount}"); 
// Console.WriteLine($"Grant: {grantAmount:F2}"); 
// Console.WriteLine($"Enrolled: {enrolledAt:yyyy-MM-dd}"); 
// Console.WriteLine($"Campus: {campusRegion ?? "Not assigned"}"); 

// double grantPerStudent = 1999.99; 
// double totalAllocation = grantPerStudent * 100_000; 
// Console.WriteLine($"Total allocated (double): {totalAllocation}"); 

// decimal grantPerStudent = 1999.99m; 
// decimal totalAllocation = grantPerStudent * 100_000m; 

// Console.WriteLine($"Total allocated (decimal): {totalAllocation}"); 
// Console.WriteLine($"Total allocated (formatted): {totalAllocation:F2}");

// var enrollment = new EnrollmentRecord("STU-001", "CS-401", DateTime.UtcNow); 
// Console.WriteLine(enrollment); 


// var corrected = enrollment with { CourseCode = "CS-402" }; 
// Console.WriteLine(corrected); 

// var duplicate = new EnrollmentRecord("STU-001", "CS-401", enrollment.EnrolledAt); 
// Console.WriteLine($"Same data? {enrollment == duplicate}");  

// var course = new Course { Code = "CS-401", Title = "Advanced C#", Capacity = 30 }; 
// Console.WriteLine($"Course: {course.Title} (Capacity: {course.Capacity})"); 


// try 
// { 
// course.Capacity = -5; 
// } 
// catch (ArgumentOutOfRangeException ex) 
// { 
// Console.WriteLine($"Caught: {ex.Message}"); 
// } 

// try
// {
//   course.Title = ""; 
// } 

// catch (ArgumentException ex) 
// { 
// Console.WriteLine($"Caught: {ex.Message}"); 
// } 

// var s = new Student { Id = "S1", Name = "Abeba", Age = 20, GPA = 3.8m }; 
// Console.WriteLine($"Student: {s.Name}, GPA: {s.GPA}"); 

// // These should throw — try each one: 
// // new Student { Id = "S2", Name = "", Age = 20, GPA = 3.0m };         
// // new Student { Id = "S3", Name = "Test", Age = 12, GPA = 3.0m };   
// // new Student { Id = "S4", Name = "Test", Age = 20, GPA = 5.0m };  

// void PrintGradeReport(IEnumerable<IGradable> assessments) 
// { 
//     Console.WriteLine("--- Grade Report ---"); 
//     foreach (var item in assessments) 
//     { 
//         Console.WriteLine($"{item.Title}: {item.CalculateGrade():F2}%"); 
//     } 
// } 


// IGradable[] cohortAssessments = [ 
//     new Quiz { Title = "C# Basics", CorrectAnswers = 18, TotalQuestions = 20 }, 
//     new LabAssignment { Title = "Registration API", FunctionalityScore = 90m, CodeQualityScore = 85m } 
// ]; 

// PrintGradeReport(cohortAssessments); 


using TmsCore;

// ==========================================
// EXERCISE 4: TEST RUNNER
// ==========================================
Console.WriteLine("=== Exercise 4: Guard Clauses & Pattern Matching ===");
var service = new EnrollmentService();

// Test 1: Valid registration
var validStudent = new Student { Id = "S1", Name = "Abeba", Age = 20, GPA = 3.8m };
var validCourse = new Course { Code = "CS-401", Title = "Advanced C#", Capacity = 30 };
var result = service.ProcessRegistration(validStudent, validCourse);
Console.WriteLine($"Enrolled: {result.StudentId} in {result.CourseCode}\n");

// Test 2: Null student should throw
try
{
    service.ProcessRegistration(null, validCourse);
}
catch (ArgumentNullException ex)
{
    Console.WriteLine($"Guard caught: {ex.ParamName}\n");
}

// Test 3: Full course should throw
var fullCourse = new Course { Code = "CS-402", Title = "Full Course", Capacity = 1 };
fullCourse.EnrolledCount = 1;
try
{
    service.ProcessRegistration(validStudent, fullCourse);
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Business rule: {ex.Message}\n");
}


// ==========================================
// EXERCISE 5: THE ANALYTICS DASHBOARD
// ==========================================
Console.WriteLine("=== Exercise 5: Collections & LINQ ===");

// Step 1 Create the Student Data
List<Student> students = [
    new Student { Id = "S1", Name = "Abeba", Age = 22, GPA = 3.8m },
    new Student { Id = "S2", Name = "Kidane", Age = 21, GPA = 2.4m },
    new Student { Id = "S3", Name = "Dawit", Age = 20, GPA = 3.1m },
    new Student { Id = "S4", Name = "Sara", Age = 23, GPA = 3.9m },
    new Student { Id = "S5", Name = "Frehiwot", Age = 19, GPA = 2.0m },
    new Student { Id = "S6", Name = "Yonas", Age = 24, GPA = 3.5m },
    new Student { Id = "S7", Name = "Meron", Age = 22, GPA = 1.8m },
    new Student { Id = "S8", Name = "Tesfaye", Age = 21, GPA = 2.9m }
];

// Step 2 Build the Honors Leaderboard
var leaderboard = students
    .Where(s => s.GPA >= 3.5m)          // TODO1: Extract students where GPA is >= 3.5m
    .OrderByDescending(s => s.GPA)     // TODO2: Sort by GPA descending
    .Select(s => s.Name)               // TODO3: Project string names
    .ToList();                         // TODO4: Materialize into concrete List

Console.WriteLine($"Found {leaderboard.Count} Honors Students:");
foreach (var name in leaderboard)
{
    Console.WriteLine($"- {name}");
}

// Step 3 Class Average
// TODO5: Use LINQ to calculate average GPA
decimal averageGpa = students.Average(s => s.GPA);
Console.WriteLine($"\nClass Average GPA: {averageGpa:F2}");

// Step 4 Group by Academic Standing
// TODO6: Use .GroupBy with a switch expression
var standingGroups = students.GroupBy(s => s.GPA switch
{
    >= 3.5m => "Honors",
    >= 2.5m => "GoodStanding",
    >= 2.0m => "Probation",
    _       => "Academic Warning"
});

Console.WriteLine("\n--- Academic Standing Report ---");
foreach (var group in standingGroups)
{
    Console.WriteLine($"\n{group.Key} ({group.Count()}):");
    foreach (var s in group)
    {
        Console.WriteLine($"  {s.Name} GPA: {s.GPA}");
    }
}

// Step 5 Collection Expressions with Spread
// TODO7: Use spread operator (..) and append "Capstone"
string[] backendCourses = ["C#", "ASP.NET Core"];
string[] frontendCourses = ["TypeScript", "Angular"];
string[] allCourses = [.. backendCourses, .. frontendCourses, "Capstone"];

Console.WriteLine($"\nFull curriculum: {string.Join(", ", allCourses)}");

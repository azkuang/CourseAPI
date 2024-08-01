using CourseAPI.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<GetCoursesDto> courses = [
    new (
        1,
        "Progamming Languages",
        "Course about programming languages",
        20,
        "1"
    ),
    new (
        2,
        "Computer Vision",
        "Course about computer vision",
        30,
        "2"
    ),
    new (
        3,
        "Data Science",
        "Course about data science",
        20,
        "3"
    )
];

app.MapGet("courses", () => courses);

app.MapGet("courses/{id}", (int id) => {
    return courses.Find(course => course.Id == id);
}).WithName("GetCourse");

app.MapPost("courses", (CreateCourseDto newCourse) => {
    int id = courses.Count + 1;
    GetCoursesDto course = new (id, newCourse.Name, newCourse.Description, newCourse.NoOfChapters, newCourse.InstructorId);

    courses.Add(course);

    return Results.CreatedAtRoute("GetCourse", new {id = id}, course);
});

// app.MapGet("/", () => "Hello World!");

app.Run();

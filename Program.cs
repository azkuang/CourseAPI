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

// Gets courses data at the endpoint /courses
app.MapGet("courses", () => courses);

// Gets course data based on id
app.MapGet("courses/{id}", (int id) => {
    return courses.Find(course => course.Id == id);
}).WithName("GetCourse");

// Adds courses to the exsiting data
app.MapPost("courses", (CreateCourseDto newCourse) => {
    // Increments course id by one, generates a unique id for each course
    int id = courses.Count + 1;
    // creates new course to added using the GetCoursesDto
    GetCoursesDto course = new (id, newCourse.Name, newCourse.Description, newCourse.NoOfChapters, newCourse.InstructorId);

    courses.Add(course);

    // Returns the new course added, adds it with the "GetCourse" name and id
    return Results.CreatedAtRoute("GetCourse", new {id = id}, course);
});

// Update course contents
app.MapPut("courses/{id}", (int id, CreateCourseDto updatedCourse) => {
    // Checks if GetCoursesDto is null, then finds existing courses
    GetCoursesDto? currCourse = courses.Find(course => course.Id == id);
    // Returns 404 error if currCourse is null
    if (currCourse == null) {
        return Results.NotFound();
    }
    // Creates new course object with the updated changes
    GetCoursesDto newCourse = new (
        id,
        updatedCourse.Name,
        updatedCourse.Description,
        updatedCourse.NoOfChapters,
        updatedCourse.InstructorId
    );
    // Replaces old course with the updated version
    courses[id-1] = newCourse;
    // Sends 200 OK status code
    return Results.Ok();
});

// Delete operation for course data
app.MapDelete("courses/{id}", (int id) => {
    // Finds the index of the course based on id
    int courseID = courses.FindIndex(course => course.Id == id);
    // Return 404 error if course index is null
    if (courseID == -1) {
        return Results.NotFound();  
    }
    // Removes the course based on id
    courses.RemoveAt(id-1);
    // Sends 204 No Content response to signal deletion
    return Results.NoContent();
});

// app.MapGet("/", () => "Hello World!");

app.Run();

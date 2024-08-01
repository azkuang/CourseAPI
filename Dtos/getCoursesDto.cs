namespace CourseAPI.Dtos;

public record class GetCoursesDto(
    int Id,
    string Name,
    string Description,
    int NoOfChapter,
    string InstructorId
);
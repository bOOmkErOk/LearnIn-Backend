using System.Security.Claims;
using LearnIn.Application.Interfaces.Persistence;
using LearnIn.Domain.Entities.Courses;
using Microsoft.EntityFrameworkCore;

namespace LearnIn.Api.Projection.GraphQL.Courses;

[QueryType]
public sealed class CourseQueries
{
    public async Task<Course?> GetCourseById(
        int id,
        [Service] IDataContext context)
    {
        return await context.Courses
            .Include(c => c.Lessons)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<Course>> GetFeaturedCourses(
        [Service] IDataContext context)
    {
        return await context.Courses
            .Include(c => c.Lessons)
            .OrderBy(c => Guid.NewGuid())
            .Take(8)
            .ToListAsync();
    }

    public async Task<List<Course>> GetNewCourses(
        [Service] IDataContext context)
    {
        return await context.Courses
            .Include(c => c.Lessons)
            .OrderByDescending(c => c.CreatedAt)
            .Take(4)
            .ToListAsync();
    }

    public async Task<List<Course>> SearchCourses(
        string query,
        [Service] IDataContext context)
    {
        return await context.Courses
            .Include(c => c.Lessons)
            .Where(c => c.Title.Contains(query) || c.Description.Contains(query))
            .ToListAsync();
    }

    [UsePaging]
    [UseFiltering]
    public IQueryable<Course> GetCoursesByTeacher(
        int teacherId,
        [Service] IDataContext context)
    {
        return context.Courses
            .Include(c => c.Lessons)
            .Where(c => c.TeacherId == teacherId);
    }

    public async Task<bool> HasActiveSubscriptionToTeacher(
        int teacherId,
        [Service] IDataContext context,
        [Service] IHttpContextAccessor http)
    {
        var userIdClaim = http.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            return false;

        var now = DateTime.UtcNow;

        return await context.Subscriptions
            .AnyAsync(s => s.UserId == userId &&
                           s.TeacherId == teacherId &&
                           s.StartDate <= now &&
                           s.EndDate >= now);
    }

    public async Task<List<Lesson>> GetCourseLessons(
        int courseId,
        [Service] IDataContext context,
        [Service] IHttpContextAccessor http)
    {
        var userIdClaim = http.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            return new List<Lesson>();

        var course = await context.Courses
            .FirstOrDefaultAsync(c => c.Id == courseId);

        if (course == null)
            return new List<Lesson>();

        var now = DateTime.UtcNow;

        var hasAccess = course.TeacherId == userId || await context.Subscriptions
            .AnyAsync(s => s.UserId == userId &&
                           s.TeacherId == course.TeacherId &&
                           s.StartDate <= now &&
                           s.EndDate >= now);

        if (!hasAccess)
            return new List<Lesson>();

        return await context.Lessons
            .Where(l => l.CourseId == courseId)
            .OrderBy(l => l.SequenceNumber)
            .ToListAsync();
    }
}
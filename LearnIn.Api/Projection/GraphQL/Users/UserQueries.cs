using System.Security.Claims;
using LearnIn.Application.Interfaces.Persistence;
using LearnIn.Domain.Entities.Users;
using LearnIn.Domain.Enums.Users;
using Microsoft.EntityFrameworkCore;

namespace LearnIn.Api.Projection.GraphQL.Users;


public record TeacherDashboardStats(
    decimal Balance,
    int ActiveSubscribersCount,
    int TotalCoursesCount);

[QueryType]
public sealed class UserQueries
{
    public async Task<User?> GetMyProfile(
        [Service] IDataContext context,
        [Service] IHttpContextAccessor http)
    {
        var userIdClaim = http.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            return null;

        return await context.Users
            .Include(u => u.CreatedCourses)
            .FirstOrDefaultAsync(u => u.Id == userId);
    }

    [UsePaging]
    [UseFiltering]
    public IQueryable<User> GetTeachers([Service] IDataContext context)
    {
        return context.Users
            .Include(u => u.CreatedCourses)
            .Where(u => u.Role == UserEnums.Teacher);
    }

    public async Task<User?> GetTeacherById(
        int id,
        [Service] IDataContext context)
    {
        return await context.Users
            .Include(u => u.CreatedCourses)
                .ThenInclude(c => c.Lessons)
            .FirstOrDefaultAsync(u => u.Id == id && u.Role == UserEnums.Teacher);
    }

    public async Task<List<User>> SearchTeachers(
        string query,
        [Service] IDataContext context)
    {
        return await context.Users
            .Include(u => u.CreatedCourses)
            .Where(u => u.Role == UserEnums.Teacher &&
                       (u.FirstName.Contains(query) || u.LastName.Contains(query)))
            .ToListAsync();
    }

  
    public async Task<TeacherDashboardStats?> GetTeacherDashboard(
        [Service] IDataContext context,
        [Service] IHttpContextAccessor http)
    {
        var userIdClaim = http.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int teacherId))
            return null;

        var teacher = await context.Users
            .Include(u => u.CreatedCourses)
            .FirstOrDefaultAsync(u => u.Id == teacherId && u.Role == UserEnums.Teacher);

        if (teacher == null)
            return null;

        var now = DateTime.UtcNow;

        var activeSubscribers = await context.Subscriptions
            .CountAsync(s => s.TeacherId == teacherId && s.StartDate <= now && s.EndDate >= now);

        return new TeacherDashboardStats(
            Balance: teacher.Balance,
            ActiveSubscribersCount: activeSubscribers,
            TotalCoursesCount: teacher.CreatedCourses.Count);
    }
}
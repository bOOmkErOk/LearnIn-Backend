using System.Security.Claims;
using LearnIn.Application.Interfaces.Persistence;
using LearnIn.Domain.Entities.Subscriptions;
using Microsoft.EntityFrameworkCore;

namespace LearnIn.Api.Projection.GraphQL.Subscriptions;

[QueryType]
public sealed class SubscriptionQueries
{
    public async Task<List<Subscription>> GetMyActiveSubscriptions(
        [Service] IDataContext context,
        [Service] IHttpContextAccessor http)
    {
        var userIdClaim = http.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            return new List<Subscription>();

        var now = DateTime.UtcNow;

        return await context.Subscriptions
            .Include(s => s.Teacher)
            .Where(s => s.UserId == userId && s.StartDate <= now && s.EndDate >= now)
            .OrderByDescending(s => s.EndDate)
            .ToListAsync();
    }

    public async Task<List<Subscription>> GetMySubscriptionHistory(
        [Service] IDataContext context,
        [Service] IHttpContextAccessor http)
    {
        var userIdClaim = http.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            return new List<Subscription>();

        return await context.Subscriptions
            .Include(s => s.Teacher)
            .Where(s => s.UserId == userId)
            .OrderByDescending(s => s.CreatedAt)
            .ToListAsync();
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
}
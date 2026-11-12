using LearnIn.Application.Interfaces.Persistence;
using LearnIn.Domain.Entities.Subscriptions;
using LearnIn.Domain.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnIn.Application.Features.Subscriptions.CreateSubscription;


public class CreateSubscriptionCommandHandler : IRequestHandler<CreateSubscriptionCommand, Result<bool>>
{
    private readonly IDataContext _context;
    
    private const decimal TeacherSharePercentage = 0.85m; 

    public CreateSubscriptionCommandHandler(IDataContext context)
    {
        _context = context;
    }

    public async Task<Result<bool>> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
    {
        if (request.DurationMonths <= 0)
        {
            return Result<bool>.Failure(Errors.InvalidSubscriptionDuration());
        }

        if (request.PricePaid <= 0)
        {
            return Result<bool>.Failure(Errors.SubscriptionPriceInvalid());
        }

        var teacher = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == request.TeacherId, cancellationToken);

        if (teacher == null)
        {
            return Result<bool>.Failure(Errors.TeacherNotFound());
        }

        var now = DateTime.UtcNow;
        var expirationDate = now.AddMonths(request.DurationMonths); 

        var subscription = new Subscription
        {
            UserId = request.UserId,
            TeacherId = request.TeacherId,
            StartDate = now,
            EndDate = expirationDate,
            PricePaid = request.PricePaid
        };

        decimal teacherEarning = request.PricePaid * TeacherSharePercentage;
        teacher.Balance += teacherEarning;

        await _context.Subscriptions.AddAsync(subscription, cancellationToken);
        
        var result = await _context.SaveChangesAsync(cancellationToken);

        return result > 0 
            ? Result<bool>.Success(true) 
            : Result<bool>.Failure(Errors.ServerError());
    }
}
namespace LearnIn.Domain.Results;

public sealed record Errors(int Status, string Message)
{
    // --- User & Profile Errors qvevitt ---
    public static Errors UserNotFound() =>
        new(404, "User not found");

    public static Errors UserByIdNotFound(int id) =>
        new(404, $"User with ID {id} not found");

    public static Errors EmailEmpty() =>
        new(400, "Email cannot be empty");

    public static Errors EmailInvalid() =>
        new(400, "Invalid email format. Please enter a valid email address");

    public static Errors EmailExists() =>
        new(409, "This email is already registered. Please use a different email");

    public static Errors FirstNameEmpty() =>
        new(400, "First name cannot be empty");

    public static Errors LastNameEmpty() =>
        new(400, "Last name cannot be empty");

    public static Errors PasswordEmpty() =>
        new(400, "Password cannot be empty");

    public static Errors PasswordWeak() =>
        new(400, "Password must be at least 8 characters with uppercase, lowercase, and numbers");

    // --- Authentication Errors qvevitt ---
    
    public static Errors InvalidCode() =>
        new(400, "The verification code you entered is incorrect");

    public static Errors CodeExpired() =>
        new(400, "The verification code has expired. Please request a new one");
    
    public static Errors Unauthorized() =>
        new(401, "Your session has expired. Please log in again");

    public static Errors InvalidCredentials() =>
        new(401, "Incorrect email or password. Please try again");

    public static Errors Forbidden() =>
        new(403, "You don't have permission to perform this action");

    public static Errors EmailNotVerified() =>
        new(403, "Please verify your email before logging in");

    // --- Course & Lesson Errors qvevitt ---
    public static Errors CourseNotFound() =>
        new(404, "Requested course not found");

    public static Errors CourseByIdNotFound(int id) =>
        new(404, $"Course with ID {id} not found");

    public static Errors CourseAlreadyActivated() =>
        new(409, "You have already activated this course and have full access to it");

    public static Errors LessonNotFound() =>
        new(404, "Requested lesson not found");

    public static Errors LessonByIdNotFound(int id) =>
        new(404, $"Lesson with ID {id} not found");

    public static Errors SequenceNumberInvalid() =>
        new(400, "Lesson sequence number must be greater than 0");

    public static Errors VideoUrlEmpty() =>
        new(400, "Video URL cannot be empty");

    // --- Subscription Errors qvevitt ---
    public static Errors SubscriptionNotFound() =>
        new(404, "Active subscription not found");

    public static Errors SubscriptionExpired() =>
        new(403, "Your subscription has expired. Please renew it to access the content");

    public static Errors NoActiveSubscription() =>
        new(403, "Access denied. An active monthly or 6-month subscription is required to unlock this resource");

    public static Errors InvalidSubscriptionDuration() =>
        new(400, "Subscription duration must be greater than 0 months");

    public static Errors SubscriptionPriceInvalid() =>
        new(400, "Subscription price paid must be greater than 0");

    // --- Teacher & Balance Errors qvevitt ---
    public static Errors TeacherNotFound() =>
        new(404, "The teacher assigned to this course was not found or is unregistered");

    public static Errors NotATeacher() =>
        new(403, "Only users registered as Teachers can upload courses and manage analytics");

    public static Errors InsufficientBalance() =>
        new(400, "Insufficient token balance to activate this course");

    public static Errors PayoutFailed() =>
        new(500, "Failed to process the teacher's earning allocation for this action");

    // --- Generic Errors qvevitt ---
    public static Errors ValidationFailed() =>
        new(400, "Validation failed. Please check your input and try again");

    public static Errors NotFound() =>
        new(404, "The requested resource was not found");

    public static Errors ServerError() =>
        new(500, "An unexpected error occurred. Please try again later");
}

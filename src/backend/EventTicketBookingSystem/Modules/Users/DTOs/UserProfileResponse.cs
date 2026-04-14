namespace EventTicketSystem.Modules.Users.DTOs;

public record UserProfileResponse(
    Guid Id, 
    string Email, 
    string Role, 
    DateTime CreatedAt
);
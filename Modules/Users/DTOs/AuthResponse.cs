namespace EventTicketSystem.Modules.Users.DTOs;

public record AuthResponse(string Token, string Email, string Role);

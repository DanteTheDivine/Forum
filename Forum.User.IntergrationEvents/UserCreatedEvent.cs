using MediatR;

namespace Forum.User.IntergrationEvents;

public record UserCreatedEvent(string Id,string Email,string Username):INotification;
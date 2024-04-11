using Forum.User.IntergrationEvents;
using MediatR;

namespace Forum.Email.Core;

public class UserVerificationEmailSender:INotificationHandler<UserCreatedEvent>
{
    public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine("asdsd");
        
    }
}
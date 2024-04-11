namespace Forum.Abstractions.Events;

public abstract class IntegrationEvent
{
    public DateTime OccurredDate { get;  }
    
    public IntegrationEvent(DateTime occurredDate)
    {
        OccurredDate = occurredDate;
    }
}
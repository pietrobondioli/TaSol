namespace Domain.Events;

public class LocationCreatedEvent : BaseEvent
{
    public LocationCreatedEvent(Location location)
    {
        Location = location;
    }

    public Location Location { get; }
}
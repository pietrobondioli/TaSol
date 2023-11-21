namespace Domain.Events;

public class LocationUpdatedEvent : BaseEvent
{
    public LocationUpdatedEvent(Location location)
    {
        Location = location;
    }

    public Location Location { get; }
}

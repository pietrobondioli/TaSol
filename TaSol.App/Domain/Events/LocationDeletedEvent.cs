namespace Domain.Events;

public class LocationDeletedEvent : BaseEvent
{
    public LocationDeletedEvent(Location location)
    {
        Location = location;
    }

    public Location Location { get; }
}

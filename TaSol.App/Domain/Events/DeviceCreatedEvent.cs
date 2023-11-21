namespace Domain.Events;

public class DeviceCreatedEvent : BaseEvent
{
    public DeviceCreatedEvent(Device device)
    {
        Device = device;
    }

    public Device Device { get; }
}

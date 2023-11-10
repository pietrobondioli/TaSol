namespace Domain.Events;

public class DeviceUpdatedEvent : BaseEvent
{
    public DeviceUpdatedEvent(Device device)
    {
        Device = device;
    }

    public Device Device { get; }
}

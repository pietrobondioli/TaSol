namespace Domain.Events;

public class DeviceDeletedEvent : BaseEvent
{
    public DeviceDeletedEvent(Device device)
    {
        Device = device;
    }

    public Device Device { get; }
}

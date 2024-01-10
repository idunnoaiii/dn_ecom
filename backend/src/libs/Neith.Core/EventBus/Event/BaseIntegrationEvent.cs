using System.Runtime.InteropServices;

namespace Neith.Core.EventBus.Event;

public class BaseIntegrationEvent
{
    public Guid Id { get; private set; }
    public DateTime CreationDate { get; private set; }

    public BaseIntegrationEvent() : this(Guid.NewGuid(), DateTime.UtcNow)
    {
    }

    public BaseIntegrationEvent(Guid id, DateTime creationDate)
    {
        Id = id;
        CreationDate = creationDate;
    }

}

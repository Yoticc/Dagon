namespace Dagon;
public class WindowEvents
{
    public EventBus<ResizeEvent> Resize = new();
    public EventBus<MoveEvent> Move = new();
    public EventBus<Event> Close = new();

    public class ResizeEvent : Event
    {
        public int Width, Height;
    }

    public class MoveEvent : Event
    {
        public int X, Y;
    }
}
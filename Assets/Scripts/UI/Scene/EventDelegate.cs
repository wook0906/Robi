using System;

internal class EventDelegate
{
    private Func<object> p;

    public EventDelegate(Func<object> p)
    {
        this.p = p;
    }
}
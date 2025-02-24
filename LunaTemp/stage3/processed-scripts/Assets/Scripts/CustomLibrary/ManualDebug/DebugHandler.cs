using UnityEngine;

public abstract class BaseDebugHandler
{
    protected BaseDebugHandler _next;
    protected string _message;
    protected object[] _args;

    public void Initialize(string message, object[] args)
    {
        _message = message;
        _args = args;
    }

    public BaseDebugHandler SetNext(BaseDebugHandler handler)
    {
        _next = handler;
        return this;
    }

    public abstract void Execute();

    protected string FormatMessage()
    {
        return _args != null ? string.Format(_message, _args) : _message;
    }
}

public class NormalLog : BaseDebugHandler
{
    public override void Execute()
    {
        Debug.Log(FormatMessage());
        _next?.Execute();
    }
}

public class WarningLog : BaseDebugHandler
{
    public override void Execute()
    {
        Debug.LogWarning(FormatMessage());
        _next?.Execute();
    }
}

public class ErrorLog : BaseDebugHandler
{
    public override void Execute()
    {
        Debug.LogError(FormatMessage());
        _next?.Execute();
    }
}

public class LogBreak : BaseDebugHandler
{
    public override void Execute()
    {
        if (!string.IsNullOrEmpty(_message))
        {
            Debug.Log(FormatMessage());
        }
        Debug.Break();
        _next?.Execute();
    }
}

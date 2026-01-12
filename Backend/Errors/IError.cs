namespace PomoSyncAPI.Backend.Errors;

public interface IError
{
    public string Code { get; }
    public string Message(params dynamic?[] args);
    public string Message(string msg) => msg;
}
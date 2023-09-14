namespace SignalR
{
    /// <summary>
    /// Define the functionlity (contract) that the client has.
    /// </summary>
    public interface IClientContract
    {
        Task MessageReceived(string message);
    }
}

namespace Teban.UI.Interfaces
{
    public interface ILocalSecureStorage
    {
        Task<string> GetAsync(string item);
        Task<bool> RemoveAsync(string item);
        Task<bool> SetAsync(string name, string value);
    }
}

namespace Teban.UI.Common;

public interface ILocalSecureStorage
{
    Task<string> GetAsync(string item);
    Task<bool> RemoveAsync(string item);
    Task<bool> SetAsync(string name, string value);
}

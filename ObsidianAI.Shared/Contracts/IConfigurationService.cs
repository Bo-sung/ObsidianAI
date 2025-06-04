namespace ObsidianAI.Shared.Contracts
{
    /// <summary>
    /// 서비스 설정 인터페이스
    /// </summary>
    public interface IConfigurationService
    {
        Task<T?> GetAsync<T>(string key) where T : class;
        Task SetAsync<T>(string key, T value) where T : class;
        Task<bool> ExistsAsync(string key);
        Task SaveAsync();
    }
}
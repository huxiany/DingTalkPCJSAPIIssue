namespace EaseSource.Dingtalk.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EaseSource.Dingtalk.Entity;

    public interface IDingtalkServices
    {
        Task<string> GetAccessTokenAsync();

        Task<string> GetTicketAsync(string accessToken);

        Task<Dictionary<string, string>> GetPCConfigAsync(string clientUrl);
    }
}
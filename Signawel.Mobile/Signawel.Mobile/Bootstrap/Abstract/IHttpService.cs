using System.Net.Http;
using System.Threading.Tasks;

namespace Signawel.Mobile.Bootstrap.Abstract
{
    /// <summary>
    ///     Service that automatically checks if a request is unauthorized.
    /// </summary>
    public interface IHttpService
    {
        Task<HttpResponseMessage> GetAsync(string url);
        
        Task<HttpResponseMessage> DeleteAsync(string url);
        
        Task<HttpResponseMessage> PostAsync(string url, HttpContent content);
        
        Task<HttpResponseMessage> PutAsync(string url, HttpContent content);
        
        void SetToken(string token);
    }
}

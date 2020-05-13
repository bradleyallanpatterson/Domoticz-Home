using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace DomoticzHome.Services
{


    public class BaseProvider
    {
        protected string _baseUrl;

        protected HttpClient GetClient()
        {
            if (_baseUrl == "")
                return null;
            return GetClient(_baseUrl);
        }

        protected virtual HttpClient GetClient(string baseUrl)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);

            return client;
        }

        protected async Task<string> GetAsync(string url)
        {
            using (HttpClient client = GetClient())
            {
                if (client == null)
                    return "";
                try
                {
                    client.DefaultRequestHeaders.Add("Authorization", string.Format("Basic {0}", "YnJhZDplbGt5NDU0"));
                    var response = await client.GetAsync(url).ConfigureAwait(false);
                    if (response != null && !response.IsSuccessStatusCode)
                    {
                        //var error = await response.Content.ReadAsAsync<DomoticzApiError>();
                        //throw new DomoticzApiException(error.Message, response.StatusCode);
                    }
                    else if (response == null)
                    {
                        return null;
                    }
                    return await response.Content.ReadAsStringAsync();
                }
                catch (System.Threading.Tasks.TaskCanceledException tce)
                {
                    Debug.WriteLine(string.Format("TaskCanceledException Error: '{0}'", tce.InnerException));
                    return null;
                }
                catch (HttpRequestException ex)
                {
                    Debug.WriteLine(string.Format("HttpRequestException Error: '{0}'", ex.InnerException));
                    return null;
                    //throw new DomoticzApiException("", false, ex);
                }
            }
        }

        //// changed Task<T> to a void
        //protected async void Get<T>(string url)
        //{
        //    using (HttpClient client = GetClient())
        //    {
        //        try
        //        {
        //            var response = await client.GetAsync(url);
        //            if (!response.IsSuccessStatusCode)
        //            {
        //                var error = await response.Content.ReadAsAsync<DomoticzApiError>();
        //                var message = error != null ? error.Message : "";
        //                throw new DomoticzApiException(message, response.StatusCode);
        //            }
        //            return await response.Content.ReadAsAsync<T>();
        //            return false;
        //        }
        //        catch (HttpRequestException ex)
        //        {
        //            throw new DomoticzApiException(ex.Message, false, ex);
        //        }
        //        catch (UnsupportedMediaTypeException ex)
        //        {
        //            throw new DomoticzApiException(ex.Message, false, ex);
        //        }
        //    }
        //}

    }


}

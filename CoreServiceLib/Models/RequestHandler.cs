using RestSharp;

namespace UnitTest.Models
{
    public class RequestHandler
    {
        private readonly RestClient _client;

        public RequestHandler(string url)
        {
            _client = new RestClient(url);
        }

        public string SendRequest(object obj, string apiUrl, Method method)
        {
            var request = new RestRequest(apiUrl, method);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");//add header

            //add parameters
            var type = obj.GetType();
            var properties = type.GetProperties();
            foreach (var property in properties)
                request.AddParameter(property.Name, property.GetValue(obj, null));
            
            //send request
            var response = _client.Execute(request);
            var content = response.Content; // raw content as string

            return content;
        }
    }
}
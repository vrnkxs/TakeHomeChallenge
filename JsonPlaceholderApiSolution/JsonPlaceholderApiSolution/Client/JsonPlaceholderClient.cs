using RestSharp;
using Serilog;
using ApiTests.Utilities.Logging;

namespace ApiTests.Client
{
    /*
     * API client wrapper for JSONPlaceholder.
     * Includes all HTTP operations (GET, POST, PUT, DELETE)
     * and provides logging + retry logic for reliability.
     */
    public class JsonPlaceholderClient
    {
        private readonly RestClient _client;

        /*
         * Constructor initializes the RestClient with a base URL.
         * All requests will use this base URL automatically.
         */
        public JsonPlaceholderClient(string baseUrl)
        {
            _client = new RestClient(baseUrl);
        }

        /*
         * Sends a GET request to the given endpoint.
         * Logs the request and response details for debugging.
         * Wrapped with retry logic for handling transient failures.
         */
        public async Task<RestResponse> Get(string endpoint)
        {
            Logger.Log.Information("[REQUEST] GET {Endpoint}", endpoint);
            var request = new RestRequest(endpoint, Method.Get);
            var response = await _client.ExecuteAsync(request);

            Logger.Log.Information("[RESPONSE] Status Code: {Status}", response.StatusCode);
            Logger.Log.Information("[RESPONSE] Body: {Body}", response.Content);

            return await ExecuteWithRetryAsync(request);
        }

        /*
         * Sends a POST request with a JSON body.
         * Uses generics so any DTO object can be sent.
         * Logs request + response for transparency.
         */
        public async Task<RestResponse> Post<T>(string endpoint, T body) where T : class
        {
            Logger.Log.Information("[POST] Sending POST to: {Endpoint}", endpoint);
            Logger.Log.Debug("[POST] Request Body: {@Body}", body);

            var request = new RestRequest(endpoint, Method.Post);
            request.AddJsonBody(body);
            var response = await _client.ExecuteAsync(request);

            Logger.Log.Information("[POST] Status: {StatusCode}", response.StatusCode);
            Logger.Log.Debug("[POST] Response Body: {Body}", response.Content);

            return await ExecuteWithRetryAsync(request);
        }

        /*
         * Sends a PUT request to update an existing record.
         * Accepts any request body type using generics.
         */
        public async Task<RestResponse> Put<T>(string endpoint, T body) where T : class
        {
            Logger.Log.Information("[PUT] Sending PUT to: {Endpoint}", endpoint);
            Logger.Log.Debug("[PUT] Request Body: {@Body}", body);

            var request = new RestRequest(endpoint, Method.Put);
            request.AddJsonBody(body);
            var response = await _client.ExecuteAsync(request);

            Logger.Log.Information("[PUT] Status: {StatusCode}", response.StatusCode);
            Logger.Log.Debug("[PUT] Response Body: {Body}", response.Content);

            return await ExecuteWithRetryAsync(request);
        }

        /*
         * Sends a DELETE request for removing a resource.
         * Logs the response to confirm deletion results.
         */
        public async Task<RestResponse> Delete(string endpoint)
        {
            Logger.Log.Information("[DELETE] Sending DELETE to: {Endpoint}", endpoint);

            var request = new RestRequest(endpoint, Method.Delete);
            var response = await _client.ExecuteAsync(request);

            Logger.Log.Information("[DELETE] Status: {StatusCode}", response.StatusCode);
            Logger.Log.Debug("[DELETE] Response Body: {Body}", response.Content);

            return await ExecuteWithRetryAsync(request);
        }

        /*
         * Retry logic for all HTTP calls.
         * Retries the request if it fails due to transient issues.
         * Makes the client more stable.
         */
        private async Task<RestResponse> ExecuteWithRetryAsync(RestRequest request, int retries = 3, int delayMs = 500)
        {
            RestResponse response = null;

            for (int attempt = 1; attempt <= retries; attempt++)
            {
                response = await _client.ExecuteAsync(request);

                if (response.IsSuccessful)
                    return response;

                Log.Warning("[RETRY] Attempt {Attempt}/{Retries} failed. Status: {StatusCode}",
                    attempt, retries, response.StatusCode);

                await Task.Delay(delayMs);
            }
            return response;
        }
    }
}

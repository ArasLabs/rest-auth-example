using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;


namespace ODataExample
{
    class Program
    {
		const string innovatorUrl = "http://192.168.168.17/12-1-11584"; // base Innovator url
		const string innovatorUsername = "admin"; // Innovator user name
		const string innovatorPassword = "607920B64FE136F9AB2389E371852AF2"; // MD5 hash of Innovator user password (mush be SHA hash in case of FIPS)
        const string innovatorDatabase = "12-1-11584"; // database name, could be obtained from innovatorServerUrl+"dblist.aspx"
        const string oauthServerClientId = "IOMApp"; // must be registered in authorization server's oauth.config file

        const string innovatorServerDiscoveryUrl = innovatorUrl + "/Server/OAuthServerDiscovery.aspx";



        static void Main(string[] args)
        {

            //
            // Get authorization server url
            // ============================
            var oauthServerUrl = GetOAuthServerUrl(innovatorServerDiscoveryUrl);

            if (oauthServerUrl == null)
                Environment.Exit(0);

            //
            // Get token endpoint
            // ==================
            var oauthServerConfigurationUrl = oauthServerUrl + ".well-known/openid-configuration";

            var tokenUrl = GetTokenEndpoint( oauthServerConfigurationUrl );

            if (tokenUrl == null)
                Environment.Exit(0);


            //
            // Get access token
            // ================
            var body = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("client_id", oauthServerClientId),
                new KeyValuePair<string, string>("username", innovatorUsername),
                new KeyValuePair<string, string>("password", innovatorPassword),
                new KeyValuePair<string, string>("database", innovatorDatabase),
            });

            var tokenData = GetJson(tokenUrl, null, body);

            if(tokenData == null)
                Environment.Exit(0);


            //
            // Request parts using OData
            // =========================
            string accessToken = tokenData.access_token;
            string odataUrl = innovatorUrl + "/server/odata/Part";

            var parts = GetJson(odataUrl, accessToken);

            Console.WriteLine(parts);
        }
		
		
		static dynamic GetJson( string url, string accessToken = null, HttpContent body = null )
        {
            using (HttpClient client = new HttpClient())
            {

                client.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue("application/json") );

                if(accessToken != null)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                }

                HttpResponseMessage response;
                if (body == null)
                {
                    response = client.GetAsync(url).Result;
                }
                else
                {
                    response = client.PostAsync(url, body).Result;
                }

                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<dynamic>().Result;
                }
                else
                {
                    Console.WriteLine("{0}: {1} ({2})", url, (int)response.StatusCode, response.ReasonPhrase);
                    return null;
                }

            }
        }

        static string GetOAuthServerUrl( string url )
		{
            var discovery = GetJson(url);

            return discovery?.locations[0]?.uri;
		}

        static string GetTokenEndpoint(string url)
        {
            var configuration = GetJson(url);

            return configuration?.token_endpoint;
        }

    }
}

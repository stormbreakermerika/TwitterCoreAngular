using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TwitterCoreAngular.TwitterResponseData;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TwitterCoreAngular
{
    [Route("[controller]")]
    [ApiController]
    public class TwitterController : ControllerBase
    {
        // GET: api/<TwitterController>
        [HttpGet]
        public async Task<IEnumerable<Tweet>> Get()
        {

            List<Tweet> tweets = new List<Tweet>();
            HttpClient twitterClient = new HttpClient();

            twitterClient.BaseAddress = new System.Uri("https://api.twitter.com");
            twitterClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "AAAAAAAAAAAAAAAAAAAAACp4PgEAAAAADzpiA%2F4tXIXqo953R4BSqqjiivA%3D2cxDJH79lyq9By4h4eN4T8suHyWSa6VASGUM7Pt3oZX9DYLws3");


            try
            {
                HttpResponseMessage httpResponse = await twitterClient.GetAsync("/2/tweets/search/recent?query=" + WebUtility.UrlEncode("(#azure OR #dotnetcore) -is:retweet") + "&expansions=attachments.media_keys&media.fields=preview_image_url,url,width,height");
                string stringContent = await httpResponse.Content.ReadAsStringAsync();
                TwitterResponse respJson = JsonConvert.DeserializeObject<TwitterResponse>(stringContent);
                tweets = respJson.Tweets;
            }
            catch (Exception e)
            {
                Tweet errorTweet = new Tweet()
                {
                    Text = e.Message,
                    Id = "-500"
                };
                tweets.Add(errorTweet);
            }

            return tweets;

        }
    }
}

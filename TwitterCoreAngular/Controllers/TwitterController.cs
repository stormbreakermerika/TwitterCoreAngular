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
        private readonly IHttpClientFactory _httpClientFactory;
        public TwitterController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        // GET: api/<TwitterController>
        [HttpGet]
        public async Task<IEnumerable<Tweet>> Get()
        {

            List<Tweet> tweets = new List<Tweet>();
            //HttpClient twitterClient = new HttpClient();
            HttpClient twitterClient = _httpClientFactory.CreateClient("tweets");

            try
            {
                //interesting thing about this line is the expansions I believe is what cause it to be full text of the tweet... at least according to: https://twittercommunity.com/t/how-do-i-get-full-tweet-text-in-v2/142129/3
                HttpResponseMessage httpResponse = await twitterClient.GetAsync("/2/tweets/search/recent?query=" + WebUtility.UrlEncode("(#azure OR #dotnetcore) -is:retweet") + "&expansions=attachments.media_keys&media.fields=preview_image_url,url,width,height&max_results=15");

                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new InvalidOperationException("Twitter API has an error");
                }

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

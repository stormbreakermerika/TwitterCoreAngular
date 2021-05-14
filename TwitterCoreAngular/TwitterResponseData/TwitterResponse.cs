using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TwitterCoreAngular.TwitterResponseData
{
    public class TwitterResponse
    {
        [JsonProperty("data")]
        public List<Tweet> Tweets { get; set; }
        public TweetMeta Meta { get; set; }
    }
}

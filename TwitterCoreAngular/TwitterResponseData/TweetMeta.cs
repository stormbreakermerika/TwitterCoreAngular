using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterCoreAngular.TwitterResponseData
{
    public class TweetMeta
    {
        public string NewestId { get; set; }
        public string OldestId { get; set; }
        public int ResultCount { get; set; }
        public string NextToken { get; set; }
    }
}

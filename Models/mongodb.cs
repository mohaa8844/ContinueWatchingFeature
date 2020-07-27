using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContinueWatchingFeature.Models
{
    

        public class MongoWatching
        {

            [BsonId]
            [BsonRepresentation(BsonType.ObjectId)]
            public String Id { get; set; }
            public String User_id { get; set; }
            public int Media_Id { get; set; }
            public int Type { get; set; }
            public int SeekPosition { get; set; }
        }
        public class watchingsDatabaseSettings : IwatchingsDatabaseSettings
        {
            public string CollectionName { get; set; }
            public string mongoConnection { get; set; }
            public string DatabaseName { get; set; }
        }

        public interface IwatchingsDatabaseSettings
        {
            string CollectionName { get; set; }
            string mongoConnection { get; set; }
            string DatabaseName { get; set; }
        }
    
}

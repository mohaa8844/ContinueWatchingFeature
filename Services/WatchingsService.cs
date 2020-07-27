using ContinueWatchingFeature.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace ContinueWatchingFeature.Services
{
    public class WatchingsService
    {
        private readonly IMongoCollection<MongoWatching> _mongoWatchings;

        public WatchingsService(IwatchingsDatabaseSettings settings)
        {
            var client = new MongoClient(settings.mongoConnection);
            var database = client.GetDatabase(settings.DatabaseName);

            _mongoWatchings = database.GetCollection<MongoWatching>(settings.CollectionName);
        }

        public List<MongoWatching> Get() =>
            _mongoWatchings.Find(watching => true).ToList();
        public List<MongoWatching> Get(string id) =>
            _mongoWatchings.Find(watching => watching.User_id==id).ToList();
        public List<MongoWatching> Get(string id, List<int> epsiodes) =>
            _mongoWatchings.Find(x =>x.User_id==id && x.Type == 1 && epsiodes.Contains(x.Media_Id)).ToList();

        public MongoWatching Get(string user_id,int media_id,int type) =>
            _mongoWatchings.Find<MongoWatching>(watching => watching.User_id == user_id && watching.Media_Id==media_id && watching.Type==type).FirstOrDefault();
        
        public MongoWatching Create(MongoWatching watching)
        {
            _mongoWatchings.InsertOne(watching);
            return watching;
        }

        public void Update(string user_id, int media_id, int type, MongoWatching watchingIn) =>
            _mongoWatchings.ReplaceOne(watching => watching.User_id == user_id && watching.Media_Id == media_id && watching.Type == type, watchingIn);

        public void Update(string id, MongoWatching watchingIn) =>
            _mongoWatchings.ReplaceOne(watching => watching.Id==id, watchingIn);

        public void Remove(MongoWatching watchingIn) =>
            _mongoWatchings.DeleteOne(watching => watching.User_id == watchingIn.User_id && watching.Media_Id == watchingIn.Media_Id && watching.Type == watchingIn.Type);

        public void Remove(string user_id, int media_id,int type) =>
            _mongoWatchings.DeleteOne(watching => watching.User_id == user_id && watching.Media_Id == media_id && watching.Type == type);
    }
}
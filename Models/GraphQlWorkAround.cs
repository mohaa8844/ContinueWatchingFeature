using ContinueWatchingFeature.Data;
using ContinueWatchingFeature.Services;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContinueWatchingFeature.Models
{
    public class MongoType : ObjectGraphType<MongoWatching>
    {
        public MongoType()
        {
            Field(x => x.Id);
            Field(x => x.User_id);
            Field(x => x.Media_Id);
            Field(x => x.Type);
            Field(x => x.SeekPosition);
        }
    }


    public class RootQuery : ObjectGraphType
    {
        private readonly ContinueWatchingFeatureContext _context;
        private readonly WatchingsService _ws;
        public RootQuery(ContinueWatchingFeatureContext context, WatchingsService ws)
        {

            _ws = ws;

            Field<ListGraphType<MongoType>>("watchings", resolve: context => GetWatchings());
            Field<MongoType>("watching",arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "user_id" }, new QueryArgument<IdGraphType> { Name = "media_id" }, new QueryArgument<IdGraphType> { Name = "typ" }), resolve: context =>
            {
                var user_id = context.GetArgument<String>("user_id");
                var media_id = context.GetArgument<int>("media_id");
                var type = context.GetArgument<int>("typ");
                return GetWatchings().Where(x => x.User_id == user_id && x.Media_Id==media_id && x.Type==type ).FirstOrDefault();
              });

        }


        List<MongoWatching> GetWatchings()
        {
            return _ws.Get();
        }
    }

    public class GraphSchema : Schema
    {
        public GraphSchema(IDependencyResolver resolver) :
           base(resolver)
        {
            Query = resolver.Resolve<RootQuery>();
        }
    }


}

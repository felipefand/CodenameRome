using MongoDB.Bson;

namespace CodenameRome.Utils
{
    public class DatabaseFilters
    {
        public BsonDocument[] getCategoriesFilter()
        {
            BsonDocument stage1 = new BsonDocument
                {
                    {
                        "$group", new BsonDocument{
                            { "_id", "$category" }
                        }
                    }
                };

            BsonDocument[] pipeline = new BsonDocument[] {
                    stage1
                };

            return pipeline;
        }
    }
}

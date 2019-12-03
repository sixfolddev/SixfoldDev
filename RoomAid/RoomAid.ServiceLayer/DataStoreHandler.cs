using System;
using MongoDB.Bson;
using MongoDB.Driver;

namespace RoomAid.ServiceLayer
{
    public class DataStoreHandler : ILogHandler
    {
        MongoClient client = new MongoClient("mongodb+srv://rwUser:4agLEh9JFz7P5QC4@roomaid-logs-s3nyt.gcp.mongodb.net/test?retryWrites=true&w=majority");
        IMongoDatabase db;
        public IMongoCollection<BsonDocument> collection;
        public DataStoreHandler()
        {
            string collectionName = "Mongo_" + DateTime.Now.ToString("yyyyMMdd");
            db = client.GetDatabase("test");
            collection = db.GetCollection<BsonDocument>(collectionName);
        }
        // <summary>
        // Connects to the database and the collection specified by the time value stored within the log message
        // Creates a new document and writes it to the collection
        // <\summary>

        // TODO: Write Asynch Database Write
        public bool WriteLog(LogMessage logMessage)
        {
            for (var i = 0; i < 4; i++)
            {
                try
                {
                    var document = new BsonDocument
                    {
                        {"LogID",BsonValue.Create(logMessage.LogGUID) },
                        {"DateTime", BsonValue.Create(logMessage.Time) },
                        {"Class",BsonValue.Create(logMessage.CallingClass)},
                        {"Method",BsonValue.Create(logMessage.CallingMethod) },
                        {"Level",BsonValue.Create(logMessage.Level) },
                        {"UserID",BsonValue.Create(logMessage.UserID) },
                        {"SessionID",BsonValue.Create(logMessage.SessionID) },
                        {"Text",BsonValue.Create(logMessage.Text) }
                    };
                    collection.InsertOne(document);
                    return true;
                }
                //TODO: Call error handling exception handler
                catch (Exception e)
                {
                    if (i == 3)
                    {
                        throw e;
                    }
                }
            }
            return false;
        }
        public bool DeleteLog(LogMessage logMessage)
        {
            for (var i = 0; i < 4; i++)
            {
                try
                {
                    var deleteFilter = Builders<BsonDocument>.Filter.Eq("LogID", logMessage.LogGUID);
                    collection.FindOneAndDelete(deleteFilter);

                    return true;
                }
                //TODO: Error Handling exception handler
                catch (Exception e)
                {
                    if (i == 3)
                    {
                        throw e;
                    }
                }
            }
            return false;
        }
    }
}

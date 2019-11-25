using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace RoomAid.ServiceLayer
{
    class DataStoreHandler : ILogHandler
    {
        public bool DeleteLog(LogMessage logMessage)
        {
            return true;
        }

        public bool WriteLog(LogMessage logMessage)
        {
            var client = new MongoClient("mongodb+srv://<rwUser>:<readwrite>@logs-s3nyt.gcp.mongodb.net/test?retryWrites=true&w=majority");
            var database = client.GetDatabase("test");
            var collection = database.GetCollection<BsonDocument>("Logs");

            var document = new BsonDocument
            {
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
    }
}

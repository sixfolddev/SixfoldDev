<<<<<<< HEAD

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
=======
﻿using System;
>>>>>>> Michell
using MongoDB.Bson;
using MongoDB.Driver;

namespace RoomAid.ServiceLayer
{
    class DataStoreHandler : ILogHandler
    {
        // <summary>
        // Connects to the database and the collection specified by the time value stored within the log message
        // Creates a new document and writes it to the collection
        // <\summary>

        // TODO: Write Asynch Database Write
        public bool WriteLog(LogMessage logMessage)
        {
<<<<<<< HEAD
            for(var i = 0; i < 4; i++)
            {
                try
                {
                    string collectionName = "Mongo_" + logMessage.Time.toString("yyyyMMdd");
=======
            for (var i = 0; i < 4; i++)
            {
                try
                {
                    string collectionName = "Mongo_" + logMessage.Time.ToString("yyyyMMdd");
>>>>>>> Michell
                    var client = new MongoClient("mongodb+srv://<rwUser>:<readwrite>@logs-s3nyt.gcp.mongodb.net/test?retryWrites=true&w=majority");
                    var database = client.GetDatabase("test");
                    var collection = database.GetCollection<BsonDocument>(collectionName);
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
<<<<<<< HEAD
=======
            return false;
>>>>>>> Michell
        }
        public bool DeleteLog(LogMessage logMessage)
        {
            for (var i = 0; i < 4; i++)
            {
                try
                {
<<<<<<< HEAD
                    string collectionName = "Mongo_" + logMessage.Time.toString("yyyyMMdd");
                    var client = new MongoClient("mongodb+srv://<rwUser>:<readwrite>@logs-s3nyt.gcp.mongodb.net/test?retryWrites=true&w=majority");
                    var database = client.GetDatabase("test");
                    var collection = database.GetCollection<BsonDocument>(collectionName);
                    collection.FindOneAndDelete(Builders<BsonDocument>).Filter.Eq("LogID", logMessage.LogGUID);
                }
                //TODO: Error Handling exception handler
                catch(Exception e)
=======
                    string collectionName = "Mongo_" + logMessage.Time.ToString("yyyyMMdd");
                    var client = new MongoClient("mongodb+srv://<rwUser>:<readwrite>@logs-s3nyt.gcp.mongodb.net/test?retryWrites=true&w=majority");
                    var database = client.GetDatabase("test");
                    var collection = database.GetCollection<BsonDocument>(collectionName);
                    collection.FindOneAndDelete(Builders<BsonDocument>.Filter
                        .Eq("LogID", logMessage.LogGUID));
                }
                //TODO: Error Handling exception handler
                catch (Exception e)
>>>>>>> Michell
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

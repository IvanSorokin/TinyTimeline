﻿using DataAccess.Concrete.Mappers;
using DataAccess.Documents;
using DataAccess.Interfaces.Mappers;
using Domain.Objects;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using StructureMap;

namespace DataAccess
{
    public class DataAccessRegistry : Registry
    {
        private readonly IConfigurationRoot appSettings = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        public DataAccessRegistry()
        {           
            RegisterCollection<TimelineEventDocument>("timelineEvents");
            For(typeof(ITwoWayMapper<TimelineEventDocument, TimelineEvent>)).Use(typeof(TimelineEventsMapper));
        }

        private void RegisterCollection<T>(string collectionName)
        {
            var connectionString = appSettings["MongoConnection:ConnectionString"];
            var dbString = appSettings["MongoConnection:Database"];
            var collection = new MongoClient(connectionString).GetDatabase(dbString).GetCollection<T>(collectionName);
            For<IMongoCollection<T>>().Use(collection);
        }
    }
}
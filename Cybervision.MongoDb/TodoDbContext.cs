﻿//using Microsoft.Extensions.Configuration;
//using MongoDB.Driver;

//public class MongoDbContext
//{
//    private readonly IMongoDatabase _database;
//    private readonly IConfiguration _configuration;
//    private readonly string _connectionString;

//    public MongoDbContext(IConfiguration configuration)
//    {
//        _configuration = configuration;
//        _connectionString = _configuration[""];
//        var databaseName = _configuration["MongoSettings:DatabaseName"];

//        var client = new MongoClient(_connectionString);
//        _database = client.GetDatabase(databaseName);
//    }

//    public IMongoCollection<T> GetCollection<T>(string collectionName)
//    {
//        return _database.GetCollection<T>(collectionName);
//    }
//}
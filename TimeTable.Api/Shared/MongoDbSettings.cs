﻿namespace TimeTable.Api.Shared;

public sealed class MongoDbSettings
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
}
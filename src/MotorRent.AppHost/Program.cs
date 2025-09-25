var builder = DistributedApplication.CreateBuilder(args);
var cache = builder.AddRedis("cache");

var rabbitMqPassword = builder.AddParameter("RabbitMqPassword", true);
var rabbitMq = builder.AddRabbitMQ("RabbitMq", password: rabbitMqPassword)
    .WithDataVolume()
    .WithVolume("/etc/rabbitmq")
    .WithManagementPlugin();

var postgresPassword = builder.AddParameter("PostgresInstancePassword", true);
var postgresPort = int.Parse(builder.AddParameter("PostgresInstancePort", true).Resource.Value);

var postgres = builder.AddPostgres("PostgresInstance", postgresPassword, port: postgresPort)
    .WithDataVolume();

var database = postgres.AddDatabase("MotorRent", "MotorRent");

var apiService = builder.AddProject<Projects.MotorRent_ApiService>("apiservice")
    .WithReference(rabbitMq)
    .WaitFor(rabbitMq)
    .WithReference(database)
    .WaitFor(database);

builder.AddProject<Projects.MotorRent_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();

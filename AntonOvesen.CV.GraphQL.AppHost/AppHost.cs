var builder = DistributedApplication.CreateBuilder(args);
#pragma warning disable ASPIRECERTIFICATES001

var db = builder.AddMySql("mysql")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithoutHttpsCertificate();

var mysql = db.AddDatabase("graphql");

builder.AddProject<Projects.AntonOvesen_CV_GraphQL>("antonovesen-cv-graphql")
    .WithReference(db)
    .WaitFor(db);

builder.Build().Run();

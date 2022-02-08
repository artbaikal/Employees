using Employees.Models;
using Grpc.Net.Client;
using GrpcService1;
//using GrpcService1;
using System.Net.Http;
//using GrpcService1;

namespace Employees.Services.GRPC
{
    internal class grpcClient
    {
        
        public static int AddNewPersonRequest(Employee empl)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            using var channel = GrpcChannel.ForAddress("https://127.0.0.1:5001", new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new Persons.PersonsClient(channel);


            var reply = client.AddNewPerson(new Person() { Fm = "fm1", Im = "im1", Ot = "ot1", Dtr = "10.02.1981" });
            return 1;

        }
    }
}

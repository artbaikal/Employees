using Grpc.Net.Client;
using GrpcService1;
using GrpcService1.DB;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleAppSendToServer
{
    class Program
    {
        const string connectAddr = "https://127.0.0.1:5001";

        static async Task Main(string[] args)
        {
            Console.WriteLine("sending to server AddOrEditPerson");



            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            using var channel = GrpcChannel.ForAddress(connectAddr, new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new Persons.PersonsClient(channel);

            using var dbase = new Context();

            using var call = client.AddOrEditPerson();

            var empls = dbase.Employees.ToList();
            foreach (var empl in empls)
            {
                var p = new Person();
                p.Id = empl.Id;
                p.Fm = empl.Surname;
                p.Im = empl.Name;
                p.Ot = empl.Patronymic;
                p.Dtr = empl.Birthday.ToShortDateString();
                p.Sex = empl.Sex;
                p.HasChilds = Convert.ToInt32(empl.HasChild);
                await call.RequestStream.WriteAsync(p);
            }
            await call.RequestStream.CompleteAsync();

            var reply = call.ResponseAsync.Result;


            Console.WriteLine("completed. result="+reply.Res);
            Console.ReadKey();
        }
    }
}

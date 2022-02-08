
using Grpc.Net.Client;
using GrpcService1;
using System;
using System.Collections.Generic;
//using GrpcService1;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
//using GrpcService1;
using Model.Models;

namespace Employees.Services.GRPC
{
    internal class grpcClient
    {
        const string connectAddr = "https://127.0.0.1:5001";

        public static int DelEmployeeRequest(Employee empl)
        {
            try
            {
                var httpHandler = new HttpClientHandler();
                httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

                using var channel = GrpcChannel.ForAddress(connectAddr, new GrpcChannelOptions { HttpHandler = httpHandler });

                var client = new Persons.PersonsClient(channel);


                var reply = client.DeletePerson(new PersonId() { Id = empl.Id });

                return reply.Res;
            }
            catch
            {
                return -1;
            }
        }
        public static int EditEmployeeRequest(Employee empl)
        {
            try
            {
                var httpHandler = new HttpClientHandler();
                httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

                using var channel = GrpcChannel.ForAddress(connectAddr, new GrpcChannelOptions { HttpHandler = httpHandler });

                var client = new Persons.PersonsClient(channel);


                var reply = client.EditPerson(new Person()
                {
                    Id = empl.Id,
                    Fm = empl.Surname,
                    Im = empl.Name,
                    Ot = empl.Patronymic,
                    Dtr = empl.Birthday.ToShortDateString(),
                    Sex = empl.Sex,
                    HasChilds = Convert.ToInt32(empl.HasChild)
                }) ;

                return reply.Res;
            }
            catch
            {
                return -1;
            }
        }
        public static int AddNewEmployeeRequest(Employee empl)
        {
            try
            {
                var httpHandler = new HttpClientHandler();
                httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

                using var channel = GrpcChannel.ForAddress(connectAddr, new GrpcChannelOptions { HttpHandler = httpHandler });

                var client = new Persons.PersonsClient(channel);


                var reply = client.AddNewPerson(new Person()
                {
                    Fm = empl.Surname,
                    Im = empl.Name,
                    Ot = empl.Patronymic,
                    Dtr = empl.Birthday.ToShortDateString(),
                    Sex = empl.Sex,
                    HasChilds = Convert.ToInt32(empl.HasChild)
                });

                return reply.Id;
            }
            catch
            {
                return -1;
            }

        }

        async public static Task GetEmployees(List<Employee>  empls)
        {
      


            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            using var channel = GrpcChannel.ForAddress(connectAddr, new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new Persons.PersonsClient(channel);



            using var call = client.ListPersons(new PersonId() { Id = 0 });


            while (
             await call.ResponseStream.MoveNext(CancellationToken.None))
            {


                var resp = call.ResponseStream.Current;
                var empl = new Employee();
                empl.Id = resp.Id;
                empl.Surname = resp.Fm;
                empl.Name = resp.Im;
                empl.Patronymic = resp.Ot;
                empl.Birthday = Convert.ToDateTime( resp.Dtr);
                empl.Sex = resp.Sex;
                empl.HasChild = resp.HasChilds == 1 ? true : false;

                empls.Add(empl);

            }





        }
    }
}

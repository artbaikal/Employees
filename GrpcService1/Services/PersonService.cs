using Grpc.Core;
using GrpcService1.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GrpcService1
{
    public class PersonService : Persons.PersonsBase
    {
        private readonly ILogger<PersonService> _logger;
        public PersonService(ILogger<PersonService> logger)
        {
            _logger = logger;
        }

        async public override Task<Result> AddOrEditPerson(IAsyncStreamReader<Person> requestStream, ServerCallContext context)
        {
            int res = 1;

            try
            {

                using var dbase = new Context();

                while (
                 await requestStream.MoveNext(CancellationToken.None))
                {

                    var resp = requestStream.Current;
                    //resp.Fm
                    //resp.Im
                    //resp.Ot
                    //resp.Dtr
                    Employee empl;



                    var empls = dbase.Employees.Where(p => p.Name == resp.Im).Where(p => p.Surname == resp.Fm).
                            Where(p => p.Patronymic == resp.Ot).Where(p => p.Birthday == Convert.ToDateTime(resp.Dtr)).ToList();

                    if (empls.Count == 0)
                    {
                        empl = new Employee();

                    }
                    else
                    if (empls.Count == 1)
                    {

                        empl = empls[0];

                    }
                    else
                    {
                        continue;
                    }
                    empl.Name = resp.Im;
                    empl.Surname = resp.Fm;
                    empl.Patronymic = resp.Ot;
                    empl.Birthday = Convert.ToDateTime(resp.Dtr);
                    empl.Sex = resp.Sex;
                    empl.HasChild = resp.HasChilds == 1 ? true : false;


                    if (empls.Count == 0)
                    {
                        dbase.Employees.Add(empl);

                    }
                    else
                    if (empls.Count == 1)
                    {

                        dbase.Employees.Update(empl);

                    }








                }

                dbase.SaveChanges();


            }
            catch
            {

            }
            return new Result
            {
                Res = res

            };
        }



        public override Task<PersonId> AddNewPerson(Person request, ServerCallContext context)
        {
            //add to database
            int res = -1;

            try
            {
                var empl = new Employee();


                using (var dbase = new Context())
                {
                    empl.Name = request.Im;
                    empl.Surname = request.Fm;
                    empl.Patronymic = request.Ot;
                    empl.Birthday = Convert.ToDateTime(request.Dtr);
                    empl.Sex = request.Sex;
                    empl.HasChild = request.HasChilds == 1 ? true : false;

                    dbase.Employees.Add(empl);

                    dbase.SaveChanges();
                    res = empl.Id;

                };
            }
            catch
            {

            }


            return Task.FromResult(new PersonId
            {
                Id = res

            }); ;


        }

        public override Task<Result> DeletePerson(PersonId request, ServerCallContext context)
        {
            //delete from database
            int res = -1;
            try
            {


                using var dbase = new Context();
                var empl = dbase.Employees.Find(request.Id);
                if (empl != null)
                {
                    dbase.Employees.Remove(empl);
                    dbase.SaveChanges();
                    res = 1;

                }
            }
            catch
            {

            }


            return Task.FromResult(new Result
            {
                Res = res

            });


        }

        public override Task<Result> EditPerson(Person request, ServerCallContext context)
        {
            //edit in database
            int res = -1;
            try
            {


                using var dbase = new Context();
                var empl = dbase.Employees.Find(request.Id);
                if (empl != null)
                {
                    empl.Name = request.Im;
                    empl.Surname = request.Fm;
                    empl.Patronymic = request.Ot;
                    empl.Birthday = Convert.ToDateTime(request.Dtr);
                    empl.Sex = request.Sex;
                    empl.HasChild = request.HasChilds == 1 ? true : false;
                    dbase.SaveChanges();
                    res = 1;
                }
            }
            catch
            {

            }


            return Task.FromResult(new Result
            {
                Res = res

            });


        }

        //public virtual global::System.Threading.Tasks.Task ListPersons(global::GrpcService1.Result request, grpc::IServerStreamWriter<global::GrpcService1.Person> responseStream, grpc::ServerCallContext context)
        //{
        //  throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
        //}
        public override async Task ListPersons(PersonId request, IServerStreamWriter<Person> response, ServerCallContext context)
        {
            try
            {
                using var dbase = new Context();
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
                    await response.WriteAsync(p);

                }

            }
            catch
            {

            }

        }




    }
}

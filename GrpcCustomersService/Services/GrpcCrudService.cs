using Grpc.Core;
using GrpcCustomersService;
using LibraryModel.Data;
using LibraryModel.Models;

namespace GrpcCustomersService.Services
{
    public class GrpcCrudService : CustomerService.CustomerServiceBase
    {
        private Ciurca_Radu_Lab2Context db = null;
        public GrpcCrudService(Ciurca_Radu_Lab2Context db)
        {
            this.db = db;
        }
        public override Task<CustomerList> GetAll(Empty empty, ServerCallContext
       context)
        {

            CustomerList pl = new CustomerList();
            var query = from cust in db.Customer
                        select new Customer()
                        {
                            CustomerId = cust.CustomerID,
                            Name = cust.Name,
                            Adress = cust.Adress
                        };
            pl.Item.AddRange(query.ToArray());
            return Task.FromResult(pl);
        }
        public override Task<Customer> Get(CustomerId requestData, ServerCallContext context)
        {
            var data = db.Customer.Find(requestData.Id);

            Customer emp = new Customer()
            {
                CustomerId = data.CustomerID,
                Name = data.Name,
                Adress = data.Adress

            };
            return Task.FromResult(emp);
        }
        public override Task<Empty> Insert(Customer requestData, ServerCallContext
       context)
        {
            db.Customer.Add(new LibraryModel.Models.Customer 
            {
                Name = requestData.Name,
                Adress = requestData.Adress,
                BirthDate = DateTime.Parse(requestData.Birthdate)
            });
            db.SaveChanges();
            return Task.FromResult(new Empty());
        }
        public override Task<Customer> Update(Customer requestData, ServerCallContext context)
        {
            db.Customer.Update(new LibraryModel.Models.Customer()
            {
                CustomerID = requestData.CustomerId,
                Name = requestData.Name,
                Adress = requestData.Adress,
                BirthDate = DateTime.Parse(requestData.Birthdate)
            });
            db.SaveChanges();
            return Task.FromResult(requestData);
        }
        public override Task<Empty> Delete(CustomerId requestData, ServerCallContext
context)
        {
            var data = db.Customer.Find(requestData.Id);
            db.Customer.Remove(data);

            db.SaveChanges();
            return Task.FromResult(new Empty());
        }
    }
}

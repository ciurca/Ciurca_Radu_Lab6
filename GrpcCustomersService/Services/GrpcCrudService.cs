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
        public override Task<Customer> Get(CustomerId id, ServerCallContext
       context)
        {

            Customer customer = new Customer();
            var query = from cust in db.Customer where cust.CustomerID == id.Id 
                        select new Customer()
                        {
                            CustomerId = cust.CustomerID,
                            Name = cust.Name,
                            Adress = cust.Adress,
                            Birthdate = cust.BirthDate.ToString()
                        };
            customer = query.First();
            return Task.FromResult(customer);
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
        public override Task<Customer> Update(Customer requestData, ServerCallContext
       context)
        {
            LibraryModel.Models.Customer customer = db.Customer.Find(requestData.CustomerId);
            customer.CustomerID = requestData.CustomerId;
            customer.Name = requestData.Name;
            customer.Adress = requestData.Adress;
            customer.BirthDate = DateTime.Parse(requestData.Birthdate);
            db.Customer.Update(customer);
            db.SaveChanges();

            return Task.FromResult(requestData);
        }
        public override Task<Empty> Delete(CustomerId request, ServerCallContext context)
        {
            LibraryModel.Models.Customer customer = db.Customer.Find(request.Id);
            db.Remove(customer);
            db.SaveChanges();
            return Task.FromResult(new Empty());
        }
    }
}

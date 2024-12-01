using Banking.API.Dtos;

namespace Banking.API.Domain.Customers;
public static class CustomerDtoExtensions
{
    public static CustomerInfoResponse ToCustomerResponse(this Customer customer)
    {
        return new CustomerInfoResponse
        {
            CustomerId = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            PhoneNumber = customer.PhoneNumber,
            Email = customer.Email,
        };
    }
}
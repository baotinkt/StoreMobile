using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StoreMobile.Data;

namespace StoreMobile
{
	public interface IRestService
	{
        //Gereral
        Task<int> Login(Customer item);
        Task<Customer> GetCustomerAsync(int id);

        // Product
        Task<List<OnlineProduct>> GetProductListAsync ();

        Task<string> GetProductAsync(int id);

        Task SaveProductAsync (OnlineProduct item, bool isNewItem);

		Task DeleteProductAsync (int id);

        // Ticket
        Task<List<TicketObject>> GetTicketListAsync();

        Task<string> GetTicketObjectAsync(int id);

        Task SaveTicketObjectAsync(TicketObject item, bool isNewItem);

        Task DeleteTicketObjectAsync(int id);

        // Ticket Detail
        Task<List<TicketDetailObject>> GetTicketDetailListAsync(int ticketId);

        Task<string> GetTicketDetailObjectAsync(int id);

        Task SaveTicketDetailObjectAsync(List<TicketDetailObject> items);

        Task DeleteTicketDetailObjectAsync(int id);

    }
}

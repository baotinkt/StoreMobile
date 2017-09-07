using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StoreMobile.Data;

namespace StoreMobile
{
	public class OnlineProductManager
	{
		IRestService restService;

        #region General
        public Task<int> PostLogin(Customer item)
        {
            return restService.Login(item);
        }
        #endregion

        public OnlineProductManager (IRestService service)
		{
			restService = service;
		}

        public Task<List<OnlineProduct>> GetProductListTasksAsync()
        {
            return restService.GetProductListAsync();
        }

        public Task<string> GetProductTasksAsync(int id)
        {
            return restService.GetProductAsync(id);
        }

        public Task SaveProductTaskAsync(OnlineProduct item, bool isNewItem = false)
		{
			return restService.SaveProductAsync(item, isNewItem);
		}

		public Task DeleteProductTaskAsync(OnlineProduct item)
		{
			return restService.DeleteProductAsync (Convert.ToInt32(item.Id));
		}

        public Task<List<TicketObject>> GetTicketListTasksAsync()
        {
            return restService.GetTicketListAsync();
        }

        public Task<string> GetTicketObjectTasksAsync(int id)
        {
            return restService.GetTicketObjectAsync(id);
        }

        public Task SaveTicketObjectTaskAsync(TicketObject item, bool isNewItem = false)
        {
            return restService.SaveTicketObjectAsync(item, isNewItem);
        }

        public Task DeleteTicketObjectTaskAsync(int id)
        {
            return restService.DeleteTicketObjectAsync(id);
        }

        public Task<List<TicketDetailObject>> GetTicketDetailListTasksAsync(int ticketId)
        {
            return restService.GetTicketDetailListAsync(ticketId);
        }

        public Task<string> GetTicketDetailObjectTasksAsync(int id)
        {
            return restService.GetTicketDetailObjectAsync(id);
        }

        public Task SaveTicketDetailObjectTaskAsync(List<TicketDetailObject> items)
        {
            return restService.SaveTicketDetailObjectAsync(items);
        }

        public Task DeleteTicketDetailObjectTaskAsync(int id)
        {
            return restService.DeleteTicketDetailObjectAsync(id);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StoreMobile.Data;

namespace StoreMobile
{
	public class RestService : IRestService
	{
		HttpClient client;

		//public List<OnlineProduct> Items { get; private set; }
        public string result;
        public static string token;
        public DateTime productUpdated, ticketUpdated, ticketDetailUpdated;

        #region General
        public async Task<bool> DataSync()
        {
            List<OnlineProduct> lstProduct = await GetProductListAsync();
            await App.Database.SaveProductsAsync(lstProduct, true);
            List<TicketObject> lstTicket = await GetTicketListAsync();
            await App.Database.SaveTicketsAsync(lstTicket, true);
            List<TicketDetailObject> lstDetail = await GetTicketDetailListAsync(0);
            await App.Database.SaveTicketDetailsAsync(lstDetail, true);
            return true;
        }

        public async Task<int> Login(Customer item)
        {
            var uri = new Uri(string.Format(Constants.Login, string.Empty));
            int customerId = 0;

            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(new Uri(string.Format(Constants.Login, String.Empty)), content);

                if (response.IsSuccessStatusCode)
                {
                    var res = await response.Content.ReadAsStringAsync();
                    Customer cus = JsonConvert.DeserializeObject<StoreMobile.Customer>(res);

                    try
                    {
                        customerId = ((Customer)await App.Database.GetCustomerAsync(cus.CustomerId)).CustomerId;
                        token = cus.Token;
                    }
                    catch (Exception) { }
                    if (customerId == 0)
                    {
                        await App.Database.SaveCustomerAsync(cus, true);
                        await DataSync();
                    }                    
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return customerId;
        }

        public async Task<Customer> GetCustomerAsync(int id)
        {
            var uri = new Uri(string.Format(Constants.CustomerGet, id));
            Customer item = null;
            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    item = JsonConvert.DeserializeObject<StoreMobile.Customer>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return item;
        }

        #endregion

        public void LastUpdated()
        {
            Customer cus = App.Database.GetFirstCustomerAsync().Result;
            productUpdated = cus.ProductUpdated;
            ticketUpdated = cus.TicketUpdated;
            ticketDetailUpdated = cus.TicketDetailUpdated;
        }

		public RestService ()
		{
			var authData = string.Format ("{0}:{1}", Constants.Username, Constants.Password);
			var authHeaderValue = Convert.ToBase64String (Encoding.UTF8.GetBytes (authData));

			client = new HttpClient ();
			client.MaxResponseContentBufferSize = 256000;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue ("Basic", authHeaderValue);
		}

		public async Task<List<OnlineProduct>> GetProductListAsync ()
		{
            
            List<OnlineProduct> Items = new System.Collections.Generic.List<StoreMobile.OnlineProduct> ();

			// RestUrl = http://developer.xamarin.com:8081/api/OnlineProducts{0}
			var uri = new Uri (string.Format (Constants.ProductList, productUpdated));

			try {
				var response = await client.GetAsync (uri);
				if (response.IsSuccessStatusCode) {
					var content = await response.Content.ReadAsStringAsync ();
                    Items = JsonConvert.DeserializeObject<System.Collections.Generic.List<StoreMobile.OnlineProduct>>(content);
                    foreach(OnlineProduct item in Items)
                    {
                        item.ImageUrl = Constants.Image + item.Id + ".jpg";
                        await App.Database.SaveProductAsync(item, false);
                    }
                    Items = await App.Database.GetProductsAsync();
                }
			} catch (Exception ex) {
				Debug.WriteLine (@"ERROR {0}", ex.Message);
			}

			return Items;
		}


        public async Task<string> GetProductAsync(int id)
        {
            var uri = new Uri(string.Format(Constants.ProductGet, id));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    result = content.ToString();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return result;
        }

        public async Task SaveProductAsync (OnlineProduct item, bool isNewItem = false)
		{
			try {
				var json = JsonConvert.SerializeObject (item);
				var content = new StringContent (json, Encoding.UTF8, "application/json");

				HttpResponseMessage response = null;
				if (isNewItem) {
					response = await client.PostAsync (new Uri(string.Format(Constants.ProductInsert, String.Empty)), content);
                    //await db.SaveProductAsync(JsonConvert.DeserializeObject<StoreMobile.OnlineProduct>(content));
				} else {
					response = await client.PutAsync (new Uri(string.Format(Constants.ProductUpdate, String.Empty)), content);
				}
				
				if (response.IsSuccessStatusCode) {
					Debug.WriteLine (@"Item successfully saved.");
				}
				
			} catch (Exception ex) {
				Debug.WriteLine (@"ERROR {0}", ex.Message);
			}
		}

		public async Task DeleteProductAsync (int id)
		{
			// RestUrl = http://developer.xamarin.com:8081/api/OnlineProducts{0}
			var uri = new Uri (string.Format (Constants.ProductDelete, id));

			try {
				var response = await client.DeleteAsync (uri);

				if (response.IsSuccessStatusCode) {
					Debug.WriteLine (@"Item successfully deleted.");	
				}
				
			} catch (Exception ex) {
				Debug.WriteLine (@"ERROR {0}", ex.Message);
			}
		}

        public async Task UploadBitmapAsync(StreamContent streamContent, string fileName)
        {
            var content = new MultipartFormDataContent();

            streamContent.Headers.ContentDisposition = ContentDispositionHeaderValue.Parse("form-data");

            streamContent.Headers.ContentDisposition.Parameters.Add(new NameValueHeaderValue("name", "contentFile"));

            streamContent.Headers.ContentDisposition.Parameters.Add(new NameValueHeaderValue("filename", "\"" + fileName + "\""));

            content.Add(streamContent);

            try
            {
                HttpResponseMessage response = await client.PostAsync(new Uri(string.Format(Constants.Image, String.Empty)), content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"Item successfully saved.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
        }

        #region TicketObject
        public async Task<List<StoreMobile.TicketObject>> GetTicketListAsync()
        {
            List<TicketObject> Items = new System.Collections.Generic.List<StoreMobile.TicketObject>();

            // RestUrl = http://developer.xamarin.com:8081/api/OnlineProducts{0}
            var uri = new Uri(string.Format(Constants.TicketList, string.Empty));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<System.Collections.Generic.List<StoreMobile.TicketObject>>(content);
                    foreach (var item in Items) item.Status();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return Items;
        }


        public async Task<string> GetTicketObjectAsync(int id)
        {
            var uri = new Uri(string.Format(Constants.TicketObjectGet, id));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    result = content.ToString();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return result;
        }

        public async Task SaveTicketObjectAsync(TicketObject item, bool isNewItem = false)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    response = await client.PostAsync(new Uri(string.Format(Constants.TicketObjectInsert, String.Empty)), content);
                }
                else
                {
                    response = await client.PutAsync(new Uri(string.Format(Constants.TicketObjectUpdate, String.Empty)), content);
                }

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"Item successfully saved.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
        }

        public async Task DeleteTicketObjectAsync(int id)
        {
            // RestUrl = http://developer.xamarin.com:8081/api/OnlineProducts{0}
            var uri = new Uri(string.Format(Constants.TicketObjectDelete, id));

            try
            {
                var response = await client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"Item successfully deleted.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
        }
        #endregion

        #region TicketDetailObject
        public async Task<List<StoreMobile.TicketDetailObject>> GetTicketDetailListAsync(int ticketId)
        {
            List<TicketDetailObject> items = new System.Collections.Generic.List<StoreMobile.TicketDetailObject>();
            var uri = new Uri(string.Format(Constants.TicketDetailList, ticketId));
            
            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    items = JsonConvert.DeserializeObject<System.Collections.Generic.List<StoreMobile.TicketDetailObject>>(content);
                    foreach(var item in items)
                    {
                        try
                        {
                            item.Total = item.Price * item.Quantity;
                        }
                        catch (Exception) { item.Total = 0; }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return items;
        }


        public async Task<string> GetTicketDetailObjectAsync(int id)
        {
            var uri = new Uri(string.Format(Constants.TicketDetailObjectGet, id));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    result = content.ToString();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return result;
        }

        public async Task SaveTicketDetailObjectAsync(List<TicketDetailObject> items)
        {
            List<TicketDetailObject> lst = new List<TicketDetailObject>();
            try
            {
                foreach(var item in items)
                {
                    if (item.Quantity > 0) lst.Add(item);
                }
                var json = JsonConvert.SerializeObject(lst);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                response = await client.PostAsync(new Uri(string.Format(Constants.TicketDetailObjectInsert, String.Empty)), content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"Item successfully saved.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
        }

        public async Task DeleteTicketDetailObjectAsync(int id)
        {
            // RestUrl = http://developer.xamarin.com:8081/api/OnlineProducts{0}
            var uri = new Uri(string.Format(Constants.TicketDetailObjectDelete, id));

            try
            {
                var response = await client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"Item successfully deleted.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
        }
        #endregion

    }
}

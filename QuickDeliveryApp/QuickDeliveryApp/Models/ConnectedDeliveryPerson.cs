using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickDeliveryApp.Services;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Threading.Tasks;

namespace QuickDeliveryApp.Models
{
    public class ConnectedDeliveryPerson : DeliveryPerson
    {
        // Connection to hub
        private DeliveryProxy deliveryProxy;
        
        public ConnectedDeliveryPerson()
        {
            //Open connection to delivery proxy
            this.deliveryProxy = new DeliveryProxy();
            Device.StartTimer(TimeSpan.FromSeconds(10), () => OnTimer());
            ConnectToServer();
        }

        private async void ConnectToServer()
        {
            string[] arr = new string[0];
            await this.deliveryProxy.Connect(arr);
        }
        
        private async void DisconnectToServer(int orderId)
        {
            string[] orders = { orderId.ToString() };
            await this.deliveryProxy.Disconnect(orders);
        }

        public async void UpdateOrderStatus(int orderId, int statusId)
        {
            Order o = this.Orders.Where(or => or.OrderId == orderId).FirstOrDefault();
            if (o != null)
            {
                o.StatusOrderId = statusId;
                await this.deliveryProxy.UpdateOrderStatus(orderId.ToString(), statusId.ToString());
                if (orderId == 4)
                {
                    this.DisconnectToServer(orderId);
                }
            }
        }

        private bool OnTimer()
        {
            var location = GetLocation();
            
            return true;
        }

        private async Task<bool> GetLocation()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null && this.Orders != null)
                {
                    Order[] orders = this.Orders.Where(o => o.StatusOrderId < 4).ToArray();
                    if (orders.Length == 0)
                        return true;

                    List<string> orderIds = new List<string>();
                    foreach (Order o in orders)
                        orderIds.Add(o.OrderId.ToString());

                    await this.deliveryProxy.UpdateDeliveryLocation(orderIds.ToArray(), location.Latitude.ToString(), location.Longitude.ToString());
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
            return true;
        }
    }
}

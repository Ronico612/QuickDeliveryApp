using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace QuickDeliveryApp.Services
{
    class DeliveryProxy
    {
        private const string CLOUD_URL = "TBD"; //API url when going on the cloud
        private const string DEV_ANDROID_EMULATOR_URL = "http://10.0.2.2:38367/delivery"; //Chat url when using emulator on android
        private const string DEV_ANDROID_PHYSICAL_URL = "http://192.168.1.14:38367/delivery"; //Chat url when using physucal device on android
        private const string DEV_WINDOWS_URL = "http://localhost:38367/delivery"; //API url when using windoes on development

        private readonly HubConnection hubConnection;

        public DeliveryProxy()
        {
            string deliveryUrl = GetDeliveryUrl();
            hubConnection = new HubConnectionBuilder().WithUrl(deliveryUrl).Build();
        }

        private string GetDeliveryUrl()
        {
            if (App.IsDevEnv)
            {
                if (Device.RuntimePlatform == Device.Android)
                {
                    if (DeviceInfo.DeviceType == DeviceType.Virtual)
                    {
                        return DEV_ANDROID_EMULATOR_URL;
                    }
                    else
                    {
                        return DEV_ANDROID_PHYSICAL_URL;
                    }
                }
                else
                {
                    return DEV_WINDOWS_URL;
                }
            }
            else
            {
                return CLOUD_URL;
            }
        }

        //Connect gets a list of groups the user belongs to!
        public async Task Connect(string[] orders)
        {
            if (hubConnection.State == HubConnectionState.Disconnected)
                await hubConnection.StartAsync();
            await hubConnection.InvokeAsync("OnConnect", orders);
        }

        //Use this method when the chat is finished so the connection will not stay open
        public async Task Disconnect(string[] orders)
        {
            if (hubConnection.State == HubConnectionState.Connected)
            {
                await hubConnection.InvokeAsync("OnDisconnect", orders);
                await hubConnection.StopAsync();
            }
            
        }

        //This message send message to all clients!
        public async Task UpdateOrderStatus(string orderId, string statusId)
        {
            if (hubConnection.State == HubConnectionState.Disconnected)
                await hubConnection.StartAsync();
            await hubConnection.InvokeAsync("UpdateOrderStatus", orderId, statusId);
        }

        //This method send a message to specific group
        public async Task UpdateDeliveryLocation(string[] orders, string latitude, string longitude)
        {
            if (hubConnection.State == HubConnectionState.Disconnected)
                await hubConnection.StartAsync();
            await hubConnection.InvokeAsync("UpdateDeliveryLocation", orders, latitude, longitude);
        }

        //This method register a method to be called upon receiving a message
        public void RegisterToUpdateOrderStatus(Action<string, string> UpdateOrderStatus)
        {
            hubConnection.On("UpdateOrderStatus", UpdateOrderStatus);
        }

        //This method register a method to be called upon receiving a message from specific group
        public void RegisterToUpdateDeliveryLocation(Action<string, string> UpdateDeliveryLocation)
        {
            hubConnection.On("UpdateDeliveryLocation", UpdateDeliveryLocation);
        }
    }
}

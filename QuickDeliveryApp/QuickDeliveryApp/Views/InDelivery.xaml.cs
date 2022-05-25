using QuickDeliveryApp.DTO;
using QuickDeliveryApp.Helpers;
using QuickDeliveryApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace QuickDeliveryApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InDelivery : ContentPage
    {
        public InDelivery()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            InDeliveryViewModel context = (InDeliveryViewModel)this.BindingContext;
            context.OnUpdateMapEvent += OnUpdateMap;
            context.OnDeliveryLocation += OnUpdateDeliveryPosition;
            context.OnGo();
            deliveryElement = null;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            InDeliveryViewModel context = (InDeliveryViewModel)this.BindingContext;
            context.OnUpdateMapEvent -= OnUpdateMap;
        }

        private Circle deliveryElement;

        public void OnUpdateDeliveryPosition(double lat, double longi)
        {
            Position pos = new Position(lat, longi);
            if (deliveryElement == null)
            {
                this.deliveryElement = new Circle()
                {
                    FillColor = Color.Red,
                    Center = pos,
                    Radius = Distance.FromMeters(500)
                };

                map.MapElements.Add(this.deliveryElement);
            }
            else
                this.deliveryElement.Center = pos;
        }

        public void OnUpdateMap()
        {
            //Clear all routes and pins from the map
            map.MapElements.Clear();

            InDeliveryViewModel vm = (InDeliveryViewModel)this.BindingContext;

            //Create two pins for origin and destination and add them to the map
            Pin pin1 = new Pin
            {
                Type = PinType.Place,
                Position = new Position(vm.RouteOrigin.Latitude, vm.RouteOrigin.Longitude),
                Label = vm.RouteOrigin.Name,
                Address = ""
            };
            map.Pins.Add(pin1);
            Pin pin2 = new Pin
            {
                Type = PinType.Place,
                Position = new Position(vm.RouteDestination.Latitude, vm.RouteDestination.Longitude),
                Label = vm.RouteDestination.Name,
                Address = ""
            };
            map.Pins.Add(pin2);

            //Move the map to show the environment of the origin place! with radius of 5 KM... should be changed
            //according to the specific needs
            Position center = new Position((pin1.Position.Latitude + pin2.Position.Latitude) / 2, (pin1.Position.Longitude + pin2.Position.Longitude) / 2);
            //double distance = Math.Sqrt(Math.Pow(pin1.Position.Latitude - pin2.Position.Latitude,2) + Math.Pow(pin1.Position.Longitude - pin2.Position.Longitude, 2));
            MapSpan span = MapSpan.FromCenterAndRadius(center, Distance.BetweenPositions(pin1.Position, pin2.Position));
            
            
            map.MoveToRegion(span);

            //Create the polyline between origin and destination
            GoogleDirection directions = vm.RouteDirections;
            Xamarin.Forms.Maps.Polyline path = new Xamarin.Forms.Maps.Polyline()
            {
                StrokeColor = Xamarin.Forms.Color.Blue,
                StrokeWidth = 15
            };
            //run through each leg of the route, then, through each step
            foreach (Leg leg in directions.Routes[0].Legs)
            {
                foreach (Step step in leg.Steps)
                {
                    var p = step.Polyline;
                    //Decode all positions of the line in this specific step!
                    IEnumerable<Position> positions = PolylineHelper.Decode(p.Points);

                    //Add the positions to the line
                    foreach (Position pos in positions)
                    {
                        path.Geopath.Add(pos);
                    }

                }
            }
            //Add the line to the map!
            map.MapElements.Add(path);
        }
    }
}
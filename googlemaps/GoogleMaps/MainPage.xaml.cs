using GoogleMaps.Method;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace GoogleMaps
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        double lati;
        double longi;
        public MainPage()
        {
            InitializeComponent();
            // Use this for multiple pins
            //Localizar();
            //GeneratePins();
            GenerateAPinMorsa();
            GenerateAPinGansoC();
            map.InfoWindowClicked += Map_InfoWindowClicked;
        }

        private void GeneratePins()
        {
            var pins = new List<Pin>
            {
                new Pin { Type = PinType.Place, Label = "Danta de paramo", Address = "Cauca", Position = new Position(2.442782, -76.607465) },
            };

            foreach (var pin in pins)
            {
                // We can use FromBundle, FromStream or FromView
                pin.Icon = BitmapDescriptorFactory.FromBundle("danta.png");
                map.Pins.Add(pin);
            }
        }

        //private void GenerateAPinOso()
        //{
        //    string lB = Locateb();
        //   string[] split = lB.Split('/');
        //    var pins = new List<Pin>
        //    {
        //        new Pin { Type = PinType.Place, Label = "Oso Polar", Position = new Position(Convert.ToDouble(split[0]), Convert.ToDouble(split[1]))  },
        //    };
        //    foreach (var pin in pins)
        //    {
        //        // We can use FromBundle, FromStream or FromView
        //        pin.Icon = BitmapDescriptorFactory.FromBundle("oso.png");
        //        map.Pins.Add(pin);
        //    }
        //}

        private void GenerateAPinMorsa()
        {
            string lB = Locateb("Morsa");
            string[] split = lB.Split('/');
            var pins = new List<Pin>
            {


                new Pin { Type = PinType.Place, Label = "Morsa", Position = new Position(Convert.ToDouble(split[0]), Convert.ToDouble(split[1]))  },
            };
            foreach (var pin in pins)
            {
                // We can use FromBundle, FromStream or FromView
                pin.Icon = BitmapDescriptorFactory.FromBundle("morsa.png");
                map.Pins.Add(pin);
            }
        }
        private void GenerateAPinGansoC()
        {
            string lB = Locateb("GansoCareto");
            string[] split = lB.Split('/');
            var pins = new List<Pin>
            {


                new Pin { Type = PinType.Place, Label = "Ganso Careto", Position = new Position(Convert.ToDouble(split[0]), Convert.ToDouble(split[1]))  },
            };
            foreach (var pin in pins)
            {
                // We can use FromBundle, FromStream or FromView
                pin.Icon = BitmapDescriptorFactory.FromBundle("ganso.png");
                map.Pins.Add(pin);
            }
        }

        private void Map_InfoWindowClicked(object sender, InfoWindowClickedEventArgs e)
        {
            DisplayAlert("Map Sample", "This is an awesome message", "Done");
        }

        //private void GenerateTrack()
        //{
        //    // map as Xamarin.Forms.GoogleMaps.Map

        //    var polyline = new Polyline();
        //    polyline.Positions.Add(new Position(-23.58, -46.77));
        //    polyline.Positions.Add(new Position(-23.62, -46.87));
        //    polyline.Positions.Add(new Position(-23.78, -46.97));

        //    // Add pins just for reference
        //    var pins = new List<Pin>
        //    {
        //        new Pin { Type = PinType.Place, Label = "This is my home", Address = "Here", Position = new Position(-23.58, -46.77) },
        //        new Pin { Type = PinType.Place, Label = "This is my home", Address = "Here", Position = new Position(-23.62, -46.87) },
        //        new Pin { Type = PinType.Place, Label = "This is my home", Address = "Here", Position = new Position(-23.78, -46.97) },
        //    };

        //    foreach (var pin in pins)
        //    {
        //        map.Pins.Add(pin);
        //    }


        //    polyline.StrokeColor = Color.Blue;
        //    polyline.StrokeWidth = 5f;
        //    polyline.Tag = "POLYLINE"; // Can set any object

        //    polyline.IsClickable = true;
        //    polyline.Clicked += (s, e) =>
        //    {
        //        // handle click polyline
        //    };

        //    map.Polylines.Add(polyline);
        //}

        private async void Localizar()
        {
            var locator = CrossGeolocator.Current; //Acceso a la API
            locator.DesiredAccuracy = 50; //Precisión (en metros)
            if (locator.IsGeolocationAvailable) //Servicio existente en el dispositivo
            {
                if (locator.IsGeolocationEnabled) //GPS activado en el dispositivo
                {
                    var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                    var location = await Geolocation.GetLocationAsync(request);
                    if (!locator.IsListening) //Comprueba si el dispositivo escucha al servicio
                    {
                        await locator.StartListeningAsync(TimeSpan.FromSeconds(1), 5); //Inicio de la escucha
                    }
                    locator.PositionChanged += (cambio, args) =>
                    {
                        var loc = args.Position;
                        longi = double.Parse(loc.Longitude.ToString());
                        lati = double.Parse(loc.Latitude.ToString());
                    };
                }
            }
        }


        private string Locateb(string animal)
        {
            ConexionData cd = new ConexionData();
            return cd.read_file(animal);
        }
    }
}

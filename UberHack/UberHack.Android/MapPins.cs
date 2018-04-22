using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace UberHack.Droid
{
    [Activity(Label = "MapPins")]
    public class MapPins : Activity, IOnMapReadyCallback
    {
        MapFragment _mapFragment;
        GoogleMap _map;
        LocationManager _locMgr;

        List<Model.problema> problemas;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            SetContentView(Resource.Layout.Map);

            _mapFragment = FragmentManager.FindFragmentByTag("map") as MapFragment;
           
            if (_mapFragment == null)
            {
               GoogleMapOptions mapOptions = new GoogleMapOptions()
                    .InvokeMapType(GoogleMap.MapTypeSatellite)
                    .InvokeZoomControlsEnabled(false)
                    .InvokeCompassEnabled(true);

                FragmentTransaction fragTx = FragmentManager.BeginTransaction();
                _mapFragment = MapFragment.NewInstance(mapOptions);
                fragTx.Add(Resource.Id.map, _mapFragment, "map");
                fragTx.Commit();
            }
            _mapFragment.GetMapAsync(this);

            _locMgr = GetSystemService(Context.LocationService) as LocationManager;
        }

        public void OnMapReady(GoogleMap map)
        {
            _map = map;

            if (_map != null)
            {
                _map.MapType = GoogleMap.MapTypeNormal;
                _map.UiSettings.CompassEnabled = true;

                MarkerOptions markerOpt1 = new MarkerOptions();
                markerOpt1.SetPosition(new LatLng(-19.9019194, -43.9462491));
                markerOpt1.SetTitle("Vimy Ridge");
                _map.AddMarker(markerOpt1);

                LatLng location = new LatLng(-19.9019194, -43.9462491);
                CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
                builder.Target(location);
                builder.Zoom(18);
                builder.Bearing(155);
                builder.Tilt(65);
                CameraPosition cameraPosition = builder.Build();
                CameraUpdate cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);

                _map.MoveCamera(cameraUpdate);

                if(problemas!= null && problemas.Any())
                {
                    foreach(Model.problema prob in problemas)
                    {
                        MarkerOptions markerOpt = new MarkerOptions();
                        markerOpt1.SetPosition(new LatLng(prob.latirude, prob.longitude));
                        Marker marker1 = _map.AddMarker(markerOpt1);

                        if (prob.problema_tipo_id == 1)
                        {
                            markerOpt1.SetTitle("Segurança");
                            markerOpt1.SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueBlue));
                        }
                        else if (prob.problema_tipo_id == 1)
                        {
                            markerOpt1.SetTitle("Calçada Irregular");
                            markerOpt1.SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueViolet));
                        }
                        else if (prob.problema_tipo_id == 1)
                        {
                            markerOpt1.SetTitle("Iluminação Pública");
                            markerOpt1.SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueGreen));
                        }
                        else if (prob.problema_tipo_id == 1)
                        {
                            markerOpt1.SetTitle("Acidente");
                            markerOpt1.SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueYellow));

                        }
                        _map.AddMarker(markerOpt1);
                    }
                }
            }
        }
    }
}
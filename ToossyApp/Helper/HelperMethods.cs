using ToossyApp.Models;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.Permissions.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;
using ToossyApp.Views;
using ToossyApp.Services;

namespace ToossyApp.Helper
{
    public static class HelperMethods
    {
        public delegate Task<T> TaskNameDelegate<T>();

        public static T syncGetMethod<T>(TaskNameDelegate<T> taskNameDelegate)
        {
            Task<T> resultTask = Task<T>.Factory.StartNew(() =>
            {
                return taskNameDelegate().Result;
            }

                    , CancellationToken.None, TaskCreationOptions.AttachedToParent, TaskScheduler.Default
            );

            while (resultTask.Status != TaskStatus.RanToCompletion)
            { }

            return resultTask.Result;
        }

        public static async Task<GeoCoords> getPositionFromDevice()
        {


            //var storagePermissionStatus = await PermissionsChecking.CheckPermissions(Permission.Storage);


            /*if (storagePermissionStatus != PermissionStatus.Granted)
            {
                if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Storage))
                {

                    await itineraryPage.DisplayAlert("Position GPS", "Vous devez autoriser l'accès à la mémoire pour cette application", "OK");
                }


                var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage);
                storagePermissionStatus = results[Permission.Storage];
            }*/

            //if (locationPermissionStatus == PermissionStatus.Granted && storagePermissionStatus == PermissionStatus.Granted)

            var itineraryPage = new ItineraryListPage();

            //var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
            var status = await PermissionsChecking.CheckPermissions(Permission.Location);

            /*if (status != PermissionStatus.Granted)
            {
                await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location);

                var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Location });

                status = results[Permission.Location];
            }*/

            if (status == PermissionStatus.Granted)
            {
                IGeolocator _locator = CrossGeolocator.Current;

                _locator.DesiredAccuracy = 30;

                var devicePosition = await _locator.GetPositionAsync(TimeSpan.FromSeconds(10)).ConfigureAwait(continueOnCapturedContext: false);

                var result = new GeoCoords();
                result.Latitude = devicePosition.Latitude;
                result.Longitude = devicePosition.Longitude;

                return result;
            }

            return new GeoCoords();
        }
    }
}

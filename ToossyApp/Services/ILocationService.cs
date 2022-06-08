using ToossyApp.Models;
using System.Threading.Tasks;

namespace ToossyApp.Services
{
    public interface ILocationService
    {
        Task<GeoCoords> GetGeoCoordinatesAsync();
    }
}

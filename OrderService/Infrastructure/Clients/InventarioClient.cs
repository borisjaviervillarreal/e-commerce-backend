using OrderService.Domain.Ports;

namespace OrderService.Infrastructure.Clients
{
    public class InventarioClient : IInventarioClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _inventarioApiUrl;

        public InventarioClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _inventarioApiUrl = configuration["InventarioApiUrl"];
        }

        public async Task<bool> VerificarStock(int productoId, int cantidad)
        {
            // Llamada a la API de Inventario para verificar el stock

            var response = await _httpClient.GetAsync($"{_inventarioApiUrl}/{productoId}/verificar-stock?cantidad={cantidad}");


            if (response.IsSuccessStatusCode)
            {
                var stockDisponible = await response.Content.ReadAsStringAsync();
                return bool.Parse(stockDisponible);
            }

            return false;
        }
    }
}

using FJCO20240905.DTOs.ProductFJCO_DTOs;

namespace FJCO20240905.AppWebBlazor.Data
{
    public class ProductService
    {
        readonly HttpClient _httpClient;

        public ProductService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient("FJCO20240905API");
        }

        public async Task<SearchResultProductDTO> Search(SearchQueryProductDTO searchQueryProduct)
        {
           var response = await _httpClient.PostAsJsonAsync("/product/search", searchQueryProduct);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<SearchResultProductDTO>();
                return result ?? new SearchResultProductDTO();
            }
          return new SearchResultProductDTO();
        }

        public async Task<GetIdResultProductDTO> GetProductById(int id)
        {
            var response = await _httpClient.GetAsync($"/product/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<GetIdResultProductDTO>();
                return result ?? new GetIdResultProductDTO();
            }
            return new GetIdResultProductDTO();
        }

        public async Task<int> Create(CreateProductDTO createProduct)
        {
            int result = 0;
            var response = await _httpClient.PostAsJsonAsync("/product", createProduct);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                if(int.TryParse(responseBody, out result)== false)
                    return result = 0;
            }
            return result;
        }

        public async Task<int> Edit(EditProductDTO editProduct)
        {
           int result = 0;
            var response = await _httpClient.PutAsJsonAsync("/product", editProduct);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                if (int.TryParse(responseBody, out result) == false)
                    return result = 0;
            }
            return result;
        }

        public async Task<int> Delete(int id)
        {
            int result = 0;
            var response = await _httpClient.DeleteAsync($"/product/{id}");
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                if (int.TryParse(responseBody, out result) == false)
                    return result = 0;
            }
            return result;
        }

    }

}

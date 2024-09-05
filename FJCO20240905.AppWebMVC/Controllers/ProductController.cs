using FJCO20240905.DTOs.ProductFJCO_DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FJCO20240905.AppWebMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("FJCO20240905API");
        }
        // GET: ProductController
        public async Task<ActionResult> Index(SearchQueryProductDTO searchQueryProductDTO, int CountRow = 0)
        {
            if (searchQueryProductDTO.SendRowCount == 0)
                searchQueryProductDTO.SendRowCount = 2;
            if(searchQueryProductDTO.take == 0)
                searchQueryProductDTO.take = 10;

            var result = new SearchResultProductDTO();

            var response = await _httpClient.PostAsJsonAsync("/product/search", searchQueryProductDTO);

            if (response.IsSuccessStatusCode)
                result = await response.Content.ReadFromJsonAsync<SearchResultProductDTO>();

            result = result != null ? result : new SearchResultProductDTO();

            if(result.CountRow == 0 && searchQueryProductDTO.SendRowCount == 1)
                result.CountRow = CountRow;

            ViewBag.CountRow = result.CountRow;
            searchQueryProductDTO.SendRowCount = 0;
            ViewBag.SearchQuery = searchQueryProductDTO;

            return View(result);

        }

        // GET: ProductController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var result = new GetIdResultProductDTO();

            var response = await _httpClient.GetAsync($"/product/{id}");
            if (response.IsSuccessStatusCode)
                result = await response.Content.ReadFromJsonAsync<GetIdResultProductDTO>();

            return View(result ?? new GetIdResultProductDTO());
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateProductDTO createProduct)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/product/", createProduct);
                if (response.IsSuccessStatusCode) {
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.Error = "Error al crear el producto";
                return View();
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var result = new GetIdResultProductDTO();
            var response = await _httpClient.GetAsync($"/product/{id}");
            if (response.IsSuccessStatusCode)
                result = await response.Content.ReadFromJsonAsync<GetIdResultProductDTO>();
            return View(new EditProductDTO(result ?? new GetIdResultProductDTO()) );
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, EditProductDTO editProduct)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"/product", editProduct);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.Error = "Error al editar el producto";
                return View();
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var result = new GetIdResultProductDTO();
            var response = await _httpClient.GetAsync($"/product/{id}");
            if (response.IsSuccessStatusCode)
                result = await response.Content.ReadFromJsonAsync<GetIdResultProductDTO>();
            return View(result ?? new GetIdResultProductDTO());
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, GetIdResultProductDTO getIdResultProductDTO)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/product/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.Error = "Error al eliminar el producto";
                return View(getIdResultProductDTO);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(getIdResultProductDTO);
            }
        }
    }
}

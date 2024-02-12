using DeliveryAppAPI.Models;

namespace DeliveryAppAPI.Methods
{
    public static class ProductCount
    {
        public static async Task<int> GetProductCount(List<Models.PdfTable> pdfTables)
        {
            try
            {
                return pdfTables.Count();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}

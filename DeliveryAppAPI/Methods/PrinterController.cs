using DeliveryAppAPI.Models;
using Printer_Service.Data;
using Printer_Service.Models;

namespace DeliveryAppAPI.Methods
{
    public static class PrinterController
    {
        public static void PrintLabel(int copies)
        {
            try
            {
                // Connecting to printer and printing
                string ipAddress = "192.168.1.201";
                int port = 9100;

                // Open connection
                System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient();
                client.Connect(ipAddress, port);

                // Write ZPL String to connection
                StreamWriter writer = new StreamWriter(client.GetStream());
                if (copies == 0) { copies = 1; }

                var dataForLabel = DeliveryAppAPI.Methods.PdfController.PdfToLabel;
                var clientName = PdfController.PdfToLabel;
                if (clientName != null)
                {
                    LabelData.CompanyName = clientName.ClientName;
                }
                else
                {
                    throw new NullReferenceException();
                }

                List<PdfTable> populatedItems = new();

                if (dataForLabel != null)
                {
                    populatedItems = dataForLabel.ListOfItems.Where(x => !string.IsNullOrEmpty(x.ItemName)).ToList();
                }
                else
                {
                    throw new NullReferenceException();
                }

                foreach (var item in populatedItems)
                {
                    var numberOfProducts = populatedItems.Count;
                    new LabelData();
                    LabelData.ContentsOfPackage = item.ItemName;
                    LabelData.Qty = Convert.ToInt32(item.Qty);
                    LabelData.DatePrinted = DateTime.Now.ToString("d");
                    LabelData.BarcodeData = $"Collected from D.Code Mobility, Contents of this package are/ is: {LabelData.ContentsOfPackage}, with a QTY of: {LabelData.Qty}.";
                    if (clientName != null)
                    {
                        LabelData.CompanyName = clientName.ClientName;
                    }

                    Labels labels = new Labels();
                    for (int j = 1; j <= copies; j++)
                    {
                        writer.Write(labels.CollectionLabel);
                        writer.Flush();
                    }
                }

                writer.Close();

                // Close Connection
                client.Close();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}

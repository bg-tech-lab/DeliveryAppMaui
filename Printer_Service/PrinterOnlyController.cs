using PdfGenerator.Models;
using Printer_Service.Data;
using Printer_Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printer_Service
{
    public class PrinterOnlyController
    {
        public bool PrintLabel(int copies, string clientName, List<PdfTable> pdfTableList)
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

                //var dataForLabel = PdfController.PdfToLabel;
                //var clientName = PdfController.PdfToLabel;
                bool printingSuccessful = false;

                if (clientName != null)
                {
                    LabelData.CompanyName = clientName;
                }
                else
                {
                    return false;
                }

                List<PdfTable> populatedItems = new();

                if (pdfTableList != null)
                {
                    populatedItems = pdfTableList.Where(x => !string.IsNullOrEmpty(x.ItemName)).ToList();
                }
                else
                {
                    return false;
                }

                var i = 0;

                foreach (var item in populatedItems)
                {
                    var numberOfProducts = populatedItems.Count;
                    new LabelData();
                    LabelData.ContentsOfPackage = item.ItemName;
                    LabelData.Qty = Convert.ToInt32(item.Qty);
                    LabelData.DatePrinted = DateTime.Now.ToString("d");
                    LabelData.BarcodeData = $"Content: {LabelData.ContentsOfPackage}. Content Description: {populatedItems[i].ItemDescription}. QTY: {LabelData.Qty}.";
                    i++;

                    if (clientName != null)
                    {
                        LabelData.CompanyName = clientName;
                    }

                    Labels labels = new Labels();
                    for (int j = 1; j <= copies; j++)
                    {
                        writer.Write(labels.CollectionLabel);
                        writer.Flush();
                        printingSuccessful = true;
                    }
                }


                writer.Close();

                // Close Connection
                client.Close();
                return printingSuccessful;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

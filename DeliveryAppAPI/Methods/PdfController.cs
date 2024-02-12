using DeliveryAppAPI.Models;
using PdfGenerator.Models;
using PdfTable = DeliveryAppAPI.Models.PdfTable;

namespace DeliveryAppAPI.Methods
{
    public class PdfController
    {
        public static FormFields? PdfToLabel { get; set; }
        public string GeneratePdf(string clientName, string clientAddress, string clientContactDetails, string location, int productCount, List<Models.PdfTable> listOfProducts)
        {
            try
            {
                var pdfDirectory = HTMLString(clientName, clientAddress, clientContactDetails, location, productCount, listOfProducts);

                if (string.IsNullOrEmpty(pdfDirectory))
                {
                    return "";
                }
                return pdfDirectory;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        private string HTMLString(string clientName, string clientAddress, string clientContactDetails, string location, int productCount, List<Models.PdfTable> listOfProducts)
        {
            try
            {
                IronPdf.License.LicenseKey = "IRONSUITE.JONATHANQOQONGA.GMAIL.COM.29951-C127900F39-AHMFRX5-QUPQJLRUZPDL-GNJUGDHI7YVU-B3WV75JF2QHQ-JQ5Y7RS4OHB5-LZFF6TS7L4NI-UM7DNAONZBY4-AB4NP6-TF2RYW3Z6V2LEA-DEPLOYMENT.TRIAL-OZMHWX.TRIAL.EXPIRES.22.DEC.2023";
                var dir = CreateDirectory();
                // Render any HTML fragment or document to HTML
                var Renderer = new IronPdf.ChromePdfRenderer();
                // Set Margins (in millimeters)
                Renderer.RenderingOptions.MarginTop = 0;
                Renderer.RenderingOptions.MarginLeft = 0;
                Renderer.RenderingOptions.MarginRight = 0;
                Renderer.RenderingOptions.MarginBottom = 0;
                var pdfDocument = GetHtmlPdf(clientName, clientAddress, clientContactDetails, location, productCount, listOfProducts);
                using var PDF = Renderer.RenderHtmlAsPdf(pdfDocument);
                //Renderer.RenderingOptions.TextFooter = new HtmlHeaderFooter() { HtmlFragment = "<div style='text-align:right'><em style='color:pink'>page {page} of {total-pages}</em></div>" };
                var OutputPath = $"{clientName}-{PdfToLabel.ReferenceNumber}.pdf";
                //var OutputPath = "ChromePdfRenderer.pdf";

                if (string.IsNullOrEmpty(dir))
                {
                    return "";
                }

                var dirFile = dir + OutputPath;
                PDF.SaveAs(dirFile);
                Renderer.RenderingOptions.CssMediaType = IronPdf.Rendering.PdfCssMediaType.Screen;

                return dirFile;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static string CreateDirectory()
        {
            try
            {
                var directory = @"C:\DeliveryNoteDocuments\";
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                return directory;
            }
            catch (Exception ex)
            {
                return "";
            }

        }


        private string GetHtmlPdf(string clientName, string clientAddress, string clientContactDetails, string location, int productCount, List<Models.PdfTable> listOfProducts)
        {
            var pdfParams = GetValuesForPdf(clientName, clientAddress, clientContactDetails, location, productCount, listOfProducts);
            PdfToLabel = pdfParams;
            var tableData = pdfParams.ListOfItems;
            var tablesData = String.Empty;

            for (int i = 0; i < tableData.Count; i++)
            {
                var singleTableData = $"<tr style=\"border-bottom: 1px solid black; border-right: 1px solid black;font-size: 1rem; text-align: center;\"><td style=\"border-bottom: 1px solid black; border-right: 1px solid black; padding: 1rem 0;\">{tableData[i].ItemName}</td><td style=\"border-bottom: 1px solid black; border-right: 1px solid black; padding: 1rem 0;\">{tableData[i].ItemDescription}</td><td style=\"border-bottom: 1px solid black; padding: 1rem 0;\">{tableData[i].Qty}</td></tr>";
                tablesData += singleTableData;
            }


            var pdfAsHtml = $"<body>    <!-- Body -->    " +
                $"<div style=\"font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;\">" +
                $"        <!-- Top Row -->   <div       " +
                $"style=\"display: flex; width: 100%; justify-content: space-between; align-items: center; " +
                $"padding: 2rem 0rem; border-bottom: 4px solid black;\">       <h1 style=\"font-size: 2rem; margin: 0;\">Delivery Note</h1>" +
                $"            <img src='http://www.dcodemobility.com/assets/images/logo.png' style=\"width: 25%;\" alt=\"dcode logo\">   </div>" +
                $"        <!-- Document Body -->   <div       style=\"width: 100%; display: flex; justify-content: center; margin-top: 3rem; margin-bottom:" +
                $" 3rem; border-bottom: 4px solid black; padding-bottom: 3rem\">       <!-- left side container -->       <div           style=\"width: 50%; " +
                $"height: auto; display: flex; flex-direction: column; align-items: center; justify-content: center;\">           <!-- dcode details -->           " +
                $"<div style=\"margin-bottom: 5rem; display: flex; flex-direction: column; width: 100%;\">               <h2 style=\"font-size: 1.5rem; margin-bottom: .7rem;\">" +
                $"d.Code Mobility</h2>               <p style=\"font-size: 1rem; font-weight: 500; margin-bottom: .5rem;\">Regency Terrace Block A, Route                   " +
                $"21 Corporate Park 81 Regency Drive Irene, 0062</p>               <p style=\"font-size: 1rem; font-weight: bold; margin: 0;\">Phone: <span" +
                $"                            style=\"font-weight: 500;\">012 345 2868</span></p>           </div>           <!-- client details -->           " +
                $"<div style=\"margin-bottom: 2.5rem; display: flex; flex-direction: column; width: 100%;\">               <!-- <h2 style=\"font-size: 1.7rem; margin-bottom: .7rem; " +
                $"margin-top: 0rem;\">To</h2> -->               <h3 style=\"font-size: 1.5rem; margin-bottom: .7rem; margin-top: 0rem;\">{pdfParams.ClientName}</h3>" +
                $"                    <p style=\"font-size: 1rem; margin-bottom: .7rem; margin-top: 0rem;\">{pdfParams.ClientAddress}</p>               " +
                $"<p style=\"font-size: 1rem;  margin: 0;\">{pdfParams.ClientContactDetails}</p>           </div>       </div>       " +
                $"<!-- right side container -->       <div style=\"width: 50%; display: flex; justify-content: end;\">           " +
                $"<div style=\"background-color: rgb(203, 231, 255); width: 80%; padding: 1rem;\">               " +
                $"<p style=\"font-size: 1rem; font-weight: bold; margin-bottom: 0.7rem;\">Reference Number: <span                       style=\"font-weight: 500;\">" +
                $"{pdfParams.ReferenceNumber}</span></p>               <p style=\"font-size: 1rem; font-weight: bold; margin-bottom: 0.7rem;\">Date Pdf Document printed: <span" +
                $"                            style=\"font-weight: 500;\">{pdfParams.DatePdfPrinted}</span></p>               <p style=\"font-size: 1rem; font-weight: bold; " +
                $"margin-bottom: 0.7rem;\">Location: <span                       style=\"font-weight: 500;\">{pdfParams.Location}</span></p>               " +
                $"<p style=\"font-size: 1rem; font-weight: bold; margin-bottom: 0.7rem;\">Carrier: <span                       style=\"font-weight: 500;\">{pdfParams.Carrier}</span>" +
                $"</p>               <p style=\"font-size: 1rem; font-weight: bold; margin-bottom: 0.7rem;\">Delivery Method: <span                       style=\"font-weight: 500;\">" +
                $"{pdfParams.DeliveryMethod}</span></p>               <p style=\"font-size: 1rem; font-weight: bold; margin-bottom: 0.7rem; margin-top: 3rem;\">d.Code" +
                $"                        Representitive Full Name:</p>               <div style=\"border-bottom: 2px solid black; margin-top: 4rem;\"></div>               " +
                $"<p style=\"font-size: 1rem; font-weight: bold; margin-bottom: 0.7rem;\">d.Code Representitive                   Signature:</p>               " +
                $"<div style=\"border-bottom: 2px solid black; margin-top: 4rem;\"></div>           </div>       </div>   </div>      <!-- table -->" +
                $"        <div style=\"width: 100%; display: flex;\">       <table style=\"width: 100%; background-color: rgba(235, 245, 239, 0.479); border: 1px solid black;\">  " +
                $"              <thead style=\"background-color: rgb(203, 231, 255); height: 2rem; font-size: 1.2rem;\">               <tr>                   <th style=\"border-bottom:" +
                $" 1px solid black; border-right: 1px solid black; padding: 1rem; width: 40%;\">Item Name</th>                   <th style=\"border-bottom: 1px solid black; border-right:" +
                $" 1px solid black; padding: 1rem; width: 50%;\">Item Description</th>                   <th style=\"border-bottom: 1px solid black; padding: 1rem; width: 10%;\">QTY</th>" +
                $"                    </tr>           </thead>           <tbody>               {tablesData}                               </tbody>" +
                $"               </table>   </div>  <div style='page-break-after: always;'> </div>  <!-- receiptient signature -->   <div style=\"display: flex; width: 100%; margin-top: 3rem;\">       <div style=\"width:" +
                $" 100%;\">           <!-- left side -->           <h4 style=\"font-size: 1.3rem;\">Goods Recieved By:</h4>              <div style=\"width: 100%;\">               " +
                $"<p style=\"font-size: 1.2rem; font-weight: 600; margin-bottom: 5rem;\">Print Name</p>               <div style=\"border-bottom: 1px solid black; width: 50%;\"></div>               " +
                $"<!-- signature -->               <div style>                   <p style=\"font-size: 1.2rem; font-weight: 600; margin-bottom: 5rem;\">Signature</p>                   " +
                $"<div style=\"border-bottom: 1px solid black; width: 50%;\"></div>               </div>           </div>       </div>              <!-- right side -->" +
                $"            <div style=\"width: 100%; margin-top: 4.8rem;\">           <p style=\"font-size: 1.2rem; font-weight: 600; margin-bottom: 4rem;\">Date:</p>           <div style=\"border-bottom: " +
                $"1px solid black; width: 50%;\"></div></div></div></div><script></script></body>";


            return pdfAsHtml;
        }

        private FormFields GetValuesForPdf(string clientName, string clientAddress, string clientContactDetails, string location, int productCount, List<Models.PdfTable> listOfProducts)
        {
            FormFields formFields = new FormFields();

            formFields.ClientName = clientName;

            formFields.ClientAddress = clientAddress;

            formFields.ClientContactDetails = clientContactDetails;

            formFields.ReferenceNumber = Guid.NewGuid();

            formFields.Location = location;

            for (int i = 0; i < productCount; i++)
            {
                formFields.ListOfItems.Add(new PdfTable());
                formFields.ListOfItems[i].ItemName = listOfProducts[i].ItemName;
                formFields.ListOfItems[i].ItemDescription = listOfProducts[i].ItemDescription;
                formFields.ListOfItems[i].Qty = listOfProducts[i].Qty;
            }

            return formFields;
        }
    }
}

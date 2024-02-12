using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfGenerator.Models
{
    public class PdfModel
    {
        // Client Information
        public string ClientName { get; set; }
        public string ClientAddress { get; set; }
        public string ClientContactDetails { get; set; }

        // General Delivery Information
        public Guid ReferenceNumber { get; set; }
        public string DatePdfPrinted 
        { 
            get { return DateTime.Now.ToString("d"); }
            private set { }
        }
        public string Location { get; set; }
        public string Carrier 
        {
            get { return "Delivery Company"; }
            private set { } 
        }

        public string DeliveryMethod
        {
            get { return "Air Freight/ Courier"; }
            private set { }
        }

        //Table 
        public List<PdfTable> ListOfItems { get; set; }

        public PdfModel()
        {
            // Initialize ListOfItems to an empty list
            ListOfItems = new List<PdfTable>();
        }
    }
}

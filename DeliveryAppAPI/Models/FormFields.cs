namespace DeliveryAppAPI.Models
{
    public class FormFields
    {
        // General Information Properties
        public string ClientName { get; set; }
        public string ClientAddress { get; set; }
        public string ClientContactDetails { get; set; }
        public string Location { get; set; }
        public Guid ReferenceNumber { get; set; }
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
        public string DatePdfPrinted
        {
            get { return DateTime.Now.ToString("d"); }
            private set { }
        }


        // Label Printing Functionality Properties
        public int NumberOfLabel { get; set; }

        // Email Functionality Properties
        public string UserEmail { get; set; } = "jonathanqoqonga@gmail.com";
        public string UserPassword { get; set; } = "xnusvctyogssqybg";
        public string EmailAddresses { get; set; }

        // Reminder Properties
        public string StartTime { get; set; } = "c";
        public string EndTime { get; set; } = "20240108T100000";

        // Products Properties
        public List<PdfTable> ListOfItems { get; set; }

        public FormFields()
        {
            // Initialize ListOfItems to an empty list
            ListOfItems = new List<PdfTable>();
        }
    }
}

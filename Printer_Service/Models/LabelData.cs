using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printer_Service.Models
{
    public class LabelData
    {
        //private static string _BarcodeData = $"Collected from D.Code Mobility, Contents of this package are/ is: {ContentsOfPackage}, with a QTY of: {Qty}.";
        public static string CompanyName { get; set; }
        public static int Qty { get; set; }
        public static string ContentsOfPackage { get; set; }
        public static string DatePrinted { get; set; }
        private static string _BarcodeData { get; set; }
        public static string BarcodeData 
        {
            get { return _BarcodeData; }
            set { _BarcodeData = value; }
        }
    }
}
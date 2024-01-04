using Printer_Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printer_Service.Data
{
    public class Labels
    {
        
        //private static string _CollectionLabel = $"^XA\r\n^FO0,0^GFA,06144,06144,00032,:Z64:\r\neJztU71OwzAYdBOiVqrULK3YkBU2FlYG1KYSD5ChVlZ4A0ZGi4mJvkIFCzIDbwCvYmBBTIwVQ83Z+XNDqyotGz71bF8/Xc6/hDg4ODg4ODhsh1a8JON63ZdLUtbrm/w1R2N/w/zzv80/EveSeEI8gDOomew+Cl7VGRvHpMXSCZhAJfEgZdY3lJKShErNNY0aoanCjX/A2CQAoeKYnY2Tsu6/RYeSDKOIDu+iof8ZUflN6bysB2b+fYzAQYDg8aUZ5wi5XvEJRmBvqhXCT2t+nQcGUATTsfw9s/9Xmb89Rf8O/7Cs79n+9ko/8ue5/xb9K/wHq/1b5PeNH0vWNP6L9flhkV/t3+r8xPb/yg/V1xp/2/jb1vmvWn+44Bvyie3332h+fmGx/gpBuf5+5m8t+3H+Pu/o88ae9bSSHzV/QIr708f9CeLErvuf4uWajoQQoRDP/rd44gv9EAq0UsbwiRT3F4RKyb5+CCWUWhDexd33lfow6ti+/3gxsWknZjRgCQmY/X5w8wk1bcGs+S+gGXVHvLyhDfwdDppfJhvGezrMM3PwqnyvQX7DwDX5lFb/NFs/eJNtAyFcS56LnWa1G3bNd3BwcHBwcHD4p/gBtxGgiA==:783C\r\n^BY108,108^FT476,135^BXN,6,200,0,0,1,~\r\n^FH\\^FD{LabelData.BarcodeData}^FS\r\n^FT26,192^A0N,23,24^FH\\^FDCompany Name:^FS\r\n^FT26,243^A0N,23,24^FH\\^FDContents of Package:^FS\r\n^FT26,295^A0N,23,24^FH\\^FDQuantity:^FS\r\n^FT26,342^A0N,23,24^FH\\^FDDate Label was Printed:^FS\r\n^FT280,183^AAN,18,10^FH\\^FD{LabelData.CompanyName}^FS\r\n^FT280,238^AAN,18,10^FH\\^FD{LabelData.ContentsOfPackage}^FS\r\n^FT280,296^AAN,18,10^FH\\^FD{LabelData.Qty}^FS\r\n^FT280,343^AAN,18,10^FH\\^FD{DateTime.Now.ToString("d")}^FS\r\n^PQ1,0,1,Y^XZ";
        private string _CollectionLabel = $"\r\n^XA~TA000~JSN^LT0^MNW^MTT^PON^PMN^LH0,0^JMA^PR4,4~SD20^JUS^LRN^CI0^XZ\r\n^XA\r\n^MMT\r\n^PW609\r\n^LL0406\r\n^LS0\r\n^FO0,0^GFA,06144,06144,00032,:Z64:\r\neJztU71OwzAYdBOiVqrULK3YkBU2FlYG1KYSD5ChVlZ4A0ZGi4mJvkIFCzIDbwCvYmBBTIwVQ83Z+XNDqyotGz71bF8/Xc6/hDg4ODg4ODhsh1a8JON63ZdLUtbrm/w1R2N/w/zzv80/EveSeEI8gDOomew+Cl7VGRvHpMXSCZhAJfEgZdY3lJKShErNNY0aoanCjX/A2CQAoeKYnY2Tsu6/RYeSDKOIDu+iof8ZUflN6bysB2b+fYzAQYDg8aUZ5wi5XvEJRmBvqhXCT2t+nQcGUATTsfw9s/9Xmb89Rf8O/7Cs79n+9ko/8ue5/xb9K/wHq/1b5PeNH0vWNP6L9flhkV/t3+r8xPb/yg/V1xp/2/jb1vmvWn+44Bvyie3332h+fmGx/gpBuf5+5m8t+3H+Pu/o88ae9bSSHzV/QIr708f9CeLErvuf4uWajoQQoRDP/rd44gv9EAq0UsbwiRT3F4RKyb5+CCWUWhDexd33lfow6ti+/3gxsWknZjRgCQmY/X5w8wk1bcGs+S+gGXVHvLyhDfwdDppfJhvGezrMM3PwqnyvQX7DwDX5lFb/NFs/eJNtAyFcS56LnWa1G3bNd3BwcHBwcHD4p/gBtxGgiA==:783C\r\n^BY120,120^FT448,138^BXN,3,200,0,0,1,~\r\n^FH\\^FD{LabelData.BarcodeData}^FS\r\n^FT26,192^A0N,23,24^FH\\^FDCompany Name:^FS\r\n^FT26,243^A0N,23,24^FH\\^FDContents of Package:^FS\r\n^FT26,295^A0N,23,24^FH\\^FDQuantity:^FS\r\n^FT26,342^A0N,23,24^FH\\^FDLabel Printed:^FS\r\n^FT280,183^AAN,18,10^FH\\^FD{LabelData.CompanyName}^FS\r\n^FT280,238^AAN,18,10^FH\\^FD{LabelData.ContentsOfPackage}^FS\r\n^FT280,296^AAN,18,10^FH\\^FD{LabelData.Qty}^FS\r\n^FT280,343^AAN,18,10^FH\\^FD{LabelData.DatePrinted}^FS\r\n^PQ1,0,1,Y^XZ\r\n";
        public string CollectionLabel 
        {
            get { return _CollectionLabel; }
            set { _CollectionLabel = value; } 
        } 
    }
}

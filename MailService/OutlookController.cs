using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Outlook;
using Exception = System.Exception;

namespace MailService
{
    public class OutlookController
    {
        public void SendOutLookEmail(string attachmentDirectory, List<string> listOfReceipients, string emailAddress, string emailPassword, string clientName, DateTime startDate, DateTime endDate, out bool hasError)
        {
            hasError = false;

            try
            {
                // create the outlook application.
                Microsoft.Office.Interop.Outlook.Application app = new Microsoft.Office.Interop.Outlook.Application();

                MailItem mailItem = (MailItem)app.CreateItem(OlItemType.olMailItem);

                mailItem.Subject = "This is the subject";
                mailItem.To = "jonathanqoqonga@gmail.com";
                mailItem.Body = "This is the message.";
                mailItem.Attachments.Add(attachmentDirectory);
                mailItem.Importance = OlImportance.olImportanceHigh;
                mailItem.Display(false);
                mailItem.Send();
            }
            catch (Exception ex)
            {

                throw;
            }

        }



        //public static AlternateView OutLooking()
        //{
        //    StringBuilder str = new StringBuilder();
        //    str.AppendLine("BEGIN:VCALENDAR");

        //    //PRODID: identifier for the product that created the Calendar object
        //    str.AppendLine("PRODID:-//ABC Company//Outlook MIMEDIR//EN");
        //    str.AppendLine("VERSION:2.0");
        //    str.AppendLine("METHOD:REQUEST");

        //    str.AppendLine("BEGIN:VEVENT");

        //    str.AppendLine(string.Format("DTSTART:{0:yyyyMMddTHHmmssZ}", "20231208100000"));//TimeZoneInfo.ConvertTimeToUtc("BeginTime").ToString("yyyyMMddTHHmmssZ")));
        //    str.AppendLine(string.Format("DTSTAMP:{0:yyyyMMddTHHmmssZ}", DateTime.UtcNow));
        //    str.AppendLine(string.Format("DTEND:{0:yyyyMMddTHHmmssZ}", "20231208110000"));//TimeZoneInfo.ConvertTimeToUtc("EndTime").ToString("yyyyMMddTHHmmssZ")));
        //    str.AppendLine(string.Format("LOCATION: {0}", "Location"));

        //    // UID should be unique.
        //    str.AppendLine(string.Format("UID:{0}", Guid.NewGuid()));
        //    str.AppendLine(string.Format("DESCRIPTION:{0}", "Test Appointment"));
        //    str.AppendLine(string.Format("X-ALT-DESC;FMTTYPE=text/html:{0}", "Test Appointment"));
        //    str.AppendLine(string.Format("SUMMARY:{0}", "Test Subject"));

        //    str.AppendLine("STATUS:CONFIRMED");
        //    str.AppendLine("BEGIN:VALARM");
        //    str.AppendLine("TRIGGER:-PT15M");
        //    str.AppendLine("ACTION:Accept");
        //    str.AppendLine("DESCRIPTION:Reminder");
        //    str.AppendLine("X-MICROSOFT-CDO-BUSYSTATUS:BUSY");
        //    str.AppendLine("END:VALARM");
        //    str.AppendLine("END:VEVENT");

        //    str.AppendLine(string.Format("ORGANIZER:MAILTO:{0}", "jonathanqoqonga@gmail.com"));
        //    str.AppendLine(string.Format("ATTENDEE;CN=\"{0}\";RSVP=TRUE:mailto:{1}", "Jonathan Dcode Account", "jonathan@dcode.mobi"));

        //    str.AppendLine("END:VCALENDAR");
        //    System.Net.Mime.ContentType ct = new System.Net.Mime.ContentType("text/calendar");
        //    ct.Parameters.Add("method", "REQUEST");
        //    ct.Parameters.Add("name", "meeting.ics");
        //    AlternateView avCal = AlternateView.CreateAlternateViewFromString(str.ToString(), ct);

        //    return avCal;
        //}
    }
}

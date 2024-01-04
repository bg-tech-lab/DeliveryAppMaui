using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MailService
{
    public class MailContoller
    {
        public void SendEmail(string attachmentDirectory, List<string> listOfReceipients, string emailAddress, string emailPassword, string clientName, string startDate, string endDate, out bool hasError)
        {
            try
            {
                var fromMail = emailAddress;

                //var fromPass = "uancgfyfqakjqsos";
                var fromPass = emailPassword;

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress(fromMail);

                foreach (var item in listOfReceipients)
                {
                    mail.To.Add(item);
                }

                //mail.To.Add(toAddress);
                mail.Subject = $"Delivery Note for {clientName}";
                mail.Body = $"This email contains a delivery note in the PDF document format. And a calendar reminder for {clientName}\n" +
                    $"Make sure to add the reminder to your calendar. If you have any queries, contact Lesley at (lesley@dcode.mobi).";

                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment(attachmentDirectory);
                mail.Attachments.Add(attachment);

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(fromMail, fromPass);
                SmtpServer.EnableSsl = true;

                //appointment.BaseUri = new Uri(fromMail);
                //mail.AlternateViews.Add(appointment);

                byte[] byteArray = Encoding.ASCII.GetBytes(MeetingRequestString(mail.Subject, mail.Body, startDate, endDate));
                MemoryStream stream = new MemoryStream(byteArray);
                Attachment attach = new Attachment(stream, "event-meeting.ics");
                mail.Attachments.Add(attach);

                SmtpServer.Send(mail);

                hasError = true;
            }
            catch (Exception ex)
            {
                hasError = false;
                throw;
            }
            
        }

        private static string MeetingRequestString(string subject, string messageBody, string startDate, string endDate, int? eventID = null, bool isCancel = false)
        {
            StringBuilder str = new StringBuilder();

            str.AppendLine("BEGIN:VCALENDAR");
            str.AppendLine("PRODID:-//Microsoft Corporation//Outlook 12.0 MIMEDIR//EN");
            str.AppendLine("VERSION:2.0");
            str.AppendLine(string.Format("METHOD:{0}", (isCancel ? "CANCEL" : "REQUEST")));
            str.AppendLine("METHOD:REQUEST");
            str.AppendLine("BEGIN:VEVENT");

            str.AppendLine(string.Format("CREATED:{0:yyyyMMddTHHmmssZ}", DateTime.Now.ToUniversalTime()));

            // Date format - yyyyMMddTHHmmssZ
            str.AppendLine(string.Format("DTSTART:{0}", startDate)); // 20231208T080000
            str.AppendLine(string.Format("DTSTAMP:{0:yyyyMMddTHHmmss}", DateTime.Now.ToUniversalTime()));
            str.AppendLine(string.Format("DTEND:{0:}", endDate)); // 20231208T090000

            str.AppendLine(string.Format("LAST-MODIFIED:{0:yyyyMMddTHHmmssZ}", DateTime.Now.ToUniversalTime()));

            // web meeting link
            str.AppendLine(string.Format("LOCATION: {0}", "d.Code Mobility Offices"));
            //or
            //str.AppendLine(string.Format("LOCATION: {0}", mailTemplate.EventLocation));

            str.AppendLine("PRIORITY: 5");
            str.AppendLine("SEQUENCE: 0");

            str.AppendLine(string.Format("UID:{0}", (eventID.HasValue ? "EventId" + eventID : Guid.NewGuid().ToString())));
            str.AppendLine(string.Format("DESCRIPTION:{0}", messageBody.Replace("\n", "<br>")));
            str.AppendLine(string.Format("X-ALT-DESC;FMTTYPE=text/html:{0}", messageBody.Replace("\n", "<br>")));
            str.AppendLine(string.Format("SUMMARY:{0}", subject));
            str.AppendLine("STATUS:CONFIRMED");
            str.AppendLine(string.Format("ORGANIZER;CN={0}:MAILTO:{1}", "jonathanqoqonga@gmail.com", "jonathanqoqonga@gmail.com"));
            str.AppendLine(string.Format("ATTENDEE;CN={0};RSVP=TRUE:mailto:{1}", string.Join(",", "jonathan@dcode.mobi"), string.Join(",", "jonathan@dcode.mobi")));

            str.AppendLine("BEGIN:VALARM");
            str.AppendLine("TRIGGER:-PT15M");
            str.AppendLine("ACTION:DISPLAY");
            str.AppendLine("DESCRIPTION:Reminder");
            str.AppendLine("END:VALARM");
            str.AppendLine("END:VEVENT");
            str.AppendLine("END:VCALENDAR");

            return str.ToString();
        }
    }
}

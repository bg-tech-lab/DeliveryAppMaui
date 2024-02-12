namespace DeliveryAppAPI.Methods
{
    public static class GettingEmailAddress
    {
        public static async Task<List<string>> GetEmailReceipients(string emailAddresses)
        {
            try
            {
                List<string> stringList = new List<string>(emailAddresses.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries));
                List<string> emailList = new List<string>();

                foreach (var item in stringList)
                {
                    emailList.Add(item);
                }

                return emailList;
            }
            catch (Exception ex)
            {
                return new List<string>();
            }
        }
    }
}

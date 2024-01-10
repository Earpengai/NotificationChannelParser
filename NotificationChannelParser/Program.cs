using System.Net.Http.Headers;
using System.Text.RegularExpressions;

namespace NotificationChannelParser
{
    internal class Program
    {
        /// <summary>
        /// Declare channels with HashSet to make sure there's no duplicate channel.
        /// Assign default channel.
        /// </summary>
        readonly static HashSet<string> channels = new() { "BE", "FE", "QA", "Urgent" };  
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the notification channels (e.g., [BE][FE][Urgent] your message):");
            var titleInput = Console.ReadLine();
            List<string> result = ReceiverMessage(titleInput ?? string.Empty);
            if (result.Count > 0)
                Console.WriteLine($"Receive channels: {string.Join(", ", result)}");
            else
                Console.WriteLine($"There is no channels to received!");
            
        }

        static List<string> ReceiverMessage(string titleMessage)
        {
            string pattern = @"\[([A-Za-z]+)\]";
            // Define the valid channel
            var matches = Regex.Matches(titleMessage, pattern);
            return matches.Cast<Match>().Select(p => p.Groups[1].Value)
                .Where(p => channels.Contains(p))
                .Distinct()
                .ToList();
        }
    }
}
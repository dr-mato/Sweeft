using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sweeft
{
    class Program
    {
        static bool sPalindrome(string text)
        {
            if (text == null || text == "") return true;

            var start = 0;
            var end = text.Length - 1;

            while (start < end)
            {
                if (text[start] != text[end]) { return false; }

                start++;
                end--;
            }

            return true;
        }



        static int MinSplit(int amount)
        {
            var fiftiesCount = amount / 50;
            var twentiesCount = (amount - 50 * fiftiesCount) / 20;
            var tensCount = (amount - 50 * fiftiesCount - 20 * twentiesCount) / 10;
            var fivesCount = (amount - 50 * fiftiesCount - 20 * twentiesCount - 10 * tensCount) / 5;
            var onesCount = amount - 50 * fiftiesCount - 20 * twentiesCount - 10 * tensCount - 5 * fivesCount;
            return fiftiesCount + twentiesCount + tensCount + fivesCount + onesCount;
        }



        static int NotContains(int[] array)
        {
            if (array == null) return 1;
            var hash = array.ToHashSet();
            var min = 1;
            while (hash.Contains(min)) { min++; }
            return min;
        }



        static bool IsProperly(string sequence)
        {
            if (sequence == null || sequence == "") return true;

            var stack = new Stack<char>();

            foreach (var c in sequence)
            {
                if (c == '(') { stack.Push(c); }
                else if (c == ')' && stack.Count > 0) { stack.Pop(); }
                else return false;
            }
            return stack.Count == 0;
        }



        static int CountVariants(int stairCount)
        {
            if (stairCount < 0) return 0;

            if (stairCount == 0 || stairCount == 1) return 1;

            int[] stairs = new int[stairCount + 1];
            stairs[0] = 1;
            stairs[1] = 1;

            for (int i = 2; i <= stairCount; i++)
            {
                stairs[i] = stairs[i - 1] + stairs[i - 2];
            }

            return stairs[stairCount];
        }



        static Teacher[] GetAllTeachersByStudent(string studentName)
        {
            using (var context = new AppDbContext())
            {
                return context.TeacherPupils
                    .Include(tp => tp.Teacher)
                    .Include(tp => tp.Pupil)
                    .Where(tp => tp.Pupil.FirstName == studentName)
                    .Select(tp => tp.Teacher)
                    .Distinct()
                    .ToArray();
            }
        }

        static string GetAllTeachersByStudentTest(string studentName)
        {
            var teachers = GetAllTeachersByStudent(studentName);

            if (teachers.Length == 0)
            {
                return $"No teachers found for student: {studentName}";
            }

            var result = $"Teachers for {studentName}:\n";
            foreach (var teacher in teachers)
            {
                result += $"- {teacher.FirstName} {teacher.LastName}, Subject: {teacher.Subject}\n";
            }
            return result;
        }



        static async Task GenerateCountryDataFiles()
        {
            var apiUrl = "https://restcountries.com/v3.1/all";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();

                    var jsonResponse = await response.Content.ReadAsStringAsync();

                    var options = new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    };

                    var countries = JsonSerializer.Deserialize<List<Country>>(jsonResponse, options);

                    //var i = 0;
                    foreach (var country in countries)
                    {
                        if (country.Name?.Common != null)
                        {
                            string fileName = $"{country.Name.Common.Replace("/", "-")}.txt";
                            string content = $"Region: {country.Region ?? "N/A"}\n" +
                                             $"Subregion: {country.Subregion ?? "N/A"}\n" +
                                             $"LatLng: {string.Join(", ", country.Latlng ?? new List<double>())}\n" +
                                             $"Area: {country.Area?.ToString() ?? "N/A"}\n" +
                                             $"Population: {country.Population?.ToString() ?? "N/A"}";

                            File.WriteAllText(fileName, content);
                        }

                        //i++;
                        //if (i == 5) break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }

        class Country
        {
            public Name Name { get; set; }
            public string Region { get; set; }
            public string Subregion { get; set; }
            public List<double> Latlng { get; set; }
            public double? Area { get; set; }
            public long? Population { get; set; }
        }

        class Name
        {
            public string Common { get; set; }
        }



        private static readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);
        private static bool isRunning = true;

        private static async Task PrintNumbersAsync()
        {
            while (isRunning)
            {
                await semaphore.WaitAsync();
                try
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("1");
                    Console.Write("0");
                }
                finally
                {
                    semaphore.Release();
                }
                await Task.Delay(100);
            }
        }

        private static async Task ShowMessagePeriodicallyAsync()
        {
            while (isRunning)
            {
                await Task.Delay(5000);

                await semaphore.WaitAsync();
                try
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("Neo, you are the chosen one");
                }
                finally
                {
                    semaphore.Release();
                }

                await Task.Delay(5000);
            }
        }

        static async Task Main(string[] args)
        {
            //Console.WriteLine(sPalindrome(""));
            //Console.WriteLine(sPalindrome("abc"));
            //Console.WriteLine(sPalindrome("abba"));
            //Console.WriteLine(sPalindrome("ab ab"));
            //Console.WriteLine(sPalindrome("ab ba"));
            //Console.WriteLine(MinSplit(92));
            //Console.WriteLine(MinSplit(-59));
            //Console.WriteLine(MinSplit(149));
            //Console.WriteLine(MinSplit(76));
            //Console.WriteLine(NotContains(new int[] { 1, 2, 3 }));
            //Console.WriteLine(NotContains(new int[] { 2, 3, 4 }));
            //Console.WriteLine(NotContains(new int[] { 1, 3, 4 }));
            //Console.WriteLine(NotContains(new int[] { }));
            //Console.WriteLine(IsProperly(null));
            //Console.WriteLine(IsProperly(""));
            //Console.WriteLine(IsProperly("(()())"));
            //Console.WriteLine(IsProperly(")()("));
            //Console.WriteLine(CountVariants(0));
            //Console.WriteLine(CountVariants(2));
            //Console.WriteLine(CountVariants(5));
            //Console.WriteLine(CountVariants(10));
            //Console.WriteLine(GetAllTeachersByStudentTest("გიორგი"));
            //await GenerateCountryDataFiles();
            //var numberTask = Task.Run(PrintNumbersAsync);
            //var messageTask = Task.Run(ShowMessagePeriodicallyAsync);
            //await Task.WhenAll(numberTask, messageTask);
        }
    }
}


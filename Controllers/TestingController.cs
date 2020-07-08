using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContinueWatchingFeature.Data;
using ContinueWatchingFeature.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContinueWatchingFeature.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestingController : ControllerBase
    {
        private readonly ContinueWatchingFeatureContext _context;

        public TestingController(ContinueWatchingFeatureContext context)
        {
            _context = context;
        }
        [HttpPost("seed")]

        public ActionResult<Result> seed([Bind("FlushDB","UsersCount","SeasonsCount","SeriesCount","MoviesCount","EpsoidsCount","SeeksCount")] SeedOptions seedOptions)
        {
            String text = "Done";

            AddUsers(seedOptions.UsersCount, seedOptions.FlushDB);
            AddSeries(seedOptions.SeriesCount,seedOptions.SeasonsCount, seedOptions.FlushDB);
            AddMovies(seedOptions.MoviesCount, seedOptions.FlushDB);
            AddEpsoids(seedOptions.EpsoidsCount, seedOptions.FlushDB);
            AddSeeks(seedOptions.SeeksCount, seedOptions.FlushDB);

            return new Result{ Text = text };
        }

        private  void AddUsers(int usersCount, bool flush)
        {
            if (flush)
            {
                var users = _context.Users;
                _context.Users.RemoveRange(users);
                 _context.SaveChanges();
            }

            Console.WriteLine("********************************************");
            Console.WriteLine("Seeding Users");

            Random rand = new Random();
            String[] names = { "Aalam", "Aali", "Aaliyah", "Abbas", "Abdalah", "Abdukrahman", "Abdul", "Abdulkareem", "Abdullah", "Abdulrahman", "Abednego", "Abia", "Abla", "Adil", "Adila", "Adli", "Adnan", "Afaf", "Ahmad", "Ahmaud", "Ahmed", "Aisha", "Aizza", "Akeem", "Akon", "Akram", "Ala", "Aladdin", "Alawi", "Alem", "Ali", "Alia", "Alim", "Alima", "Aliya", "Aliyah", "Aliyya", "Almas", "Almonzo", "Altaf", "Alya", "Amal", "Aman", "Amar", "Amaya", "Amena", "Amil", "Amin", "Amina", "Aminah", "Amir", "Amira", "Amirah", "Amiri", "Amjad", "Ammar", "Amna", "Anas", "Anass", "Anisa", "Anwar", "Anwer", "Aqil", "Aqila", "Arif", "Arifah", "Elam", "Elyes", "Eman", "Esmail", "Eyad", "Fadi", "Fadia", "Fahd", "Fahima", "Faisal", "Faiz", "Fakhiri", "Farah", "Fareeda", "Fariat", "Farid", "Farida", "Faris", "Faruq", "Fathi", "Fathia", "Fathiyya", "Fatima", "Fatimah", "Fatin", "Fatma", "Fawzi", "Fawzia", "Fayiz", "Fayruz", "Faysal", "Fazal", "Feryal", "Fidda", "Firuz", "Fizza", "Galal", "Gazali", "Ghada", "Ghadir", "Ghaith", "Ghaleb", "Ghalib", "Ghassan", "Ghayth", "Ghazi", "Giza", "Gizeh", "Gulzar", "Habib", "Habiba", "Hadi", "Hadia", "Hafiz", "Hafsa", "Haidar", "Hajar", "Hajra", "Hakeem", "Hakim", "Hala", "Halima", "Hamal", "Hamid", "Hamida", "Hamza" };
            int len = names.Length;
            string name = "";
            for (int i = 0; i < usersCount; i++)
            {
                name = names[rand.Next(0, len)] + " " + names[rand.Next(0, len)];
                _context.Users.Add(new User { Name = name });
            }
             _context.SaveChanges();
        }

        private  void AddMovies(int moviesCount, bool flush)
        {
            if (flush)
            {
                var movies = _context.Movies;
                _context.Movies.RemoveRange(movies);
                 _context.SaveChanges();
            }

            Console.WriteLine("********************************************");
            Console.WriteLine("Seeding Movies");

            Random rand = new Random();
            String[] words = { "a", "abandon", "ability", "able", "abortion", "about", "above", "abroad", "absence", "absolute", "absolutely", "absorb", "abuse", "academic", "accept", "access", "accident", "accompany", "accomplish", "according", "add", "addition", "additional", "address", "adequate", "adjust", "adjustment", "administration", "administrator", "admire", "admission", "admit", "adolescent", "adopt", "adult", "advance", "advanced", "advantage", "adventure", "advertising", "breath", "breathe", "brick", "bridge", "brief", "briefly", "bright", "brilliant", "bring", "british", "broad", "broken", "brother", "brown", "brush", "buck", "budget", "build", "building", "bullet", "bunch", "burden", "burn", "bury", "bus", "business", "busy", "but", "butter", "button", "buy", "buyer", "by", "cabin", "cabinet", "cable", "cake", "calculate", "call", "camera", "camp", "campaign", "campus", "can", "canadian", "cancer", "candidate", "cap", "capability", "capable", "capacity", "capital", "captain", "capture", "car", "carbon", "card", "care", "career", "careful", "carefully", "carrier", "carry", "case", "cash", "cast", "cat", "catch", "category", "catholic", "cause", "ceiling", "celebrate", "celebration", "celebrity", "cell", "center", "central", "century", "cEO", "ceremony", "contribute", "contribution", "control", "controversial", "controversy", "convention", "conventional", "conversation", "convert", "conviction", "convince", "cook", "cookie", "cooking", "cool", "cooperation", "cop", "courage", "critic", "critical", "criticism", "criticize", "crop", "cross", "crowd", "crucial", "cry", "cultural", "culture", "cup", "curious", "current", "currently", "curriculum", "custom", "customer", "cut", "cycle", "dad", "daily", "damage", "dance", "danger", "dangerous", "dare", "dark", "darkness", "data", "differently", "difficult", "difficulty", "dig", "digital", "dimension", "dining", "dinner", "direct", "direction", "directly", "director", "dirt", "dirty", "disability", "disagree", "disappear", "disaster", "discipline", "discourse", "discover", "discovery", "discrimination", "discuss", "discussion", "disease", "dish", "dismiss", "disorder", "display", "dispute", "distance", "distant", "during", "dust", "duty", "each", "eager", "ear", "early", "earn", "earnings", "earth", "ease", "easily", "east", "eastern", "easy", "eat", "economic", "economics", "economist", "economy", "edge", "edition", "editor", "educate", "education", "educational", "educator", "effect", "effective", "effectively", "efficiency", "efficient", "effort", "egg", "eight", "either", "elderly", "elect", "election", "electric", "electricity", "electronic", "element", "elementary", "eliminate", "elite", "else", "elsewhere", "e-mail", "embrace", "emerge", "emergency", "emission", "emotion", "emotional", "emphasis", "emphasize", "exchange", "exciting", "executive", "exercise", "exhibit", "exhibition", "exist", "existence", "existing", "expand", "expansion", "expect", "expectation", "expense", "expensive", "experience", "experiment", "expert", "explain", "explanation", "explode", "explore", "explosion", "expose", "exposure", "express", "expression", "extend", "extension", "extensive", "extent", "external", "extra", "extraordinary", "extreme", "extremely", "eye", "fabric", "face", "facility", "fact", "factor", "factory", "faculty", "fade", "fail", "failure", "fair", "flesh", "flight", "float", "floor", "flow", "flower", "fly", "focus", "folk", "follow", "following", "food", "foot", "football", "for", "force", "foreign", "forest", "forever", "forget", "form", "formal", "formation", "former", "formula", "forth", "gold", "golden", "golf", "good", "government", "governor", "grab", "grade", "gradually", "graduate", "ground", "group", "grow", "growing" };
            String[] stopWords = { "i", "me", "my", "myself", "we", "our", "ours", "ourselves", "you", "your", "yours", "yourself", "yourselves", "he", "him", "his", "himself", "she", "her", "hers", "herself", "it", "its", "itself", "they", "them", "their", "theirs", "themselves", "what", "which", "who", "whom", "this", "that", "these", "those", "am", "is", "are", "was", "were", "be", "been", "being", "have", "has", "had", "having", "do", "does", "did", "doing", "a", "an", "the", "and", "but", "if", "or", "because", "as", "until", "while", "of", "at", "by", "for", "with", "about", "against", "between", "into", "through", "during", "before", "after", "above", "below", "to", "from", "up", "down", "in", "out", "on", "off", "over", "under", "again", "further", "then", "once", "here", "there", "when", "where", "why", "how", "all", "any", "both", "each", "few", "more", "most", "other", "some", "such", "no", "nor", "not", "only", "own", "same", "so", "than", "too", "very", "s", "t", "can", "will", "just", "don", "should", "now" };
            int len = words.Length;
            int len2 = stopWords.Length;
            string name;
            for (int i = 0; i < moviesCount; i++)
            {
                name = words[rand.Next(0, len)] + " " + stopWords[rand.Next(0, len2)] + " " + words[rand.Next(0, len)];
                _context.Movies.Add(new Movie { Name = name,Length=rand.Next(1200,7200) });
            }
             _context.SaveChanges();
        }
        private  void AddSeries(int seriesCount,int seasonsCount, bool flush)
        {
            if (flush)
            {
                var series = _context.Series;
                _context.Series.RemoveRange(series);
                 _context.SaveChanges();
            }

            Console.WriteLine("********************************************");
            Console.WriteLine("Seeding Series");

            Random rand = new Random();
            String[] words = { "a", "abandon", "ability", "able", "abortion", "about", "above", "abroad", "absence", "absolute", "absolutely", "absorb", "abuse", "academic", "accept", "access", "accident", "accompany", "accomplish", "according", "add", "addition", "additional", "address", "adequate", "adjust", "adjustment", "administration", "administrator", "admire", "admission", "admit", "adolescent", "adopt", "adult", "advance", "advanced", "advantage", "adventure", "advertising", "breath", "breathe", "brick", "bridge", "brief", "briefly", "bright", "brilliant", "bring", "british", "broad", "broken", "brother", "brown", "brush", "buck", "budget", "build", "building", "bullet", "bunch", "burden", "burn", "bury", "bus", "business", "busy", "but", "butter", "button", "buy", "buyer", "by", "cabin", "cabinet", "cable", "cake", "calculate", "call", "camera", "camp", "campaign", "campus", "can", "canadian", "cancer", "candidate", "cap", "capability", "capable", "capacity", "capital", "captain", "capture", "car", "carbon", "card", "care", "career", "careful", "carefully", "carrier", "carry", "case", "cash", "cast", "cat", "catch", "category", "catholic", "cause", "ceiling", "celebrate", "celebration", "celebrity", "cell", "center", "central", "century", "cEO", "ceremony", "contribute", "contribution", "control", "controversial", "controversy", "convention", "conventional", "conversation", "convert", "conviction", "convince", "cook", "cookie", "cooking", "cool", "cooperation", "cop", "courage", "critic", "critical", "criticism", "criticize", "crop", "cross", "crowd", "crucial", "cry", "cultural", "culture", "cup", "curious", "current", "currently", "curriculum", "custom", "customer", "cut", "cycle", "dad", "daily", "damage", "dance", "danger", "dangerous", "dare", "dark", "darkness", "data", "differently", "difficult", "difficulty", "dig", "digital", "dimension", "dining", "dinner", "direct", "direction", "directly", "director", "dirt", "dirty", "disability", "disagree", "disappear", "disaster", "discipline", "discourse", "discover", "discovery", "discrimination", "discuss", "discussion", "disease", "dish", "dismiss", "disorder", "display", "dispute", "distance", "distant", "during", "dust", "duty", "each", "eager", "ear", "early", "earn", "earnings", "earth", "ease", "easily", "east", "eastern", "easy", "eat", "economic", "economics", "economist", "economy", "edge", "edition", "editor", "educate", "education", "educational", "educator", "effect", "effective", "effectively", "efficiency", "efficient", "effort", "egg", "eight", "either", "elderly", "elect", "election", "electric", "electricity", "electronic", "element", "elementary", "eliminate", "elite", "else", "elsewhere", "e-mail", "embrace", "emerge", "emergency", "emission", "emotion", "emotional", "emphasis", "emphasize", "exchange", "exciting", "executive", "exercise", "exhibit", "exhibition", "exist", "existence", "existing", "expand", "expansion", "expect", "expectation", "expense", "expensive", "experience", "experiment", "expert", "explain", "explanation", "explode", "explore", "explosion", "expose", "exposure", "express", "expression", "extend", "extension", "extensive", "extent", "external", "extra", "extraordinary", "extreme", "extremely", "eye", "fabric", "face", "facility", "fact", "factor", "factory", "faculty", "fade", "fail", "failure", "fair", "flesh", "flight", "float", "floor", "flow", "flower", "fly", "focus", "folk", "follow", "following", "food", "foot", "football", "for", "force", "foreign", "forest", "forever", "forget", "form", "formal", "formation", "former", "formula", "forth", "gold", "golden", "golf", "good", "government", "governor", "grab", "grade", "gradually", "graduate", "ground", "group", "grow", "growing" };
            String[] stopWords = { "i", "me", "my", "myself", "we", "our", "ours", "ourselves", "you", "your", "yours", "yourself", "yourselves", "he", "him", "his", "himself", "she", "her", "hers", "herself", "it", "its", "itself", "they", "them", "their", "theirs", "themselves", "what", "which", "who", "whom", "this", "that", "these", "those", "am", "is", "are", "was", "were", "be", "been", "being", "have", "has", "had", "having", "do", "does", "did", "doing", "a", "an", "the", "and", "but", "if", "or", "because", "as", "until", "while", "of", "at", "by", "for", "with", "about", "against", "between", "into", "through", "during", "before", "after", "above", "below", "to", "from", "up", "down", "in", "out", "on", "off", "over", "under", "again", "further", "then", "once", "here", "there", "when", "where", "why", "how", "all", "any", "both", "each", "few", "more", "most", "other", "some", "such", "no", "nor", "not", "only", "own", "same", "so", "than", "too", "very", "s", "t", "can", "will", "just", "don", "should", "now" };
            int len = words.Length;
            int len2 = stopWords.Length;
            string name = "";
            for (int i = 0; i < seriesCount; i++)
            {
                name = words[rand.Next(0, len)] + " " + stopWords[rand.Next(0, len2)] + " " + words[rand.Next(0, len)]+" Series";
                _context.Series.Add(new Series { Name = name,Seasons=seasonsCount });
            }
             _context.SaveChanges();
        }

        private  void AddEpsoids(int epsoidesCount, bool flush)
        {
            if (flush)
            {
                var epsoides = _context.Epsoides;
                _context.Epsoides.RemoveRange(epsoides);
                 _context.SaveChanges();
            }

            Console.WriteLine("********************************************");
            Console.WriteLine("Seeding Epsoids");

            Random rand = new Random();

            List<Series> series = _context.Series.ToList();
            int epsoidesPerSeries= epsoidesCount / series.Count();
            int epsoidesPerSeason;

            for (int i = 0; i < series.Count(); i++)
            {
                epsoidesPerSeason = epsoidesPerSeries / series[i].Seasons;
                for (int j = 0; j < series[i].Seasons; j++)
                {
                    for (int k = 0; k < epsoidesPerSeason; k++)
                    {
                        _context.Epsoides.Add(new Epsoide { Series_Id=series[i].Id , Length=rand.Next(600,4000),Season=j });
                    }
                }
            }
             _context.SaveChanges();
        }

        private void AddSeeks(int seeksCount, bool flush)
        {
            if (flush)
            {
                var seeks = _context.Still_Watchings;
                _context.Still_Watchings.RemoveRange(seeks);
                _context.SaveChanges();
            }

            Console.WriteLine("********************************************");
            Console.WriteLine("Seeding Seeks");

            Random rand = new Random();
            List<User> users = _context.Users.ToList();
            List<Movie> movies = _context.Movies.ToList();
            List<Series> series = _context.Series.ToList();
            List<Epsoide> epsoids = _context.Epsoides.ToList();
            int epsoidsPerSeason = _context.Epsoides.Where(x => x.Series_Id == 1 && x.Season == 0).Count();


            Console.WriteLine("Seeding movies seeks");

            int seeksForMovies, seeksPerUser, seeksPerMovie, seeksPerSeries, seeksForSeries;


            seeksForMovies = seeksCount / 2;
            seeksPerUser = seeksForMovies / users.Count();
            for (int i = 0; i < users.Count(); i++)
            {
                seeksPerMovie = seeksPerUser / movies.Count();
                for (int j = 0; j < movies.Count(); j++)
                {
                    for (int k = 0; k < seeksPerMovie; k++)
                    {
                        _context.Still_Watchings.Add(new Still_Watching { Media_Id = movies[j].Id, Type = 0, User_id = users[i].Id, SeekPosition = rand.Next(0, movies[j].Length) });
                    }
                }
            }

            _context.SaveChanges();

            Console.WriteLine("Seeding series seeks");
            seeksForSeries = seeksCount / 2;
            
            seeksPerUser = seeksForSeries / users.Count();
            for (int i = 0; i < users.Count(); i++)
            {
                seeksPerSeries = seeksPerUser / epsoids.Count();
                for (int j = 0; j < epsoids.Count(); j++)
                {
                    for (int k = 0; k < seeksPerSeries; k++)
                    {
                        _context.Still_Watchings.Add(new Still_Watching { Media_Id = epsoids[j].Id, Type = 1, User_id = users[i].Id, SeekPosition = rand.Next(0, epsoids[j].Length) });

                    }
                }



            }

            _context.SaveChanges();
            Console.WriteLine("Seeding Done");
        }

    }
}

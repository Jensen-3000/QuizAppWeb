using QuizAppWeb.Models;
using System.Text.Json;

namespace QuizAppWeb.Services
{
    public class QuizDataService
    {
        private readonly HttpClient _httpClient;

        public QuizDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Question>> LoadQuestionsAsync(string selectedQuiz)
        {
            string filePath = @$"/Assets/{selectedQuiz}.json";
            string jsonData = await _httpClient.GetStringAsync(filePath);
            return JsonSerializer.Deserialize<List<Question>>(jsonData);
        }

        public async Task<List<string>> LoadQuizzesAsync()
        {
            string filePath = $"/Assets/quizzes.json";
            string jsonData = await _httpClient.GetStringAsync(filePath);
            return JsonSerializer.Deserialize<List<string>>(jsonData);
        }

        public void RandomizeOptions(List<Question> questions)
        {
            var random = new Random();
            foreach (Question question in questions)
            {
                question.QuestionOptions = question.QuestionOptions.OrderBy(s => random.Next()).ToList();
            }
        }
    }
}

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
            string filePath = $"/Assets/{selectedQuiz}.json";
            string jsonData = await _httpClient.GetStringAsync(filePath);
            return JsonSerializer.Deserialize<List<Question>>(jsonData);
        }

        public async Task<List<string>> LoadQuizzesAsync()
        {
            string filePath = $"/Assets/_List of Quizzes.json";
            string jsonData = await _httpClient.GetStringAsync(filePath);
            return JsonSerializer.Deserialize<List<string>>(jsonData);
        }

        public List<Question> RandomizeQuestionsAndOptions(List<Question> questions)
        {
            var random = new Random();

            // Shuffle the list of questions
            questions = questions.OrderBy(q => random.Next()).ToList();

            foreach (Question question in questions)
            {
                // Shuffle the options within each question
                question.QuestionOptions = question.QuestionOptions.OrderBy(s => random.Next()).ToList();
            }

            return questions;
        }
    }
}

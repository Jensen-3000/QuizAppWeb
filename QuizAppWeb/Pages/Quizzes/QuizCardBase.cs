using Microsoft.AspNetCore.Components;
using QuizAppWeb.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace QuizAppWeb.Pages.Quizzes
{
    public class QuizCardBase : ComponentBase
    {
        public List<Question> Questions { get; set; } = new List<Question>();

        protected int questionIndex = 0;
        protected int score = 0;
        const string quizFolderPath = "/Assets/prog_quiz.json";

        [Inject]
        protected HttpClient httpClient { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadQuestions(quizFolderPath);
            RandomizeOptions(Questions);
            await base.OnInitializedAsync();
        }

        protected void OptionSelected(string option)
        {
            if (option == Questions[questionIndex].QuestionCorrectAnswer)
            {
                score++;
            }
            questionIndex++;
        }

        protected void RestartQuiz()
        {
            score = 0;
            questionIndex = 0;
            RandomizeOptions(Questions);
        }

        private async Task LoadQuestions(string filePath)
        {
            string jsonData = await httpClient.GetStringAsync(filePath);
            Questions = JsonSerializer.Deserialize<List<Question>>(jsonData);
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

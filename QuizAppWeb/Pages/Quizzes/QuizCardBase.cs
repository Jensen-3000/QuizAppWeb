using Microsoft.AspNetCore.Components;
using QuizAppWeb.Models;
using System.Text.Json;

namespace QuizAppWeb.Pages.Quizzes
{
    public class QuizCardBase : ComponentBase
    {
        public List<Question> Questions { get; set; } = new List<Question>();

        protected int questionIndex = 0;
        protected int score = 0;

        [Inject]
        protected HttpClient httpClient { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadQuestionsTest("prog_quiz");
            RandomizeOptions(Questions);
            await base.OnInitializedAsync();
        }

        private async Task LoadQuestionsTest(string quizFilename)
        {
            string filePath = $"/Assets/{quizFilename}.json";
            string jsonData = await httpClient.GetStringAsync(filePath);
            Questions = JsonSerializer.Deserialize<List<Question>>(jsonData);
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

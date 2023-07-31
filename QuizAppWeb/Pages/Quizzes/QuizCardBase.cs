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
            await LoadQuestions();
            await base.OnInitializedAsync();
        }

        protected void OptionSelected(string option)
        {
            if (option == Questions[questionIndex].QuestionOptions.ElementAt(Questions[questionIndex].QuestionCorrectAnswerIndex))
            {
                score++;
            }
            questionIndex++;
        }

        protected void RestartQuiz()
        {
            score = 0;
            questionIndex = 0;
        }

        private async Task LoadQuestions()
        {
            string jsonData = await httpClient.GetStringAsync("/Assets/prog_quiz.json");
            Questions = JsonSerializer.Deserialize<List<Question>>(jsonData);
        }
    }
}

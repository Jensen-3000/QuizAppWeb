﻿using Microsoft.AspNetCore.Components;
using QuizAppWeb.Models;
using QuizAppWeb.Services;
using System.Text.Json;

namespace QuizAppWeb.Pages.Quizzes
{
    public class QuizCardBase : ComponentBase
    {
        [Inject]
        protected HttpClient HttpClient { get; set; }

        [Inject]
        protected QuizDataService QuizDataService { get; set; }

        protected List<Question> QuestionsList { get; set; } = new List<Question>();
        protected List<string> QuizzesList { get; set; } = new List<string>();

        protected int questionIndex = 0;
        protected int score = 0;
        protected string selectedQuiz = string.Empty;
        protected bool quizStarted = false;

        protected override async Task OnInitializedAsync()
        {
            QuizzesList = await QuizDataService.LoadQuizzesAsync();
        }

        protected async Task OnQuizSelectionChanged(ChangeEventArgs e)
        {
            selectedQuiz = e.Value.ToString();
            if (!string.IsNullOrEmpty(selectedQuiz))
            {
                QuestionsList = await QuizDataService.LoadQuestionsAsync(selectedQuiz);
                QuizDataService.RandomizeOptions(QuestionsList);
                quizStarted = true;
            }
            else
            {
                quizStarted = false;
            }
        }

        protected void OptionSelected(string option)
        {
            if (option == QuestionsList[questionIndex].QuestionCorrectAnswer)
            {
                score++;
            }
            questionIndex++;
        }

        protected void RestartQuiz()
        {
            score = 0;
            questionIndex = 0;
            quizStarted = false;
            QuizDataService.RandomizeOptions(QuestionsList);
        }
    }
}

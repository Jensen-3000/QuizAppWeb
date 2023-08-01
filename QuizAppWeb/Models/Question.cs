using System.Text.Json.Serialization;

namespace QuizAppWeb.Models
{
    public class Question
    {
        [JsonPropertyName("QuestionText")]
        public string QuestionText { get; set; } = string.Empty;

        [JsonPropertyName("Options")]
        public List<string> QuestionOptions { get; set; } = new List<string>();

        [JsonPropertyName("CorrectAnswer")]
        public string QuestionCorrectAnswer { get; set; } = string.Empty;

        [JsonPropertyName("CodeSnippet")]
        public string QuestionCodeSnippet { get; set; } = string.Empty;
    }
}

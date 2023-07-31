using System.Text.Json.Serialization;

namespace QuizAppWeb.Models
{
    public class Question
    {
        [JsonPropertyName("QuestionText")]
        public string QuestionText { get; set; } = string.Empty;

        [JsonPropertyName("Options")]
        public IEnumerable<string> QuestionOptions { get; set; } = new List<string>();

        [JsonPropertyName("CorrectAnswerIndex")]
        public int QuestionCorrectAnswerIndex { get; set; }

        [JsonPropertyName("CodeSnippet")]
        public string QuestionCodeSnippet { get; set; } = string.Empty;
    }
}

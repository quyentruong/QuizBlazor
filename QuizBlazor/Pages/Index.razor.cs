using System.Net.Http.Json;
using MoreLinq;
using QuizBlazor.Models;

namespace QuizBlazor.Pages
{
    public partial class Index
    {
        private string Value { get; } = "text *italics* and **bold**";
        private int currentQuesNumber = 0;
        private string selectedOption = string.Empty;

        private int totalCorrect = 0;
        private int totalInCorrect = 0;
        private bool isAnswer = false;
        private readonly int maxQuestion = 10;

        private List<Quiz>? quizzes;
        private List<Quiz>? randomQuizzes;

        protected override async Task OnInitializedAsync()
        {
            quizzes = await Http.GetFromJsonAsync<List<Quiz>>("data/sqlcode.json");
            randomQuizzes = quizzes?.RandomSubset(maxQuestion).ToList();
        }

        private void OptionChanged(string selectedOption)
        {
            this.selectedOption = selectedOption;
            if (selectedOption.Equals("True"))
            {
                totalCorrect++;
            }
            else if (selectedOption.Equals("False"))
            {
                totalInCorrect++;
            }

            isAnswer = true;
        }

        public void NextQuestion()
        {
            currentQuesNumber++;
            isAnswer = false;
        }

        public void Restart()
        {
            randomQuizzes = quizzes?.RandomSubset(maxQuestion).ToList();
            currentQuesNumber = 0;
            totalCorrect = 0;
            totalInCorrect = 0;
            isAnswer = false;
        }
    }
}
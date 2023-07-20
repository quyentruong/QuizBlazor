using System.Net.Http.Json;
using System.Text.Json.Serialization;
using MoreLinq;
using Newtonsoft.Json;
using QuizBlazor.Models;
using QuizBlazor.Utils;

namespace QuizBlazor.Pages
{
    public partial class Index
    {
        private int currentQuesNumber = 0;
        private string selectedOption = string.Empty;

        private int totalCorrect = 0;
        private int totalInCorrect = 0;
        private bool isAnswer = false;
        private readonly int maxQuestion = 10;

        private List<Quiz>? quizzes;
        private List<Quiz>? randomQuizzes;
        private List<Answer>? shuffleAnswers;
        private Answer? correctAnswer;
        private Quiz? currentQuiz = null;

        protected override async Task OnInitializedAsync()
        {
            var quizDeflate = await Http.GetStringAsync("deflate/sqlcode.txt");
            quizzes = JsonConvert.DeserializeObject<List<Quiz>>(BasicUtility.DecompressStringDeflate(quizDeflate));
            randomQuizzes = quizzes?.RandomSubset(maxQuestion).ToList();
            ShuffleAnswer();
        }

        private void ShuffleAnswer()
        {
            if (currentQuesNumber < randomQuizzes?.Count)
            {
                currentQuiz = randomQuizzes[currentQuesNumber];
                shuffleAnswers = MoreEnumerable.Shuffle(currentQuiz.Answers).ToList();
                correctAnswer = shuffleAnswers.Where(x => x.IsCorrect).FirstOrDefault();
            }
        }

        private void OptionChanged(string selectedOption)
        {
            this.selectedOption = selectedOption;
            if (selectedOption.Contains("True"))
            {
                totalCorrect++;
            }
            else if (selectedOption.Contains("False"))
            {
                totalInCorrect++;
            }

            isAnswer = true;
        }

        public void NextQuestion()
        {
            currentQuesNumber++;
            isAnswer = false;
            ShuffleAnswer();
        }

        public void Restart()
        {
            randomQuizzes = quizzes?.RandomSubset(maxQuestion).ToList();
            currentQuesNumber = 0;
            totalCorrect = 0;
            totalInCorrect = 0;
            isAnswer = false;
            ShuffleAnswer();
        }
    }
}
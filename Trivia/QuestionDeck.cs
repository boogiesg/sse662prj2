using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Trivia
{
    public class QuestionDeck
    {
        private readonly LinkedList<string> popQuestions;
        private readonly LinkedList<string> scienceQuestions;
        private readonly LinkedList<string> sportsQuestions;
        private readonly LinkedList<string> rockQuestions;

        public QuestionDeck()
        {
            popQuestions = new LinkedList<string>();
            scienceQuestions = new LinkedList<string>();
            sportsQuestions = new LinkedList<string>();
            rockQuestions = new LinkedList<string>();
        }

        public void FillQuestions()
        {
            for (var i = 0; i < 50; i++)
            {
                popQuestions.AddLast(CreateQuestion(i, "Pop"));
                scienceQuestions.AddLast(CreateQuestion(i, "Science"));
                sportsQuestions.AddLast(CreateQuestion(i, "Sports"));
                rockQuestions.AddLast(CreateQuestion(i, "Rock"));
            }
        }

        public string CreateQuestion(int index, string category)
        {
            return category + " Question " + index;
        }

        //method extracted from currentcategory in Game class
        public string CurrentCategoryPlace(int currentPlace)
        {
            if (currentPlace == 0) return "Pop";
            if (currentPlace == 4) return "Pop";
            if (currentPlace == 8) return "Pop";
            if (currentPlace == 1) return "Science";
            if (currentPlace == 5) return "Science";
            if (currentPlace == 9) return "Science";
            if (currentPlace == 2) return "Sports";
            if (currentPlace == 6) return "Sports";
            if (currentPlace == 10) return "Sports";
            return "Rock";
        }

        public string AskCategoryQuestion(string category)
        {
            string question = null;

            if (category == "Pop")
            {
                question = popQuestions.First();
                popQuestions.RemoveFirst();
            }
            else if (category == "Science")
            {
                question = scienceQuestions.First();
                scienceQuestions.RemoveFirst();
            }
            else if (category == "Sports")
            {
                question = sportsQuestions.First();
                sportsQuestions.RemoveFirst();
            }
            else if (category == "Rock")
            {
                question = rockQuestions.First();
                rockQuestions.RemoveFirst();
            }
            return question;
        }
    }
}

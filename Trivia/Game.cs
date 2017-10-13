using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UglyTrivia;

namespace Trivia
{
    public class Game
    {
        Player _CurrentPlayer;
        List<Player> Players = new List<Player>();
        HashSet<Player> PenaltyBox = new HashSet<Player>();
        bool isGettingOutOfPenaltyBox;
        private QuestionDeck questionDeck;  //JCook private instance of QuestionDeck 

        public Game()
        {
            questionDeck = new QuestionDeck();  
            questionDeck.FillQuestions();
            //JCook - Refactored into QuestionDeck class
            //for (int i = 0; i < 50; i++)
            //{
            //    popQuestions.AddLast("Pop Question " + i);
            //    scienceQuestions.AddLast(("Science Question " + i));
            //    sportsQuestions.AddLast(("Sports Question " + i));
            //    rockQuestions.AddLast(createRockQuestion(i));
            //}
        }

        //John Cook - Moved all question creation into QuestionDeck.createQuestion()
        //public String createRockQuestion(int index)
        //{
        //    return "Rock Question " + index;
        //}

        //JCook "I don't believe isPlayable() needs refactoring. Thoughts?"
        //Changed to use Count directly to remove extra method call
        public bool isPlayable()
        {
            return Players.Count >= 2;
        }
        
        //JCook "I don't believe add(playerName) needs refactoring. Thoughts?"
        //With addition of Player class it was necessary
        public void AddPlayer(string playerName)
        {
            Players.Add(new Player(playerName));

            Console.WriteLine($"{playerName} was added");
            Console.WriteLine($"They are player number {Players.Count}");
        }

        //JCook "I don't believe howManyPlayers() needs refactoring. Thoughts?"
        //Brian Looks fine to me
        public int howManyPlayers()
        {
            return Players.Count;
        }

        Player GetNextPlayer()
        {
            if (_CurrentPlayer == null)
            {
                _CurrentPlayer = Players.FirstOrDefault();
                if (_CurrentPlayer == null)
                {
                    throw new InvalidOperationException("No players found!");
                }
                return _CurrentPlayer;
            }
            else
            {
                int CurrentPlayerIndex = Players.IndexOf(_CurrentPlayer);
                return Players.ElementAtOrDefault(CurrentPlayerIndex + 1) ?? Players[0];
            }
        }

        public void EvaluateRoll(int roll)
        {
            _CurrentPlayer = GetNextPlayer();

            Console.WriteLine($"{_CurrentPlayer.Name} is the current player");
            Console.WriteLine($"They have rolled a {roll}");

            if (roll % 2 == 0 && PenaltyBox.Contains(_CurrentPlayer))
            {
                Console.WriteLine($"{_CurrentPlayer.Name} is not getting out of the penalty box");
                isGettingOutOfPenaltyBox = false;
            }
            else
            {
                if (PenaltyBox.Contains(_CurrentPlayer))
                {
                    Console.WriteLine($"{_CurrentPlayer.Name} is getting out of the penalty box");
                    isGettingOutOfPenaltyBox = true;
                }

                _CurrentPlayer.MovePosition(roll);
                Console.WriteLine($"{_CurrentPlayer.Name}'s new location is {_CurrentPlayer.Position}");

                Console.WriteLine("The category is " + currentCategory());
                askQuestion();
            }
        }

        private void askQuestion()
        {
            var question = questionDeck.AskCategoryQuestion(currentCategory());
            Console.WriteLine(question);
            //John Cook - moved to questiondeck class
            //if (currentCategory() == "Pop")
            //{
            //    Console.WriteLine(popQuestions.First());
            //    popQuestions.RemoveFirst();
            //}
            //if (currentCategory() == "Science")
            //{
            //    Console.WriteLine(scienceQuestions.First());
            //    scienceQuestions.RemoveFirst();
            //}
            //if (currentCategory() == "Sports")
            //{
            //    Console.WriteLine(sportsQuestions.First());
            //    sportsQuestions.RemoveFirst();
            //}
            //if (currentCategory() == "Rock")
            //{
            //    Console.WriteLine(rockQuestions.First());
            //    rockQuestions.RemoveFirst();
            //}
        }


        private String currentCategory()
        {
            //category tracking method moved to questiondeck class
            return questionDeck.CurrentCategoryPlace(_CurrentPlayer.Position);
            
            //if (places[currentPlayer] == 0) return "Pop";
            //if (places[currentPlayer] == 4) return "Pop";
            //if (places[currentPlayer] == 8) return "Pop";
            //if (places[currentPlayer] == 1) return "Science";
            //if (places[currentPlayer] == 5) return "Science";
            //if (places[currentPlayer] == 9) return "Science";
            //if (places[currentPlayer] == 2) return "Sports";
            //if (places[currentPlayer] == 6) return "Sports";
            //if (places[currentPlayer] == 10) return "Sports";
            //return "Rock";
        }

        //Returns true if NOT a winner (TODO: fix this)
        public bool wasCorrectlyAnswered()
        {
            if (PenaltyBox.Contains(_CurrentPlayer) && !isGettingOutOfPenaltyBox)
            {
                return true;
            }
            else
            {
                Console.WriteLine("Answer was correct!!!!");
                _CurrentPlayer.Purse.AddGoldCoin();
                Console.WriteLine($"{_CurrentPlayer.Name} now has {_CurrentPlayer.Purse.GoldCoins} Gold Coins.");

                return hasPlayerWon();
            }
        }

        public bool wrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine($"{_CurrentPlayer.Name} was sent to the penalty box");
            PenaltyBox.Add(_CurrentPlayer);

            return true;
        }

        private bool hasPlayerWon()
        {
            return _CurrentPlayer.Purse.GoldCoins != 6;
        }
    }

}

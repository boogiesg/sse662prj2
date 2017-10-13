using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trivia;

namespace TriviaUnitTestProject
{
    [TestClass]
    public class TestTrivia
    {
        Game testGame = new Game();

        [TestMethod]
        public void TestAddPlayer()
        {
            testGame.AddPlayer("Player1");
            Assert.AreEqual(1, testGame.howManyPlayers());
            testGame.AddPlayer("Player2");
            Assert.AreNotEqual(1, testGame.howManyPlayers());
        }

        [TestMethod]
        public void TestIsPlayable()
        {
            testGame.AddPlayer("Player1");
            Assert.IsFalse(testGame.isPlayable());
            testGame.AddPlayer("Player2");
            Assert.IsTrue(testGame.isPlayable());
        }

        //[TestMethod]
        //public void TestCreateRockQuestion()
        //{
        //    var Question = testGame.createRockQuestion(1);
        //    StringAssert.Contains(Question, "1");
        //}

        [TestMethod]
        public void TestPlayerWin()
        {
            //Reminder: The wasCorrectlyAnswered method furthers the game
            //but also checks for the end game condition.
            //Returns the inverse of what would be expected (false for win condition).

            testGame.AddPlayer("Player1");
            testGame.EvaluateRoll(1);
            for (int i = 0; i < 5; ++i)
            {
                Assert.IsTrue(testGame.wasCorrectlyAnswered());
            }

            Assert.IsFalse(testGame.wasCorrectlyAnswered());
        }

        [TestMethod]
        public void TestPenaltyBox()
        {
            testGame.AddPlayer("Player1");

            //Pose a question
            testGame.EvaluateRoll(2);

            //Places player into penalty box
            testGame.wrongAnswer();

            //Even rolls stay in penalty box
            testGame.EvaluateRoll(2);
            for (int i = 0; i < 5; ++i)
            {
                Assert.IsTrue(testGame.wasCorrectlyAnswered());
            }
            //Player does not win game in six correct answers (since they were in penalty box)
            Assert.IsTrue(testGame.wasCorrectlyAnswered());

            //Player "isGettingOutOfPenaltyBox" but never actually gets out
            //Still ultimately the same as getting out though
            testGame.EvaluateRoll(1);
            for (int i = 0; i < 5; ++i)
            {
                Assert.IsTrue(testGame.wasCorrectlyAnswered());
            }
            //Player wins game in six correct answers
            Assert.IsFalse(testGame.wasCorrectlyAnswered());
        }

        [TestMethod]
        public void TestQuestions()
        {
            testGame.AddPlayer("Player1");
            testGame.EvaluateRoll(1);
            for (int i = 0; i < 201; ++i)
            {
                testGame.EvaluateRoll(1);
            }
        }
    }
}
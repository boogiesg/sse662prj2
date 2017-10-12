using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Trivia;

namespace UglyTrivia
{
    public class GameRunner
    {

        private static bool notAWinner;

        /*
         * I needed to split the seed generation away from the code that runs the game
         * This allows running the game multiple times with the same random seed.
         * This was preventing the program from being deterministic.
         * */
        public static void Main(String[] args)
        {
            Random rand = new Random();
            Run(rand);
        }

        public static void Run(Random rand)
        {
            Game aGame = new Game();

            aGame.add("Chet");
            aGame.add("Pat");
            aGame.add("Sue");
        
            //Random rand = new Random();

            do
            {

                aGame.roll(rand.Next(5) + 1);

                if (rand.Next(9) == 7)
                {
                    notAWinner = aGame.wrongAnswer();
                }
                else
                {
                    notAWinner = aGame.wasCorrectlyAnswered();
                }



            } while (notAWinner);

        }


    }

}


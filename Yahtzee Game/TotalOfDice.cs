using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yahtzee_Game {

    /*
    *
    * Title: TotalOfDice.cs
    *
    * Description: Contains a method (CalculateScore) to determine if dice values given are valid for a given TotalOfDice scoring combination.
    *              Method sets the number of points to the sum of dice values, if the dice values are valid, otherwise, sets to 0.
    *
    * Author: Martin-Timothy Vu, 9454870
    *
    * Date: June 2016
    *
    */
    [Serializable]
    class TotalOfDice : Combination {
        private const int SAME_NUMBERS_REQUIRED_FOR_THREE_OF_A_KIND = 3;
        private const int SAME_NUMBERS_REQUIRED_FOR_FOUR_OF_A_KIND = 4;
        private const int SAME_NUMBERS_REQUIRED_FOR_CHANCE = 0;


        private int numberOfOneKind;

        public TotalOfDice (ScoreType combination, Label combinationScore) : base(combinationScore) {

            switch((int)combination) {
                case (int)ScoreType.ThreeOfAKind:
                    numberOfOneKind = SAME_NUMBERS_REQUIRED_FOR_THREE_OF_A_KIND;
                    break;
                case (int)ScoreType.FourOfAKind:
                    numberOfOneKind = SAME_NUMBERS_REQUIRED_FOR_FOUR_OF_A_KIND;
                    break;
                case (int)ScoreType.Chance:
                    numberOfOneKind = SAME_NUMBERS_REQUIRED_FOR_CHANCE;
                    break;
                    
            }
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        public override void CalculateScore(int[] diceValues) {
            bool isOfAKind = false;
            int score = 0;
            Array.Sort(diceValues);
            done = true;
            
            switch (numberOfOneKind) {
                case 3:
                    if ((diceValues[0] == diceValues[1]) && (diceValues[1] == diceValues[2])) {
                        isOfAKind = true;
                    } else if ((diceValues[1] == diceValues[2]) && (diceValues[2] == diceValues[3])) {
                        isOfAKind = true;
                    } else if ((diceValues[2] == diceValues[3]) && (diceValues[3] == diceValues[4])) {
                        isOfAKind = true;
                    } else {
                        isOfAKind = false;
                    }
                    break;
                case 4:
                    if ((diceValues[0] == diceValues[1]) && (diceValues[1] == diceValues[2]) && (diceValues[2] == diceValues[3])) {
                        isOfAKind = true;
                    } else if ((diceValues[1] == diceValues[2]) && (diceValues[2] == diceValues[3]) && (diceValues[3] == diceValues[4])) {
                        isOfAKind = true;
                    } else {
                        isOfAKind = false;
                    }
                    break;
                case 0:
                    isOfAKind = true;
                    break;
            }

            if (isOfAKind) {
                foreach(int singleDie in diceValues) {
                    score += singleDie;
                }
            } else {

            }
            CheckForYahtzee(diceValues);
            Points = score;
        }
    }
}

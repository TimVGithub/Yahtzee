using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yahtzee_Game {

    /*
    *
    * Title: FixedScore.cs
    *
    * Description: Contains a method (CalculateScore) to determine if dice values given are valid for a given FixedScore scoring combination.
    *              Method sets the number of points given for this scoring combination to a fixed amount of points, if the dice values are valid,
    *              otherwise, sets the points to 0.
    * 
    * Author: Martin-Timothy Vu, 9454870
    *
    * Date: June 2016
    *
    */
    [Serializable]
    class FixedScore : Combination {
        //Constant for the score received for a Full House
        private const int FULL_HOUSE_SCORE = 25;
        //Constant for the score received for a Small Straight
        private const int SMALL_STRAIGHT_SCORE = 30;
        //Constant for the score received for a Large Straight
        private const int LARGE_STRAIGHT_SCORE = 40;
        //Constant for the score received for a Yahtzee
        private const int YAHTZEE_SCORE = 50;

        private ScoreType scoreType;

        public FixedScore(ScoreType combination, Label combinationScore) : base(combinationScore) {
            scoreType = combination;
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        public override void CalculateScore(int[] diceValues) {
            int scoreCase = (int)scoreType;
            done = true;
            bool isFullHouseCombo = false;
            bool isSmallStraightCombo = false;
            bool isLargeStraightCombo = false;
            bool isYahtzeeCombo = false;
            bool isPlayingYahtzeeJokers = false;
            Array.Sort(diceValues);

            switch (scoreCase) {

                case (int)ScoreType.FullHouse:

                    //If dice values are FullHouse then set isFullHouseCombo to true. Or if dice values are Yahtzee PlayYahtzeeJokers()
                    //and set isPlayingYahtzeeJokers to true. Otherwise, is set isFullHouseCombo to false.
                    if (((diceValues[0] == diceValues[1]) && (diceValues[1] == diceValues[2]))
                        && (diceValues[3] == diceValues[4]) && (diceValues[0] != diceValues[4])) {
                        isFullHouseCombo = true;
                    } else if (((diceValues[2] == diceValues[3]) && (diceValues[3] == diceValues[4]))
                        && (diceValues[0] == diceValues[1]) && (diceValues[0] != diceValues[4])) {
                        isFullHouseCombo = true;
                    } else if ((diceValues[0] == diceValues[1]) && (diceValues[0] == diceValues[2])
                        && (diceValues[0] == diceValues[3]) && (diceValues[0] == diceValues[4])) {
                        YahtzeeNumber = diceValues[0];
                        isPlayingYahtzeeJokers = true;
                        PlayYahtzeeJoker();
                    } else {
                        isFullHouseCombo = false;
                    }
                    break;

                case (int)ScoreType.SmallStraight:

                    //If dice values are SmallStraight then set isSmallStraight to true. Or if dice values are Yahtzee PlayYahtzeeJokers()
                    //and set isPlayingYahtzeeJokers to true. Otherwise, is set isSmallStraight to false.
                    if (((diceValues[0] + 1) == diceValues[1]) && ((diceValues[0] + 2) == diceValues[2])
                        && ((diceValues[0] + 3) == diceValues[3])) {
                        isSmallStraightCombo = true;
                    } else if (((diceValues[1] + 1) == diceValues[2]) && ((diceValues[1] + 2) == diceValues[3])
                        && ((diceValues[1] + 3) == diceValues[4])) {
                        isSmallStraightCombo = true;
                    } else if (((diceValues[0] + 1) == diceValues[1]) && ((diceValues[0] + 2) == diceValues[3])
                        && ((diceValues[0] + 3) == diceValues[4])) {
                        isSmallStraightCombo = true;
                    } else if (((diceValues[0] + 1) == diceValues[1]) && ((diceValues[0] + 2) == diceValues[2])
                        && ((diceValues[0]) + 3 == diceValues[4])) {
                        isSmallStraightCombo = true;
                    } else if ((diceValues[0] == diceValues[1]) && (diceValues[0] == diceValues[2])
                        && (diceValues[0] == diceValues[3]) && (diceValues[0] == diceValues[4])) {
                        YahtzeeNumber = diceValues[0];
                        isPlayingYahtzeeJokers = true;
                        PlayYahtzeeJoker();
                    } else {
                        isSmallStraightCombo = false;
                    }
                    break;

                case (int)ScoreType.LargeStraight:

                    //If dice values are LargeStraight then set isLargeStraight to true. Or if dice values are Yahtzee PlayYahtzeeJokers()
                    //and set isPlayingYahtzeeJokers to true. Otherwise, is set isLargeStraight to false.
                    if (((diceValues[0] + 1) == diceValues[1]) && ((diceValues[0] + 2) == diceValues[2])
                        && ((diceValues[0] + 3) == diceValues[3]) && ((diceValues[0] + 4) == diceValues[4])) {
                        isLargeStraightCombo = true;
                    } else if ((diceValues[0] == diceValues[1]) && (diceValues[0] == diceValues[2])
                        && (diceValues[0] == diceValues[3]) && (diceValues[0] == diceValues[4])) {
                        YahtzeeNumber = diceValues[0];
                        isPlayingYahtzeeJokers = true;
                        PlayYahtzeeJoker();
                    } else {
                        isLargeStraightCombo = false;
                    }
                    break;

                case (int)ScoreType.Yahtzee:
                    if ((diceValues[0] == diceValues[1]) && (diceValues[0] == diceValues[2])
                        && (diceValues[0] == diceValues[3]) && (diceValues[0] == diceValues[4])) {
                        isYahtzeeCombo = true;
                    } else {
                        isYahtzeeCombo = false;
                    }
                    break;

            }

            //If YahtzeeJokers is not being played assign points as usual
            if (!isPlayingYahtzeeJokers) {
                CheckForYahtzee(diceValues);
                if (isFullHouseCombo) {
                    Points = FULL_HOUSE_SCORE;
                } else if (isSmallStraightCombo) {
                    Points = SMALL_STRAIGHT_SCORE;
                } else if (isLargeStraightCombo) {
                    Points = LARGE_STRAIGHT_SCORE;
                } else if (isYahtzeeCombo) {
                    Points = YAHTZEE_SCORE;
                } else {
                    Points = 0;
                }
            } else {

            }
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        public void PlayYahtzeeJoker() {
            int scoreCase = (int)scoreType;
            switch (scoreCase) {
                case (int)ScoreType.FullHouse:
                    Points = FULL_HOUSE_SCORE;
                    break; 
                case (int)ScoreType.SmallStraight:
                    Points = SMALL_STRAIGHT_SCORE;
                    break;
                case (int)ScoreType.LargeStraight:
                    Points = LARGE_STRAIGHT_SCORE;
                    break;
            }
                
            } 
        }
    }


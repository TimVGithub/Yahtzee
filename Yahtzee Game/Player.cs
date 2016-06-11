using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yahtzee_Game {

    /*
    *
    * Title: Player.cs
    *
    * Description: Creates score objects using a for loop in conjunction with a switch statement. Also, calls the correct class method
    *              for a certain scoring type and adds the calculated score to the appropriate subtotal (also handles addition of bonuses).
    *              Contains C# properties for name and grandTotal which allow for getting/setting outside of the player class. Has methods
    *              that: determine the availability of a scoring combination, display the players scores on associated score labels,
    *              and determine whether a players has completed all available scoring combinations
    *
    * Author: Martin-Timothy Vu, 9454870
    *
    * Date: June 2016
    *
    */
    [Serializable]
    class Player {

        //Constant for total number of scoring combinations in a game of Yahtzee
        private const int TOTAL_COMBINATIONS = 13;

        //The amount of bonus points given for scoring 63 or above in the Upper Section
        private const int BONUS_FOR_63 = 35;

        //The minimum, inclusive, amount of points required to score 35 bonus points
        private const int MIN_POINTS_FOR_BONUS = 63;

        //Number of bonus points for subsequent Yahtzees after the first
        private const int BONUS_FOR_SUBSEQUENT_YAHTZEES = 100;

        //Number of points received for a Yahtzee
        private const int POINTS_FOR_YAHTZEE = 50;

        private string name;
        private int combinationsToDo;
        private Score[] scores = new Score[(int)ScoreType.GrandTotal + 1];
        private int grandTotal;

        public Player(string name, Label[] scoreLabels) {
            this.name = name;
            combinationsToDo = TOTAL_COMBINATIONS;

            for (ScoreType combination = ScoreType.Ones; combination <= ScoreType.GrandTotal; combination++ ) {
                switch (combination) {
                    case ScoreType.Ones:
                    case ScoreType.Twos:
                    case ScoreType.Threes:
                    case ScoreType.Fours:
                    case ScoreType.Fives:
                    case ScoreType.Sixes:
                        this.scores[(int)combination] = new CountingCombination(combination, scoreLabels[(int)combination]);
                        break;                    
                    case ScoreType.SmallStraight:
                    case ScoreType.LargeStraight:
                    case ScoreType.FullHouse:
                    case ScoreType.Yahtzee:
                        this.scores[(int)combination] = new FixedScore(combination, scoreLabels[(int)combination]);
                        break;
                    case ScoreType.ThreeOfAKind:
                    case ScoreType.FourOfAKind:
                    case ScoreType.Chance:
                        this.scores[(int)combination] = new TotalOfDice(combination, scoreLabels[(int)combination]);
                        break;
                    case ScoreType.SubTotal:
                    case ScoreType.BonusFor63Plus:
                    case ScoreType.SectionATotal:
                    case ScoreType.YahtzeeBonus:
                    case ScoreType.SectionBTotal:
                    case ScoreType.GrandTotal:
                        this.scores[(int)combination] = new BonusOrTotal(scoreLabels[(int)combination]);
                        break;
                }
            }
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------     

        public string Name {
            get {
                return name;
            }

            set {
                name = value;
            }
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------     

        public void ScoreCombination(ScoreType scoreType, int[] dieFaceValues) {            
            int calculatedScore;
            bool yahtzeeOccurred;


            if ((scores[(int)ScoreType.Yahtzee].Points > 0)){
                yahtzeeOccurred = true;
            } else {
                yahtzeeOccurred = false;
            }

            ((Combination)scores[(int)scoreType]).CalculateScore(dieFaceValues);

            switch (scoreType) {
                case ScoreType.Ones:
                case ScoreType.Twos:
                case ScoreType.Threes:
                case ScoreType.Fours:
                case ScoreType.Fives:
                case ScoreType.Sixes:

                    //If  Yahtzee has been previously attempted and player rolls another Yahtzee, add Yahtzee Bonus Points
                    if ((((Combination)scores[(int)scoreType]).IsYahtzee)
                        && (yahtzeeOccurred)) {
                        scores[(int)ScoreType.YahtzeeBonus].Points += BONUS_FOR_SUBSEQUENT_YAHTZEES;
                        scores[(int)ScoreType.SectionBTotal].Points += BONUS_FOR_SUBSEQUENT_YAHTZEES;
                    }

                    //Adds points from a scoring combination to the sub-total, and, if the subtotal is 63+, apply Bonus Points for 63+
                    calculatedScore = scores[(int)scoreType].Points;
                    scores[(int)ScoreType.SubTotal].Points += calculatedScore;                    
                    if (scores[(int)ScoreType.SubTotal].Points >= MIN_POINTS_FOR_BONUS) {
                        scores[(int)ScoreType.BonusFor63Plus].Points = BONUS_FOR_63;
                    }   
                              
                    scores[(int)ScoreType.SectionATotal].Points = scores[(int)ScoreType.SubTotal].Points 
                                                                + scores[(int)ScoreType.BonusFor63Plus].Points;
                    break;

                case ScoreType.SmallStraight:
                case ScoreType.LargeStraight:
                case ScoreType.FullHouse:

                    int meme = scores[(int)scoreType].Points;

                    //To handle when YahtzeeNumber is equal to zero, which would result in an index out of range in the if loop nested within this if loop
                    if ((((Combination)scores[(int)scoreType]).YahtzeeNumber) == 0) {
                    } else {

                        //If Yahtzee Jokers is valid then do not change points, else if, Yahtzee Jokers is invalid, change points scored to 0
                        if ((scores[(((Combination)scores[(int)scoreType]).YahtzeeNumber) - 1]).Done) {

                        } else {
                            scores[(int)scoreType].Points = 0;
                        }
                    }

                    //Adds points from a scoring combination to the section total
                    calculatedScore = scores[(int)scoreType].Points;
                    scores[(int)ScoreType.SectionBTotal].Points += calculatedScore;
                    break;

                case ScoreType.Yahtzee:
                case ScoreType.ThreeOfAKind:
                case ScoreType.FourOfAKind:
                case ScoreType.Chance:

                    //If  Yahtzee has been previously attempted and player rolls another Yahtzee, add Yahtzee Bonus Points
                    if ((((Combination)scores[(int)scoreType]).IsYahtzee)
                        && (yahtzeeOccurred)) {
                        scores[(int)ScoreType.YahtzeeBonus].Points += BONUS_FOR_SUBSEQUENT_YAHTZEES;
                        scores[(int)ScoreType.SectionBTotal].Points += BONUS_FOR_SUBSEQUENT_YAHTZEES;
                    }

                    //Adds points from a scoring combination to the section total
                    calculatedScore = scores[(int)scoreType].Points;
                    scores[(int)ScoreType.SectionBTotal].Points += calculatedScore;
                    break;                                    
            }

            scores[(int)ScoreType.GrandTotal].Points = scores[(int)ScoreType.SectionBTotal].Points 
                                                     + scores[(int)ScoreType.SectionATotal].Points;
            grandTotal = scores[(int)ScoreType.GrandTotal].Points;

            combinationsToDo -= 1;
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        public int GrandTotal {
            get {
                return grandTotal;
            }

            set {
                grandTotal = value;
            }
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        public bool IsAvailable(ScoreType which) {
            if (scores[(int)which].Done) {
                return false;
            } else {
                return true;
            }
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        public void ShowScores() {
            for (ScoreType combination = ScoreType.Ones; combination <= ScoreType.GrandTotal; combination++) {           
                scores[(int)combination].ShowScore();
            }                
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        public bool IsFinished() {
            return combinationsToDo == 0;
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        public void Load(Label[] scoreTotals) {
            for (int i = 0; i < scores.Length; i++) {
                scores[i].Load(scoreTotals[i]);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yahtzee_Game {

    /*
    *
    * Title: Combination.cs
    *
    * Description: Base class for the other scoring combination classes (TotalOfDice, FixedScore, CountingCombination) Has methods that are usable
    *              by child classes that: sort dice values in an array into ascending order or check if the given dice values are a Yahtzee.
    *
    * Author: Martin-Timothy Vu, 9454870
    *
    * Date: June 2016
    *
    */
    [Serializable]
    abstract class Combination : Score {
        private bool isYahtzee;
        private int yahtzeeNumber;

        public Combination(Label combinationScore) : base(combinationScore) {

        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        public abstract void CalculateScore(int[] diceValues);

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        public void Sort(int[] diceValues) {
            Array.Sort(diceValues);
            done = true;
            }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        public bool IsYahtzee {
            get {
                return isYahtzee;
            }

            set {
                isYahtzee = value;
            }
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        public int YahtzeeNumber {
            get {
                return yahtzeeNumber;
            }

            set {
                yahtzeeNumber = value;
            }
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        public void CheckForYahtzee(int[] diceValues) {
            if ((diceValues[0] == diceValues[1]) && (diceValues[0] == diceValues[2])
                && (diceValues[0] == diceValues[3]) && (diceValues[0] == diceValues[4])) {
                IsYahtzee = true;
            }
        }
    }
}

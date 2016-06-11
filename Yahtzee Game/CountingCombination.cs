using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yahtzee_Game {

    /*
    *
    * Title: CountingCombination.cs
    *
    * Description: Contains a method (CalculateScore) to determine if dice values given are valid for a given CountingCombination scoring combination.
    *              Method sets points to the sum of dice values that match the scoring combination specified die value.
    *
    * Author: Martin-Timothy Vu, 9454870
    *
    * Date: June 2016
    *
    */
    [Serializable]
    class CountingCombination : Combination {
        private int dieValue;

        public CountingCombination (ScoreType combination, Label combinationScore) : base(combinationScore) {
            dieValue = (int)combination + 1;                       
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        public override void CalculateScore(int[] diceValues) {
            int score;
            int matchCount = 0;
            done = true;
            Array.Sort(diceValues);
            
            foreach (int singleDie in diceValues) {
                if (singleDie == dieValue) {
                    matchCount += 1;
                } else {

                }
            }
            CheckForYahtzee(diceValues);
            score = matchCount * dieValue;
            Points = score; 
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yahtzee_Game {

    /*
    *
    * Title: BonusOrTotal.cs
    *
    * Description: A subclass of Score.cs. It is used in Player.cs, to display scores on the Bonus/Total labels and update those scores for the
    *              labels, using the Points property and the ShowScore method in Score.cs.
    *
    * Author: Martin-Timothy Vu, 9454870
    *
    * Date: June 2016
    *
    */
    [Serializable]
    class BonusOrTotal : Score {

        public BonusOrTotal (Label combinationScore) : base(combinationScore) {
            done = true;
        }
    }
}

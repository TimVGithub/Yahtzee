using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yahtzee_Game {

    /*
    *
    * Title: Score.cs
    *
    * Description: Contains properties for whether or not a scoring combination has been done or not (done) and the number of points a scoring
    *              combination has accrued. Also has a method that is accessible by objects of it's child class' classes (ShowScores) that
    (              shows the points accured for that object (a scoring combination) on it's associated label. 
    *
    * Author: Martin-Timothy Vu, 9454870
    *
    * Date: June 2016
    *
    */
    [Serializable]
    abstract class Score {
        private int points;
        [NonSerialized]
        private Label label;
        protected bool done;

        public Score(Label label) {
            Points = 0;
            this.label = label;
            Done = false;
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        public int Points {
            get {
                return points;
            }

            set {
                points = value;
            }
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        public bool Done {
            get {
                return done;
            }

            set {
                done = value;
            }
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        public void ShowScore() {
            if (Done) {
                label.Text = Points.ToString();
            } else {
                label.Text = " ";
            }
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        public void Load(Label label) {
            this.label = label;
        }
    }
}


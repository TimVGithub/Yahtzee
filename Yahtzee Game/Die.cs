using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Yahtzee_Game {

    /*
    *
    * Title: Die.cs
    *
    * Description: Has properties faceValue and active which get/set the value shown on a die face and whether or not a die is active.
    *              Also contains a method which is called by Game.cs when dice are to be rolled. This method chooses a random value between
    *              1 and 6 (inclusive) and outputs this value to the faceValue variable. The method also sets the text of it's associated
    *              label to the face value of the die.
    *
    * Author: Martin-Timothy Vu, 9454870
    *
    * Date: June 2016
    *
    */
    [Serializable]
    class Die {
        //Constant for minimum face value of a die
        private const int MINIMUM_DIE_VALUE = 1;
        //Constant for maximum face value of a die
        private const int MAXIMUM_DIE_VALUE = 6;

        private int faceValue;
        private bool active;
        [NonSerialized]
        private Label label;
        private static Random random = new Random();
        private static string rollFileName = Game.defaultPath + "\\basictestrolls.txt";
        [NonSerialized]
        private static StreamReader rollFile = new StreamReader(rollFileName);
        private static bool DEBUG = true;


        public Die(Label label) {
            this.label = label;
            faceValue = MINIMUM_DIE_VALUE;
            active = true;
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------     

        public int FaceValue {
            get {
                return faceValue;
            }

            set {
                faceValue = value;
            }
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------     

        public bool Active {
            get {
                return active;
            }

            set {
                active = value;
            }
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------     

        public void Roll() {
            if (!DEBUG) {
                faceValue = random.Next(MINIMUM_DIE_VALUE, MAXIMUM_DIE_VALUE + 1);
                label.Text = faceValue.ToString();
                label.Refresh();
            } else {
                faceValue = int.Parse(rollFile.ReadLine());
                label.Text = faceValue.ToString();
                label.Refresh();
            }
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------     

        public void Load(Label label) {
            this.label = label;
            if (faceValue == 0) {
                label.Text = string.Empty;
            } else {
                label.Text = faceValue.ToString();
            }
        }
    
    }
}
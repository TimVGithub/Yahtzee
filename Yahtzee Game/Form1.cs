using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yahtzee_Game {

    /*
    *
    * Title: Form1.cs
    *
    * Description: Contains numerous methods that can adjust the state of controls on the GUI (enabling/disabling scorebuttons/checkboxes etc.)
    *              Additionally, also has methods that return objects on the gui for use in other methods. Necessary event handlers for controls
    *              have also been implemented in Form1.cs (including but not limited to, checkboxes, roll button, numericUpDown).
    *
    * Author: Martin-Timothy Vu, 9454870
    *
    * Date: June 2016
    *
    */
    public partial class Form1 : Form {
        
        //Constant for number of dice
        private const int NUM_OF_DICE = 5;

        private Label[] dice = new Label[NUM_OF_DICE];
        private Button[] scoreButtons = new Button[(int)ScoreType.Yahtzee + 1];
        private Label[] scoreTotals = new Label[(int)ScoreType.GrandTotal + 1];
        private CheckBox[] checkBoxes = new CheckBox[NUM_OF_DICE];
        private Game game;

        public Form1() {
            InitializeComponent();
            InitializeLabelsAndButtons();
            DisableAndClearCheckBoxes();
            
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------     
   
        private void InitializeLabelsAndButtons() {
            dice[0] = die1;
            dice[1] = die2;
            dice[2] = die3;
            dice[3] = die4;
            dice[4] = die5;

            scoreButtons[(int)ScoreType.Ones] = button1;
            scoreButtons[(int)ScoreType.Twos] = button2;
            scoreButtons[(int)ScoreType.Threes] = button3;
            scoreButtons[(int)ScoreType.Fours] = button4;
            scoreButtons[(int)ScoreType.Fives] = button5;
            scoreButtons[(int)ScoreType.Sixes] = button6;
            scoreButtons[(int)ScoreType.ThreeOfAKind] = button7;
            scoreButtons[(int)ScoreType.FourOfAKind] = button8;
            scoreButtons[(int)ScoreType.FullHouse] = button9;
            scoreButtons[(int)ScoreType.SmallStraight] = button10;
            scoreButtons[(int)ScoreType.LargeStraight] = button11;
            scoreButtons[(int)ScoreType.Chance] = button12;
            scoreButtons[(int)ScoreType.Yahtzee] = button13;

            scoreTotals[(int)ScoreType.Ones] = scoreLabel1;
            scoreTotals[(int)ScoreType.Twos] = scoreLabel2;
            scoreTotals[(int)ScoreType.Threes] = scoreLabel3;
            scoreTotals[(int)ScoreType.Fours] = scoreLabel4;
            scoreTotals[(int)ScoreType.Fives] = scoreLabel5;
            scoreTotals[(int)ScoreType.Sixes] = scoreLabel6;
            scoreTotals[(int)ScoreType.ThreeOfAKind] = scoreLabel7;
            scoreTotals[(int)ScoreType.FourOfAKind] = scoreLabel8;
            scoreTotals[(int)ScoreType.FullHouse] = scoreLabel9;
            scoreTotals[(int)ScoreType.SmallStraight] = scoreLabel10;
            scoreTotals[(int)ScoreType.LargeStraight] = scoreLabel11;
            scoreTotals[(int)ScoreType.Chance] = scoreLabel12;
            scoreTotals[(int)ScoreType.Yahtzee] = scoreLabel13;
            scoreTotals[(int)ScoreType.SubTotal] = subTotalScoreLabel;
            scoreTotals[(int)ScoreType.BonusFor63Plus] = bonusFor63ScoreLabel;
            scoreTotals[(int)ScoreType.SectionATotal] = upperTotalScoreLabel;
            scoreTotals[(int)ScoreType.YahtzeeBonus] = yahtzeeBonusScoreLabel;
            scoreTotals[(int)ScoreType.SectionBTotal] = lowerTotalScoreLabel;
            scoreTotals[(int)ScoreType.GrandTotal] = grandTotalScoreLabel;

            checkBoxes[0] = checkBox1;
            checkBoxes[1] = checkBox2;
            checkBoxes[2] = checkBox3;
            checkBoxes[3] = checkBox4;
            checkBoxes[4] = checkBox5;

        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------     

        public Label[] GetDice() {
            return dice;
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------     

        public Label[] GetScoreTotals() {
            return scoreTotals;
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------     

        public void ShowPlayerName(string name) {
            playerLabel.Text = name;
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------     

        public void EnableRollButton() {
            rollButton.Enabled = true;
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------     

        public void DisableRollButton() {
            rollButton.Enabled = false;
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------     

        public void EnableCheckBoxes() {
            for (int i = 0; i < NUM_OF_DICE; i++) {
                checkBoxes[i].Enabled = true;
            }
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------     

        public void DisableAndClearCheckBoxes() {
            for (int i = 0; i < NUM_OF_DICE; i++) {
                checkBoxes[i].Enabled = false;
                checkBoxes[i].Checked = false;
            }
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------     

        public void EnableScoreButton(ScoreType combo) {
            scoreButtons[(int)combo].Enabled = true;
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------     

        public void DisableScoreButton(ScoreType combo) {
            scoreButtons[(int)combo].Enabled = false;
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------     

        /// <summary>
        /// Disables all score buttons, rather than one at a time
        /// </summary>
        private void DisableAllScoreButtons() {
            DisableScoreButton(ScoreType.Ones);
            DisableScoreButton(ScoreType.Twos);
            DisableScoreButton(ScoreType.Threes);
            DisableScoreButton(ScoreType.Fours);
            DisableScoreButton(ScoreType.Fives);
            DisableScoreButton(ScoreType.Sixes);
            DisableScoreButton(ScoreType.ThreeOfAKind);
            DisableScoreButton(ScoreType.FourOfAKind);
            DisableScoreButton(ScoreType.FullHouse);
            DisableScoreButton(ScoreType.SmallStraight);
            DisableScoreButton(ScoreType.LargeStraight);
            DisableScoreButton(ScoreType.Chance);
            DisableScoreButton(ScoreType.Yahtzee);
        }
        //end DisableAllScoreButtons

        //---------------------------------------------------------------------------------------------------------------------------------------------------     

        /// <summary>
        /// Clears all score labels, rather than one at a time
        /// </summary>
        private void ClearAllScoreLabels() {
            scoreTotals[(int)ScoreType.Ones].Text = " ";
            scoreTotals[(int)ScoreType.Twos].Text = " ";
            scoreTotals[(int)ScoreType.Threes].Text = " ";
            scoreTotals[(int)ScoreType.Fours].Text = " ";
            scoreTotals[(int)ScoreType.Fives].Text = " ";
            scoreTotals[(int)ScoreType.Sixes].Text = " ";
            scoreTotals[(int)ScoreType.ThreeOfAKind].Text = " ";
            scoreTotals[(int)ScoreType.FourOfAKind].Text = " ";
            scoreTotals[(int)ScoreType.FullHouse].Text = " ";
            scoreTotals[(int)ScoreType.SmallStraight].Text = " ";
            scoreTotals[(int)ScoreType.LargeStraight].Text = " ";
            scoreTotals[(int)ScoreType.Chance].Text = " ";
            scoreTotals[(int)ScoreType.Yahtzee].Text = " ";
            scoreTotals[(int)ScoreType.SubTotal].Text = " ";
            scoreTotals[(int)ScoreType.BonusFor63Plus].Text = " ";
            scoreTotals[(int)ScoreType.SectionATotal].Text = " ";
            scoreTotals[(int)ScoreType.YahtzeeBonus].Text = " ";
            scoreTotals[(int)ScoreType.SectionBTotal].Text = " ";
            scoreTotals[(int)ScoreType.GrandTotal].Text = " ";
        }
        //end ClearAllScoreLabels

        //---------------------------------------------------------------------------------------------------------------------------------------------------     

        public void CheckCheckBox(int index) {
            checkBoxes[index].Checked = true;
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------     

        public void ShowMessage(string message) {
            messageLabel.Text = message;
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------     

        public void ShowOKButton() {
            okButton.Visible = true;
            okButton.Enabled = true;
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------     

        public void StartNewGame() {
            game = new Game(this);
            okButton.Enabled = false;
            okButton.Visible = false;
            numericUpDown1.Enabled = true;
            label4.Visible = true;
            saveToolStripMenuItem.Enabled = true;
            loadToolStripMenuItem.Enabled = false;
            messageLabel.Text = "Roll 1";
            playerLabel.Text = "Player 1";
            playerBindingSource.DataSource = game.Players;
            ClearAllScoreLabels();
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------     

        private void UpdatePlayersDataGridView() {
            game.Players.ResetBindings();
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------     

        private void newToolStripMenuItem_Click(object sender, EventArgs e) {
            StartNewGame();                      
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------     

        private void rollButton_Click(object sender, EventArgs e) {
            game.RollDice();
            numericUpDown1.Enabled = false;
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------     

        private void checkBox_CheckedChanged(object sender, EventArgs e) {
            if (sender == checkBox1) {
                if (checkBox1.Checked == false) {
                    game.ReleaseDie(0);
                } else {
                    game.HoldDie(0);
                }
            } else if (sender == checkBox2) {
                if (checkBox2.Checked == false) {
                    game.ReleaseDie(1);
                } else {
                    game.HoldDie(1);
                }
            } else if (sender == checkBox3) {
                if (checkBox3.Checked == false) {
                    game.ReleaseDie(2);
                } else {
                    game.HoldDie(2);
                }
            } else if (sender == checkBox4) {
                if (checkBox4.Checked == false) {
                    game.ReleaseDie(3);
                } else {
                    game.HoldDie(3);
                }
            } else if (sender == checkBox5) {
                if (checkBox5.Checked == false) {
                    game.ReleaseDie(4);
                } else {
                    game.HoldDie(4);
                }
            }

        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------     

        private void button_Click(object sender, EventArgs e) {
            if (sender == button1) {
                game.ScoreCombination(ScoreType.Ones);                
            } else if (sender == button2) {
                game.ScoreCombination(ScoreType.Twos);
            } else if (sender == button3) {
                game.ScoreCombination(ScoreType.Threes);
            } else if (sender == button4) {
                game.ScoreCombination(ScoreType.Fours);
            } else if (sender == button5) {
                game.ScoreCombination(ScoreType.Fives);
            } else if (sender == button6) {
                game.ScoreCombination(ScoreType.Sixes);
            } else if (sender == button7) {
                game.ScoreCombination(ScoreType.ThreeOfAKind);
            } else if (sender == button8) {
                game.ScoreCombination(ScoreType.FourOfAKind);
            } else if (sender == button9) {
                game.ScoreCombination(ScoreType.FullHouse);
            } else if (sender == button10) {
                game.ScoreCombination(ScoreType.SmallStraight);
            } else if (sender == button11) {
                game.ScoreCombination(ScoreType.LargeStraight);
            } else if (sender == button12) {
                game.ScoreCombination(ScoreType.Chance);
            } else if (sender == button13) {
                game.ScoreCombination(ScoreType.Yahtzee);
            }

            ShowMessage("Your turn has ended - click OK");
            DisableAndClearCheckBoxes();
            DisableAllScoreButtons();
            DisableRollButton();
            UpdatePlayersDataGridView();
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------     

        private void okButton_Click(object sender, EventArgs e) {
            okButton.Visible = false;
            okButton.Enabled = false;
            game.NextTurn();                       
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------     

        private void numericUpDown1_ValueChanged(object sender, EventArgs e) {
            game.Players.Clear();
            EnableRollButton();
            playerLabel.Visible = true;
            messageLabel.Visible = true;
            label4.Visible = false;
            for (int i = 0; i < (numericUpDown1.Value); i++) {
                game.Players.Add(new Player("Player " + (i + 1), GetScoreTotals()));                
            }           
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------     

        private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
            game.Save();
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------     

        private void loadToolStripMenuItem_Click(object sender, EventArgs e) {
            game = Game.Load(this);
            playerBindingSource.DataSource = game.Players;
            UpdatePlayersDataGridView();
        }
    }

}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Yahtzee_Game {

    public enum ScoreType {
        Ones, Twos, Threes, Fours, Fives, Sixes,
        SubTotal, BonusFor63Plus, SectionATotal,
        ThreeOfAKind, FourOfAKind, FullHouse,
        SmallStraight, LargeStraight, Chance, Yahtzee,
        YahtzeeBonus, SectionBTotal, GrandTotal
    }
    /*
    *
    * Title: Game.cs
    *
    * Description: Contains the logic for a game of Yahtzee. Namely: handling the changing of players, calling methods to roll five dice,
    *              calling methods to enable scoring buttons, holding/releasing dice, calling methods to scoring a selected combination,
    *              display/determing the winner and presenting controls to start a new game once the current one is finished
    *
    * Author: Martin-Timothy Vu, 9454870
    *
    * Date: June 2016
    *
    */
    [Serializable]
    class Game {

        //Constant for number of dice
        private const int NUMBER_OF_DICE = 5;

        //An array of integers that holds the face value of die rolls
        private int[] faceValues;

        //An array of integers that is used to hold each players grand total score
        private int[] playersGrandTotals;

        //An array of strings that holds a number of messages that need to be displayed
        private string[] messages = new string[] { "Roll 2 or choose a combination to score", "Roll 3 or choose a combination to score"
                                                    , "Choose a combination to score", "Roll 1" };

        //An int that holds the number of times the OK button has been clicked after the last player's last turn is over
        private int timesOKButtonClicked;

        public static string defaultPath = Environment.CurrentDirectory;
        private static string savedGameFile = defaultPath + "\\YahtzeeGame.dat";

        private BindingList<Player> players;        
        private int currentPlayerIndex;
        private Player currentPlayer;
        private Die[] dice;
        private int playersFinished;
        private int numRolls;
        [NonSerialized]
        private Form1 form;
        [NonSerialized]
        private Label[] dieLabels;
        

        public Game(Form1 form) {
            players = new BindingList<Player>();
            currentPlayerIndex = 0;
            playersFinished = 0;
            numRolls = 0;
            playersFinished = 0;
            this.form = form;

            dice = new Die[NUMBER_OF_DICE];
            dieLabels = form.GetDice();
            for (int i = 0; i < NUMBER_OF_DICE; i++) {
                dice[i] = new Die(dieLabels[i]);
            }

            faceValues = new int[] { 0, 0, 0, 0, 0 };
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        public void NextTurn() {

            //If game is finished display the winner and handle the start of a new game
            if (playersFinished == players.Count) {
                if (timesOKButtonClicked == 0) {
                    DisplayWinner(DetermineWinner());
                } else {
                    form.StartNewGame();
                }

                timesOKButtonClicked += 1;
            
            //Else, iterate through players as appropriate
            } else {
                
                switch (currentPlayerIndex) {
                    case 0:
                        if (players.Count == 1) {
                            currentPlayerIndex = 0;
                        } else {
                            currentPlayerIndex = 1;
                        }
                        break;
                    case 1:
                        if (players.Count == 2) {
                            currentPlayerIndex = 0;
                        } else {
                            currentPlayerIndex = 2;
                        }
                        break;
                    case 2:
                        if (players.Count == 3) {
                            currentPlayerIndex = 0;
                        } else {
                            currentPlayerIndex = 3;
                        }
                        break;
                    case 3:
                        if (players.Count == 4) {
                            currentPlayerIndex = 0;
                        } else {
                            currentPlayerIndex = 4;
                        }
                        break;
                    case 4:
                        if (players.Count == 5) {
                            currentPlayerIndex = 0;
                        } else {
                            currentPlayerIndex = 5;
                        }
                        break;
                    default:
                        currentPlayerIndex = 0;
                        break;
                }

                currentPlayer = players[currentPlayerIndex];
                numRolls = 0;
                currentPlayer.ShowScores();
                form.ShowPlayerName(players[currentPlayerIndex].Name);
                form.ShowMessage(messages[3]);
                form.EnableRollButton();
            }
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        public void RollDice() {
            currentPlayer = players[currentPlayerIndex];
            for (int i = 0; i < NUMBER_OF_DICE; i++) {
                if (dice[i].Active) {
                    dice[i].Roll();
                    faceValues[i] = dice[i].FaceValue;
                } else {

                }  
            }
            numRolls += 1;
            
            if (numRolls == 1) {
                form.ShowMessage(messages[numRolls-1]);
                form.EnableCheckBoxes();

                //If a scoring combination is available for currentPlayer, enable it.
                if (currentPlayer.IsAvailable(ScoreType.Ones)) {
                    form.EnableScoreButton(ScoreType.Ones);
                }
                if (currentPlayer.IsAvailable(ScoreType.Twos)) {
                    form.EnableScoreButton(ScoreType.Twos);
                }
                if (currentPlayer.IsAvailable(ScoreType.Threes)) {
                    form.EnableScoreButton(ScoreType.Threes); 
                }
                if (currentPlayer.IsAvailable(ScoreType.Fours)) {
                    form.EnableScoreButton(ScoreType.Fours); 
                }
                if (currentPlayer.IsAvailable(ScoreType.Fives)) {
                    form.EnableScoreButton(ScoreType.Fives); 
                }
                if (currentPlayer.IsAvailable(ScoreType.Sixes)) {
                    form.EnableScoreButton(ScoreType.Sixes); 
                }
                if (currentPlayer.IsAvailable(ScoreType.ThreeOfAKind)) {
                    form.EnableScoreButton(ScoreType.ThreeOfAKind);
                }
                if (currentPlayer.IsAvailable(ScoreType.FourOfAKind)) {
                    form.EnableScoreButton(ScoreType.FourOfAKind);
                }
                if (currentPlayer.IsAvailable(ScoreType.FullHouse)) {
                    form.EnableScoreButton(ScoreType.FullHouse);
                }
                if (currentPlayer.IsAvailable(ScoreType.SmallStraight)) {
                    form.EnableScoreButton(ScoreType.SmallStraight);
                }
                if (currentPlayer.IsAvailable(ScoreType.LargeStraight)) {
                    form.EnableScoreButton(ScoreType.LargeStraight);
                }
                if (currentPlayer.IsAvailable(ScoreType.Chance)) {
                    form.EnableScoreButton(ScoreType.Chance);
                }
                if (currentPlayer.IsAvailable(ScoreType.Yahtzee)) {
                    form.EnableScoreButton(ScoreType.Yahtzee);
                }

            } else if (numRolls == 2) {
                form.ShowMessage(messages[numRolls-1]);
            } else if (numRolls == 3) {
                form.ShowMessage(messages[numRolls-1]);
                form.DisableRollButton();
                form.DisableAndClearCheckBoxes();
            }
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        public void HoldDie(int index) {
            dice[index].Active = false;
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        public void ReleaseDie(int index) {
            dice[index].Active = true;
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        public void ScoreCombination(ScoreType combination) {
            currentPlayer.ScoreCombination(combination, faceValues);
            form.ShowOKButton();
            currentPlayer.ShowScores();

            if (currentPlayer.IsFinished()) {
                playersFinished += 1;
            }
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Assigns each players grand total to a position in playerGrandTotals and loops through playersGrandTotals to find the index of the
        /// highest score.
        /// </summary>
        /// <returns>Returns the index of the highest scoring player</returns>
        private int DetermineWinner() {
            int highestScoreIndex = 0;

            playersGrandTotals = new int[players.Count];

            for (int i = 0; i < players.Count; i++) {
                playersGrandTotals[i] = 0;
            }

            //For loop assigns each players grand total to an element within the array, playersGrandTotals
            for (int i = 0; i < players.Count; i++) {
                playersGrandTotals[i] = players[i].GrandTotal;

            }

            //For loop iterates through playersGrandTotals and finds the index of the highest score
            for (int i = 0; i < players.Count; i++) {
                if (playersGrandTotals[i] > playersGrandTotals[highestScoreIndex]) {
                    highestScoreIndex = i;
                } else {

                }
            }
            return highestScoreIndex;           
        }
        //end DetermineWinner

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Displays the winner and the winner's score using the method ShowMessage, belonging to Form1.cs.
        /// </summary>
        /// <param name="winnersPosition">The position of the winner which is also the position of the highest score in playersGrandTotals </param>
        private void DisplayWinner (int winnersPosition) {
            if (players.Count != 1) {
                form.ShowMessage("Game over! Player " + (winnersPosition + 1) + " wins! Play again? (Click OK)");
            } else {
                form.ShowMessage("Game over! Play again? (Click OK)");
            }
            form.ShowOKButton();
        }
        //end DisplayWinner

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Load a saved game from the default save game file
        /// </summary>
        /// <param name="form">the GUI form</param>
        /// <returns>the saved game</returns>
        public static Game Load(Form1 form) {
            Game game = null;
            if (File.Exists(savedGameFile)) {
                try {
                    Stream bStream = File.Open(savedGameFile, FileMode.Open);
                    BinaryFormatter bFormatter = new BinaryFormatter();
                    game = (Game)bFormatter.Deserialize(bStream);
                    bStream.Close();
                    game.form = form;
                    game.ContinueGame();
                    return game;
                } catch {
                    MessageBox.Show("Error reading saved game file.\nCannot load saved game.");
                }
            } else {
                MessageBox.Show("No current saved game.");
            }
            return null;
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Save the current game to the default save file
        /// </summary>
        public void Save() {
            try {
                Stream bStream = File.Open(savedGameFile, FileMode.Create);
                BinaryFormatter bFormatter = new BinaryFormatter();
                bFormatter.Serialize(bStream, this);
                bStream.Close();
                MessageBox.Show("Game saved");
            } catch (Exception e) {

                //   MessageBox.Show(e.ToString());
                MessageBox.Show("Error saving game.\nNo game saved.");
            }
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Continue the game after loading a saved game
        /// 
        /// Assumes game was saved at the start of a player's turn before they had rolled dice.
        /// </summary>
        private void ContinueGame() {
            LoadLabels(form);
            for (int i = 0; i < dice.Length; i++) {
                //uncomment one of the following depending how you implmented Active of Die
                // dice[i].SetActive(true);
                dice[i].Active = true;
            }

            form.ShowPlayerName(currentPlayer.Name);
            form.EnableRollButton();
            form.EnableCheckBoxes();
            // can replace string with whatever you used
            form.ShowMessage(messages[3]);
            currentPlayer.ShowScores();
        }//end ContinueGame

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        public BindingList<Player> Players {
            get {
                return players;
            }
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Link the labels on the GUI form to the dice and players
        /// </summary>
        /// <param name="form"></param>
        private void LoadLabels(Form1 form) {
            Label[] diceLabels = form.GetDice();
            for (int i = 0; i < dice.Length; i++) {
                dice[i].Load(diceLabels[i]);
            }
            for (int i = 0; i < players.Count; i++) {
                players[i].Load(form.GetScoreTotals());
            }

        }
    }
}

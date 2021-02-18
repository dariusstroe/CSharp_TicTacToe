using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region PrivateMembers

        //Holds the current result of the cell in the active game
        private MarkType[] mResult;

        //True if it's player one's turn (X) or false if it's player's two turn(O)
        private bool mPlayerOneTurn;

        //True if the game ended, false if not :)
        private bool mGameEnded;

        #endregion
        #region Constructor
        //Default Constructor
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }

        #endregion


        //Starts a new game and clear all values
        private void NewGame()
        {
            //Create a new blank array of free cells
            mResult = new MarkType[9];

            for (var i = 0; i < mResult.Length; i++)
                mResult[i] = MarkType.Free;

            //Make sure player 1 starts the game
            mPlayerOneTurn = true;

            //Integrate every button on grid
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                //Change background to default
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });

            //Game hasn't finished
            mGameEnded = false;
        }


        //Handles button a click event
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Start a new game after finish
            if(mGameEnded)
            {
                NewGame();
                return;
            }

            //Cast sender to button
            var button = (Button)sender;

            //Find the buttons position in array
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);

            //Don't do anything if the cell already has a value in it
            if (mResult[index] != MarkType.Free)
                return;

            //Set the cell value based on player turn
            if (mPlayerOneTurn)
                mResult[index] = MarkType.Cross;
            else
                mResult[index] = MarkType.Nought;

            //Set button text to result
            if (mPlayerOneTurn)
                button.Content = "X";
            else
                button.Content = "O";

            //Change O's to green
            if (!mPlayerOneTurn)
                button.Foreground = Brushes.Red;

            //Toggle the player's turns
            if (mPlayerOneTurn)
                mPlayerOneTurn = false;
            else
                mPlayerOneTurn = true;

            //Check for winner
            CheckForWinner();
        }

        //Check if there is a winner to the game(3 line straight)
        private void CheckForWinner()
        {
            #region Horizontal Wins
            //Check for horizontal wins
            //
            //  -Row 0
            //
            if (mResult[0]!=MarkType.Free && (mResult[0] & mResult[1] & mResult[2])==mResult[0])
            {
                //Endgame
                mGameEnded = true;

                //Highlight winning cells in green
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
            }

            //Check for horizontal wins
            //
            //  -Row 1
            //
            if (mResult[3] != MarkType.Free && (mResult[3] & mResult[4] & mResult[5]) == mResult[3])
            {
                //Endgame
                mGameEnded = true;

                //Highlight winning cells in green
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
            }

            //Check for horizontal wins
            //
            //  -Row 2
            //
            if (mResult[6] != MarkType.Free && (mResult[6] & mResult[7] & mResult[8]) == mResult[6])
            {
                //Endgame
                mGameEnded = true;

                //Highlight winning cells in green
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
            }
            #endregion

            #region Vertical Wins
            //Check for vertical wins
            //
            //  -Column 0
            //
            if (mResult[0] != MarkType.Free && (mResult[0] & mResult[3] & mResult[6]) == mResult[0])
            {
                //Endgame
                mGameEnded = true;

                //Highlight winning cells in green
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
            }

            //Check for vertical wins
            //
            //  -Column 1
            //
            if (mResult[1] != MarkType.Free && (mResult[1] & mResult[4] & mResult[7]) == mResult[1])
            {
                //Endgame
                mGameEnded = true;

                //Highlight winning cells in green
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
            }

            //Check for vertical wins
            //
            //  -Column 2
            //
            if (mResult[2] != MarkType.Free && (mResult[2] & mResult[5] & mResult[8]) == mResult[2])
            {
                //Endgame
                mGameEnded = true;

                //Highlight winning cells in green
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
            }
            #endregion

            #region Diagonal Wins

            //  -Diagonal 1
            //
            if (mResult[0] != MarkType.Free && (mResult[0] & mResult[4] & mResult[8]) == mResult[0])
            {
                //Endgame
                mGameEnded = true;

                //Highlight winning cells in green
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
            }

            //  -Diagonal 2
            //
            if (mResult[2] != MarkType.Free && (mResult[2] & mResult[4] & mResult[6]) == mResult[2])
            {
                //Endgame
                mGameEnded = true;

                //Highlight winning cells in green
                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;
            }

            #endregion region

            #region No Winners
            //Check for no winner and full board
            if (!mResult.Any(result => result== MarkType.Free))
            {
                //Endgame
                mGameEnded = true;

                //Everybody lost
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Orange;
                });
            }
            #endregion
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tic_tac_toe_Graphical
{
    public partial class Form1 : Form
    {

        private int[,] board = new int[3, 3];  // Initialize empty board.
        private bool player1; // Which player is playing right now.
        private bool gameBegun = false;
        private bool AImode = false;
        private Random r = new Random();
        List<Button> buttons;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            buttons = new List<Button>() { button1, button2, button3, button4, button5, button6, button7, button8, button9 };
            MessageBox.Show("Please Choose player mode 1P, or 2P from the menu.");
        }
        private void new2PToolStripMenuItem_Click(object sender, EventArgs e)
        {
            enablebuttons();
            gameBegun = true;
            this.board = new int[3, 3];  // Initialize empty board.
            Random r = new Random();
            player1 = (r.Next(1, 2) == 1) ? true : false;//Choose player randomly.
            displayBoard();
            string firstplayer = player1 ? "X" : "O";
            MessageBox.Show($"{firstplayer} begins");
        }
        private bool makeMove(int a, int b)
        {
            int c = player1 ? 1 : 2;
            if (board[a, b] == 0)
            {
                board[a, b] = c;
                return true;
            }
            return false;
        }
        //Checks if there is horizontal win, if there is, it returns row. else, returns -1.
        private int checkHorizontals()
        {
            for (int i = 0; i < 3; i++)
                if (board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2] && board[i, 0] != 0)
                    return i;
            return -1; // If there's no win, return -1.

        }
        // If there is vertical win, returns the column, else returns -1;
        private int checkVerticals()
        {
            for (int i = 0; i < 3; i++)
                if (board[0, i] == board[1, i] && board[1, i] == board[2, i] && board[0, i] != 0)
                    return i;
            return -1; // If there's no win, return -1.
        }

        // If there is a diagonal win, returns 1, else -1;
        private bool checkDiagonals() // Returns 1 if there is a diagonal win. Returns -1 otherwise.
        {
            if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2] && board[0, 0] != 0)
                return true;
            else if (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0] && board[0, 2] != 0)
                return true;
            return false;
        }
        // Checks if there is a win, returns winner number(1,2), or -1 if there is no winner.
        private int checkWin()
        {
            if (checkHorizontals() != -1)
                return (int)board[checkHorizontals(), 0];
            else if (checkVerticals() != -1)
                return (int)board[0, checkVerticals()];
            else if (checkDiagonals())
                return (int)board[1, 1];
            return -1;
        }

        // Plain display.
        private void displayBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == 1)
                    {
                        buttons[i * 3 + j].Text = "X";
                    }
                    else if (board[i, j] == 2)
                    {
                        buttons[i * 3 + j].Text = "O";
                    }
                    else
                    {
                        buttons[i * 3 + j].Text = "";
                    }
                }
            }
        }
        private void checkGUIwin()
        {
            if (checkWin() > 0)
            {
                MessageBox.Show($"Player {checkWin()} won");
                board = new int[3, 3];
                displayBoard();
                player1 = (r.Next(1, 2) == 1) ? true : false;//Choose player randomly.
                displayBoard();
                if (!AImode)
                {
                    string firstplayer = player1 ? "X" : "O";
                    MessageBox.Show($"{firstplayer} begins");
                }
                else
                {
                    player1 = true;
                }
            }
            if (board[0, 0] > 0 && board[0, 1] > 0 && board[0, 2] > 0 && board[1, 0] > 0 && board[1, 1] > 0 && board[1, 2] > 0 && board[2, 0] > 0 && board[2, 1] > 0 && board[2, 2] > 0)
            {
                MessageBox.Show("Draw!"); // If the board is full, then it is draw.
                board = new int[3, 3]; // Zero board
                displayBoard();         // Display  board.
                player1 = (r.Next(1, 2) == 1) ? true : false;//Choose player randomly.
                displayBoard(); //
                if (!AImode)
                {
                    string firstplayer = player1 ? "X" : "O";
                    MessageBox.Show($"{firstplayer} begins");
                }
                else
                {
                    player1 = true;
                }
            }

        }
        private void enablebuttons()
        {
            for (int i = 0; i < 9; i++)
                buttons[i].Enabled = true;
        }
        private bool validate_move(int a, int b)
        {
            bool legal = false;
            if (board[a, b] == 0 && gameBegun)
            {
                if (makeMove(a, b))
                    legal = true;
                displayBoard();
                player1 = !player1;
            }
            checkGUIwin();
            return legal;
        }
        private void makeAImove(int a, int b)
        {
            if (AImode)
            {
                if (!makeMove(a, b))
                {
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (makeMove(i, j))
                            {
                                player1 = !player1;
                                displayBoard();
                                checkGUIwin();
                                return;
                            }
                        }
                    }
                }
                else
                {
                    player1 = !player1;
                    displayBoard();
                    checkGUIwin();
                    return;
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (validate_move(0, 0))
                makeAImove(0, 1);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (validate_move(0, 1))
                makeAImove(0, 2);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (validate_move(0, 2))
                makeAImove(0, 1);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (validate_move(1, 0))
                makeAImove(0, 2);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (validate_move(1, 1))
                makeAImove(0, 0);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (validate_move(1, 2))
                makeAImove(0, 0);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (validate_move(2, 0))
                makeAImove(2, 2);

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (validate_move(2, 1))
                makeAImove(0, 2);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (validate_move(2, 2))
                makeAImove(0, 2);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hello there, this is tic tac toe with gui.");
        }

        private void new1PToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            enablebuttons();
            AImode = true; // Enables AI mode
            player1 = true; // First player is AI
            gameBegun = true; // Game is begun, clicks now take effect.
            makeMove(1, 1); // Makes first move
            player1 = !player1; // Swaps player
            displayBoard(); // Display the init result.
        }
    }
}


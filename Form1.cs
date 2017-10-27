using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace _2048
{
    public partial class Form1 : Form
    {
        Button[,] board = new Button[4, 4];
        public int counter = 0;
        public bool[,] globalOccupied = new bool[4, 4];
        enum Position
        {
            Left, Right, Up, Down
        }
        private int addNum = 2;
        Random randomNumber = new Random();
        string path = @"C:\temp\Best.txt";

        string fBest;
        private int score;
        private int best;
        Label tScore = new Label();
        Label tBest = new Label();
        bool checkGameWon = false;
        public Form1()
        {
            InitializeComponent();
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("0");                   
                }
            }
            using (StreamReader sr = File.OpenText(path))
            {
                
                //while (sr.ReadLine() != null)
                //{
                    fBest = sr.ReadLine();
                    best = Convert.ToInt32(fBest);
                //}
            }

            foreach (Control c in Controls)
            {
                if (c is Button)
                {
                    Button b = (Button)c;
                    FillBoard(b);
                }

            }

            tScore.Size = new Size(100, 22);
            tBest.Size = new Size(200, 22);
            tScore.Location = new Point(12, 46);
            tBest.Location = new Point(180, 46);
            tScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            tBest.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            tScore.Text = tScore.Text = ("wynik: " + score.ToString());
            tBest.Text = tBest.Text = ("rekord: " + best.ToString());
            this.Controls.Add(tScore);
            this.Controls.Add(tBest);


        }


        private void FillBoard(Button b)
        {
            int _x = 0;
            int _y = 0;
            #region Assign visible buttons to occupied array
            if (b.TabIndex <= 3)
            {
                switch (b.TabIndex)
                {
                    case 0:
                        _y = 0;
                        break;
                    case 1:
                        _y = 1;
                        break;
                    case 2:
                        _y = 2;
                        break;
                    case 3:
                        _y = 3;
                        break;
                }
            }
            else if ((b.TabIndex > 3) && (b.TabIndex <= 7))
            {
                _x = 1;
                switch (b.TabIndex)
                {
                    case 4:
                        _y = 0;
                        break;
                    case 5:
                        _y = 1;
                        break;
                    case 6:
                        _y = 2;
                        break;
                    case 7:
                        _y = 3;
                        break;
                }
            }
            else if ((b.TabIndex > 7) && (b.TabIndex <= 11))
            {
                _x = 2;
                switch (b.TabIndex)
                {
                    case 8:
                        _y = 0;
                        break;
                    case 9:
                        _y = 1;
                        break;
                    case 10:
                        _y = 2;
                        break;
                    case 11:
                        _y = 3;
                        break;
                }
            }
            else if ((b.TabIndex > 11) && (b.TabIndex <= 15))
            {
                _x = 3;
                switch (b.TabIndex)
                {
                    case 12:
                        _y = 0;
                        break;
                    case 13:
                        _y = 1;
                        break;
                    case 14:
                        _y = 2;
                        break;
                    case 15:
                        _y = 3;
                        break;
                }
            }
            board[_x, _y] = b;
            board[_x, _y].Text = "0";
            board[_x, _y].Visible = false;
            #endregion
        }
        int randomNr;
        private void FieldGenerator(Button b)
        {

            randomNr = randomNumber.Next(0, 9);
            int number = 0;
            switch (randomNr)
            {
                #region Base Number generator
                case 0:
                    number = 2;
                    break;
                case 1:
                    number = 2;
                    break;
                case 2:
                    number = 2;
                    break;
                case 3:
                    number = 2;
                    break;
                case 4:
                    number = 2;
                    break;
                case 5:
                    number = 2;
                    break;
                case 6:
                    number = 2;
                    break;
                case 7:
                    number = 4;
                    break;
                case 8:
                    number = 2;
                    break;
                case 9:
                    number = 2;
                    break;
                #endregion
            }

            try
            {
                if ((b.Visible == true) && (counter <= 15))
                {
                    GenerateField();
                }
                else if (b.Visible == false)
                {
                    b.Visible = true;
                    b.Text = number.ToString();
                    colorChange(b);
                    counter++;
                    addNum--;
                }
            }
            catch { }
        }

        private void GenerateAnimation(Button b)
        {


        }
        int r1;
        int r2;
        private void GenerateField()
        {
            while (addNum > 0)
            {
                r1 = randomNumber.Next(0, 4);
                r2 = randomNumber.Next(0, 4);
                FieldGenerator(board[r1, r2]);
            }
        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            bool checkIfMoved = false;
            switch (e.KeyCode)
            {
                case Keys.Left:
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            for (int k = j + 1; k < 4; k++)
                            {
                                if (board[i, k].Text == "0")
                                {
                                    continue;
                                }
                                else if (board[i, k].Text == board[i, j].Text)
                                {
                                    int x;
                                    Int32.TryParse(board[i, k].Text, out x);
                                    x *= 2;
                                    board[i, j].Text = x.ToString();
                                    colorChange(board[i, j]);
                                    board[i, j].Visible = true;
                                    score += x;
                                    board[i, k].Text = "0";
                                    board[i, k].Visible = false;
                                    checkIfMoved = true;
                                    counter--;
                                    break;
                                }
                                else
                                {
                                    if (board[i, j].Text == "0" && board[i, k].Text != "0")
                                    {
                                        board[i, j].Text = board[i, k].Text;
                                        board[i, j].Visible = true;
                                        colorChange(board[i, j]);
                                        board[i, k].Text = "0";
                                        board[i, k].Visible = false;
                                        j--;
                                        checkIfMoved = true;
                                        break;
                                    }
                                    else if (board[i, j].Text != "0")
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    break;
                case Keys.Down:
                    for (int j = 0; j < 4; j++)
                    {
                        for (int i = 3; i >= 0; i--)
                        {
                            for (int k = i - 1; k >= 0; k--)
                            {
                                if (board[k, j].Text == "0")
                                {
                                    continue;
                                }
                                else if (board[k, j].Text == board[i, j].Text)
                                {
                                    int x;
                                    Int32.TryParse(board[i, j].Text, out x);
                                    x *= 2;
                                    board[i, j].Text = x.ToString();
                                    board[i, j].Visible = true;
                                    colorChange(board[i, j]);
                                    score += x;
                                    board[k, j].Text = "0";
                                    board[k, j].Visible = false;
                                    checkIfMoved = true;
                                    counter--;
                                    break;
                                }
                                else
                                {
                                    if (board[i, j].Text == "0" && board[k, j].Text != "0")
                                    {
                                        board[i, j].Text = board[k, j].Text;
                                        board[i, j].Visible = true;
                                        colorChange(board[i, j]);
                                        board[k, j].Text = "0";
                                        board[k, j].Visible = false;
                                        i++;
                                        checkIfMoved = true;
                                        break;
                                    }
                                    else if (board[i, j].Text != "0")
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    break;
                case Keys.Right:
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 3; j >= 0; j--)
                        {
                            for (int k = j - 1; k >= 0; k--)
                            {
                                if (board[i, k].Text == "0")
                                {
                                    continue;
                                }
                                else if (board[i, k].Text == board[i, j].Text)
                                {
                                    int x;
                                    Int32.TryParse(board[i, j].Text, out x);
                                    x *= 2;
                                    board[i, j].Text = x.ToString();
                                    board[i, j].Visible = true;
                                    colorChange(board[i, j]);
                                    score += x;
                                    board[i, k].Text = "0";
                                    board[i, k].Visible = false;
                                    checkIfMoved = true;
                                    counter--;
                                    break;
                                }
                                else
                                {
                                    if (board[i, j].Text == "0" && board[i, k].Text != "0")
                                    {
                                        board[i, j].Text = board[i, k].Text;
                                        board[i, j].Visible = true;
                                        colorChange(board[i, j]);
                                        board[i, k].Text = "0";
                                        board[i, k].Visible = false;
                                        j++;
                                        checkIfMoved = true;
                                        break;
                                    }
                                    else if (board[i, j].Text != "0")
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    break;
                case Keys.Up:
                    for (int j = 0; j < 4; j++)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            for (int k = i + 1; k < 4; k++)
                            {
                                if (board[k, j].Text == "0")
                                {
                                    continue;
                                }
                                else if (board[k, j].Text == board[i, j].Text)
                                {
                                    int x;
                                    Int32.TryParse(board[i, j].Text, out x);
                                    x *= 2;
                                    board[i, j].Text = x.ToString();
                                    board[i, j].Visible = true;
                                    colorChange(board[i, j]);
                                    score += x;
                                    board[k, j].Text = "0";
                                    board[k, j].Visible = false;
                                    checkIfMoved = true;
                                    counter--;
                                    break;
                                }
                                else
                                {
                                    if (board[i, j].Text == "0" && board[k, j].Text != "0")
                                    {
                                        board[i, j].Text = board[k, j].Text;
                                        board[i, j].Visible = true;
                                        colorChange(board[i, j]);
                                        board[k, j].Text = "0";
                                        board[k, j].Visible = false;
                                        i--;
                                        checkIfMoved = true;
                                        break;
                                    }
                                    else if (board[i, j].Text != "0")
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    break;
            }


            if (checkIfMoved)
            {
                if (score > best)
                {
                    best = score;
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        sw.WriteLine(best.ToString());
                    }

                }
                tScore.Text = tScore.Text = ("wynik: " + score.ToString());
                tBest.Text = tBest.Text = ("rekord: " + best.ToString());

                ++addNum;
            }

            gameWon();
            gameOver();
        }

        private void gameWon()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if ((board[i, j].Text == "2048")&&(checkGameWon == false))
                    {
                        DialogResult result;
                        result =  MessageBox.Show("\t       WYGRAŁEŚ! \n\n           chcesz zacząć od początku?", "Brawo", MessageBoxButtons.YesNo);


                        if (result == DialogResult.Yes)
                        {
                            newGame();
                        }
                        else
                        {
                            checkGameWon = true;
                        }
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            GenerateField();
        }
        private void colorChange(Button b)
        {
            switch (b.Text)
            {
                case "0":
                    b.BackColor = Color.Bisque;
                    break;
                case "2":
                    b.BackColor = Color.AntiqueWhite;
                    break;
                case "4":
                    b.BackColor = Color.AntiqueWhite;
                    break;
                case "8":
                    b.BackColor = Color.Bisque;
                    break;
                case "16":
                    b.BackColor = Color.Bisque;
                    break;
                case "32":
                    b.BackColor = Color.NavajoWhite;
                    break;
                case "64":
                    b.BackColor = Color.NavajoWhite;
                    break;
                case "128":
                    b.BackColor = Color.Orange;
                    break;
                case "256":
                    b.BackColor = Color.Orange;
                    break;
                case "512":
                    b.BackColor = Color.DarkOrange;
                    break;
                case "1024":
                    b.BackColor = Color.DarkOrange;
                    break;
                case "2048":
                    b.BackColor = Color.Red;
                    b.ForeColor = Color.WhiteSmoke;
                    break;

            }
        }

        private void gameOver()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (i - 1 >= 0)
                    {
                        if (board[i - 1, j].Text == board[i, j].Text)
                        {
                            return;
                        }
                    }

                    if (i + 1 < 4)
                    {
                        if (board[i + 1, j].Text == board[i, j].Text)
                        {
                            return;
                        }
                    }

                    if (j - 1 >= 0)
                    {
                        if (board[i, j - 1].Text == board[i, j].Text)
                        {
                            return;
                        }
                    }

                    if (j + 1 < 4)
                    {
                        if (board[i, j + 1].Text == board[i, j].Text)
                        {
                            return;
                        }
                    }

                    if (board[i, j].Text == "0")
                    {
                        return;
                    }

                }
            }

            MessageBox.Show("Game Over!");
        }

        private void newGame()
        {
            foreach (Control c in Controls)
            {
                if (c is Button)
                {
                    Button b = (Button)c;
                    FillBoard(b);
                }

            }
            addNum = 2;
            score = 0;
            tScore.Text = tScore.Text = ("wynik: " + score.ToString());
            checkGameWon = false;
            GenerateField();
        }
        private void nowaGraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newGame();
        }

        private void zakończToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

    }
}


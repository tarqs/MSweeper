using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSweeper
{
    public partial class Form1 : Form
    {
        // Public variable declarations
        int chance;
        Random rand1 = new Random();
        static int bombAmount;

        public Form1()
        {
            this.AutoSize = false;
            this.Width = 520;
            this.Height = 520;
            // maakt een 8x8 grid van Cell objecten
            createGrid();
        }

        public void createGrid()
        {
            for (int i = 0; i < 8; i++)
            {
                // declareren van dummy variabele voor de lambda delegate voor eventhandling
                int i1 = i;
                grid[i] = new Cell[8];
                for (int j = 0; j < 8; j++)
                {
                    // declareren van dummy variabele voor de lambda delegate voor eventhandling
                    int j1 = j;

                    // random object om op willekeurige locaties bommen te plaatsen
                    chance = rand1.Next(0, 6);
                    if (chance == 0 && bombAmount < 10)
                    {
                        grid[i][j] = new Cell(true);
                        bombAmount++;
                    }
                    else
                    {
                        grid[i][j] = new Cell();
                    }

                    grid[i][j].Name = "grid" + i.ToString() + j.ToString();
                    grid[i][j].Location = new System.Drawing.Point(i * 49, j * 49);
                    grid[i][j].Size = new System.Drawing.Size(50, 50);
                    grid[i][j].TabIndex = 0;
                    //grid[i][j].Text = chance.ToString(); // Debugging purpose
                    grid[i][j].Click += (s, e) => Cell_click(i1, j1);
                    grid[i][j].Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                    this.Controls.Add(grid[i][j]);

                }
            }
        }

        // method om de text te veranderen van de Cell. Wordt pas geactiveerd bij klikken is op heden nog niet bugfree
        // niet op alle plekken worden de juiste counters weergegeven. Potentieel moet ik in deze functie CheckNeighbours() aanroepen
        public void RevealCell(int n, int m)
        {
            if (grid[n][m].HasBomb == true)
            {
                grid[n][m].Text = "B";
            }
            else if (grid[n][m].NeighbourBombCount > 0)
            {
                grid[n][m].Text = grid[n][m].NeighbourBombCount.ToString();
            }
            else
            {
                grid[n][m].Text = "NB";
                grid[n][m].Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            }

            grid[n][m].IsRevealed = true;
        }

        
        // method voor het checken van de hoeveelheid bommen bij de buren. Deze is ook niet bugfree. Edge case scenario's moet ik nog debuggen
        // mogelijk moet ik de edge case scenario's herschrijven, want de bovenste rij functioneert niet naar behoren en laat het programma crashen.
        public void CheckNeighbours(int cswitch, int n, int m)
        {
            switch (cswitch)
            {
                // Left upper corner (i = 0, j = 0)
                case 1:
                    for (int i = 0; i <= 1; i++)
                    {
                        for (int j = 0; j < 1; j++)
                        {
                            if (i == 0 && j == 0) { }
                            else
                            {
                                if (grid[n+i][m+j].HasBomb == true)
                                {
                                    grid[n][m].NeighbourBombCount++;
                                }
                            }
                        }
                    }
                    break;

                // Upper edge (i = 0, 0 < j < 7)
                case 2:
                    for (int i = 0; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            if (i == 0 && j == 0) { }
                            else
                            {
                                if (grid[n+i][m+j].HasBomb == true)
                                {
                                    grid[n][m].NeighbourBombCount++;
                                }
                            }
                        }
                    }
                    break;

                // Right upper corner (i = 0, j = 7)
                case 3:
                    for (int i = 0; i <= 1; i++)
                    {
                        for (int j = -1; j <= 0; j++)
                        {
                            if (i == 0 && j == 0) { }
                            else
                            {
                                if (grid[n+i][m+j].HasBomb == true)
                                {
                                    grid[n][m].NeighbourBombCount++;
                                }
                            }
                        }
                    }
                    break;

                // Right edge (0 < i < 7, j = 7)
                case 4:
                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 0; j++)
                        {
                            if (i == 0 && j == 0) { }
                            else
                            {
                                if (grid[n+i][m+j].HasBomb == true)
                                {
                                    grid[n][m].NeighbourBombCount++;
                                }
                            }
                        }
                    }
                    break;

                // Right down corner (i = 7, j = 7)
                case 5:
                    for (int i = -1; i <= 0; i++)
                    {
                        for (int j = -1; j <= 0; j++)
                        {
                            if (i == 0 && j == 0) { }
                            else
                            {
                                if (grid[n+i][m+j].HasBomb == true)
                                {
                                    grid[n][m].NeighbourBombCount++;
                                }
                            }
                        }
                    }
                    break;

                // Bottom edge (i = 7, 0 < j < 7)
                case 6:
                    for (int i = -1; i <= 0; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            if (i == 0 && j == 0) { }
                            else
                            {
                                if (grid[n+i][m+j].HasBomb == true)
                                {
                                    grid[n][m].NeighbourBombCount++;
                                }
                            }
                        }
                    }
                    break;

                // Left down corner (i = 7, j = 0)
                case 7:
                    for (int i = -1; i <= 0; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            if (i == 0 && j == 0) { }
                            else
                            {
                                if (grid[n+i][m+j].HasBomb == true)
                                {
                                    grid[n][m].NeighbourBombCount++;
                                }
                            }
                        }
                    }
                    break;

                // Left edge (i = 0, 0 < j < 7)
                case 8:
                    for (int i = 0; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            if (i == 0 && j == 0) { }
                            else
                            {
                                if (grid[n+i][m+j].HasBomb == true)
                                {
                                    grid[n][m].NeighbourBombCount++;
                                }
                            }
                        }
                    }
                    break;

                default:
                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            if (i == 0 && j == 0) { }
                            else
                            {
                                if (grid[n+i][m+j].HasBomb == true)
                                {
                                    grid[n][m].NeighbourBombCount++;
                                }
                            }
                        }
                    }
                    break;

            }
        }

    
        // Private Field declarations
        private Cell[][] grid = new Cell[8][];


        // eventhandler voor Cell Click.
        private void Cell_click(int i, int j)
        {

            if(i == 0 && j == 0)
            {
                CheckNeighbours(1, i, j);
            }

            if (i == 0 && j > 0 && j < 7)
            {
                CheckNeighbours(2, i, j);
            }

            if (i == 0 && j == 7)
            {
                CheckNeighbours(3, i, j);
            }

            if (i > 0 && i < 7 && j == 7)
            {
                CheckNeighbours(4, i, j);
            }

            if (i == 7 && j == 7)
            {
                CheckNeighbours(5, i, j);
            }

            if (i == 7 && j > 0 && j < 7)
            {
                CheckNeighbours(6, i, j);
            }

            if (i == 7 && j == 0)
            {
                CheckNeighbours(7, i, j);
            }

            if(i > 0 && i < 7 && j == 0)
            {
                CheckNeighbours(8, i, j);
            }

            RevealCell(i, j);

        }

        
        // code gegenereerd door de Form1.Designer.cs
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSweeper
{
    public partial class Cell : System.Windows.Forms.Button
    {
        private bool hasBomb;
        private bool isRevealed;
        private int neighbourBombCount;
        //static int BombAmount;

        // Properties
        public bool HasBomb
        {
            get { return hasBomb; }
            set { hasBomb = value; }
        }

        public bool IsRevealed
        {
            get { return isRevealed; }
            set { isRevealed = value; }
        }

        public int NeighbourBombCount
        {
            get { return neighbourBombCount; }
            set { neighbourBombCount = value; }
        }


        // constructors
        public Cell()
        {
            hasBomb = false;
            isRevealed = false;
            neighbourBombCount = 0;
            this.Size = new System.Drawing.Size(50, 50);

        }

        public Cell(bool bmb)
        {
            hasBomb = bmb;
            isRevealed = false;
            neighbourBombCount = 0;
            this.Size = new System.Drawing.Size(50, 50);
        }
    }
}

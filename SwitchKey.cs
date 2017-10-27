using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048
{
    class SwitchKey
    {
        public int line;
        public int column;
        
        public SwitchKey(int i, int j, System.Windows.Forms.KeyEventArgs e)
        {
            
            
            #region Keycode switch
            switch (e.KeyCode)
            {
                case (System.Windows.Forms.Keys.Left):
                    {
                        column = j-1;
                        line = i;
                        break;
                    }
                case (System.Windows.Forms.Keys.Right):
                    {
                        column = j + 1;
                        line = i;
                        break;
                    }
                case (System.Windows.Forms.Keys.Up):
                    {
                        column = j;
                        line = i-1;
                        break;
                    }
                case (System.Windows.Forms.Keys.Down):
                    {
                        column = j;
                        line = i+1;
                        break;
                    }
            }
            #endregion
        }
    }
}

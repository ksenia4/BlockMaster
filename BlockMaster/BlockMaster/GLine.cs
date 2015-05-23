using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace BlockMaster
{
    class GLine : IBlockSheme
    {
        private Shape GrLine;
        private Label GComment;
        private Line Line;

        GLine()
        { }
        
        GLine(Shape InLine, Label InComment)
        {
            GrLine = InLine;
            GComment = InComment;
        }

        public int SetStartPosition(Shape IElement)
        {
            return 1;
        }
        public int SetEndPosition(Shape IElement)
        {
            return 1;
        }
    }
}

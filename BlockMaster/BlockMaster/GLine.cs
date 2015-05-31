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
        private Shape[] GrLine;
        private Label GComment;
        private Link Line;

        GLine()
        { }
        
        GLine(Shape[] InLine, Label InComment)
        {
            GrLine = InLine;
            GComment = InComment;
        }

        public int SetStartPosition(string ID, Condition CurrentCondition)
        {
            return 1;
        }
        public int SetEndPosition(string ID, Condition CurrentCondition)
        {
            return 1;
        }

        public int SetComment(string ID, Condition CurrentCondition, string Comment)
        {
            GLine Element = CurrentCondition.LineIDs[ID];
            Element.Line.Comment = Comment;
            return 0;
        }

        public int SetTitle(string ID, Condition CurrentCondition, string Title)
        {
            GLine Element = CurrentCondition.LineIDs[ID];
            Element.Line.Title = Title;
            return 0;
        }
    }
}

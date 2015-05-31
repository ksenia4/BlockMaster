﻿using System;
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

        //Sticky
        public GLine(string StartID, string EndID, string LinkID)
        {
            StartID = StartID.Substring(1);
            EndID = EndID.Substring(1);
            LinkID = LinkID.Substring(1);
            Line = new Link(LinkID);
            Line.Start = StartID;
            Line.End = EndID;
        }
        //--Sticky

        public int SetStartPosition()
        {
            return 1;
        }
        public int SetEndPosition()
        {
            return 1;
        }

        public int SetComment(string Comment)
        {
            Line.Comment = Comment;
            return 0;
        }

        public int SetTitle(string Title)
        {
            Line.Title = Title;
            return 0;
        }

        public int SetPositionAndSize(double Top, double Left)
        {
            return 1;
        }

        //Sticky
        public Link GetLink()
        {
            return Line;
        }
        //--Sticky
    }
}

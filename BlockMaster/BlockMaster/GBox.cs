﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace BlockMaster
{
    [Serializable]
    public class GBox: IBlockSheme
    {
        [XmlIgnore]
        public Shape GElement;

        public Box Element;

        public GBox()
        { }

        public GBox(Shape InElement)
        {
            GElement = InElement;

            Element = new Box();

            Element.ID = InElement.Name;
        }

        public GBox(Shape InElement, string Title,  string Comment, int Type)
        {
           
            GElement    = InElement;

            Element     = new Box();

            //Sticky
            
            //Element.ID = InElement.Name;
            Element.ID = InElement.Name.Substring(1);

            //--Sticky

            Element.Top     = Canvas.GetTop(InElement);
            Element.Left    = Canvas.GetLeft(InElement);
            Element.Height  = InElement.Height;
            Element.Width   = InElement.Width;

            Element.Comment = Comment;
            Element.Title   = Title;
            Element.Type    = Type;

        }

        public int SetStartPosition()
        {
            Element.StartPosition = true;
            return 0;
        }

        public int SetEndPosition()
        {
            Element.EndPosition = true;
            return 0;
        }

        public int SetComment(string Comment)
        {
            Element.Comment = Comment;
            return 0;
        }

        public int SetTitle(string Title)
        {
            Element.Title = Title;
            return 0;
        }

        public int SetPositionAndSize(double InTop, double InLeft, double Height, double Width)
        {
            Element.Top = InTop;
            Element.Left = InLeft;
            Element.Height = /*GElement.*/Height;
            Element.Width = /*GElement.*/Width;
            return 0;
        }
    }
}

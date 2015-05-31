using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Controls;
namespace BlockMaster
{
    class GBox: IBlockSheme
    {
       
        private Shape GElement;
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

            Element.ID = InElement.Name;

            Element.Top     = Canvas.GetTop(InElement);
            Element.Left    = Canvas.GetLeft(InElement);
            Element.Height  = InElement.Height;
            Element.Width   = InElement.Width;

            Element.Comment = Comment;
            Element.Title   = Title;
            Element.Type    = Type;

        }

        public int SetStartPosition(string ID, Condition CurrentCondition)
        {
            GBox Element = CurrentCondition.BoxIDs[ID];
            Element.Element.StartPosition = true;
            return 0;
        }

        public int SetEndPosition(string ID, Condition CurrentCondition)
        {
            GBox Element = CurrentCondition.BoxIDs[ID];
            Element.Element.EndPosition = true;
            return 0;
        }

        public int SetComment(string ID, Condition CurrentCondition, string Comment)
        {
            GBox Element = CurrentCondition.BoxIDs[ID];
            Element.Element.Comment = Comment;
            return 0;
        }

        public int SetTitle(string ID, Condition CurrentCondition, string Title)
        {
            GBox Element = CurrentCondition.BoxIDs[ID];
            Element.Element.Title = Title;
            return 0;
        }

    }
}

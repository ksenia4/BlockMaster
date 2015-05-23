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
        private Label GComment;
        private Box Element; 

        
        GBox()
        { }

        GBox(Shape InElement, Label InComment)
        {
            GElement = InElement;
            GComment = InComment;

            Element = new Box();
            Element.Label_MBottom = InComment.Margin.Bottom;
            Element.Label_MLeft = InComment.Margin.Left;
            Element.Shape_MRight = InComment.Margin.Right;
            Element.Label_MTop = InComment.Margin.Top;
            Element.Label_Text = InComment.Content.ToString();

            Element.Shape_MBottom = InElement.Margin.Bottom;
            Element.Shape_MLeft = InElement.Margin.Left;
            Element.Shape_MRight = InElement.Margin.Right;
            Element.Shape_MTop = InElement.Margin.Top;

        }

        public int SetStartPosition(Shape IElement)
        {
            Element.StartPosition = true;
            return 0;
        }
        public int SetEndPosition(Shape IElement)
        {
            Element.EndPosition = true;
            return 0;
        }
    }
}

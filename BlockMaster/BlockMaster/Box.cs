using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockMaster
{
    [Serializable]
    public class Box
    {
        public string ID;

        public double Left;
        public double Top;
        public double Height;
        public double Width;

        public bool StartPosition;
        public bool EndPosition;

        public string Title;
        public string Comment;
        public int Type; // 0 - Ellips; 1 - Rectangle; 2 - Rhomb

        public Box()
        {  }

    }
    
}

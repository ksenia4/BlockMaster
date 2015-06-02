using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockMaster
{
    [Serializable]
    public class Link
    {
        public string ID;

        public string Title;
        public string Comment;

        //Sticky
        public string Start;
        public string End;
        //--Sticky

        public Link()
        {}

        public Link(string InID)
        {
            ID = InID;
        }
    }
}

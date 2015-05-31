using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockMaster
{
    class Link
    {
        public int ID;

        public string Title;
        public string Comment;

        public Link()
        {}

        public Link(int InID)
        {
            ID = InID;
        }
    }
}

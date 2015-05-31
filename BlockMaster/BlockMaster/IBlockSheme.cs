using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace BlockMaster
{
    interface IBlockSheme
    {
        int SetStartPosition(string ID, Condition CurrentCondition);

        int SetEndPosition(string ID, Condition CurrentCondition);

        int SetComment(string ID, Condition CurrentCondition, string Comment);

        int SetTitle(string ID, Condition CurrentCondition, string Title);

    }
}

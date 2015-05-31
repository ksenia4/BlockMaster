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
        int SetStartPosition();

        int SetEndPosition();

        int SetComment(string Comment);

        int SetTitle(string Title);

        int SetPositionAndSize(double Top, double Left);

    }
}

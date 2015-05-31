using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockMaster
{
    class Condition
    {
        public Dictionary<string, GBox> BoxIDs;
        public Dictionary<string, GLine> LineIDs;

        Dictionary<string, int> IDsForMatrix;

        string[,] CondMatrix;
        int AmountOfelements;

        public Condition()
        {
            AmountOfelements = 0;
            BoxIDs = new  Dictionary<string, GBox>();
            LineIDs = new Dictionary<string, GLine>();
            IDsForMatrix = new Dictionary<string, int>();
            CondMatrix = new string[0,0];
        }

        public Condition(Condition CurrentCondition)
        {
            AmountOfelements = CurrentCondition.AmountOfelements;
            BoxIDs = new Dictionary<string, GBox>();
            LineIDs = new Dictionary<string, GLine>();
            IDsForMatrix = new Dictionary<string, int>();
            CondMatrix = new string[0, 0];

            foreach (KeyValuePair<string, GBox> Element in CurrentCondition.BoxIDs)
            {
                BoxIDs.Add(Element.Key, Element.Value);
            }

            foreach (KeyValuePair<string, GLine> Element in CurrentCondition.LineIDs)
            {
                LineIDs.Add(Element.Key, Element.Value);
            }

            foreach (KeyValuePair<string, int> Element in CurrentCondition.IDsForMatrix)
            {
                IDsForMatrix.Add(Element.Key, Element.Value);
            }

            for (int i = 0; i < AmountOfelements; i++)
            {
                for (int j = 0; j < AmountOfelements; j++)
                {
                   // CondMatrix[i, j] = CurrentCondition.CondMatrix[i, j];
                }
            }
        }
   
        public int AddElementInCondition(GBox Element)
        {
            AmountOfelements++;
            BoxIDs.Add(Element.Element.ID, Element);
            IDsForMatrix.Add(Element.Element.ID, AmountOfelements);

            return 0;
        }

        public int AddConnection(string ID1, string ID2, GLine GlineEl, string LineID)
        {
            int FirstIndex = IDsForMatrix[ID1];
            int SecondIndex = IDsForMatrix[ID2];

            LineIDs.Add(LineID, GlineEl);
            CondMatrix[FirstIndex, SecondIndex] = LineID;

            return 0;
        }

        public GBox TakeGBoxFromCondition(string ID)
        {
            return BoxIDs[ID];
        }

        public GLine TakeGLineFromCondition(string ID)
        {
            return LineIDs[ID];
        }

    }
}

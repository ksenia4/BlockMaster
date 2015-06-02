using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockMaster
{
    [Serializable()]
    public class Condition
    {
        public SerializableDictionary<string, GBox> BoxIDs;
        public SerializableDictionary<string, GLine> LineIDs;

        public SerializableDictionary<string, int> IDsForMatrix;

        public List<List<string>> CondMatrix;
        public int AmountOfelements;
        public int AmountOfLines;

        public Condition()
        {
            AmountOfelements = 0;
            AmountOfLines = 0;
            BoxIDs = new SerializableDictionary<string, GBox>();
            LineIDs = new SerializableDictionary<string, GLine>();
            IDsForMatrix = new SerializableDictionary<string, int>();
            CondMatrix = new  List<List<string>>(0);
        }

        public Condition(Condition CurrentCondition)
        {
            AmountOfelements = CurrentCondition.AmountOfelements;
            AmountOfLines = CurrentCondition.AmountOfLines;

            BoxIDs = new SerializableDictionary<string, GBox>();
            LineIDs = new SerializableDictionary<string, GLine>();
            IDsForMatrix = new SerializableDictionary<string, int>();
            CondMatrix = new List<List<string>>(CurrentCondition.CondMatrix);

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

            //for (int i = 0; i < AmountOfelements; i++)
            //{
            //    for (int j = 0; j < AmountOfelements; j++)
            //    {
            //        CondMatrix[i][j] = CurrentCondition.CondMatrix[i][j];
            //    }
            //}
        }
   
        public int AddElementInCondition(GBox Element)
        {
            AmountOfelements++;
            BoxIDs.Add(Element.Element.ID, Element);
            IDsForMatrix.Add(Element.Element.ID, AmountOfelements-1);
            CondMatrix.Add(new List<string>(AmountOfelements));

            for (int i = 0; i < AmountOfelements-1; i++)
            {
                CondMatrix[AmountOfelements-1].Add("");
            }

            for (int i = 0; i < AmountOfelements; i++)
            {
                CondMatrix[i].Add("");
            }
            
            return 0;
        }

        public int AddConnection(string ID1, string ID2, GLine GlineEl, string LineID)
        {
            AmountOfLines++;

            int FirstIndex = IDsForMatrix[ID1];
            int SecondIndex = IDsForMatrix[ID2];

            LineIDs.Add(LineID, GlineEl);
            CondMatrix[FirstIndex][SecondIndex] = LineID;

            return 0;
        }

        public int DeleteLineFromCondition(GLine Line)
        {
            AmountOfLines--;
            
            LineIDs.Remove(Line.Line.ID);

            for (int i = 0; i < AmountOfelements; i++)
            {
                for (int j = 0; j < AmountOfelements; j++)
                {
                    if (CondMatrix[i][j]==Line.Line.ID) CondMatrix[i][j] = "";
                }
            }

            return 0;
        }

        public int DeleteElementFromCondition(GBox Element)
        {
            int IndexOfElement = IDsForMatrix[Element.Element.ID];
               
            for (int i = 0; i < AmountOfelements; i++)
            {
                if (CondMatrix[IndexOfElement][i] != "")
                {
                    AmountOfLines--;
                    LineIDs.Remove(CondMatrix[IndexOfElement][i]);
                }
                CondMatrix[IndexOfElement][i] = "";
            }
            for (int j = 0; j < AmountOfelements; j++)
            {
                if (CondMatrix[j][IndexOfElement] != "")
                {
                    AmountOfLines--;
                    LineIDs.Remove(CondMatrix[j][IndexOfElement]);
                }
                CondMatrix[j][IndexOfElement] = "";
            }
            
            BoxIDs.Remove(Element.Element.ID);
            
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

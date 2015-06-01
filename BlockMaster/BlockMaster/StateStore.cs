using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockMaster
{
    class StateStore
    {
        Stack<Condition> StateStoreStack;
        Stack<Condition> DopStateStoreStack;

        public StateStore()
        {
            StateStoreStack = new Stack<Condition>();
            DopStateStoreStack = new Stack<Condition>();
        }

        public StateStore(Condition NewCondition)
        {
            StateStoreStack = new Stack<Condition>();
            DopStateStoreStack = new Stack<Condition>();
            StateStoreStack.Push(NewCondition);
        }

        public int AddConditionInStore(Condition NewCondition)
        {
            StateStoreStack.Push(NewCondition); 
            return 0;
        }

        public Condition TakeConditionFromStore()
        {
            Condition TakenCondition = StateStoreStack.Pop();
            DopStateStoreStack.Push(TakenCondition);
            return TakenCondition;
        }

        public Condition TakeConditionFromDopStore()
        {
            Condition TakenCondition = DopStateStoreStack.Pop();
            StateStoreStack.Push(TakenCondition);
            return TakenCondition;
        }
    }
}

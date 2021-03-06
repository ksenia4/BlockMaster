﻿using System;
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

        public Condition TakeConditionFromStore(Condition CurrentCondition)
        {
            if (StateStoreStack.Count == 0) return CurrentCondition;
            Condition TakenCondition = StateStoreStack.Pop();
            DopStateStoreStack.Push(CurrentCondition);
            return TakenCondition;
        }

        public Condition TakeConditionFromDopStore(Condition CurrentCondition)
        {
            if (DopStateStoreStack.Count == 0) return CurrentCondition;
            Condition TakenCondition = DopStateStoreStack.Pop();
            StateStoreStack.Push(CurrentCondition);
            return TakenCondition;
        }
    }
}

using System;
using System.Collections.Generic;

namespace Utils
{
    public class TypeSwitch
    {
        Dictionary<Type, Action<object>> matches = new Dictionary<Type, Action<object>>(); 

        public TypeSwitch Case<T>(Action<T> action)
        {
            matches[typeof (T)] = (x) => action((T)x);
            return this;
        }

        public void Switch<T>(T arg)
        {
            matches[typeof (T)](arg);
        }
    }
}
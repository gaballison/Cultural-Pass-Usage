using System;
using System.Collections.Generic;
using System.Text;

namespace Cultural_Pass_Usage
{
    interface IData
    {
        public interface IData<T>
        {
            void Save(IList<T> objCollection);

            void SaveOne(T obj, Func<IList<T>, int> query);

            IList<T> Get();

            T GetByQuery(Func<IList<T>, T> query);
        }
    }
}

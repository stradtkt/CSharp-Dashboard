using Login.Models;
using LoginPlus.Models;    //because BaseEntity is in this namespace
using System.Collections.Generic;
namespace LoginPlus.Factory
{
    public interface IFactory<T> where T : BaseEntity
    {
        void Add(T instance);
        T FindByID(int id);
        IEnumerable<T> FindAll();
        //what else might you want to include here?
    }
}
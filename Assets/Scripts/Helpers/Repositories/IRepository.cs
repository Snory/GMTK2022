using System;
using System.Collections.Generic;

public interface IRepository<T>
{
    T Add(T item);
    IEnumerable<T> FindAll();

    void Load();
    void Save();
}

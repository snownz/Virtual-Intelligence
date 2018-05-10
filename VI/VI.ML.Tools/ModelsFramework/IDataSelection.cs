using System.Collections.Generic;
using VI.Genetic.Data;

namespace VI.ML.Tools.ModelsFramework
{
    public interface IDataSelection<T> where T : class
    {
        List<T> Select(List<T> dt);
    }
}
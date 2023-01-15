using System;
using System.Collections.Generic;

namespace Assets.Sources.Tools
{
    /// <summary>
    /// Permùet de mapper 2 listes de l'inspector pour les utiliser comme un dico
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="V"></typeparam>
    public class DictionaryForInspector<K, V>
    {
        public List<K> Keys;
        public List<V> Values;

        //Unity doesn't know how to serialize a Dictionary
        private Dictionary<K, V> _myDictionary = new Dictionary<K, V>();

        public DictionaryForInspector(List<K> keys, List<V> values)
        {
            if (keys.Count != values.Count)
                throw new Exception("Il doit y avoir autant de clés que de valeurs !");

            Keys = keys;
            Values = values;
            _myDictionary = new Dictionary<K, V>();

            for (var i = 0; i != Math.Min(Keys.Count, Values.Count); i++)
                _myDictionary.Add(Keys[i], Values[i]);
        }

        public Dictionary<K, V> Get()
        {
            return _myDictionary;
        }

    }
}

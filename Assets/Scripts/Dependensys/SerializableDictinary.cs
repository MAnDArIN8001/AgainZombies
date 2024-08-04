using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableDictinary<TKey, TValue>
{
    [SerializeField] private List<TKey> _keys = new List<TKey>();
    [SerializeField] private List<TValue> _values = new List<TValue>();
     
    public void Add(TKey key, TValue value)
    {
        if (_keys.Contains(key))
        {
            Debug.LogError($"the serializable dictionary alredy contains key {key}");

            return;
        }

        _keys.Add(key);
        _values.Add(value);
    }

    public void Clear()
    {
        _keys.Clear();
        _values.Clear();
    }

    public bool ContainsKey(TKey key) => _keys.Contains(key);

    public bool ContainsValue(TValue value) => _values.Contains(value);

    public TValue GetValue(TKey key)
    {
        if (!_keys.Contains(key))
        {
            throw new Exception($"the dictionry doesnt contains value with key {key}");
        }

        int indexOfValue = _keys.IndexOf(key);

        return _values[indexOfValue];
    }

    public Dictionary<TKey, TValue> ToDictionary()
    {
        Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();

        for (int i = 0; i < _keys.Count; i++)
        {
            if (i < _values.Count)
            {
                dictionary.Add(_keys[i], _values[i]);
            }
            else
            {
                break;
            }
        }

        return dictionary;
    }
}

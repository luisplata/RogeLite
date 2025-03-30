using System;
using System.Collections.Generic;
using UnityEngine;

public static class JsonHelper
{
    public static string ToJson<T>(List<T> list)
    {
        return JsonUtility.ToJson(new Wrapper<T> { Items = list });
    }

    public static List<T> FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items ?? new List<T>();
    }

    [Serializable]
    private class Wrapper<T>
    {
        public List<T> Items;
    }
}
using UnityEngine;

[CreateAssetMenu(menuName = "Events/BoolEvent")]
public class BoolEvent : ScriptableObject
{
    private event System.Action<bool> listeners;

    public void Raise(bool value) => listeners?.Invoke(value);
    public void Register(System.Action<bool> listener) => listeners += listener;
    public void Unregister(System.Action<bool> listener) => listeners -= listener;
}
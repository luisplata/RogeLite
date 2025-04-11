using UnityEngine;

public class BaseTerrain : MonoBehaviour
{
    [Header("Anchors")]
    [SerializeField] private Transform anchorN;
    [SerializeField] private Transform anchorS;
    [SerializeField] private Transform anchorE;
    [SerializeField] private Transform anchorO;

    public Transform GetAnchor(string anchorName)
    {
        switch (anchorName)
        {
            case "N": return anchorN;
            case "S": return anchorS;
            case "E": return anchorE;
            case "O": return anchorO;
            default: return null;
        }
    }
}
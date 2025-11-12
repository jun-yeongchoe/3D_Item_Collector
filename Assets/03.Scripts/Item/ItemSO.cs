using UnityEngine;

[CreateAssetMenu(fileName = "Item_", menuName = "Game/ItemData")]
public class ItemSO : ScriptableObject
{
    public int ID;
    public string itemName;
    public int point;
    public GameObject prefab;
}


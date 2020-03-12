using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Items/Database")]
public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemObject[] ItemObject;
    //public Dictionary<int, ItemObject> GetItem = new Dictionary<int, ItemObject>();
    public void UpdateID()
    {
        for (int i = 0; i < ItemObject.Length; i++)
        {
            if (ItemObject[i].data.Id != i)
                ItemObject[i].data.Id = i;
        }
    }

    public void OnAfterDeserialize()
    {
        //for (int i = 0; i < Items.Length; i++)
        //{
        //    Items[i].data.Id = i;
        //    //GetItem.Add(i, Items[i]);
        //}

        UpdateID();
    }

    public void OnBeforeSerialize()
    {
        //GetItem = new Dictionary<int, ItemObject>();
    }
}

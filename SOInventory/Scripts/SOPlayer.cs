using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOPlayer : MonoBehaviour
{
    public InventoryObject inventory;
    public InventoryObject equipement;

    public Attribute[] attributes;

    // Start is called before the first frame update
    private void Start()
    {
        for (int i = 0; i < attributes.Length; i++)
        {
            attributes[i].SetParent(this);
        }
        for (int i = 0; i < equipement.GetSlots.Length; i++)
        {
            equipement.GetSlots[i].OnBeforeUpdate += OnBeforeSlotUpdate;
            equipement.GetSlots[i].OnAfterUpdate += OnAfterSlotUpdate;
        }
    }

    public void OnBeforeSlotUpdate(InventorySlot _slot)
    {
        if (_slot.ItemObject == null)
            return;
        switch (_slot.parent.inventory.type)
        {
            case InterfaceType.Inventory:
                break;
            case InterfaceType.Equipement:
                for (int i = 0; i < _slot.item.buffs.Length; i++)
                {
                    for (int j = 0; j < attributes.Length; j++)
                    {
                        if (attributes[j].type == _slot.item.buffs[i].attribute)
                            attributes[j].value.RemoveModifier(_slot.item.buffs[i]);
                    }
                }
                break;
            case InterfaceType.Chest:
                break;
            default:
                break;
        }

    }

    public void OnAfterSlotUpdate(InventorySlot _slot)
    {
        if (_slot.ItemObject == null)
            return;
        switch (_slot.parent.inventory.type)
        {
            case InterfaceType.Inventory:
                break;
            case InterfaceType.Equipement:
                for (int i = 0; i < _slot.item.buffs.Length; i++)
                {
                    for (int j = 0; j < attributes.Length; j++)
                    {
                        if (attributes[j].type == _slot.item.buffs[i].attribute)
                            attributes[j].value.AddModifier(_slot.item.buffs[i]);
                    }
                }
                break;
            case InterfaceType.Chest:
                break;
            default:
                break;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        var item = other.GetComponent<GroundItem>();
        if (item)
        {
            Item _item = new Item(item.item);
            if (inventory.AddItem(_item, 1))
            {
                Destroy(other.gameObject);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inventory.Save();
            equipement.Save();
            Debug.Log("saved");
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            inventory.Load();
            equipement.Load();
            Debug.Log("loaded");
        }
    }

    public void AttributeModifed(Attribute attribute)
    {
        Debug.Log(string.Concat(attribute.type, " was updated! Value is now ", attribute.value.ModifiedValue));
    }

    private void OnApplicationQuit()
    {
        inventory.Clear();
        equipement.Clear();
    }
}

[System.Serializable]
public class Attribute
{
    [System.NonSerialized]
    public SOPlayer parent;
    public Attributes type;
    public ModifiableInt value;

    public void SetParent(SOPlayer _parent)
    {
        parent = _parent;
        value = new ModifiableInt(AttributeModified);
    }

    public void AttributeModified()
    {
        parent.AttributeModifed(this);
    }
}

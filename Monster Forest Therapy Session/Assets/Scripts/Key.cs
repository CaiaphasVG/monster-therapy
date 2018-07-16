using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Key")]
public class Key : Item {

    public GameObject gameMaster;

    public override void Use()
    {
        base.Use();
        EquipmentManager.instance.OpenGate();
        RemoveFromInventory();
    }
}

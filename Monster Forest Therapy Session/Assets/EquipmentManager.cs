using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour {

    public static EquipmentManager instance;
    public GameObject gate;

    private void Awake()
    {
        instance = this;
    }

    public void OpenGate()
    {
        gate.SetActive(false);
    }
}

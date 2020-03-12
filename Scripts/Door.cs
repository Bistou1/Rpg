﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform doorEntryPosition;

    public void OpenDoor()
    {
        FindObjectOfType<RpgPlayer>().transform.position = doorEntryPosition.position;
    }
}
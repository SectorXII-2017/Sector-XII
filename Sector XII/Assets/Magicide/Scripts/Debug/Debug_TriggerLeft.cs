﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debug_TriggerLeft : MonoBehaviour {

    public Text TextComp;
    public Char_Necromancer NecroTested;

    void Start() {

    }


    void Update() {

        if (TextComp != null && NecroTested != null) {

            TextComp.text = NecroTested.GetLeftTriggerInput.ToString();
        }
    }
}
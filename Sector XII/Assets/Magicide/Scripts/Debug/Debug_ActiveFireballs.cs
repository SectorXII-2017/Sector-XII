﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debug_ActiveFireballs : MonoBehaviour {

    public Text TextComp;
    public Wep_Orb OrbTested;

    private int iCurrentDelay;

    void Start() {

    }


    void Update() {

        if (TextComp != null && OrbTested != null) {

            TextComp.text = OrbTested.GetPoolActiveCount().ToString();
        }
    }
}

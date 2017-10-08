﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_Wyrm : Character {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 4.10.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // VARIABLES



    //--------------------------------------------------------------
    // CONSTRUCTORS

    public override void Start() {

        // Set starting health
        _StartingHealth = AiManager._pInstance._WyrmStartingHealth;
        base.Start();

        // Set movement speed
        _MovementSpeed = AiManager._pInstance._WyrmMovementSpeed;

        // Add to inactive array
        ///AiManager._pInstance.GetInactiveMinions().Add(this.gameObject);
        AiManager._pInstance.GetActiveMinions().Add(this);
    }

    //--------------------------------------------------------------
    // FRAME

    public override void Update() {

    }

    public override void FixedUpdate() {

        // Move forward constantly
        ///transform.Translate(transform.forward * Time.deltaTime * _MovementSpeed);
    }

    //--------------------------------------------------------------
    //  HEALTH & DAMAGE

    public override void OnDeath() {

        // Get last known alive position and store it
        base.OnDeath();

        // hide character & move out of playable space
        GetComponentInChildren<Renderer>().enabled = false;
        transform.position = new Vector3(1000, 0, 1000);

        // Find self in active pool
        foreach (var wyrm in AiManager._pInstance.GetActiveMinions()) {

            // Once we have found ourself in the pool
            if (wyrm == this) {

                // Move to inactive pool
                AiManager._pInstance.GetInactiveMinions().Add(wyrm);
                AiManager._pInstance.GetActiveMinions().Remove(wyrm);
                break;
            }
        }
    }
}
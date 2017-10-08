﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 7.10.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // VARIABLES

    /// Public (designers)
    [Header("Phase Timer")]
    public Text _MatchTimer;                                        // Reference to the match timer text that is displayed in the HUD.
    public Color _TimeLowColour = Color.red;
    public Color _TimeMediumColour = Color.yellow;
    public Color _TimeOkayColour = Color.white;
    [Header("Players")]
    public Char_Necromancer PlayerAlpha;                            // Reference to player 1's character.
    public Color _PlayerAlphaColour;                                // Player 1's HUD colour pallet.
    public Char_Necromancer PlayerBravo;                            // Reference to player 2's character.
    public Color _PlayerBravoColour;                                // Player 2's HUD colour pallet.
    public Char_Necromancer PlayerCharlie;                          // Reference to player 3's character.
    public Color _PlayerCharlieColour;                              // Player 3's HUD colour pallet.
    public Char_Necromancer PlayerDelta;                            // Reference to player 4's character.
    public Color _PlayerDeltaColour;                                // Player 4's HUD colour pallet.

    /// Public (internal)
    [HideInInspector]
    public static HUD _pInstance;                                   // This is a singleton script, Initialized in Startup().

    /// Private
    private bool _DisplayHUD = false;                               // Returns TRUE if the HUD is being displayed on screen.

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    public void Awake() {

        // If the singleton has already been initialized yet
        if (_pInstance != null && _pInstance != this) {

            Destroy(this.gameObject);
            return;
        }

        // Set singleton
        _pInstance = this;
    }

    //--------------------------------------------------------------
    // *** FRAME ***

    public void Update() {

        // Only display HUD if its allowed to
        if (_DisplayHUD == true) {

            // Phase1 in session
            if (MatchManager._pInstance.GetPhase1Active() == true) {

                // Update HUD
                _MatchTimer.text = MatchManager._pInstance.GetPhase1Timer().ToString("00");
            }

            // Phase2 in session
            if (MatchManager._pInstance.GetPhase2Active() == true) {

                // Phase2 has a time limit
                if (MatchManager._pInstance.GetMaxMatchTime() == true) {

                    // Update HUD
                    _MatchTimer.text = MatchManager._pInstance.GetPhase2Timer().ToString("00");
                }

                // Phase2 has NO time limit
                else {

                    // Hide match timer text
                    _MatchTimer.gameObject.SetActive(false);
                }
            }
        }
    }

    //--------------------------------------------------------------
    // *** HUD ***

    public void ShowHUD(bool display) {

        // Show or hide the hud
        _DisplayHUD = display;
    }
}
﻿using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 4.10.2017
    ///--------------------------------------///

    //----------------------------------------------------------------------------------
    // *** VARIABLES ***

    /// Public (designers) 
    [Header("---------------------------------------------------------------------------")]
    [Header("*** BUTTON SFX")]
    [Header("")]
    public AudioSource _SFX_ButtonClick;
    public AudioSource _SFX_ButtonHover;
    public AudioSource _SFX_ButtonGoBack;

    [Header("---------------------------------------------------------------------------")]
    [Header("*** DASH SFX")]
    [Header("")]
    public List<AudioSource> _SFX_Dash;

    [Header("---------------------------------------------------------------------------")]
    [Header("*** ORB FIREBALL SFX")]
    [Header("")]
    public List<AudioSource> _SFX_FireballAttack;
    public List<AudioSource> _SFX_FireballImpact;

    [Header("---------------------------------------------------------------------------")]
    [Header("*** FLAMETHROWER SFX")]
    [Header("")]
    public List<AudioSource> _SFX_FlamethrowerAttack;

    [Header("---------------------------------------------------------------------------")]
    [Header("*** DEVICE SFX")]
    [Header("")]
    public List<AudioSource> _SFX_OnTeleport;
    [Header("")]
    public List<AudioSource> _SFX_OnTagPickupMinion;
    public List<AudioSource> _SFX_OnTagPickupSpeed;
    public List<AudioSource> _SFX_OnTagPickupHealth;

    [Header("---------------------------------------------------------------------------")]
    [Header("*** CRYSTAL SFX")]
    [Header("")]
    public List<AudioSource> _SFX_CrystalHit;
    public List<AudioSource> _SFX_CrystalDeath;

    [Header("---------------------------------------------------------------------------")]
    [Header("*** MUSIC & AMBIENCE")]
    [Header("")]
    public AudioSource _MUSIC_MainMenu;
    public AudioSource _MUSIC_Gameplay;
    [Header("")]
    public AudioSource _AMBIENCE_MainMenu;
    public AudioSource _AMBIENCE_Gameplay;

    [Header("---------------------------------------------------------------------------")]
    [Header("*** CHARACTER DIALOG")]
    [Header("")]
    public List<Dialog> _VOX_Dialoglist;

    [Header("---------------------------------------------------------------------------")]
    [Header("*** FACE TREE DIALOG")]
    [Header("")]
    public List<AudioSource> _VOX_FaceTreeNorthDialoglist;
    public List<AudioSource> _VOX_FaceTreeSouthDialoglist;

    /// Public (internal)
    [HideInInspector]
    public static SoundManager _pInstance;                          // This is a singleton script, Initialized in Startup().

    /// Private 
    private bool _IsPlayingVoxel = false;
    private List<AudioSource> _VoxelWaitingList;
    private float _TimeSinceLastVoxel = 0f;
    private List<bool> _DialogsUse;
    private bool _FaceTreeSoundIsPlaying = false;

    //--------------------------------------------------------------
    // *** CONSTRUCTORS ***

    public void Awake() {

        // if the singleton hasn't been initialized yet
        if (_pInstance != null && _pInstance != this) {

            Destroy(this.gameObject);
            return;
        }

        // Set singleton
        _pInstance = this;
    }

    public void Start() {

        _VoxelWaitingList = new List<AudioSource>();
        _DialogsUse = new List<bool>();

        for (int i = 0; i < _VOX_Dialoglist.Count; i++) {

            // Dialog isnt used by default
            _DialogsUse.Add(false);
        }
    }

    //--------------------------------------------------------------
    // *** FRAME ***

    public void Update() {

        // If there are voxel sounds waiting to be played
        if (_VoxelWaitingList.Count > 1) {

            if (_IsPlayingVoxel == true) {

                // Find the voxel sound that is current playing
                AudioSource vox = null;
                foreach (var sound in _VoxelWaitingList) {

                    // If a sound from the voxel list is playing
                    if (sound.isPlaying == true) {

                        // Then a voxel is playing
                        vox = sound;
                        break;
                    }
                }

                _IsPlayingVoxel = vox != null;
            }

            // A vox has finished playing
            else { /// _IsPlayingVoxel == false

                // Get the last voxel that was playing (should be at the front of the list) & remove it from the queue
                _VoxelWaitingList.RemoveAt(0);

                // If there are still voxels in the queue
                if (_VoxelWaitingList.Count > 0) {

                    // Play the next vox sound in the queue
                    _VoxelWaitingList[0].Play();
                    _IsPlayingVoxel = true;
                    _TimeSinceLastVoxel = 0f;
                }
            }
        }

        // No more voxels are left in the playing queue
        else { /// _VoxelWaitingList.Count < 1

            // Add to timer
            _TimeSinceLastVoxel += Time.deltaTime;
        }
    }

    //--------------------------------------------------------------
    // *** SOUNDS ***

    public int RandomSoundInt(List<AudioSource> SoundList) {

        // Returns a random integer between 0 & the size of the audio source list
        int i = Random.Range(0, SoundList.Count);
        return i;
    }

    public Dialog GetDialog() {

        int i = 0;
        foreach (var dialog in _VOX_Dialoglist) {

            if (_DialogsUse[i] == false) {

                _DialogsUse[i] = true;
                return dialog;
            }
            ++i;
        }
        return null;
    }

    public List<AudioSource> GetVoxelWaitingList() {

        return _VoxelWaitingList;
    }

    public void StartingPlayingVoxels() {

        _IsPlayingVoxel = true;
    }

    /// -------------------------------------------
    /// 
    ///     BUTTON SFX 
    /// 
    /// -------------------------------------------

    public void PlayButtonClick() {

        // Precautions
        if (_SFX_ButtonClick != null)
            _SFX_ButtonClick.Play();
    }

    public void PlayButtonHover() {

        // Precautions
        if (_SFX_ButtonHover != null)
            _SFX_ButtonHover.Play();
    }

    public void PlayButtonGoBack() {

        // Precautions
        if (_SFX_ButtonGoBack != null)
            _SFX_ButtonGoBack.Play();
    }

    /// -------------------------------------------
    /// 
    ///     DASH SFX 
    /// 
    /// -------------------------------------------

    public void PlayDash() {

        // Precautions
        if (_SFX_Dash.Count > 0) {

            // Get random sound from list
            int i = RandomSoundInt(_SFX_Dash);
            AudioSource sound = _SFX_Dash[i];

            // Play the sound
            sound.Play();
        }
    }

    /// -------------------------------------------
    /// 
    ///     ORB FIREBALL SFX 
    /// 
    /// -------------------------------------------

    public void PlayFireballAttack() {

        // Precautions
        if (_SFX_FireballAttack.Count > 0) {

            // Get random sound from list
            int i = RandomSoundInt(_SFX_FireballAttack);
            AudioSource sound = _SFX_FireballAttack[i];

            // Play the sound
            sound.Play();
        }
    }

    public void PlayFireballImpact() {

        // Precautions
        if (_SFX_FireballImpact.Count > 0) {

            // Get random sound from list
            int i = RandomSoundInt(_SFX_FireballImpact);
            AudioSource sound = _SFX_FireballImpact[i];

            // Play the sound
            sound.Play();
        }
    }

    /// -------------------------------------------
    /// 
    ///     FLAMETHROWER SFX 
    /// 
    /// -------------------------------------------

    public void PlayFlamethrowerAttack() {

        // Precautions
        if (_SFX_FlamethrowerAttack.Count > 0) {

            // Get random sound from list
            int i = RandomSoundInt(_SFX_FlamethrowerAttack);
            AudioSource sound = _SFX_FlamethrowerAttack[i];

            // Play the sound
            sound.Play();
        }
    }

    /// -------------------------------------------
    /// 
    ///     DEVICE SFX 
    /// 
    /// -------------------------------------------

    public void PlayTeleport() {

        // Precautions
        if (_SFX_OnTeleport.Count > 0) {

            // Get random sound from list
            int i = RandomSoundInt(_SFX_OnTeleport);
            AudioSource sound = _SFX_OnTeleport[i];

            // Play the sound
            sound.Play();
        }
    }

    public void PlayPickupMinion() {

        // Precautions
        if (_SFX_OnTagPickupSpeed.Count > 0) {

            // Get random sound from list
            int i = RandomSoundInt(_SFX_OnTagPickupSpeed);
            AudioSource sound = _SFX_OnTagPickupSpeed[i];

            // Play the sound
            sound.Play();
        }
    }

    public void PlayPickupSpeedBoost() {

        // Precautions
        if (_SFX_OnTagPickupMinion.Count > 0) {

            // Get random sound from list
            int i = RandomSoundInt(_SFX_OnTagPickupMinion);
            AudioSource sound = _SFX_OnTagPickupMinion[i];

            // Play the sound
            sound.Play();
        }
    }

    public void PlayPickupHealthpack() {

        // Precautions
        if (_SFX_OnTagPickupHealth.Count > 0) {

            // Get random sound from list
            int i = RandomSoundInt(_SFX_OnTagPickupHealth);
            AudioSource sound = _SFX_OnTagPickupHealth[i];

            // Play the sound
            sound.Play();
        }
    }

    /// -------------------------------------------
    ///     
    ///     CRYSTAL SFX 
    /// 
    /// -------------------------------------------

    public void PlayCrystalHit() {

        // Precautions
        if (_SFX_CrystalHit.Count > 0) {

            // Get random sound from list
            int i = RandomSoundInt(_SFX_CrystalHit);
            AudioSource sound = _SFX_CrystalHit[i];

            // Play the sound
            sound.Play();
        }
    }

    public void PlayCrystalDeath() {

        // Precautions
        if (_SFX_CrystalDeath.Count > 0) {

            // Get random sound from list
            int i = RandomSoundInt(_SFX_CrystalDeath);
            AudioSource sound = _SFX_CrystalDeath[i];

            // Play the sound
            sound.Play();
        }
    }

    /// -------------------------------------------
    ///     
    ///     MUSIC & AMBIENCE SFX
    /// 
    /// -------------------------------------------

    public void PlayMusicMainMenu() {

        // Precautions
        if (_MUSIC_MainMenu != null)
            _MUSIC_MainMenu.Play();
    }

    public void PlayMusicGameplay() {

        // Precautions
        if (_MUSIC_Gameplay != null)
            _MUSIC_Gameplay.Play();
    }

    public void PlayAmbienceMainMenu() {

        // Precautions
        if (_AMBIENCE_MainMenu != null)
            _AMBIENCE_MainMenu.Play();
    }

    public void PlayAmbienceGameplay() {

        // Precautions
        if (_AMBIENCE_Gameplay != null)
            _AMBIENCE_Gameplay.Play();
    }

    /// -------------------------------------------
    ///     
    ///     CHARACTER DIALOG
    /// 
    /// -------------------------------------------

    public Dialog GetRandomDialog() {

        // Precautions
        if (_VOX_Dialoglist.Count > 0) {

            // Loop until we find a valid dialog
            Dialog dialog = null;
            for (int i = 0; i < _VOX_Dialoglist.Count; ++i) {

                dialog = _VOX_Dialoglist[i];

                // Has it already been used?
                if (_DialogsUse[i] == true) {

                    dialog = null;
                }
            }

            // A dialog reference has been successfully found
            return dialog;
        }

        else { /// _VOX_Dialoglist.Count == 0

            return null;
        }
    }
    
    /// -------------------------------------------
    ///     
    ///     FACE TREE DIALOG
    /// 
    /// -------------------------------------------

    public void SetFaceTreeSoundPlaying(bool value) { _FaceTreeSoundIsPlaying = value; }

    public bool GetFaceTreeSoundIsPlaying() { return _FaceTreeSoundIsPlaying; }

}
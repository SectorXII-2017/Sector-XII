﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wep_Shield : Weapon {

    ///--------------------------------------///
    /// Created by: Daniel Marton
    /// Created on: 4.10.2017
    ///--------------------------------------///

    //---------------------------------------------------------------------------------
    // VARIABLES

    public GameObject _ShieldMinionPrefab;

    private int _MaxMinions = 14;                                   // Cap of how many minions are allowed to make the shield.
    private int _MinionCount = 6;                                   // Amount of minions that composes the weapon.
    private float _MinionSpacing = 1f;                              // Unit of space between each minion.
    private float _OrbitSpeed = 5f;                                // The speed in which the minions rotate around the character that owns this weapon.
    private Quaternion rotation;

    private List<GameObject> _POOL_Minions;

    //--------------------------------------------------------------
    // CONSTRUCTORS

    public override void Start() {
        
        // Set orbit speed
        ///_OrbitSpeed = WeaponManager._pInstance._OrbitSpeed;

        // Set minion cap
        _MaxMinions = WeaponManager._pInstance._MaxSize;

        // Set initial rotation
        rotation = transform.rotation;

        // Create object pool
        _POOL_Minions = new List<GameObject>();
    }

    public override void Init() {

        if (_Owner != null) {

            // Create aura minions based on the defined size
            for (int i = 0; i < _MinionCount; i++) {

                // If the designers do their job
                if (_ShieldMinionPrefab != null) {

                    // Determine the position of the minion in the pool
                    float angle = i * Mathf.PI * 2 / _MinionCount;
                    Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * _MinionSpacing;
                    pos += new Vector3(transform.position.x, transform.position.y, transform.position.z);

                    // Create the minion prefab
                    var minion = Instantiate(_ShieldMinionPrefab, pos, Quaternion.identity, gameObject.transform).GetComponent<Proj_ShieldMinion>();
                    minion.GetComponentInChildren<Projectile>().Init();
                }
            }

            // Hide the templated minion prefab
            _ShieldMinionPrefab.GetComponent<Renderer>().enabled = false;

            // Set initial rotation
            rotation = transform.rotation;
        }
    }

    //--------------------------------------------------------------
    // FRAME

    public override void Update() {

    }

    public override void FixedUpdate() {
                
        if (_Owner != null) {

            /// IF RIGHT TRIGGER IS BEING USED
            if (_Owner.GetRightTriggerInput.y != 0f) {

                // Rotate shield right
                rotation = new Quaternion(rotation.x, transform.rotation.y + _OrbitSpeed * Time.deltaTime, rotation.z, rotation.w);
                ///transform.Rotate(0f, transform.rotation.y + _OrbitSpeed * Time.deltaTime, 0f);
            }

            /// IF LEFT TRIGGER IS BEING USED
            if (_Owner.GetLeftTriggerInput.y != 0f) {

                // Rotate shield left
                rotation = new Quaternion(rotation.x, transform.rotation.y - _OrbitSpeed * Time.deltaTime, rotation.z, rotation.w);
                ///transform.Rotate(0f, transform.rotation.y - _OrbitSpeed * Time.deltaTime, 0f);
            }
        }

        // Update weapons position based of the owning character's position (if VALID)
        if (_Owner != null)
        {
            transform.position = _Owner.transform.position;
        }

        // Maintain rotation
        ///rotation.y = transform.rotation.y;
        transform.rotation = rotation;
    }

    //--------------------------------------------------------------
    // FIRING

    public override void Fire() {

        // DO NOTHING
    }

    IEnumerator RotateObj(GameObject spinner, float timeToRotate, Vector3 direction) {

        float t = 0;

        while (t < timeToRotate) {

            spinner.transform.Rotate(direction * (Time.fixedDeltaTime / timeToRotate));
            t += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        yield break;
    }

    public int GetMaxMinions() {

        return _MaxMinions;
    }

    public int GetMinionCount() {

        return _MinionCount;
    }

}
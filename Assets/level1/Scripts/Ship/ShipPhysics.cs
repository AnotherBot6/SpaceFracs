﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FLFlight
{
    // Applies linear and angular forces to a ship.
    public class ShipPhysics : MonoBehaviour
    {
        [Tooltip("X: Lateral thrust\nY: Vertical thrust\nZ: Longitudinal Thrust")]
        public Vector3 linearForce = new Vector3(100.0f, 100.0f, 100.0f);

        [Tooltip("X: Pitch\nY: Yaw\nZ: Roll")]
        public Vector3 angularForce = new Vector3(100.0f, 100.0f, 100.0f);

        [Range(0.0f, 1.0f)]
        [Tooltip("Multiplier for longitudinal thrust when reverse thrust is requested.")]
        public float reverseMultiplier = 1.0f;

        [Tooltip("Multiplier for all forces. Can be used to keep force numbers smaller and more readable.")]
        public float forceMultiplier = 100.0f;

        [Tooltip("Multiplier for the boost applied to the ship.")]
        public float boostMultiplier = 2.0f;

        private bool isBoosting = false;
        private bool canBoost = true;

        private Vector3 appliedLinearForce = Vector3.zero;
        private Vector3 appliedAngularForce = Vector3.zero;

        // Accessor for the Rigidbody controlling this ship.
        public Rigidbody Rigidbody { get; private set; }

        // Use this for initialization
        void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            if (Rigidbody == null)
            {
                Debug.LogWarning(name + ": ShipPhysics has no rigidbody.");
            }
        }

        void FixedUpdate()
        {
            if (Rigidbody != null)
            {
                if (isBoosting)
                {
                    Rigidbody.AddRelativeForce(appliedLinearForce * forceMultiplier * boostMultiplier, ForceMode.Force);
                }
                else
                {
                    Rigidbody.AddRelativeForce(appliedLinearForce * forceMultiplier, ForceMode.Force);
                }

                Rigidbody.AddRelativeTorque(appliedAngularForce * forceMultiplier, ForceMode.Force);
            }
        }

        /// <summary>
        /// Sets the input for how much of linearForce and angularForce are applied
        /// to the ship. Each component of the input vectors is assumed to be scaled
        /// from -1 to 1, but is not clamped.
        /// </summary>
        public void SetPhysicsInput(Vector3 linearInput, Vector3 angularInput)
        {
            appliedLinearForce = Vector3.Scale(linearForce, linearInput);
            appliedAngularForce = Vector3.Scale(angularForce, angularInput);
            //appliedLinearForce = MultiplyByComponent(linearInput, linearForce);
            //appliedAngularForce = MultiplyByComponent(angularInput, angularForce);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && canBoost)
            {
                isBoosting = true;
                canBoost = false;
                Invoke("ResetCanBoost", 9.0f);
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                isBoosting = false;
            }
        }

        private void ResetCanBoost()
        {
            canBoost = true;
        }
    }
}

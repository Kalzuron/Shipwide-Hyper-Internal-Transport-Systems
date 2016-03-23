using System;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using System.Linq;
using System.Text;
using UnityEngine;
//using Tansport_Beta.Kerbs;


namespace Tansport_Beta
{
    
    public class SHITransport : VesselModule
    {

        
        KerbalEVA Jeb;

        Vessel _vessel;


        public bool isReady;

      [KSPField(guiActive = true, guiName = "ATM", guiFormat = "F2")]
        public double aTm;

        public int layerMask = 0;
        private float lastUpdate = 0.0f;
        private float lastFixedUpdate = 0.0f;
        private float logInterval = 5.0f;
        

        public SHITransport()
        {

            Debug.Log("SHITransport [" + this.GetInstanceID().ToString("X")
                + "][" + Time.time.ToString("0.0000") + "]: Constructor");
        }

        public void isKerbal()
        {
            
           
            if (FlightGlobals.ActiveVessel == Jeb)
                 Debug.Log("got a kerbal " + Jeb.name);
             
        }

         void Awake()
        {
             
             
            Debug.Log("SHITransport [" + this.GetInstanceID().ToString("X")
                + "][" + Time.time.ToString("0.0000") + "]: Awake: " + this.name);
        }

        /*
         * Called next.
         */
        void Start()
        {
           
            Debug.Log("SHITransport [" + this.GetInstanceID().ToString("X")
                + "][" + Time.time.ToString("0.0000") + "]: Start");
            
            Jeb = this.GetComponent<KerbalEVA>();
            _vessel = this.GetComponent<Vessel>();


            if (Jeb != null)
            {
               ConfigNode node = new ConfigNode("MODULE");
                node.AddValue("name", "kerbMenu");
                Jeb.part.AddModule(node);
                Jeb.gameObject.layer = Physics.IgnoreRaycastLayer;
                Debug.LogWarning("Kerbal in EVA....!" + Jeb.name + node);
                            
                isReady = true;


            }
            else
            {
                Debug.LogWarning("Not Kerbal in EVA");
                return;
            }
            if (isReady)
                Debug.Log("He's ready");
        }
        

        /*
         * Called every frame
         */
        void Update()
        {
            
            isKerbal();
            if (this.Jeb != null)
                aTm = this.Jeb.vessel.atmDensity;
          
            if ((Time.time - lastUpdate) > logInterval)
            {
                lastUpdate = Time.time;
                Debug.Log("SHITransport [" + this.GetInstanceID().ToString("X")
                    + "][" + Time.time.ToString("0.0000") + "]: Update" + aTm);
            }
        }

        /*
         * Called at a fixed time interval determined by the physics time step.
         */
        void FixedUpdate()
        {
            if ((Time.time - lastFixedUpdate) > logInterval)
            {
                lastFixedUpdate = Time.time;
                Debug.Log("SHITransport [" + this.GetInstanceID().ToString("X")
                    + "][" + Time.time.ToString("0.0000") + "]: FixedUpdate");
            }
        }

        /*
         * Called when the game is leaving the scene (or exiting). Perform any clean up work here.
         */
        void OnDestroy()
        {
            Debug.Log("SHITransport [" + this.GetInstanceID().ToString("X")
                + "][" + Time.time.ToString("0.0000") + "]: OnDestroy");
        }
    }
}

    
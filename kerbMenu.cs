using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Tansport_Beta.Kerbs
{
    class kerbMenu : PartModule
    {
        private float lastUpdate = 0.0f;
        private float lastFixedUpdate = 0.0f;
        private float logInterval = 5.0f;

        [KSPField(guiActive = true, guiName = "speed", guiFormat = "F1")]
        float i;
        
        KerbalEVA jeb;
        
        Vector3d k;
        
        Vector3d l;
        
        Vector3 m;
        
        Vector3 n;
        
        Vector3 o;
        
        public static int p;

        public KFSMState FSM;
 
        int layerMask;

        public Vessel vesy;

        RaycastHit _raycastHit = new RaycastHit();

        private Rect _windowPosition = new Rect();

        [KSPField(guiActive = true, guiName = "From Ship")]
        public string shiP;
        
        [KSPField(guiActive = true, guiName = "ArG")]
        public bool ArG;

        [KSPField(guiActive = true, guiName = "ATM", guiFormat = "F1")]
        public float atm;

        [KSPField(guiActive = true, guiName = "State")]
        Vessel.Situations mop;

        
        [KSPEvent(guiName = "walk", guiActive = true)]
        public void ArtificialGrav()
        {
            ArG = !ArG;
            Debug.Log("Bark");
            
        }
       
        
   
        public void walkMe()
        {
            jeb.fsm.CurrentState.name = "Idle (Grounded)";
            layerMask = Physics.IgnoreRaycastLayer;
           
            if (Physics.Raycast(this.vessel.evaController.footPivot.transform.position, -(this.vessel.evaController.footPivot.up), out _raycastHit, layerMask))
                Debug.Log("There is something in front of " + this.name + "<>" + this.gameObject.layer + "<>" + _raycastHit.distance + "<>" + _raycastHit.rigidbody.name.ToString());

            this.vessel.situation  = Vessel.Situations.LANDED;
            //this.vessel.Landed = true;
            this.vessel.rigidbody.AddForce(this.vessel.evaController.footPivot.up * -0.0025f, ForceMode.Force);
            this.vessel.rigidbody.transform.up = _raycastHit.normal;
            this.vessel.rigidbody.useGravity = false;
            
            Debug.Log("Walking" + mop);
        }
        public PartModule nom = new PartModule();
        public kerbMenu()
        {
            Debug.Log("kerbMenu [" + this.GetInstanceID().ToString("X")
               + "][" + Time.time.ToString("0.0000") + "]: Constructor");
        }

    
        /*
         * Called after the scene is loaded.
         */
        public override void OnAwake()
        {

            RenderingManager.AddToPostDrawQueue(0, OnDraw);
 
            vesy = FlightGlobals.FindNearestControllableVessel(this.vessel);
            shiP = vesy.vesselName;
            jeb = this.GetComponent<KerbalEVA>();
            FSM = jeb.fsm.CurrentState;
            
           
            Debug.Log("kerbMenu [" + this.GetInstanceID().ToString("X")
                + "][" + Time.time.ToString("0.0000") + "]: OnAwake: " + this.name + "<from>" + vesy.name);
        }

        /*
         * Called when the part is activated/enabled. This usually occurs either when the craft
         * is launched or when the stage containing the part is activated.
         * You can activate your part manually by calling part.force_activate().
         */
        public override void OnActive()
        {
            Debug.Log("kerbMenu [" + this.GetInstanceID().ToString("X")
                + "][" + Time.time.ToString("0.0000") + "]: OnActive");
        }

        /*
         * Called after OnAwake.
         */
        public override void OnStart(PartModule.StartState state)
        {
           
            Debug.Log("kerbMenu [" + this.GetInstanceID().ToString("X")
                + "][" + Time.time.ToString("0.0000") + "]: OnStart: " + state);
        }

        private void OnDraw()
        {
            
                _windowPosition = GUILayout.Window(10, _windowPosition, OnWindow, "Renamer");
        }

        private void OnWindow(int windowId)
        {
            GUILayout.BeginVertical(GUILayout.Width(250f));
            GUILayout.Label("From Ship");
            GUILayout.Box(">" + shiP);
            GUILayout.Label("Layer");
            GUILayout.Box("<>" + p);
            GUILayout.Label("ATM");
            GUILayout.Box(">" + atm);
            GUILayout.Label("Speed");
            GUILayout.Box(">" + i);
            GUILayout.Label("Velocity");
            GUILayout.Box(">" + n.ToString());
            GUILayout.Label("State");
            GUILayout.Box(">" + jeb.fsm.CurrentState.name);
            GUILayout.Box(">" + this.vessel.situation);
            GUILayout.Label("Acceleration");
            GUILayout.Box(">" + k.ToString());
            GUILayout.Label("Centrifugal");
            GUILayout.Box(">" + l.ToString());
            





            GUILayout.EndVertical();

            GUI.DragWindow();
            
        }

        public void KerbStatus()
        {
            Vessel bob = this.vessel;
             i = (float)bob.speed;
            
            k = bob.acceleration;
             l = bob.CentrifugalAcc;
             m = bob.GetFwdVector();
             n = bob.rb_velocity;
             o = bob.upAxis;
             p = bob.gameObject.layer;
          
        }
        /*

         * Called every frame
         */
        public override void OnUpdate()
        {
             atm = (float)this.vessel.atmDensity;

            mop = this.vessel.situation;
            KerbStatus();
             if (ArG)
            {
                walkMe();
                
            }
             Debug.Log("State: " + this.vessel.evaController.fsm.CurrentState.name);
           
            if ((Time.time - lastUpdate) > logInterval)
            {
                lastUpdate = Time.time;
                Debug.Log("kerbMenu [" + this.GetInstanceID().ToString("X")
                    + "][" + Time.time.ToString("0.0000") + "]: OnUpdate" + "<>");
            }
        }

        /*
         * Called at a fixed time interval determined by the physics time step.
         */
        public override void OnFixedUpdate()
        {
           

            if ((Time.time - lastFixedUpdate) > logInterval)
            {
                lastFixedUpdate = Time.time;
                Debug.Log("kerbMenu [" + this.GetInstanceID().ToString("X")
                    + "][" + Time.time.ToString("0.0000") + "]: OnFixedUpdate");
            }
        }

        /*
         * KSP adds the return value to the information box in the VAB/SPH.
         */
        public override string GetInfo()
        {
            Debug.Log("kerbMenu [" + this.GetInstanceID().ToString("X")
                + "][" + Time.time.ToString("0.0000") + "]: GetInfo");
            return "\nContains the TAC Example - Simple Part Module\n";
        }

        /*
         * Called when the part is deactivated. Usually because it was destroyed.
         */
        public override void OnInactive()
        {
            Debug.Log("kerbMenu [" + this.GetInstanceID().ToString("X")
                + "][" + Time.time.ToString("0.0000") + "]: OnInactive");
        }

        /*
         * Called when the game is loading the part information. It comes from: the part's cfg file,
         * the .craft file, the persistence file, or the quicksave file.
         */
        public override void OnLoad(ConfigNode node)
        {
            Debug.Log("kerbMenu [" + this.GetInstanceID().ToString("X")
                + "][" + Time.time.ToString("0.0000") + "]: OnLoad: " + node);
        }

        /*
         * Called when the game is saving the part information.
         */
        public override void OnSave(ConfigNode node)
        {
            Debug.Log("kerbMenu [" + this.GetInstanceID().ToString("X")
                + "][" + Time.time.ToString("0.0000") + "]: OnSave: " + node);
        }
    }
}

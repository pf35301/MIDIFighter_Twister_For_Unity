﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using MidiJack;
using TwisterForUnity.Extensions;

namespace TwisterForUnity {
    [CreateAssetMenu(menuName = "MIDIFighterTwisterForUnity/Twister Parameter Object")]
    public sealed class TwisterParams : ScriptableObject {

        public string TwisterPortName;
        private bool m_Initialized = false;

        public MidiChannel Channel {
            get {
                if (!m_Initialized) {
                    GetInfo();
                    m_Initialized = true;
                }
                return channel;
            }
        }
        public uint Id {
            get {
                if(!m_Initialized) {
                    GetInfo();
                    m_Initialized = false;
                }
                return id;
            }
        }

        private MidiChannel channel;
        private uint id;

        [Header("Gain")]
        [Header("Position")]
        public float MovePositionGain;
        public float MovePositionGainMax;
        public float MovePositionGainMin;

        [Header("Rotation")]
        public float MoveRotationGain;
        public float MoveRotationGainMax;
        public float MoveRotationGainMin;

        //[TODO]
        //public PropertyChangeEvent ChangeTheNumberOfTwisterHandler = new PropertyChangeEvent(); 

        [Header("MIDI Fighter Twister")]
        public byte CCMidiRangeMin;
        public byte CCMidiRangeMax;
        public byte TheNumberOfTwister = 0x0f;
        
        //[TODO]
        /*
        public byte TheNumberOfTwister {
            set {
                ChangeTheNumberOfTwisterHandler.Invoke();
                theNumberOfTwister = value;
            }
            get {
                return theNumberOfTwister;
            }
        }
        */

        public TwisterParams(string TwisterPortName) {
            this.TwisterPortName = TwisterPortName;
        }

        public void GetInfo() {
            channel = MidiJackEx.GetChannel(TwisterPortName);
            id = MidiJackEx.GetId(channel);
        }
    }

    public enum Direction3FTo41 : byte {
        Right = 0x41,
        Left = 0x3f,
        PressDown = 0x7f,
        PressUp = 0x00
    }

    public enum TwisterMidiStatus : byte {
        Press = 0xB1,
        Roll = 0xB0
    }

    public sealed class PropertyChangeEvent : UnityEvent {

    }
}

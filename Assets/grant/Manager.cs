using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace grant
{
    public class Manager : MonoBehaviour
    {
        public AudioClip music;

        private void Start()
        {
            Managers.AudioManager.CreateAudioSource().PlayOneShot(music);
        }
    }
}

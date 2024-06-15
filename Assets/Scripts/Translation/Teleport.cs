using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Keraz.Transition
{
    public class Teleport : MonoBehaviour
    {
        public string sceneNameToGO;
        public Vector3 targetPositionToGo;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                  EventHandle.CallTransitionEvent(sceneNameToGO,targetPositionToGo);
            }
        }
    }
}

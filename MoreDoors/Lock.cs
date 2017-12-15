using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MoreDoors
{
    public class Lock : MonoBehaviour
    {
        private const int MaxAngle = 30;
        private const int Speed = 15;

        private int _state;
        
        /// <summary>
        /// 
        /// </summary>
        private void Start ()
        {
            this._state = (int) Math.Floor(Random.value * 10000); // random angle
        }
	
        /// <summary>
        /// 
        /// </summary>
        private void Update ()
        {
            this._state += 1;
            var angle = (float) Math.Sin(((float) _state) / Speed) * MaxAngle;
            this.transform.localEulerAngles = new Vector3(0, 0, angle);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shouldLock"></param>
        public void SetLocked(bool shouldLock)
        {
            if (shouldLock)
            {

            }
            else
            {
                StartCoroutine(Kill());
            }
        }

        private IEnumerator Kill()
        {
            this.GetComponent<AudioSource>().Play();
            this.GetComponent<Renderer>().enabled = false;
            yield return new WaitForSeconds(1);
            this.gameObject.SetActive(false);
        }
    }
}

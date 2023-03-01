using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGamePlinko
{
    public class Reward : MonoBehaviour
    {
        public int jackpot;
        public int targetAccuracy;
        public int targetTime;

        public bool dropped;

            // Start is called before the first frame update
        void Start()
        {
            dropped = false;
        }

       
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                dropped = true;
            }
        }

        public bool GetDropped()
        {
            return dropped;
        }

        public void SetJackpot(int value)
        {
            jackpot = value;
        }

        public void SetTargetAccuracy(int value)
        {
            targetAccuracy = value;
        }

        public void SetTargetTime(int value)
        {
            targetTime = value;
        }

    }
}

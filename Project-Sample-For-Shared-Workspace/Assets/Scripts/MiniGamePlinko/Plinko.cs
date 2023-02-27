using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MiniGamePlinko
{
    public class Plinko : MonoBehaviour
    {
        //[SerializeField] private int rewardCount; //represents each slot
        [SerializeField] private List<GameObject> rewardZones; //represents each slot

        
        [SerializeField] private List<GameObject> rewardText; //the text showing the rewards

        // public List<int> rewardValues; // List of reward values for each slot
        public GameObject win; //win ui

        public TMP_Text TextReward;
            
        [System.Serializable]
        public class PlinkoReward
        {
            public int jackpot;
            public int targetAccuracy;
            public int targetTime;

            //constructor
            public PlinkoReward(int a, int b, int c)
            {
                jackpot = a;
                targetAccuracy = b;
                targetTime = c;
            }
        }

        //use dto see rewards in the editor
        public List<PlinkoReward> plinkoSlots;

        // Start is called before the first frame update
        private void Start()
        {
            //add the colliders to list
            rewardZones = new List<GameObject>();
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Finish");

            foreach (GameObject obj in objectsWithTag)
            {
                rewardZones.Add(obj);
            }

            // Assign random reward values to each slot/collider present
            plinkoSlots = new List<PlinkoReward>();
            for (int i = 0; i < rewardZones.Count; i++)
            {
                int JackpotIndex = Random.Range(500, 10000);
                int roundedJackpot = Mathf.RoundToInt(JackpotIndex / 500f) * 500;

                //  int accuracyIndex = Random.Range(60, 90);
                //accuracy increase by 5 by about every 1500 jackpot increment
                int accuracyIndex = Mathf.RoundToInt(Mathf.Lerp(60, 90, Mathf.InverseLerp(500, 10000, JackpotIndex)));
                int roundedAccuracy = Mathf.RoundToInt(accuracyIndex / 5f) * 5;

                //int timeIndex = Random.Range(10, 45);
                int timeIndex = Mathf.RoundToInt(Mathf.Lerp(15, 60, Mathf.InverseLerp(500, 10000, JackpotIndex)));
                int roundedTime = Mathf.RoundToInt(timeIndex / 5f) * 5;

                // plinkoSlots.Add(new PlinkoReward(JackpotIndex, accuracyIndex, timeIndex));
                plinkoSlots.Add(new PlinkoReward(roundedJackpot, roundedAccuracy, roundedTime));
                rewardZones[i].GetComponent<Reward>().jackpot = roundedJackpot;
                rewardZones[i].GetComponent<Reward>().targetAccuracy = roundedAccuracy;
                rewardZones[i].GetComponent<Reward>().targetTime = roundedTime;

                rewardText[i].GetComponent<TextMeshProUGUI>().SetText(""+roundedJackpot);
                //rewards.Add(plinkoSlots[index]);
                //plinkoSlots.RemoveAt(index);
            }
        }

        // Update is called once per frame
        private void Update()
        {
            for (int i = 0; i < rewardZones.Count; i++)
            {
                if (rewardZones[i].GetComponent<Reward>().GetDropped() == true)
                {
                    Debug.Log("You won: " + rewardZones[i].GetComponent<Reward>().jackpot + " Accuracy is: " + rewardZones[i].GetComponent<Reward>().targetAccuracy);

                    // win.GetComponent<TMPro.TextMeshProUGUI>().SetText("Your jackpot is: "+ rewardZones[i].GetComponent<Reward>().jackpot); //needs to get child first to get text
                    TextReward.text = "Your Jackpot is: " + rewardZones[i].GetComponent<Reward>().jackpot + "\n Accuracy is: " + rewardZones[i].GetComponent<Reward>().targetAccuracy
                        + "\n Target Time is: " + rewardZones[i].GetComponent<Reward>().targetTime;

                    win.SetActive(true);
                }
            }
        }
    }
}
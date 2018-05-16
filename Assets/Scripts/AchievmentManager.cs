using UnityEngine;
using System.Collections;
using Assets.Core;

public class AchievmentManager : MonoBehaviour
{
    Achievement spikeAchievement = null;
    Achievement timeAchievment = null;

    private void Start()
    {
        spikeAchievement = new SpikeAchievment();
        timeAchievment = new TimeAchievment();
    }

    public void ManageSpikeAchievment()
    {
        if (spikeAchievement != null)
        {
            int count = PlayerPrefs.GetInt(spikeAchievement.GetName());
            Debug.Log("Saving achievement" + count);
            PlayerPrefs.SetInt(spikeAchievement.GetName(), count + 1);
        }
    }

    public void ManageTimeAchievment(int newTime)
    {
        if (timeAchievment != null)
        {
            int bestTime = PlayerPrefs.GetInt(timeAchievment.GetName());
            Debug.Log("Saving achievement" + bestTime);
            if (bestTime == 0 || newTime < bestTime)
            {
                Debug.Log("Saving best time" + newTime);
                PlayerPrefs.SetInt(timeAchievment.GetName(), newTime);
            }
        }
    }
}


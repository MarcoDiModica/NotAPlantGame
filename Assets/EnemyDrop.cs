using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;


public class EnemyDrop : MonoBehaviour
{

    public Drop[] possible_drops;

    public RatingPop poppt;

    public UnityEvent<string[]> drop_event;

    private void Awake()
    {
        poppt = FindAnyObjectByType<RatingPop>();
    }

    public void DropLoot(float total_health )
    {
        float player_score = poppt.total_points;
        float multiplier = player_score / (3 * total_health);
        if(multiplier < 0.5f) { multiplier = 0.5f; }
        List<string> drop_list = new List<string>();



        for(int i = 0; i < 4 * multiplier; ++i)
        {
            float total = 0;
            float value = Random.value;

            for(int j = 0; j < possible_drops.Length; ++j)
            {
                if(value >= total && value < possible_drops[j].chance + total)
                {
                    int current_amount = PlayerPrefs.GetInt(possible_drops[j].name);
                    PlayerPrefs.SetInt(possible_drops[j].name, ++current_amount);
                    drop_list.Add(possible_drops[j].name);
                    break;
                }
                total += possible_drops[j].chance;
            }

        }

        drop_event?.Invoke(drop_list.ToArray());
    }
}

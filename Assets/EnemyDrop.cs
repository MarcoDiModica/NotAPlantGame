using UnityEngine;

public class EnemyDrop : MonoBehaviour
{

    public Drop[] possible_drops;
    private string[] received_items;

    public RatingPop poppt;

    public void DropLoot(float total_health )
    {
        float player_score = poppt.total_points;
        float multiplier = player_score / (5 * total_health);

        for(int i = 0; i < possible_drops.Length; ++i)
        {
            if (possible_drops[i].chance * multiplier <= Random.value)
            {
                int current_amount = PlayerPrefs.GetInt(possible_drops[i].name);
                PlayerPrefs.SetInt(possible_drops[i].name, ++current_amount);
            }
        }
    }
}

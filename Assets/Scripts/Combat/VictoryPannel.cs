using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VictoryPannel : MonoBehaviour
{
    Image img;
    VerticalLayoutGroup vertical_container;
    public GameObject drop_prefab;
    public Drop[] possible_drops;

  
    void Awake()
    {
        vertical_container = GetComponentInChildren<VerticalLayoutGroup>();


        EnemyDrop enemy_drop = GameObject.FindAnyObjectByType<EnemyDrop>();
        enemy_drop.drop_event.AddListener(ShowDrops);

        gameObject.SetActive(false);

    }

    public void ShowDrops(string[] drops)
    {
        gameObject.SetActive(true);
       

        for (int i = 0; i < drops.Length; ++i)
        {
            var drop = Instantiate(drop_prefab, vertical_container.transform);
            drop.GetComponent<TextMeshProUGUI>().text = drops[i];

        }
    }
}

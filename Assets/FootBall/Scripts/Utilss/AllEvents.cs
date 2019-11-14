using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AllEvents : MonoBehaviour
{
    [SerializeField]
    GameObject reward_ui;
    [SerializeField]
    GameObject[] stars_ui;

    int index_stage;
    [SerializeField]
    Transform player;
    [SerializeField]
    Transform[] stage_spawns;

    void ShowStars()
   {
        for (int i = 0; i < stars_ui.Length; i++)
        {
            stars_ui[i].SetActive(true);
        }
   }

    void HideStars()
    {
        for (int i = 0; i < stars_ui.Length; i++)
        {
            stars_ui[i].SetActive(false);
        }
    }

    public void NextStage()
    {
        reward_ui.SetActive(false);
        HideStars();
        if (index_stage < stage_spawns.Length)
        {
            player.position = new Vector3(stage_spawns[index_stage].position.x, player.position.y, player.position.z);
            index_stage++;
        }
       
    }


    public void ShowRewardUI()
    {     
        reward_ui.SetActive(true);
        ShowStars();
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("Load");
    }
}

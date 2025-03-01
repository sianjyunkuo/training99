using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndpointOnTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == _player)
        {
            Debug.Log("Player found the endpoint!");
            
            if (SceneManager.GetActiveScene().name == "Level2")
            {
                
                // Debug.Log("level2 finished time: " + Time.timeSinceLevelLoad.ToString("0.0"));
                // float levelTimeScore =  Time.timeSinceLevelLoad;
                DataManager dm = gameObject.GetComponent<DataManager>();
                dm.Send("Level2", Time.timeSinceLevelLoad.ToString("0.0"));
                Destroy(dm);
                //SceneManager.LoadScene("LV_GameOver");
            }
            else if (SceneManager.GetActiveScene().name == "Level1")
            {
                // Debug.Log("level1 finished time: " + Time.timeSinceLevelLoad.ToString("0.0"));
                DataManager dm = gameObject.GetComponent<DataManager>();
                dm.Send("Level1", Time.timeSinceLevelLoad.ToString("0.0"));
                Destroy(dm);
                //SceneManager.LoadScene("Level2");
            }
            else
            {
                // TODO
            }

            SceneManager.LoadScene("LevelCleared");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartScreen : MonoBehaviour
{
    public Button restart;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = restart.GetComponent<Button>();

        restart.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void TaskOnClick()
    {
        SceneManager.LoadScene("SampleScene");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _pauseText;
    // Start is called before the first frame update
    void Start()
    {
        _pauseText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                _pauseText.gameObject.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                _pauseText.gameObject.SetActive(false);
            }
        }
    }
}

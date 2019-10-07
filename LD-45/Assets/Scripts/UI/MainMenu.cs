using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject panelPrep;

	// Use this for initialization
	void Start () {
        SoundManager._instance.PlayMusic(SoundType.Home);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Tuto()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Tutorial");
    }

    public void PlayGame()
    {
        this.gameObject.SetActive(false);
        panelPrep.SetActive(true);
        SoundManager._instance.PlayMusic(SoundType.Prep);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

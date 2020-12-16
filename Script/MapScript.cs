using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapScript : MonoBehaviour
{
    // Start is called before the first frame update
    private string map;
    private void Start()
    {
    }
    public void chooseMapOnClick ()
    {
        var button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        map = button.name.Substring(button.name.Length - 1, 1);
        PlayerPrefs.SetInt("map",int.Parse(map));
        SceneManager.LoadScene("2rd");
    }
    
}

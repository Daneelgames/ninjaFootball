using UnityEngine;
using System.Collections;

public class Statefull : MonoBehaviour {
    
    private SaveManager saveManager;

    void Start ()
    {
        saveManager = GameObject.Find("SceneManager").GetComponent<SaveManager>();

        SetGameObjectToDictionary();
    }

    void  SetGameObjectToDictionary()
    {
        saveManager.objectsOnScene.Add(gameObject, transform.position);
    }

    void OnDestroy()
    {
        saveManager.objectsOnScene.Remove(gameObject);
    }
}

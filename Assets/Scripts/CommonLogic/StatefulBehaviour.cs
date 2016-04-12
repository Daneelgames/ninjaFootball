using UnityEngine;
using System.Collections;

public class StatefulBehaviour : MonoBehaviour {
    
    public string path;
    bool processStates = true;
    
    void Start() {
        if (StateManager.IsObjectDead(this)){
            GameObject.Destroy(gameObject);
        } else if (processStates) {
            StateManager.StartObject(this);
        }
    }
    
    void OnApplicationQuit(){
        Debug.Log("OnApplicationQuit");
        processStates = false;
        
    }

	
	// Update is called once per frame
	void OnDestroy(){
        if (processStates){
            StateManager.DestroyObject(this);
        }
    }
}

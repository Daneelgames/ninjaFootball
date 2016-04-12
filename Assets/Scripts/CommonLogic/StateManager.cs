using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class StateManager : MonoBehaviour {
    
    static HashSet<string> liveObjects;
    static HashSet<string> deadObjects;
    
    private static StateManager _instance;
    
    void Awake(){
        
        if (_instance == null){
            //PlayerPrefs.DeleteAll();
            _instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        } else if (_instance != this){
            GameObject.Destroy(gameObject);
            return;
        }
        
        LoadState();
        
    }


    public static void StartObject(StatefulBehaviour obj){
        Debug.Log("Start Object " + obj.path, obj);
        liveObjects.Add(obj.path);
        deadObjects.Remove(obj.path);
        PlayerPrefs.SetString(obj.path + ".pos", Serialize(obj.transform.position));
        SaveState(); 
    }
    
    public static void DestroyObject(StatefulBehaviour obj){
        Debug.Log("Destroy Object " + obj.path + ": " + obj);
        liveObjects.Remove(obj.path);
        deadObjects.Add(obj.path);
        SaveState();
    }
    
    static void SaveState(){
        PlayerPrefs.SetString("liveObjects", string.Join(":", liveObjects.ToArray()));
        PlayerPrefs.SetString("deadObjects", string.Join(":", deadObjects.ToArray()));
        Debug.Log("Save state for liveObjects: " + PlayerPrefs.GetString("liveObjects"));
        PlayerPrefs.Save();
    }
    
    static void LoadState(){
        liveObjects = new HashSet<string>();
        if (PlayerPrefs.HasKey("liveObjects")){
            Debug.Log("Loading states for: " + PlayerPrefs.GetString("liveObjects"));
            foreach (var path in PlayerPrefs.GetString("liveObjects").Split(":".ToCharArray())){
                liveObjects.Add(path);
                Debug.Log("Instantiate '" + path + "'");
                var go = GameObject.Instantiate(Resources.Load<GameObject>(path));
                go.transform.position = DeserializeVector3(PlayerPrefs.GetString(path + ".pos"));
            }
        }
        deadObjects = new HashSet<string>();
        if (PlayerPrefs.HasKey("deadObjects")){
            foreach (var path in PlayerPrefs.GetString("deadObjects").Split(":".ToCharArray())){
                deadObjects.Add(path);
            }
        }
    }
    
    public static bool IsObjectDead(StatefulBehaviour obj){
        return deadObjects.Contains(obj.path);
    }
    
    static string Serialize(Vector3 vec){
        return vec.x.ToString("F2") + ":" + vec.y.ToString("F2") + ":" + vec.z.ToString("F2");
    }
    static Vector3 DeserializeVector3(string value){
        var values = value.Split(":".ToCharArray());
        return new Vector3(float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2]));
    }
}

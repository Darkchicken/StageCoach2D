using UnityEngine;
using System.Collections;

public class CleanUpScript : MonoBehaviour {

    public float destroyTime;
	// Use this for initialization
	void Start ()
    {
        Invoke("CleanUp", destroyTime);
	}
	
	void CleanUp()
    {
        Destroy(this.gameObject);
    }
}

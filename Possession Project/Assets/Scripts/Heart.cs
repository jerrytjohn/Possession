using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour 
{
    [HideInInspector]
    public GameObject myself;
    public Transform followJoint;
    public AnimationCurve growthRate;
    public Vector2 heartSizeRange;
    public bool is_Searching;

    const float search_Time_Total = 1.5f;
    private float search_Progress_Time = 0f;

	// Use this for initialization
	void Start () 
    {
        is_Searching = false;
        search_Progress_Time = 0f;

        if(myself == null)
        {
            myself = transform.parent.gameObject;
        }
		
	}

    void Search()
    {
        if(is_Searching)
        {
            search_Progress_Time += Time.fixedDeltaTime;

            float progress_Fraction = search_Progress_Time/search_Time_Total;

            transform.localScale = Vector3.one * HeartSize(progress_Fraction);

            if(search_Progress_Time > search_Time_Total)
            {
                is_Searching = false;
                search_Progress_Time = 0f;
                transform.localScale = Vector3.one * HeartSize(0f);

            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Blobian")
        {
            Debug.Log(string.Format("Possess {0}!",other.gameObject.name));
        }
    }

    void Possess( GameObject other )
    {
        
    }

	float HeartSize(float progress)
    {
        float ans = heartSizeRange.x + (heartSizeRange.y * growthRate.Evaluate(progress));
        return ans;
    }
	// Update is called once per frame
	void Update () 
    {
        UpdatePosition();
        Search();
	}

    void UpdatePosition()
    {
        if (followJoint != null)
        {
            transform.position = followJoint.position;
        }

        else
        {
            Debug.Log("Please assign a joint on the character skeleton of " + myself.name + " for the heart to follow");
        } 
    }
}

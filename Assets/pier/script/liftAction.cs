using UnityEngine;
using System.Collections;

public class liftAction : MonoBehaviour, IAction
{
    public Animator anim;
  public  float timer = 0f;
    public float speedMultiplier = 0;
	// Use this for initialization
	void Start () {
        anim.SetFloat("speed", 0);
       // anim.StartPlayback();
	}
	
	// Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * speedMultiplier;
        if (timer >= 1 || timer <= 0)
        {
           
            speedMultiplier = 0;
            anim.SetFloat("speed", speedMultiplier);
        }
    
    }

    public bool OnStartAction()
    {
        
       // anim.playbackTime = 0;
        speedMultiplier = 1;
        anim.SetFloat("speed", speedMultiplier);
        return true;
    }



    public bool OnUpdateAction()
    {
        anim.playbackTime = 0;
        return true;
    }

    public bool OnExitAction()
    {
        //anim.playbackTime = 1;
        speedMultiplier = -1;
        anim.SetFloat("speed", speedMultiplier);
        return true;
    }
}

using UnityEngine;
using System.Collections;

public interface IAction
{
     bool OnStartAction();

     bool OnUpdateAction();

     bool OnExitAction();
}
public interface IhasState 
{
    bool getState();
}
public interface ILazerAction
{
   // bool OnStartAction();

    bool OnUpdateAction();

    //bool OnExitAction();
}
public interface Iinteractable
{
    bool OnStartAction();

    bool OnUpdateAction();

    bool OnExitAction();
}
public class interfaces : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

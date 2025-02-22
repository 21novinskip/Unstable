using UnityEngine;

public class ButtonScript : MonoBehaviour
{

    public bool IsDupe = false;
    private GameObject Manager;

    private void Start()
    {
        Manager = GameObject.Find("ButtonManager");
    }

    public void Clicked()
    {
        if (IsDupe)
        {
            Debug.Log("You successfully clicked on a Dupe!");
        }
        else
        {
            Debug.Log("You clicked on a button you should not have!");
        }
    }

}

using UnityEngine;

public class TextBasedAdventure : MonoBehaviour
{
    string[] tileNames = { "Dark Cave", "Mossy Tunnel", "Crystal Room" };

    int playerIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("You are in: " + tileNames[playerIndex]);
    }

    // Update is called once per frame
    void Update()
    {
        int newIndex = playerIndex;
        if(Input.GetKeyDown(KeyCode.D))
        {
            newIndex++;
        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            newIndex--;
        }
        else
        {
            return;
        }

        if (newIndex >= 0 && newIndex < tileNames.Length)
        {
            playerIndex = newIndex;
        }
        else
        {
            Debug.Log("Can't go that way");
        }
        Debug.Log("You are in: " + tileNames[newIndex]);

        
    }
}

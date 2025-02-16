using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class SelectorController : MonoBehaviour
{
    public bool Left = true;
    public int selectedNumber = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Left)
        {
            //if (Input.GetKeyDown(KeyCode.W))
            if(Keyboard.current.wKey.wasPressedThisFrame)
            {
                if (selectedNumber > 0)
                {
                    selectedNumber--;
                    gameObject.transform.Translate(Vector2.up * 1);

                }

            }

            //if (Input.GetKeyDown(KeyCode.S))
            if (Keyboard.current.sKey.wasPressedThisFrame)
            {
                if (selectedNumber < 2) 
                {
                    selectedNumber++;
                    gameObject.transform.Translate(Vector2.down * 1);
                }

            }    

        }
        else
        {
            //if (Input.GetKeyDown(KeyCode.I))
            if (Keyboard.current.iKey.wasPressedThisFrame)
            {
                if (selectedNumber > 0)
                {
                    selectedNumber--;
                    gameObject.transform.Translate(Vector2.up * 1);
                }


            }

            //if (Input.GetKeyDown(KeyCode.K))
            if (Keyboard.current.kKey.wasPressedThisFrame)
            {
                if (selectedNumber < 2)
                {
                    selectedNumber++;
                    gameObject.transform.Translate(Vector2.down * 1);
                }


            }
        }
        
    }
}

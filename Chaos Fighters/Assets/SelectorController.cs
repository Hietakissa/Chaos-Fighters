using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class SelectorController : MonoBehaviour
{
    public bool Left = true;
    public int selectedNumber = 0;

    [SerializeField] DialogController controller;

    void Awake()
    {
        controller.SetSelectedForSide(Left, selectedNumber);
    }

    void Update()
    {
        if (!controller.countingDown) return;

        if (Left)
        {
            //if (Input.GetKeyDown(KeyCode.W))
            if(Keyboard.current.wKey.wasPressedThisFrame)
            {
                selectedNumber--;
                gameObject.transform.Translate(Vector2.up * 1);
                if (selectedNumber < 0)
                {
                    selectedNumber += 3;
                    gameObject.transform.Translate(Vector2.down * 3);
                }

                controller.SetSelectedForSide(Left, selectedNumber);

                //if (selectedNumber > 0)
                //{
                //    selectedNumber--;
                //    gameObject.transform.Translate(Vector2.up * 1);
                //
                //}
            }

            //if (Input.GetKeyDown(KeyCode.S))
            if (Keyboard.current.sKey.wasPressedThisFrame)
            {
                selectedNumber++;
                gameObject.transform.Translate(Vector2.down * 1);
                if (selectedNumber > 2)
                {
                    selectedNumber -= 3;
                    gameObject.transform.Translate(Vector2.up * 3);
                }

                controller.SetSelectedForSide(Left, selectedNumber);

                //if (selectedNumber < 2) 
                //{
                //    selectedNumber++;
                //    gameObject.transform.Translate(Vector2.down * 1);
                //}
            }
        }
        else
        {
            //if (Input.GetKeyDown(KeyCode.I))
            if (Keyboard.current.upArrowKey.wasPressedThisFrame)

            {
                selectedNumber--;
                gameObject.transform.Translate(Vector2.up * 1);
                if (selectedNumber < 0)
                {
                    selectedNumber += 3;
                    gameObject.transform.Translate(Vector2.down * 3);
                }

                controller.SetSelectedForSide(Left, selectedNumber);

                //if (selectedNumber > 0)
                //{
                //    selectedNumber--;
                //    gameObject.transform.Translate(Vector2.up * 1);
                //}
            }

            //if (Input.GetKeyDown(KeyCode.K))
            if (Keyboard.current.downArrowKey.wasPressedThisFrame)

            {
                selectedNumber++;
                gameObject.transform.Translate(Vector2.down * 1);
                if (selectedNumber > 2)
                {
                    selectedNumber -= 3;
                    gameObject.transform.Translate(Vector2.up * 3);
                }

                controller.SetSelectedForSide(Left, selectedNumber);

                //if (selectedNumber < 2)
                //{
                //    selectedNumber++;
                //    gameObject.transform.Translate(Vector2.down * 1);
                //}
            }
        }
    }
}

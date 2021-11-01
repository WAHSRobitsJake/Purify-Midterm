using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotationHandler : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (FigmentInput.GetButton(FigmentInput.FigmentButton.LeftButton))
        {
            //Minus Z
            transform.Rotate(new Vector3(0.0f, 0.0f, -0.5f));
        }
        else if (FigmentInput.GetButton(FigmentInput.FigmentButton.RightButton))
        {
            //Right Z
            transform.Rotate(new Vector3(0.0f, 0.0f, 0.5f));
        }
    }
}

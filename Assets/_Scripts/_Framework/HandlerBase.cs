using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlerBase : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string StringCapitolizeFirstLetter(string text)
    {
        if (text.Length > 1)
            return text[0].ToString().ToUpper() + text.Substring(1);
        else
            return text.ToUpper();
    }
}

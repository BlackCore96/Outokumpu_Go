using System.Collections;
using System.Collections.Generic;
using UnityEngine.Android;
using UnityEngine;

public class StartUp : MonoBehaviour
{
    private void Start()
    {
        if (Application.isEditor)
        {
            Permission.RequestUserPermission(Permission.Camera);
            Permission.RequestUserPermission(Permission.FineLocation);
            Permission.RequestUserPermission(Permission.CoarseLocation);
        }
    }

    private void Update()
    {
        if (Permission.HasUserAuthorizedPermission(Permission.Camera) && Permission.HasUserAuthorizedPermission(Permission.FineLocation) & Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
        
    }
}

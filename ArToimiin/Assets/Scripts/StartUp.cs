using System.Collections;
using System.Collections.Generic;
using UnityEngine.Android;
using UnityEngine;

public class StartUp : MonoBehaviour
{
    private void Start()
    {
        SaveManager.loadSave = true;
        if (!Application.isEditor)
        {
            StartCoroutine("Permissions");
        }
    }

    IEnumerator Permissions()
    {
        Permission.RequestUserPermission(Permission.Camera);
        yield return new WaitUntil(() => Permission.HasUserAuthorizedPermission(Permission.Camera));
        Permission.RequestUserPermission(Permission.FineLocation);
    }

    private void Update()
    {
        if (Permission.HasUserAuthorizedPermission(Permission.Camera) && Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
        
    }
}

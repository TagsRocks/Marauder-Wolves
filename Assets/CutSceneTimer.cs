using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutSceneTimer : MonoBehaviour {

    IEnumerator Start()
    {
        yield return new WaitForSeconds(4f);

        SceneManager.LoadScene(3);

    }
}

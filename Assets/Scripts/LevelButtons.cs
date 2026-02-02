using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButtons : MonoBehaviour
{
    [SerializeField] GameObject levelButtonPrefab;

    private void Awake() {
    
        for(int i = 1; i <SceneManager.sceneCountInBuildSettings; ++i)
        {
            GameObject obj = Instantiate(levelButtonPrefab, transform);
            obj.GetComponent<TextMeshProUGUI>().text = "Level " + i;
            obj.GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene(i));
        } 
    }
}

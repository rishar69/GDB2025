using UnityEngine;

public class HoldStoryData : MonoBehaviour
{
    public static HoldStoryData Instance;

    public StoryData data;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }


}

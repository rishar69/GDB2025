using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStoryData", menuName = "Story/StoryData")]
public class StoryData : ScriptableObject
{
    public List<StoryStep> steps = new List<StoryStep>(); // Semua langkah cerita.
}
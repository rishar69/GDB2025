using System.Collections.Generic;
using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StoryData))]
public class StoryDataEditor : Editor
{
    private bool[] foldouts; // Array untuk melacak apakah elemen sedang diperluas atau diminimalkan.

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        StoryData storyData = (StoryData)target;

        if (storyData.steps == null)
        {
            storyData.steps = new List<StoryStep>();
        }

        // Inisialisasi array foldout jika ukurannya tidak sesuai dengan jumlah langkah
        if (foldouts == null || foldouts.Length != storyData.steps.Count)
        {
            foldouts = new bool[storyData.steps.Count];
        }

        EditorGUILayout.LabelField("Steps", EditorStyles.boldLabel);

        for (int i = 0; i < storyData.steps.Count; i++)
        {
            var step = storyData.steps[i];

            // Foldout untuk setiap elemen
            foldouts[i] = EditorGUILayout.Foldout(foldouts[i], $"Index {i}", true);

            if (foldouts[i])
            {
                EditorGUILayout.BeginVertical("box");

                step.speaker = EditorGUILayout.TextField("Speaker", step.speaker);
                step.text = EditorGUILayout.TextArea(step.text);

                if (step.tags == null)
                    step.tags = new List<string>();

                // Tampilkan daftar tags
                EditorGUILayout.LabelField("Tags");
                for (int j = 0; j < step.tags.Count; j++)
                {
                    EditorGUILayout.BeginHorizontal();
                    step.tags[j] = EditorGUILayout.TextField($"Tag {j}", step.tags[j]);

                    // Tombol Remove Tag
                    if (GUILayout.Button("Remove Tag", GUILayout.Width(100)))
                    {
                        step.tags.RemoveAt(j);
                        j--;
                    }
                    EditorGUILayout.EndHorizontal();
                }

                // Tombol Add Tag
                if (GUILayout.Button("Add Tag"))
                {
                    step.tags.Add("");
                }

                step.hasBranch = EditorGUILayout.Toggle("Has Branch", step.hasBranch);

                if (step.hasBranch)
                {
                    if (step.branches == null)
                        step.branches = new List<StoryBranch>();

                    EditorGUILayout.LabelField("Branches", EditorStyles.boldLabel);
                    for (int j = 0; j < step.branches.Count; j++)
                    {
                        EditorGUILayout.BeginVertical("box");
                        step.branches[j].condition = EditorGUILayout.TextField("Condition", step.branches[j].condition);
                        step.branches[j].nextStepIndex = EditorGUILayout.IntField("Next Step Index", step.branches[j].nextStepIndex);

                        // Tombol Remove Branch
                        if (GUILayout.Button("Remove Branch"))
                        {
                            step.branches.RemoveAt(j);
                            j--;
                        }

                        EditorGUILayout.EndVertical();
                    }

                    // Tombol Add Branch
                    if (GUILayout.Button("Add Branch"))
                    {
                        step.branches.Add(new StoryBranch());
                    }
                }

                step.nextStepIndex = EditorGUILayout.IntField("Next Step Index", step.nextStepIndex);

                // Tombol Remove Step
                if (GUILayout.Button("Remove Step"))
                {
                    storyData.steps.RemoveAt(i);
                    Array.Resize(ref foldouts, storyData.steps.Count);
                    i--;
                }

                EditorGUILayout.EndVertical();
            }
        }

        // Tombol Add Step
        if (GUILayout.Button("Add Step"))
        {
            storyData.steps.Add(new StoryStep());
            Array.Resize(ref foldouts, storyData.steps.Count);
        }

        serializedObject.ApplyModifiedProperties();
        EditorUtility.SetDirty(target);
    }
}

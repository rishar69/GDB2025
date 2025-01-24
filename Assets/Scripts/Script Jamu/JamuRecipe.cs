using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "NewJamuRecipe", menuName = "Recipe/Jamu Recipe")]
public class JamuRecipe : ScriptableObject
{
    public string jamuName; // Nama jamu
    public Sprite icon; // Ikon jamu
    public string benefits; // Khasiat atau manfaat jamu
    public List<string> ingredients; // Daftar bahan-bahan
}

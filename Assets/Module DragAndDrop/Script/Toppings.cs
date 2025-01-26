using UnityEngine;

public class Toppings : MonoBehaviour
{
    public enum ToppingType
    {
        Base,
        Topping,
        Bobba
    }

    [SerializeField] private string toppingName;
    [SerializeField] private ToppingType type;
    //[SerializeField] private Color toppingColor;

    public string Name => toppingName;
    public ToppingType Type => type;
    //public Color Color => toppingColor;
}

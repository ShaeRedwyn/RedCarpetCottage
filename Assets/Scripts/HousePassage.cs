using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HousePassage : MonoBehaviour
{
    public Orientation orientation;
    public Passages passage;

    // referrence la furniture sur laquelle il est attaché
    // référence a passage pour le link a l'autre passage
}
public enum Orientation { Right, Left, Forward, Backward }

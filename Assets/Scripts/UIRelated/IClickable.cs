using System.Text;
using UnityEngine.UI;
using TMPro;

public interface IClickable
{
    Image MyIcon {  get; set; }

    int MyCount { get; }

    TMP_Text MyStackText
    {
        get;
    }
}

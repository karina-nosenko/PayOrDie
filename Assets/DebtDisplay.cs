using TMPro;
using UnityEngine;

public class DebtDisplay : MonoBehaviour
{
    public TMP_Text debtText; // Reference to the TextMeshPro component

    void Start()
    {
        Globals.debt = 100000;
        InvokeRepeating(nameof(UpdateDebt), 1f, 1f); // Call UpdateDebt every 1 second
    }

    void UpdateDebt()
    {
        Globals.debt += 1; // Increase the debt by 1
        debtText.text = "₪" + Globals.debt.ToString("N0"); // Update the text to show the debt
    }
}

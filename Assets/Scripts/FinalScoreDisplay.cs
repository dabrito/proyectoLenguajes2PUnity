using UnityEngine;
// using UnityEngine.UI;
using TMPro;

public class FinalScoreDisplay : MonoBehaviour
{
  public TMP_Text finalScoreText;
  // public Text finalScoreText;  // Arrastrar Text UI desde el Inspector

  void Start()
  {
    // Accede a la instancia estática y su variable 'score'
    finalScoreText.text = "Final Score: " + GameManager.instance.score;
  }
}

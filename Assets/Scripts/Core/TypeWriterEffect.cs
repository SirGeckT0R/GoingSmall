using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class TypeWriterEffect : MonoBehaviour
{

    public static IEnumerator TypeWithDelay(Queue<FileReader.GameText> gameTexts, TextMeshProUGUI textElement, float typeDelay, float waitDelay = 2f)
    {
        while (gameTexts.Count > 0)
        {
            yield return new WaitForSeconds(waitDelay);
            string fullText = gameTexts.Dequeue().text;
            string currentText = "";
            for (int i = 0; i < fullText.Length; i++)
            {
                if (fullText[i] == '$')
                {
                    currentText += '\n';
                    yield return new WaitForSeconds(2f);
                    continue;
                }
                currentText += fullText[i];
                yield return new WaitForSeconds(typeDelay);
                textElement.text = currentText;
            }
        }
    }
}

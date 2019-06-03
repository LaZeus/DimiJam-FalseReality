using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IntroDude : MonoBehaviour
{
    private PlayerDetection playerDetection;
    private bool explained = false;

    [TextArea]
    [SerializeField]
    private List<string> dialogueText;

    [Space]

    [SerializeField]
    private Animator textAnim;

    [SerializeField]
    private GameObject door;

    // Start is called before the first frame update
    void Start()
    {
        playerDetection = GetComponent<PlayerDetection>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDetection.IsClose && !explained)
            if (Input.GetKeyDown(KeyCode.E))
                StartExplaning();
    }

    private void StartExplaning()
    {
        explained = true;
        playerDetection.DestroyIndicator();
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        textAnim.SetTrigger("Show");

        yield return new WaitForSeconds(1.5f);
        // start typing

        int index = 0;
        TextMeshProUGUI textPanel = textAnim.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();

        // sentences
        while (index < dialogueText.Count)
        {
            textPanel.text = "";
            foreach (char letter in dialogueText[index])
            {
                textPanel.text += letter;
                yield return new WaitForSeconds(0.05f);
            }

            yield return new WaitForSeconds(1f);
            index++;
        }

        textAnim.SetTrigger("Hide");
        Destroy(door);

        yield return new WaitForSeconds(1.5f);
        Destroy(textAnim.gameObject);

        yield return new WaitForSeconds(10f);

        Destroy(gameObject);
    }
}

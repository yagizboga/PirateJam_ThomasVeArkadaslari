using System.IO;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DialogueSystem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dialogue;
    [SerializeField] TextMeshProUGUI answer1text;
    [SerializeField] TextMeshProUGUI answer2text;
    [SerializeField] TextAsset dialoguefile;
    int currentdialogueindex=0;
    DialogueCollection dialogueCollection;
    public bool isondialogue = false;
    public bool dialogue_ended = false;


    void Start(){
        LoadDialogue(dialoguefile);
        ShowDialogue(currentdialogueindex);
    }

    void LoadDialogue(TextAsset xmlFile){
        if(xmlFile!=null){
            XmlSerializer serializer = new XmlSerializer(typeof(DialogueCollection));
            using(StringReader reader = new StringReader(xmlFile.text)){
                DialogueCollection LoadedDialogue = serializer.Deserialize(reader) as DialogueCollection;

                if(LoadedDialogue!=null){
                    dialogueCollection = LoadedDialogue;
                }
            }
        }
    }

    void ShowDialogue(int index){
        if(dialogueCollection != null && index>=0 && index <dialogueCollection.dialogues.Length){
            DialogueData currentDialogue = dialogueCollection.dialogues[index];
            dialogue.text = currentDialogue.question;
            answer1text.text = currentDialogue.answer1;
            answer2text.text = currentDialogue.answer2;
            isondialogue = true;
            dialogue_ended = false;
        }
        if(index < 0){
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
            gameObject.SetActive(false);
            isondialogue = false;
            dialogue_ended = true;
        }
    }

    public void OnAnswer1Selected(){
        Debug.Log("button 1 clicked");
        if(dialogueCollection !=null && currentdialogueindex >= 0){
            int nextindex = dialogueCollection.dialogues[currentdialogueindex].nextdialogueindex1;
            currentdialogueindex = nextindex;
            ShowDialogue(currentdialogueindex);
        }
    }

    public void OnAnswer2Selected(){
        Debug.Log("button 2 clicked");
        if(dialogueCollection != null && currentdialogueindex >= 0){
            int nextindex = dialogueCollection.dialogues[currentdialogueindex].nextdialogueindex2;
            currentdialogueindex = nextindex;
            ShowDialogue(currentdialogueindex);
        }
    }



}

using System.IO;
using System.Xml.Serialization;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;


public class DialogueSystem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dialogue;
    [SerializeField] TextMeshProUGUI answer1text;
    [SerializeField] TextMeshProUGUI answer2text;
    [SerializeField] TextAsset dialoguefile;
    int currentdialogueindex=0;


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
                    DialogueData firstdialogue = LoadedDialogue.dialogues[0];
                    dialogue.text = firstdialogue.question;
                    answer1text.text = firstdialogue.answer1;
                    answer2text.text = firstdialogue.answer2;
                }
            }
        }
    }

    void ShowDialogue(int index){
        
    }



}

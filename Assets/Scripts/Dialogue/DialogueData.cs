using UnityEngine;
using UnityEditor.Search;
using System.Xml.Serialization;

[XmlRoot("dialogues")]
public class DialogueCollection
{
    [XmlElement("dialogue")]
    public DialogueData[] dialogues;
}

public class DialogueData
{
    [XmlElement]
    public string question;
    [XmlElement]
    public string answer1;
    [XmlElement]
    public string answer2;
    [XmlElement]
    public int nextdialogueindex1;
    [XmlElement]
    public int nextdialogueindex2;
}

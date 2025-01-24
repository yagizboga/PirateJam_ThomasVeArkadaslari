using System.Linq;
using UnityEngine;
using UnityEngine.Video;

public class IntroManager : MonoBehaviour
{
    bool firsttvtriggered = false;
    bool secondtvtrigger = false;
    [SerializeField] VideoPlayer firsttvplayer;
    [SerializeField] VideoPlayer[] secondtvplayer;

    public void triggerfirsttv(){
        firsttvtriggered = true;
    }

    public void triggersecondtv(){
        secondtvtrigger = true;
    }

    void Update(){
        if(firsttvtriggered && !firsttvplayer.isPlaying){
            firsttvplayer.Play();
        }
        if(secondtvtrigger && !secondtvplayer[0].isPlaying && !firsttvplayer.isPlaying){
           for(int i = 0;i<secondtvplayer.Length;i++){
            secondtvplayer[i].Play();
           }
        }

    }
}

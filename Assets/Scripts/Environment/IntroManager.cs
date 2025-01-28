using System.Linq;
using UnityEngine;
using UnityEngine.Video;

public class IntroManager : MonoBehaviour
{
    bool firsttvtriggered = false;
    bool secondtvtrigger = false;
    [SerializeField] VideoPlayer[] firsttvplayer;
    [SerializeField] VideoPlayer[] secondtvplayer;
    [SerializeField] VideoClip firstclip;
    [SerializeField] VideoClip secondclip;

    public void triggerfirsttv(){
        firsttvtriggered = true;
    }

    public void triggersecondtv(){
        secondtvtrigger = true;
    }

    void Update(){
        if(firsttvtriggered && !firsttvplayer[0].isPlaying){
            for(int i=0;i<firsttvplayer.Length;i++){
                firsttvplayer[i].clip = firstclip;
                firsttvplayer[i].Play();
            }
        }
        if(secondtvtrigger && !firsttvplayer[0].isPlaying){
            for(int i=0;i<firsttvplayer.Length;i++){
                firsttvplayer[i].clip = secondclip;
                firsttvplayer[i].Play();
            }
        }

    }
}

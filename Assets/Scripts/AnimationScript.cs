using System.Collections;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    [SerializeField]
    private GameObject mainCam;
    private Animator animator;
    private Animation anim;
    void Start()
    {
        animator = GetComponent<Animator>();
        anim = GetComponent<Animation>();
        animator.Play("Vibration");
    }
    private void FixedUpdate()
    {
        //sDebug.Log(animator);
        if (animator.applyRootMotion) //animator penceeresinde o secenek secili oldugundan devamlýi calisacak sasirma yani
            CamChanged(); //sure hala bir secenek ama daha akilci yontemler bulmaya calis.
    }
    private void CamChanged()
    {
        mainCam.SetActive(true);
        this.gameObject.SetActive(false);
    }
}

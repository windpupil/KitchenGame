using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IS_WALKING = "isWalking";//之所以要用一个常量来定义字符串，目的是为了防止多次用到该字符串时打错字母，此时打错了也不会报错；但是定义一个常量在使用时会报错，有助于减少问题
    private Animator anim;
    [SerializeField]private Player player;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool(IS_WALKING, player.IsWalking());
    }
}

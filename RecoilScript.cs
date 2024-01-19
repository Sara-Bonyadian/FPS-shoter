using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class RecoilScript : NetworkBehaviour
{
    public GameObject gun;
    //AudioSource shot;
    public GameObject bullet;
    //GameObject[] bullets = new GameObject[10];

    int bulletIndex = 0;
    public Transform barrelLocation;

    //public AudioClip []  allSounds;

    // Start is called before the first frame update
    void Start()
    {
        //shot = GetComponent<AudioSource>();

        //for (int i = 0; i < 10; i++)
        //{
        //    bullets[i] = Instantiate(bullet);
        //    bullets[i].SetActive(false);
        //}
    }

    void Update()
    {
        if(!IsOwner) return;
        //if (Input.GetMouseButton(0))
        //{
        //    //StartCoroutine(StartRecoil());
        //    //shot.Play();

        //    bullets[bulletIndex].SetActive(true);
        //    bullets[bulletIndex].transform.position = barrelLocation.position;
        //    bullets[bulletIndex].GetComponent<Rigidbody>().velocity = transform.up * 80;

        //    bulletIndex++;


        //    if (bulletIndex >= bullets.Length)
        //    {
        //        bulletIndex = 0;
        //    }
        //}
    }

    //IEnumerator StartRecoil()
    //{
    //    //gun.GetComponent<Animator>().Play("recoileffect");
    //    yield return new WaitForSeconds(0.2f);
    //    gun.GetComponent<Animator>().Play("New State");
    //}
}

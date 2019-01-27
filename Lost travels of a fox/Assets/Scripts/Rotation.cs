using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public GameObject Jamsplosion;


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Explode();
        }
    }

    void Explode()
    {
        GameObject Jampocalypse = Instantiate(Jamsplosion, this.gameObject.transform.position, Quaternion.identity);
        Jampocalypse.GetComponent<ParticleSystem>().Play();
        Debug.Log("TEST");
        Destroy(this.gameObject);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SubstanceManager : MonoBehaviour
{
    public void DisableCollider()
    {
        this.GetComponent<Collider2D>().enabled = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
           // this.GetComponent<Collider2D>().enabled = true;
          ///  this.gameObject.layer = 0;
            /*Tilemap tilemap = this.transform.gameObject.GetComponent<Tilemap>();
            Vector3 vector3 = new Vector3(hit.point.x, hit.point.y);
            var gridPos = tilemap.layoutGrid.WorldToCell(vector3);
            tilemap.GetTile()
            

            float move = gridPos.x + 0.5f;*/
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("le nom est " + collision.gameObject.name);
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("le nom est " + collision.gameObject.name);
            ///this.gameObject.layer = 2;
           // this.GetComponent<Collider2D>().enabled = false;
        }
    }
}

using UnityEngine;
using System.Collections;

public class paintBall : MonoBehaviour {
    public Texture2D targetTexture;
	// Use this for initialization
	void Start () {
	
	}

    void OnCollisionEnter(Collision collision)
    {
        RaycastHit hit;
        Ray ray;

        ContactPoint contact = collision.contacts[0];
        Vector3 dir = contact.point - this.transform.position;
        ray = new Ray(this.transform.position, dir);
        if (!Physics.Raycast(ray, out hit))
            return;

        Renderer rend = hit.transform.GetComponent<Renderer>();
        MeshCollider meshCollider = hit.collider as MeshCollider;
        if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null || meshCollider == null)
            return;

        Texture2D tex = Instantiate(rend.material.mainTexture) as Texture2D;// rend.material.mainTexture as Texture2D;
        Vector2 pixelUV = hit.textureCoord;
        pixelUV.x *= tex.width;
        pixelUV.y *= tex.height;
        // tex.SetPixel((int)pixelUV.x, (int)pixelUV.y, Color.black);
        // tmpTexture = new Texture2D(targetTexture.width, targetTexture.height);

        for (int y = 0; y < targetTexture.height; y++)
        {
            for (int x = 0; x < targetTexture.width; x++)
            {
                //filling the temporary texture with the target texture
                Color target = targetTexture.GetPixel(x, y);
                Color dest = tex.GetPixel((int)pixelUV.x + x - targetTexture.width / 2, (int)pixelUV.y + y - targetTexture.height / 2);
                Color final = dest;
                if (target.a == 1)
                {
                    final = target;
                    //   Debug.Log(target.a);
                }

                tex.SetPixel(x + (int)pixelUV.x - targetTexture.width / 2, y + (int)pixelUV.y - targetTexture.height / 2, final);
            }
        }
        //Apply 
        // tmpTexture.Apply();
        //change the object main texture 
        //renderer.material.mainTexture = tmpTexture;
        // tex.SetPixels((int)pixelUV.x, (int)pixelUV.y, 10, 10, Color.black)
        tex.Apply();
        rend.material.mainTexture = tex;

        Destroy(this.gameObject);
    }
}

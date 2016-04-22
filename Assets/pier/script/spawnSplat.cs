using UnityEngine;
using System.Collections;

public class spawnSplat : MonoBehaviour {
    public Transform splatter;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetButton("Fire1"))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight/2,0));
            if (Physics.Raycast(ray, out hit, 100))
            {
            //    Transform splat = Instantiate(splatter, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal)) as Transform;
              Renderer rend =hit.collider.gameObject.GetComponent<Renderer>();

              if (rend)
              {

                  Texture2D texture;
                  if (rend.material.HasProperty("_Tex_Splat"))
                  { // duplicate the original texture and assign to the material
                      texture = Instantiate(rend.material.GetTexture("_Tex_Splat")) as Texture2D;

                       rend.material.SetTexture("_Tex_Splat", texture);
                      Debug.Log("yay");
                  }
                  else {
                      // duplicate the original texture and assign to the material
                       texture = Instantiate(rend.material.mainTexture) as Texture2D;

                      rend.material.mainTexture = texture;
                  
                  }
               
                  // colors used to tint the first 3 mip levels

                  Color colors = new Color(1, 0, 0, 0);
                 
                  int mipCount = Mathf.Min(3, texture.mipmapCount);

                  // tint each mip level
                  for (var mip = 0; mip < mipCount; ++mip)
                  {
                      var cols = texture.GetPixels(mip);
                      for (var i = 0; i < cols.Length; ++i)
                      {
                          //cols[i] = Color.Lerp(cols[i], colors[mip], 0.33f);
                          cols[i] = colors;
                      }
                      texture.SetPixels(cols, mip);
                  }
                  // actually apply all SetPixels, don't recalculate mip levels
                  texture.Apply(false);
              }
            }

        }
	}
}

using Godot;
using System;

public class VectorDisp : Spatial
{
    private float mag;      // magnitude of vector

    private float sc; // multiplicative scale which converts val to length 

    private MeshInstance stem;     // mesh for the vector stem
    private CylinderMesh stemMesh;  // reference to the vector stem mesh  
    private Vector3 vecOffset;    

    //------------------------------------------------------------------------
    // Called when the node enters the scene tree for the first time.
    //------------------------------------------------------------------------
    public override void _Ready()
    {
        stem = GetNode<MeshInstance>("Stem");
        //stem = new MeshInstance();
        
        //stemMesh = (CylinderMesh)stem.Mesh;
        stemMesh = new CylinderMesh();  
        stemMesh.TopRadius = 0.04f;
        stemMesh.BottomRadius = 0.04f;
        stem.Mesh = stemMesh;

        /*
        SpatialMaterial nsp = new SpatialMaterial();
        nsp.AlbedoColor = new Color(0.0f,1.0f,1.0f);
        GD.Print(stemMesh.SurfaceGetMaterial(0));
        stemMesh.SurfaceSetMaterial(0,nsp);
        */

        vecOffset = new Vector3(0.0f, 0.0f, 0.0f);
        calcVecLen(0.02f);

        sc = 1.0f;
    }

    //------------------------------------------------------------------------
    // setColor
    //------------------------------------------------------------------------
    public void setColor(float r, float g, float b, float a=1.0f)
    {
        SpatialMaterial nsp = new SpatialMaterial();
        nsp.AlbedoColor = new Color(r,g,b,a);
        GD.Print(stemMesh.SurfaceGetMaterial(0));
        stemMesh.SurfaceSetMaterial(0,nsp);
    }

    //------------------------------------------------------------------------
    // calcVecLen:
    //------------------------------------------------------------------------
    private void calcVecLen(float val)
    {
        vecOffset.x = 0.5f * val;
        stem.Translation = vecOffset;
        stemMesh.Height = Mathf.Abs(val);
    }

    //------------------------------------------------------------------------
    // Setter for val, the magnitude of the vector displayed
    //------------------------------------------------------------------------
    public float val
    {
        set{
            mag = sc*value;
            calcVecLen(mag);
        }
    }

    public float scale
    {
        set{
            sc = value;
        }
    }
}
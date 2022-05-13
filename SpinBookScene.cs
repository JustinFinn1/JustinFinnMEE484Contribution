//============================================================================
// SpinBookScene.cs  Main code for the spinning book scene.
//============================================================================
using Godot;
using Sim;
using System;

public class SpinBookScene : Node
{
    private SpinBook b;            // simulation of the spinning book

    // Initial conditions 
    float q0IC;
    float q1IC;
    float q2IC;
    float q3IC;

    // Initial components of angular velocity about body axes
    [Export]
    float omega1_IC = 3.0f;
    [Export]
    float omega2_IC = 0.05f;
    [Export]
    float omega3_IC = 0.0f;

    // Principal moments of inertia about each of the principal axes
    float IG1;
    float IG2;
    float IG3; 

    Quat bQuat;

    private SpinBookModel bmodel;      // reference to the book model
    private SpheriCam sCam;        // reference to the sphericam

    private double[] dataForUI;
    private SpinBookUI ui;
    private float vector1DeltaX = 1.0f; // offset of Omega 1 vector
    private float vector1DeltaY = 0.0f; // offset of Omega 1 vector 
    private float vector2DeltaX = 1.2f; // offset of Omega 2 vector
    private float vector2DeltaY = 0.0f; // offset of Omega 2 vector 
    private float vector3DeltaX = 1.4f; // offset of Omega 3 vector
    private float vector3DeltaY = 0.0f; // offset of Omega 3 vector 
    private VectorDisp om1Vec;  // vector corresponding to Omega 1
    private VectorDisp om2Vec;  // vector corresponding to Omega 2
    private VectorDisp om3Vec;  // vector corresponding to Omega 3
    //------------------------------------------------------------------------
    // _Ready: Called when the node enters the scene tree for the first time.
    //------------------------------------------------------------------------
    public override void _Ready()
    {
        // presumably, we would read these parameters from a file
        q0IC = 1.0f;
        q1IC = 0.0f;
        q2IC = 0.0f;
        q3IC = 0.0f;

        //omega1_IC = 1.0f;
        //omega2_IC = 0.05f;
        //omega3_IC = 0.0f;

        IG1 = 1.0f;
        IG2 = 2.0f;
        IG3 = 3.0f;

        float pivotHeight = 0.8f;


        Vector3 pivotLoc = new Vector3(0.0f,pivotHeight,0.0f);
        float longitudeDeg = 30.0f;   // sphericam angles
        float latitudeDeg = 15.0f;
        
        // Set up the the geometric/kinematic model
        bmodel = GetNode<SpinBookModel>("SpinBookModel");
        bmodel.setCGLoc(pivotLoc);
        bmodel.Translation = pivotLoc;
        //bmodel.Translation = pivotLoc;

        // Set up the SpheriCam
        sCam = GetNode<SpheriCam>("SpheriCam");
        sCam.Translation = pivotLoc;
        sCam.LongitudeDeg = longitudeDeg;
        sCam.LatitudeDeg = latitudeDeg;

        // Set up simulation
        b = new SpinBook();
        b.IG1 = (double)IG1;
        b.IG2 = (double)IG2;
        b.IG3 = (double)IG3;

        b.omega1 = (double)omega1_IC;
        b.omega2 = (double)omega2_IC;
        b.omega3 = (double)omega3_IC;

        dataForUI = new double[3];
        dataForUI[0] = 0.0;
        dataForUI[1] = 1.1;
        dataForUI[2] = 2.2;
    
        ui = GetNode<SpinBookUI>("SpinBookUI");
        ui.connectData(dataForUI);

        bQuat = new Quat();
        bQuat = Quat.Identity;
        bmodel.setOrientation(bQuat);

        // Set up omega1 vector
        om1Vec = GetNode<VectorDisp>("OM1Vec");
        Vector3 vecxLoc = pivotLoc + new Vector3(vector1DeltaX, vector1DeltaY, 
            0.0f);
        om1Vec.Translation = vecxLoc;
        om1Vec.RotationDegrees = new Vector3(0.0f, -90.0f, 0.0f);
        om1Vec.scale = 0.25f;
        om1Vec.setColor(1.0f,1.0f,1.0f,1.0f);

        // Set up omega2 vector
        om2Vec = GetNode<VectorDisp>("OM2Vec");
        Vector3 vecyLoc = pivotLoc + new Vector3(vector2DeltaX, vector2DeltaY, 
            0.0f);
        om2Vec.Translation = vecyLoc;
        om2Vec.RotationDegrees = new Vector3(0.0f, -90.0f, 0.0f);
        om2Vec.scale = 0.25f;
        om2Vec.setColor(1.0f,0.0f,0.0f,1.0f);

        // Set up omega3 vector
        om3Vec = GetNode<VectorDisp>("OM3Vec");
        Vector3 veczLoc = pivotLoc + new Vector3(vector1DeltaX, vector1DeltaY, 
            0.0f);
        om3Vec.Translation = veczLoc;
        om3Vec.RotationDegrees = new Vector3(0.0f, -90.0f, 0.0f);
        om3Vec.scale = 0.25f;
        om3Vec.setColor(0.0f,1.0f,0.0f,1.0f);

    }
    //------------------------------------------------------------------------
    // _PhysicsProcess:
    //------------------------------------------------------------------------
    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);

        b.step((double)delta);

        bQuat.w = (float)b.q0;
        bQuat.x = (float)b.q1;
        bQuat.y = (float)b.q2;
        bQuat.z = (float)b.q3;

        bmodel.setOrientation(bQuat);

        om1Vec.val = (float)b.omega1;
        om2Vec.val = (float)b.omega2;
        om3Vec.val = (float)b.omega3;
        
    }
    //------------------------------------------------------------------------
    // Process:
    //------------------------------------------------------------------------
    public override void _Process(float delta)
    {
        dataForUI[0] = b.omega1;
        dataForUI[1] = b.omega2;
        dataForUI[2] = b.omega3;
        ui.update();
    }
}

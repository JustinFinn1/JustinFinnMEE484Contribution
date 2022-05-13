//----------------------------------------------------------------------------
//SpinBookUI.cs Code for UI of SpinBook Simulation
//----------------------------------------------------------------------------
using Godot;
using System;

public class SpinBookUI : Control
{
    double[] simData;
    Label LabelOmega1;
    Label LabelOmega2;
    Label LabelOmega3;

    //------------------------------------------------------------------------
    // _Ready: Called when the node enters the scene tree for the first time.
    //------------------------------------------------------------------------
    public override void _Ready()
    {
        //GD.Print("Inside SpinBookUI _Ready");
        LabelOmega1 = GetNode<Label>("Panel/VBox/LabelOmega1");
        LabelOmega2 = GetNode<Label>("Panel/VBox/LabelOmega2");
        LabelOmega3 = GetNode<Label>("Panel/VBox/LabelOmega3");
    }
    //------------------------------------------------------------------------
    // Connect Data
    //------------------------------------------------------------------------
    public void connectData(double[] datArray)
    {
        simData = datArray;
    }
    //------------------------------------------------------------------------
    //Update
    //------------------------------------------------------------------------
    public void update()
    {
        //GD.Print(simData[0].ToString() + " " + simData[1].ToString() + " " + 
            //simData [2].ToString());

        LabelOmega1.Text = "Omega1 = " + simData[0].ToString("0.000");
        LabelOmega2.Text = "Omega2 = " + simData[1].ToString("0.000");
        LabelOmega3.Text = "Omega3 = " + simData[2].ToString("0.000");
    }
}

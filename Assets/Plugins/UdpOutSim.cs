//////////////////////////////////////////////////////////////
// Unity Game Udp Out Sim PlugIn For PlayBox 
// QQ: 2642789658
// Version: 20190520
//////////////////////////////////////////////////////////////

//-----------------------------------------------------------
// Using System Package
//-----------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;

//-----------------------------------------------------------
// Using Unity & DLL Package
//-----------------------------------------------------------
using UnityEngine;
using OutSimData;

//-----------------------------------------------------------
// UdpOutSim Class Block
//-----------------------------------------------------------
public class UdpOutSim : MonoBehaviour {

	//-----------------------------------------------------------
	// Public Vars
	//-----------------------------------------------------------
	public string ipString = "192.168.1.101";// Motion box IP is 192.168.15.201? What's App

	//-----------------------------------------------------------
	// Private Vars
	//-----------------------------------------------------------
	// PlayBox Target Machine
	IPEndPoint 	IpEndHost; 		// Host IP End
	Socket  	UdpSocket; 		// Udp Socket

	// Game Rigidbody Component
	Rigidbody rb;

	// Game OutSimData Component
	OutSimDataClass OutSimDataUtils = new OutSimDataClass();


	//-----------------------------------------------------------
	// Use this for initialization
	//-----------------------------------------------------------
	void Start () {

		// Get IP End Host Point
		IpEndHost = new IPEndPoint(IPAddress.Parse(ipString), 10222 );
		
		//IpEndHost = new IPEndPoint(IPAddress.Parse("192.168.1.101"), 10222 );
		//IpEndHost = new IPEndPoint(IPAddress.Parse("192.168.43.98"), 10222 );
		//IpEndHost = new IPEndPoint(IPAddress.Parse("255.255.255.255"), 10222 );
		//IpEndHost = new IPEndPoint(IPAddress.Broadcast, 10222 );

		// Udp Socket 
		UdpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

		// Get RigidBodgy Component
		rb = GetComponent<Rigidbody> ();

		// OutSimData Initialize
		OutSimDataUtils.OutSimData_Init ();

	} // End: void Start () 
		

	//-----------------------------------------------------------
	// Update is called once per frame
	//-----------------------------------------------------------
	void Update () {

		// Get Target Body Velocity & EulerAngles
		OutSimDataUtils.BodyVelocity 	 = rb.velocity;
		OutSimDataUtils.BodyEulerAngles  = transform.eulerAngles;

		Debug.Log(OutSimDataUtils.BodyVelocity); // Raul
		// OutSimData Update
		OutSimDataUtils.OutSimData_Update();


		// Get Udp Send Message
		byte[] UdpSendByte = OutSimDataUtils.GameSimData.GetBytes();

		// Transmit Udp Send Message
		UdpSocket.SendTo(UdpSendByte, UdpSendByte.Length, SocketFlags.None, IpEndHost);

	}
	//===========================================================
		

	/*
	//-----------------------------------------------------------
	// Update is called once per Fix Time
	//-----------------------------------------------------------
	void FixedUpdate () {



	} // End:  void FixedUpdate ()  */
	//==========================================================
		


   //----------------------------------------------------------
   // MonoBehaviour.OnApplicationQuit()
   // Sent to all game objects before the application quits.
   //----------------------------------------------------------
   void OnApplicationQuit()
    {
		
		// Udp Socket Close
		if ( UdpSocket != null )
			UdpSocket.Close();
        
	} // End: OnApplicationQuit()
	//========================================================
    
}

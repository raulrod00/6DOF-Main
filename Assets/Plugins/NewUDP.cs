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
using System.IO;
using System.Net;
using System.Linq;
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
public class NewUDP : MonoBehaviour
{

	// Game Rigidbody Component
	Rigidbody rb;

	// Game OutSimData Component
	OutSimDataClass OutSimDataUtils = new OutSimDataClass();

	// Complete Output
	private byte[] header;
	private byte[] footer;

	// Full Byte Array
	private byte[] fullArray;

	UdpClient udpClient;

	public static byte[] StringToByteArray(string hex)
	{
		return Enumerable.Range(0, hex.Length)
						 .Where(x => x % 2 == 0)
						 .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
						 .ToArray();
	}

	void Awake()
    {
		udpClient = new UdpClient(8410);
		udpClient.EnableBroadcast = true;

		header = StringToByteArray("55aa000013010001");//12345678abcd"
		footer = StringToByteArray("12345678abcd");

		fullArray = new byte[50];

		try
		{
			udpClient.Connect(IPAddress.Parse("192.168.15.201"),7408);

			byte[] dg = StringToByteArray("55aa000013010001ffffffff000000000000000000000000000000000000000000000000000000000000000012345678abcd");
			//00	01	86	a0
			//IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
			try
			{
				//AppendAllBytes("dog.txt", dg);
				udpClient.Send(dg, dg.Length);
			}
			catch (Exception e)
			{
				Debug.Log("Boo boo, cause: " + e.ToString());
			}

		} catch (Exception e)
        {
			Debug.Log(e.ToString());
        }
    }


	void Start()
	{
		// Get RigidBodgy Component
		rb = GetComponent<Rigidbody>();
		// OutSimData Initialize
		OutSimDataUtils.OutSimData_Init();

	} // End: void Start () 

	public static void	 AppendAllBytes(string path, byte[] bytes)
    {
		using (var stream = new FileStream(path, FileMode.Append))
        {
			stream.Write(bytes, 0, bytes.Length);
        }
    }
	//-----------------------------------------------------------
	// Update is called once per frame
	//-----------------------------------------------------------
	void Update()
	{

		// Get Target Body Velocity & EulerAngles
		OutSimDataUtils.BodyVelocity = rb.velocity;
		OutSimDataUtils.BodyEulerAngles = transform.eulerAngles;

		//Debug.Log(OutSimDataUtils.BodyVelocity); // Raul
		//Debug.Log(OutSimDataUtils.BodyEulerAngles); // Raul

		// OutSimData Update
		OutSimDataUtils.OutSimData_Update();



        // Get Udp Send Message
        byte[] dg = OutSimDataUtils.GameSimData.GetBytes();
		dg[0] = 255;
		dg[1] = 255;
		//Debug.Log(dg[0]);
        AppendAllBytes("dog.txt", dg);

		//try
		//      {
		//	udpClient.Send(dg, dg.Length);
		//      } catch (Exception e)
		//      {
		//	Debug.Log("Boo boo, cause: " + e.ToString());
		//      }



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
		//if (UdpSocket != null)
		//	UdpSocket.Close();
		udpClient.Close();

	} // End: OnApplicationQuit()
	  //========================================================

}

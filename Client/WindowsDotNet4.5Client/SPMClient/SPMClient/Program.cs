using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMClient
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}

/*
Client
	CFG to support Proxy's
	Client to server connection - with ID not machine name
	DFES No in client configure
	Deployable / Requires no install 
	Start as scheduled task ? 


 Session
		First Session
	Client reads CFG, HTTP:///SERVERADDRESS, Proxy Address
	Env:Hostname, OS,SPNo, Client ID: IF no id talk to server 
	
	NewClient.php
	//	Warning any modification to this file will be replace with any updates Make sure you back up your changed files
	Take InfoProvidedFromclient,DFES,HOSTNAME,OS,SP,clientVersion
	 IF ClientVerion If Different than Current Version Tell Client to update
	 IF ClientVersion is current All is OK
		Take the INFO, Genereate the ClientID and Store into DB
		Return to the client ClientID 
	
	
		If Client ID is blank, Combine DFES-HOSTNAME-DMY-8CHRSID reply to client
		Create Entry to DB for client
		Return	YourID:DFES-HOSTNAME-DDMMYYYY-8CHARSID
		
		If Client Has ID Data, Perform lookup()
		
		perform-lookup(){    !! Its important to Read all clients in to memory to keep search quick
		Check Does (client.String.ID exists in Client[]){
			Check group and return ID for installed programs that the client should own, 
			If {results are null Return you have no programs assigned 
				!client received this message then logs it and wait for next interval / boot 
			}
			else sweet we have a valid id and an array of program ids the client should own, 
			lets send them to the client with a OTP code for it to call back and download any missing items
			GenerateAndLogOTP against ClientID in DB and date stamp ( for 5hours )
			}
			
		Else return client get lost you don't exist 
			Log this ID and tell the client to bugger off try clearing the ID from your config
			place this client and its ID in a holding place for the admin to move or clear / ignore
		}
		
	


Client - 
 On boot, 
	Wait for network
	
	Client reads CFG, HTTP:///SERVERADDRESS, Proxy Address Env:Hostname, OS,SPNo, Client ID: 
	IF no id talk to server FirstRun()
	If (Client ID is null)
		FirstRun()
	Else Check for update 
		CheckForclientUPDate();
		
//Assume first run
	FirstRun(){ Call server ask for ID
		HTTP://SErver/noID.php?DFES=XXXX?Hostname=hostame?OS?SPno
		Wait for retuen with ID
		Store ID
		If fail to do this log and quit
	If server returns with code, Write it in to the CFG Quit
	}
	
	If we do have an ID
	Call up the server with our ID and request ProgramIDS
	From the Array of IDs returned
		Check though c:\ProgramPatch\Programs\IDs
		
		If we have ID from the server that are missing in the program dir
		Download Missing Programs
			For each missing ID in prog ID
				contactserver?MYID?ProgramID?Installed=Yes
					! Server Checks your assinged this program
						then compresses the program and returns the program and a MD5
				Downloaded program Check MD5
					If complete
						Decompress with ID password
							Run the setup
						If setup fails delete program download
						Tell Server MYID?programID?FailCode!
					If download failed
					Tell Server ?MYID?PROGRAMID?Failed to download
					move on to the next program in the list


		After downloads complete
			Check for pre staging
				c:\ProgramPatch\ProgramsPrestages
				contactserver?MYID?ProgramID?Installed=Prestage
		

	For each ID that does not exsist in Programs\ID
		Run uninstall
		If program uninstall failed
		Tell the server failed to uninstall ?MYID?ProgID
		
		Else 
			Delete the program ID folder
		

			
Program Package
	
	PackAGEID
		Setup.bat 
			Must return OK
		uninstall.bat
			Return OK or failed
		Program Files
	

'********************************************************************************
'NAME         : FTPAddFeeds.vbs
'AUTHOR       : Phil Scott
'DATE CREATED : 12/08/2003
'DESCRIPTION  : VB Script that allows creation of NT user accounts and home directories
'             : from a list in an csv file. Uses standard microsoft techniques
'             : Active Directory Service Interfaces (ADSI) 
'DESIGN DOC   : 
'********************************************************************************
' Version   Ref        Name        Date         Description
' V1.0                 Phil Scott  12/08/2003   Initial version
' 
'********************************************************************************
'$log$

Dim iProcessedUsers
Dim iFailedUsers
Dim strACLCommand            'Command Line string to set ACLs

iProcessedUsers = 0
iFailedUsers = 0

' Use ADO to open the Excel spreadsheet.
Dim oCN
Set oCN = CreateObject("ADODB.Connection")
oCN.Open "ReceptionServerUsers"

'----------------------------------------------------------------
' Go through the configuration file record set one row at a time.
Dim oConfigInfo
Set oConfigInfo = oCN.Execute("SELECT * FROM Feedsconfig.csv")

Dim sPDC
Dim sHomePath
Dim sReception
Dim sIncoming
Dim sProcessing
Dim sHolding
Dim sBackup

Do Until oConfigInfo.EOF

	' Get the user information from this row.
	sPDC = oConfigInfo("MachineId")
	sHomePath   = oConfigInfo("HomeDir")
	sIncoming   = oConfigInfo("IncomingDir")
	sProcessing = oConfigInfo("ProcessingDir")
	sHolding    = oConfigInfo("HoldingDir")
	sBackup     = oConfigInfo("BackupDir")

	On Error Resume Next

	' Move to the next row in the record set.
	oConfigInfo.MoveNext
Loop

' remove comments to display debug info
'	WScript.Echo "PDC           = " & sPDC & vbCrLf & vbCrLf
'	WScript.Echo "HomeDir       = " & sHomePath & vbCrLf & vbCrLf
'	WScript.Echo "IncomingDir   = " & sIncoming & vbCrLf & vbCrLf
'	WScript.Echo "ProcessingDir = " & sProcessing & vbCrLf & vbCrLf
'	WScript.Echo "HoldingDir    = " & sHolding & vbCrLf & vbCrLf
'	WScript.Echo "BackupDir     = " & sBackup & vbCrLf & vbCrLf


'----------------------------------------------------------------------

Dim oRS
' Set oRS = oCN.Execute("SELECT * FROM [Sheet1$]")  ' use this if reception database set as excel
Set oRS = oCN.Execute("SELECT * FROM Feeds.csv")

' Use ADSI to obtain a reference to the NT domain.
Dim oDomain
Set oDomain = GetObject("WinNT://" & sPDC)


' Create an output text file to store users' initial passwords.
Dim oFSO, oTS
Set oFSO = CreateObject("Scripting.FileSystemObject")
Set oTS = oFSO.CreateTextFile("C:\passwords.txt",True)

' For each record in the record set, add the user, set the correct user
' properties, and add the user to the appropriate groups.

' Create the necessary variables.
Dim sUserID, sFullName, sDescription
Dim sGroups
Dim sPassword, oUserAcct, oFolder
Dim sGroupList, iTemp, oGroup
Dim sMsg

'create FTP Server folders
Set oFolder = oFSO.CreateFolder("C:\Gateway")
Set oFolder = oFSO.CreateFolder("C:\Gateway\bin")
Set oFolder = oFSO.CreateFolder("C:\Gateway\scr")
Set oFolder = oFSO.CreateFolder("C:\Gateway\dat")
Set oFolder = oFSO.CreateFolder("C:\Gateway\dat\Reception")

' Go through the record set one
' row at a time.

Do Until oRS.EOF

	' Get the user information from this row.
	sUserID = oRS("UserID")
	sFullName = oRS("FullName")
	sDescription = oRS("Description")
	sGroups = oRS("Groups")

	' Make up a new password.
	sPassword = Left(sUserID,2) _
		& DatePart("n",Time) & DatePart("y",Date) _
		& DatePart("s",Time)

	' Create the user account.
	On Error Resume Next
	Set oUserAcct = oDomain.Create("user",sUserID)
	If Err <> 0 Then
		sMsg = "An error occurred creating user " _
			& sUserID & vbCrLf & vbCrLf
		sMsg = sMsg & "The error is: " _
			& Err.Description
		MsgBox sMsg
	End If
	On Error Goto 0

	' Set account properties.
	oUserAcct.SetPassword sPassword
	oUserAcct.FullName = sFullName
	oUserAcct.Description = sDescription
	oUserAcct.HomeDirectory = sHomePath & sUserID

	' Save the account.
	On Error Resume Next

	oUserAcct.SetInfo
	
	If Err = 0 then
		' Get a reference to the new account.
		' This step provides a valid SID and
		' other information.
		Set oUserAcct = GetObject("WinNT://" _
			& sPDC & "/" & sUserID & ",user")
	
		' Write the password to a file.
		oTS.Write sUserID & "," & sPassword _
			& vbCrLf
	
		' Part 4A: Add the user account to groups.
		' Use the Split function to turn the
		' comma-separated list into an array.
		sGroupList = Split(sGroups, ";")
	
		' Go through the array and add the user
		' to each group.
		For iTemp = 0 To uBound(sGroupList)
	
			' Get the group.
			Set oGroup = GetObject("WinNT://" & _
				sPDC & "/" & sGroupList(iTemp) _
				& ",group")
	
			' Add the user account.
			oGroup.Add oUserAcct.ADsPath
	
			' Release the group.
			Set oGroup = Nothing

		Next

		'  Create the user's home directory. 

		Set oFolder = oFSO.CreateFolder(sHomePath & sUserID)

		'Set Change Permissions on the folder using XCACLS.exe
		' give full access to user but read only to everyone else
		
		Set oShell = WScript.CreateObject ("WSCript.shell")
                strACLCommand = "cmd /c echo y| XCACLS "
		strACLCommand = strACLCommand & sHomePath & sUserID
		strACLCommand = strACLCommand & " /G " & sPDC & "\" & sUserID & ":f"
		oShell.run strACLCommand ,0 ,true
		Set oShell = Nothing

		Set oShell = WScript.CreateObject ("WSCript.shell")
                strACLCommand = "cmd /c echo y| XCACLS "
		strACLCommand = strACLCommand & sHomePath & sUserID
		strACLCommand = strACLCommand & " /E /G Everyone:r"
		oShell.run strACLCommand ,0 ,true
		Set oShell = Nothing

		Set oShell = WScript.CreateObject ("WSCript.shell")
                strACLCommand = "cmd /c echo y| XCACLS "
		strACLCommand = strACLCommand & sHomePath & sUserID
		strACLCommand = strACLCommand & " /E /G TDUser:f"
		oShell.run strACLCommand ,0 ,true
		Set oShell = Nothing



		iProcessedUsers = iProcessedUsers+1

	ElseIf Err = -2147022672 Then
		sMsg = "An error occurred creating user " _
			& sUserID & vbCrLf & vbCrLf
		sMsg = sMsg & "An account for this user already exists "
		MsgBox sMsg
		iFailedUsers = iFailedUsers + 1

	Else
		sMsg = "An error occurred creating user " _
			& sUserID & vbCrLf & vbCrLf
		sMsg = sMsg & "The error is: " &Err _
			& " " & Err.Description
		MsgBox sMsg
		iFailedUsers = iFailedUsers + 1
	End If

	' Release the user account.
	Set oUserAcct = Nothing

	' Move to the next row in the record set.
	oRS.MoveNext


Loop

'Final clean up and close down.
oRS.Close
oTS.Close
oConfigInfo.Close
If iProcessedUsers > 0 then

	WScript.Echo iProcessedUsers & "user(s) have been created."
	WScript.Echo "Passwords have been written " _
		& "to C:\passwords.txt."
End If	

If iFailedUsers > 1 then
	WScript.Echo iFailedUsers & "user(s) could not be created."
End If	








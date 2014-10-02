'********************************************************************************
'NAME         : DataGatewayAddFeeds.vbs
'AUTHOR       : Phil Scott
'DATE CREATED : 13/08/2003
'DESCRIPTION  : VB Script that allows creation of NT  directories
'             : from a list in an csv file. Uses standard microsoft techniques
'             : Active Directory Service Interfaces (ADSI) 
'DESIGN DOC   : 
'********************************************************************************
' Version   Ref        Name        Date         Description
' V1.0                 Phil Scott  13/08/2003   Initial version
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
Set oRS = oCN.Execute("SELECT * FROM Feeds.csv")

' Use ADSI to obtain a reference to the NT domain.
Dim oDomain
Set oDomain = GetObject("WinNT://" & sPDC)


Dim oFSO
Set oFSO = CreateObject("Scripting.FileSystemObject")


' For each record in the record set, add the user, set the correct user
' properties, and add the user to the appropriate groups.

' Create the necessary variables.
Dim sUserID, sFullName, sDescription
Dim sGroups
Dim sPath, oFolder
Dim sGroupList, iTemp, oGroup
Dim sMsg

'create Data Gateway Server folders
Set oFolder = oFSO.CreateFolder("C:\Gateway")
Set oFolder = oFSO.CreateFolder("C:\Gateway\bin")
Set oFolder = oFSO.CreateFolder("C:\Gateway\scr")
Set oFolder = oFSO.CreateFolder("C:\Gateway\dat")
Set oFolder = oFSO.CreateFolder("C:\Gateway\dat\Incoming")
Set oFolder = oFSO.CreateFolder("C:\Gateway\dat\Processing")
Set oFolder = oFSO.CreateFolder("C:\Gateway\dat\Holding")
Set oFolder = oFSO.CreateFolder("C:\Gateway\dat\Backup")
Set oFolder = oFSO.CreateFolder("C:\Gateway\dat\Config")


' Go through the record set one
' row at a time.

Do Until oRS.EOF

	' Get the user information from this row.
	sUserID = oRS("UserID")
	sFullName = oRS("FullName")
	sDescription = oRS("Description")
	sGroups = oRS("Groups")

'-----------------------------------------
'       Create the user's directories. 
	IF sUserID > " " then
	iDirCount = 1
	for iDirCount = 1 to 4
	  Select Case iDirCount
		Case 1
			sPath = sIncoming
		Case 2
			sPath = sProcessing
		Case 3
			sPath = sHolding
		Case 4
			sPath = sBackup
	  End Select
	  Set oFolder = oFSO.CreateFolder(sPath & sUserID)

   	  If Err = 0 then
		iProcessedUsers = iProcessedUsers +1
	  Else
		sMsg = "An error occurred creating folder " _
			& sPath & sUserID & vbCrLf & vbCrLf
		sMsg = sMsg & "The error is: " &Err _
			& " " & Err.Description
		MsgBox sMsg
		iFailedUsers = iFailedUsers + 1
		Err = 0
	  End If

	Next
	End If
	' Move to the next row in the record set.
	oRS.MoveNext


Loop

'Final clean up and close down.
oRS.Close
oConfigInfo.Close
If iProcessedUsers > 0 then

	WScript.Echo iProcessedUsers & "user directories have been created."
End If	

If iFailedUsers > 1 then
	WScript.Echo iFailedUsers & "user directories could not be created."
End If	








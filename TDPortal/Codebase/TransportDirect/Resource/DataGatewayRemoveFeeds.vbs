'********************************************************************************
'NAME         : DataGatewayRemoveFeeds.vbs
'AUTHOR       : Phil Scott
'DATE CREATED : 14/08/2003
'DESCRIPTION  : VB Script that allows deletion of gateway directories
'             : from a list in an csv file. Uses standard microsoft techniques
'             : Active Directory Service Interfaces (ADSI) 
'DESIGN DOC   : 
'********************************************************************************
' Version   Ref        Name        Date         Description
' V1.0                 Phil Scott  14/08/2003   Initial version
'
'********************************************************************************
'$log$
Dim iProcessedUsers
Dim iFailedUsers
iProcessedUsers = 0
iFailedUsers = 0


' Use ADO to open the csv files.
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

'Debug info - remove comments to show onscreen debug info
'	WScript.Echo "PDC           = " & sPDC & vbCrLf & vbCrLf
'	WScript.Echo "HomeDir       = " & sHomePath & vbCrLf & vbCrLf
'	WScript.Echo "IncomingDir   = " & sIncoming & vbCrLf & vbCrLf
'	WScript.Echo "ProcessingDir = " & sProcessing & vbCrLf & vbCrLf
'	WScript.Echo "HoldingDir    = " & sHolding & vbCrLf & vbCrLf
'	WScript.Echo "BackupDir     = " & sBackup & vbCrLf & vbCrLf
'----------------------------------------------------------------------

Dim oRS
Set oRS = oCN.Execute("SELECT * FROM Delete_feeds.csv")
' Use ADSI to obtain a reference to the NT domain.
Dim oDomain
Set oDomain = GetObject("WinNT://" & sPDC)
Dim oFSO
Set oFSO = CreateObject("Scripting.FileSystemObject")


' Create the necessary variables.
Dim sUserID
Dim sMsg

' Go through the record set one row at a time.

Do Until oRS.EOF

	' Get the user information from this row.
	sUserID = oRS("UserID")
'	WScript.Echo "deleting folder " _
'			& sUserID & vbCrLf & vbCrLf

	On Error Resume Next

'       delete the user's directories. 
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

	  oFSO.DeleteFolder(sPath & sUserID)
	  If Err = 0 then
		iProcessedUsers = iProcessedUsers + 1	
	  else
		iFailedUsers = iFailedUsers +1
		sMsg = "An error occurred deleting folder " _
		& sPath & sUserID & vbCrLf & vbCrLf
		sMsg = sMsg & "The error is: " &Err _
		& " " & Err.Description
		MsgBox sMsg
	  End If

	Next
	' Move to the next row in the record set.
	oRS.MoveNext

Loop

' Final clean up and close down.
Set oDomain=Nothing
oRS.Close

If iProcessedUsers > 0 then
	WScript.Echo iProcessedUsers & "directories have been deleted."
End If	

If iFailedUsers > 1 then
	WScript.Echo iFailedUsers & "directories could not be deleted."
End If	






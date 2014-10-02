'********************************************************************************
'NAME         : CreateUsersGateway.vbs
'AUTHOR       : Tushar Karsan
'DATE CREATED : 16-Nov-2003
'DESCRIPTION  : Creates directories on Gateway server.
'********************************************************************************
'$Log

'Create connection.
Dim cnn, rs
Set cnn = CreateObject("ADODB.Connection")
cnn.Open "Driver={Microsoft Text Driver (*.txt; *.csv)};Dbq=C:/Gateway/InitialSetup/;Extensions=asc,csv,tab,txt;Persist Security Info=False"

'Read configuration info.
Dim mid, hom
Set rs = cnn.Execute("SELECT ComputerID, HomeDir FROM CreateUsersConfig.csv")
mid = rs("ComputerID").Value
hom = rs("HomeDir").Value
rs.Close

'Get system info.
Dim fso, wnt, uid
Set fso = CreateObject("Scripting.FileSystemObject")
Set wnt = GetObject("WinNT://" & mid)

'Read feed info ready for processing.
Set rs = cnn.Execute("SELECT * FROM CreateUsersData.csv")
WScript.Echo "Creating directories on " & wnt.ADSPath

'Create root folders.
On Error Resume Next
Set dir = fso.CreateFolder(hom)
Set dir = fso.CreateFolder(hom & "./bin")
Set dir = fso.CreateFolder(hom & "./scr")
Set dir = fso.CreateFolder(hom & "./dat")
Set dir = fso.CreateFolder(hom & "./dat/Incoming")
Set dir = fso.CreateFolder(hom & "./dat/Processing")
Set dir = fso.CreateFolder(hom & "./dat/Holding")
Set dir = fso.CreateFolder(hom & "./dat/Backup")
Set dir = fso.CreateFolder(hom & "./log")
On Error Goto 0

'Create folders for each feed.
Do Until rs.EOF

	uid = rs("UserID")

	On Error Resume Next
	Set dir = fso.CreateFolder(hom & "./dat/Incoming/"   & uid)
	Set dir = fso.CreateFolder(hom & "./dat/Processing/" & uid)
	Set dir = fso.CreateFolder(hom & "./dat/Holding/"    & uid)
	Set dir = fso.CreateFolder(hom & "./dat/Backup/"     & uid)
	On Error Goto 0

	rs.MoveNext

Loop

'Clean up before exit
rs.Close
cnn.Close

Set dir = Nothing
Set cnn = Nothing
Set fso = Nothing

WScript.Echo "Finished creating folders."

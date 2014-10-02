'********************************************************************************
'NAME         : CreateFTPDirs.vbs
'AUTHOR       : Tushar Karsan
'DATE CREATED : 28-Nov-2003
'DESCRIPTION  : Creates directories on FTP server.
'********************************************************************************
'$Log

'Create connection.
Dim cnn, rs
Set cnn = CreateObject("ADODB.Connection")
cnn.Open "Driver={Microsoft Text Driver (*.txt; *.csv)};Dbq=C:/Gateway/bin/Scripts/AD;Extensions=asc,csv,tab,txt;Persist Security Info=False"

'Read configuration info.
Dim mid, hom
Set rs = cnn.Execute("SELECT ComputerID, HomeDir FROM CreateUsersConfig.csv")
mid = rs("ComputerID").Value
hom = rs("HomeDir").Value
rs.Close

'Get system info and create TDPFeed group.
Dim fso, uid
Set fso = CreateObject("Scripting.FileSystemObject")

'Read feed info ready for processing.
Set rs = cnn.Execute("SELECT * FROM CreateUsersData.csv")
WScript.Echo "Creating FTP folders ..."

'Create root folders.
On Error Resume Next
Set dir = fso.CreateFolder(hom)
Set dir = fso.CreateFolder(hom & "./bin")
Set dir = fso.CreateFolder(hom & "./scr")
Set dir = fso.CreateFolder(hom & "./dat")
Set dir = fso.CreateFolder(hom & "./log")
Set dir = fso.CreateFolder(hom & "./dat/Reception")
Set dir = fso.CreateFolder(hom & "./log/AdditionalData")
Set dir = fso.CreateFolder(hom & "./log/NPTG")
Set dir = fso.CreateFolder(hom & "./log/TTBOHost")
On Error Goto 0

'Create users, set their group and home dir for each feed.
Do Until rs.EOF

	uid = rs("UserID").Value
	
	On Error Resume Next
	Set dir = fso.CreateFolder(hom & "./dat/Reception/"  & uid)
	On Error Goto 0

	rs.MoveNext

Loop

'Clean up before exit
rs.Close
cnn.Close

Set uid = Nothing
Set dir = Nothing
Set fso = Nothing
Set cnn = Nothing

WScript.Echo "Folder creation completed."

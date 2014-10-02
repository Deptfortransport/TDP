'********************************************************************************
'NAME         : CreateUsersFTP.vbs
'AUTHOR       : Tushar Karsan
'DATE CREATED : 16-Nov-2003
'DESCRIPTION  : Creates directories and users on FTP server.
'********************************************************************************
'$Log

'Create connection.
Dim cnn, rs
Set cnn = CreateObject("ADODB.Connection")
cnn.Open "Driver={Microsoft Text Driver (*.txt; *.csv)};Dbq=C:/Gateway/bin/Scripts/;Extensions=asc,csv,tab,txt;Persist Security Info=False"

'Read configuration info.
Dim mid, hom
Set rs = cnn.Execute("SELECT ComputerID, HomeDir FROM CreateUsersConfig.csv")
mid = rs("ComputerID").Value
hom = rs("HomeDir").Value
rs.Close

'Get system info and create TDPFeed group.
Dim fso, wnt, gr2, uid, usr
Set fso = CreateObject("Scripting.FileSystemObject")
Set wnt = GetObject("WinNT://" & mid)
Set gr1 = GetObject("WinNT://" & mid & "/Users")
Set gr2 = wnt.Create("Group", "TDPFeed")
gr2.Description = "TDP Data Provider's Group"
gr2.SetInfo

'Read feed info ready for processing.
Set rs = cnn.Execute("SELECT * FROM CreateUsersData.csv")
WScript.Echo "Creating users on " & wnt.ADSPath

'Create root folders.
On Error Resume Next
Set dir = fso.CreateFolder(hom)
Set dir = fso.CreateFolder(hom & "./bin")
Set dir = fso.CreateFolder(hom & "./scr")
Set dir = fso.CreateFolder(hom & "./dat")
Set dir = fso.CreateFolder(hom & "./dat/Reception")
On Error Goto 0

'Create users, set their group and home dir for each feed.
Do Until rs.EOF

	uid = rs("UserID").Value
	Set usr = wnt.Create("User", uid)

	On Error Resume Next
	Set dir = fso.CreateFolder(hom & "./dat/Reception/"  & uid)
	On Error Goto 0

	usr.SetPassword rs("Password").Value
	usr.FullName = rs("FeedName").Value
	usr.Description = "Data Provider for Gateway"
	usr.HomeDirectory = hom & "./dat/Reception/" & uid	
	usr.SetInfo
	
	gr1.Add wnt.ADSPath & "/" & uid
	gr2.Add wnt.ADSPath & "/" & uid
	rs.MoveNext

Loop

'Clean up before exit
rs.Close
cnn.Close

Set dir = Nothing
Set usr = Nothing
Set cnn = Nothing
Set fso = Nothing
Set gr2 = Nothing
Set gr1 = Nothing
Set wnt = Nothing

WScript.Echo "Create users under group 'TDPFeed'."

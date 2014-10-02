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
cnn.Open "Driver={Microsoft Text Driver (*.txt; *.csv)};Dbq=C:/Gateway/bin/Scripts/AD;Extensions=asc,csv,tab,txt;Persist Security Info=False"

'Read configuration info.
Dim mid, hom
Set rs = cnn.Execute("SELECT ComputerID, HomeDir FROM CreateUsersConfig.csv")
mid = rs("ComputerID").Value
hom = rs("HomeDir").Value
rs.Close

'Get system info and create TDPFeed group.
Dim w2k, usr
Set w2k = GetObject("LDAP://DC=DFTSIF,DC=CO,DC=UK")

'Read feed info ready for processing.
Set rs = cnn.Execute("SELECT * FROM CreateUsersData.csv")
WScript.Echo "Creating user accounts..."

'Create users, set their group and home dir for each feed.
Do Until rs.EOF

	Set usr = w2k.Create("user", "CN=" + rs("FeedName").Value)
	usr.Put "sAMAccountName", rs("UserID").Value
	usr.Put "title", "TDP Data Provider"
	usr.SetInfo

	usr.SetPassword rs("Password").Value
	usr.AccountDisabled = False
	usr.SetInfo

	WScript.Echo "Created user: " + rs("FeedName").Value

	rs.MoveNext

Loop

'Clean up before exit
rs.Close
cnn.Close

Set usr = Nothing
set w2k = Nothing
Set cnn = Nothing

WScript.Echo "User account creation completed."

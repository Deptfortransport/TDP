
*******************************
	   IMPORTANT
*******************************

If installing  the HttpRequestValidator tool on a 64-bit machine, the following post build actions must be performed.

****************************************************
THE INSRUCTIONS BELOW NO LONGER HAVE TO BE DONE 
- mmodi 13/06/2013:
- a post build action has been added to automate the below instructions
- for more info, see http://stackoverflow.com/questions/3253796/how-to-modify-contents-replace-a-binary-of-an-msi-file-as-a-post-build-step
****************************************************



There is a limitation of using a .NET Setup project for an ISS Extension tool
(http://blogs.iis.net/carlosag/archive/2008/11/10/creating-a-setup-project-for-iis-extensions-using-visual-studio-2008.aspx) 
and installing on to a 64-bit machine. The Setup.exe will attempt to update the IIS configuration files in the 32-bit folder
of IIS, and not in the 64-bit folder. This raises an error stating that configuration sections are missing, and thus the install
of the tool will fail.

To correct this, the install .msi file must be manually edited. Full details of the problem and solution can be found 
at the following URL, a summary of the instructions are duplicated here.

http://blogs.msdn.com/b/heaths/archive/2006/02/01/64-bit-managed-custom-actions-with-visual-studio.aspx

	1.Open the resulting .msi in Orca (see Note1) from the Windows Installer SDK
	2.Select the Binary table
	3.Click the Tables menu and then Add Row
	4.Enter, for example, InstallUtil64 for the Name
	5.Select the Data row and click the Browse button
	6.Browse to %WINDIR%\Microsoft.NET\Framework64\v2.0.50727
	7.Select InstallUtilLib.dll
	8.Click the Open button
	9.Click the OK button
	10.Select the CustomAction table
	11.For each custom action where the Source column is InstallUtil and only those custom actions that are 
	   64-bit managed custom actions (or that were built with /platform:anycpu, the default, where you want 
	   to run as 64-bit custom actions), change the value to, for example, InstallUtil64


Note1: The Orca install file has been included in the solution folder, and is also 
available at (http://www.technipages.com/download-orca-msi-editor.html)
// ********************************************************************* 
// NAME                 : UserSurveyRedirect_w3cstyle.js 
// AUTHOR               : Atos Origin
// DATE CREATED         : 21/10/2004
// DESCRIPTION			: JavaScript functionality for opening the User Survey page
// ********************************************************************** 
/* $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Scripts/UserSurveyRedirect_w3cstyle.js-arc  $
//
//   Rev 1.3   Mar 19 2010 13:39:40   mmodi
//Added header to file
//Resolution for 5471: Maps - Code Review - Add headers to Javascript files
//
//   Rev 1.2   Mar 31 2008 13:26:18   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:28:44   mturner
//Initial revision.

   Rev 1.2   Nov 02 2004 17:23:04   jmorrissey
Updated style of new window

   Rev 1.1   Oct 22 2004 15:45:34   jmorrissey
Updated 

   Rev 1.0   Oct 22 2004 15:29:50   jmorrissey
Initial revision.
*/

//opens another window containing the User Survey form
window.open('../UserSurvey/UserSurvey.aspx','UserSurveyWindow', 
config='toolbar=no, menubar=no,scrollbars=yes, resizable=yes,');
self.focus();


//JM - have left in the following alternative in case we ever decide to use this
//this will only open the user survey window when the printer friendly window is closed...

//function ShowUserSurvey(){

//window.open('../UserSurvey.aspx','UserSurveyWindow', 
//config='height=600,width=800, toolbar=no, menubar=no,scrollbars=yes, resizable=yes,');
//}
//window.onunload = ShowUserSurvey



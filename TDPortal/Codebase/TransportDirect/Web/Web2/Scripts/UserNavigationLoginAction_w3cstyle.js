// ********************************************************************* 
// NAME                 : UserNavigationLoginAction_w3cstyle.js
// AUTHOR               : Atos Origin
// DATE CREATED         : 05/09/2005
// DESCRIPTION			: JavaScript functionality for taking default login action 
//                        (Login, Register and Confirm Buttons) when user hits enter button 
// ********************************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Scripts/UserNavigationLoginAction_w3cstyle.js-arc  $ 
//
//   Rev 1.4   Jul 20 2010 12:04:52   mmodi
//Updated to have seperate Default Login and Register actions 
//Resolution for 5010: Cannot submit new user registration details using "Enter" key
//
//   Rev 1.3   Mar 19 2010 13:39:40   mmodi
//Added header to file
//Resolution for 5471: Maps - Code Review - Add headers to Javascript files
//
//   Rev 1.2   Mar 31 2008 13:26:18   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:28:42   mturner
//Initial revision.
//
//   Rev 1.1   Sep 14 2005 10:38:40   NMoorhouse
//DN079 UEE - Added User Navigation action for Login and Register
//Resolution for 2757: DEL 8 stream: Login functionality available on every page
//
//   Rev 1.0   Sep 08 2005 14:49:12   NMoorhouse
//Initial revision.
//Resolution for 2757: DEL 8 stream: Login functionality available on every page
//
//   Rev 1.0   Sep 05 2005 18:05:22   Schand
//Initial revision.

//function to trigger the Login click event
function TakeLoginAction(e) {

    var enterkeyPressed = IsEnterKeyPressed(e);

    if (enterkeyPressed) {

        if (window.event) {
            event.returnValue = false;
            event.cancel = true;
        }
        else {
            e.returnValue = false;
            e.cancel = true;
        }

        var actionButton = GetActionButton("logonButton");

        if (actionButton != null) {
            actionButton.click();
        }
    }
}

//funcion to trigger the Register click event
function TakeRegisterAction(e) {

    var enterkeyPressed = IsEnterKeyPressed(e);

    if (enterkeyPressed) {

        if (window.event) {
            event.returnValue = false;
            event.cancel = true;
        }
        else {
            e.returnValue = false;
            e.cancel = true;
        }

        var actionButton = GetActionButton("registerButton");

        if (actionButton != null) {
            actionButton.click();
        }
    }
}

//function to identify if the Enter key was pressed
function IsEnterKeyPressed(e) {

    var keyID = (window.event) ? event.keyCode : e.keyCode;

    if (keyID == 13) {
        return true;
    }
    else {
        return false;
    }
}

//function to return the Action "submit" button for the specified input button name
function GetActionButton(buttonName) {

    inputColl = document.getElementsByTagName("input");

    var actionButton;

    for (i = 0; i < inputColl.length; i++) {

        elementName = inputColl[i].name;

        if (elementName != null) {
            if (elementName.indexOf(buttonName) >= 0) {

                var actionButtonName = actionButtonName = inputColl[i].id;

                if (actionButtonName != null) {

                    actionButton = document.getElementById(actionButtonName);

                }

                break;
            }
        }
    }

    return actionButton;
}

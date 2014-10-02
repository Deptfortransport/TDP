// ***********************************************
// NAME     : BatchJourneyPlanner.js
// AUTHOR   : Atos Origin
// ************************************************ 

// Sets the radio button in a repeater row
function SetRadioButton(regex, current) {
    regEx = new RegExp(regex);
    for (i = 0; i < document.forms[0].elements.length; i++) {
        element = document.forms[0].elements[i];
        if (element.type == 'radio' && regEx.test(element.id)) {
            element.checked = false;
        }
        current.checked = true;
    }
}

// BatchJourneyPlanner.aspx - sort out check boxes
function EnableDisableCheckboxes() {
    // if PT is not selected then disable details and output format choices and vice versa
    ptCheck = document.getElementById('chkPublic');
    detailsCheck = document.getElementById('chkDetails');
    radioGroup = document.getElementById('radioListFormat');
    if (detailsCheck != null) {
        if (ptCheck.checked) {
            detailsCheck.disabled = false;
            radioGroup.disabled = false;
            if (!detailsCheck.checked) {
                radioGroup.disabled = true;
            }
        }
        else {
            detailsCheck.disabled = true;
            detailsCheck.checked = false;
            radioGroup.disabled = true;
            radioGroup.selectedIndex = -1;
        }
    }

    // Check if we have enough to allow a load
    EnableLoad();
}

// Check file has been selected for loading and, if it has, enable the load button
function EnableLoad() {
    if (document.getElementById('fileUpload').value.length > 0) {
        // at least one journey type required
        if ((document.getElementById('chkPublic').checked) || (document.getElementById('chkCar').checked) || (document.getElementById('chkCycle').checked)) {
            // at least one output type required
            if ((document.getElementById('chkStatistics').checked) || (document.getElementById('chkDetails').checked)) {
                document.getElementById('buttonFileLoad').disabled = false;
            }
            else {
                document.getElementById('buttonFileLoad').disabled = true;
            }
        }
        else {
            document.getElementById('buttonFileLoad').disabled = true;
        }
    }
    else {
        document.getElementById('buttonFileLoad').disabled = true;
    }
}

function LoadClick() {
    // disable load button and display uploading label
    document.getElementById('buttonFileLoad').value = 'Uploading...';
    setTimeout(function() { document.getElementById('buttonFileLoad').disabled = true; }, 10);
}

// Check if necessary registration information exists
function EnableRegister() {
    // Ts & Cs
    if (document.getElementById('chkTerms').checked) {
        // first name
        if (document.getElementById('textBoxFirstName').value.length > 0) {
            // last name
            if (document.getElementById('textBoxLastName').value.length > 0) {
                // telephone
                if (document.getElementById('textBoxPhone').value.length > 0) {
                    // proposed use
                    if (document.getElementById('textBoxProposedUse').value.length > 0) {
                        document.getElementById('buttonRegister').disabled = false;
                    }
                    else {
                        document.getElementById('buttonRegister').disabled = true;
                    }
                }
                else {
                    document.getElementById('buttonRegister').disabled = true;
                }
            }
            else {
                document.getElementById('buttonRegister').disabled = true;
            }
        }
        else {
            document.getElementById('buttonRegister').disabled = true;
        }
    }
    else {
        document.getElementById('buttonRegister').disabled = true;
    }
}

// Disable the register and load file buttons (attached javascript functions will enable them when appropriate)
ele = document.getElementById('buttonFileLoad');
if (ele) {
    ele.disabled = true;
    // page may come pre-populated (eg if error message shown)
    EnableLoad();
}
ele = document.getElementById('buttonRegister');
if (ele) {
    ele.disabled = true;
    // page may come pre-populated (eg if error message shown)
    EnableRegister();
}


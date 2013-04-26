// JScript File

function PopupDatePicker(dp) {
    var PopupWindow = null;
    PopupWindow = window.open('PopupCalendar.aspx?Ctl=' + dp, 'PopupWindow', 'width=10,height=250,resizable=no');
    PopupWindow.focus();
    return false;
}

function checkEnableRotation(source, args) {
    var position = document.all("ddlPosition");
    var rotation = document.getElementsByName("rdEnableRotation");
    if (position.value == "Customer Care Officer")
    {
        for (var i = 0; i < rotation.length; i++)
        {                        
            args.IsValid = (rotation[i].checked) ? true : false;
            i = (rotation[i].checked) ? rotation.length : i;
        }
    }
}

function checkPositionOthers(source, args) {
    var position = document.all("ddlPosition");
    var others = document.all("txtPositionOthers");
    if (position.value == "Others")
        args.IsValid = (others.value != "") ? true : false;
}

function checkAvailableDate(source, args) {
    var jobType = document.all("ddlPreferredJobType");   
    var availableDate = document.all("txtAvailableDate"); 
    if (jobType.value == "Full Time")
        args.IsValid = (availableDate.value != "") ? true : false;
}

function checkPeriodAvailable(source, args) {
    var jobType = document.all("ddlPreferredJobType");
    var startDate = document.all("txtAvailableStartDate");
    var endDate = document.all("txtAvailableEndDate");    
    if (jobType.value == "Part Time" || jobType.value == "Contract")
        args.IsValid = (startDate.value != "" && endDate.value != "") ? true : false;
}

function checkNRICPassport(source, args) {
    var nric = document.all("txtNRIC");
    var passport = document.all("txtPassport");
    var citizenship = document.all("ddlCitizenship");
    if (citizenship.value != "Singaporean" && citizenship.value != "Singapore PR")
        args.IsValid = (nric.value != "") ? true : (passport.value == "") ? false : true;
}

function validateNRIC(source, args) {
    var citizenship = document.all("ddlCitizenship");
    if (citizenship.value == "Singaporean" || citizenship.value == "Singapore PR")
        args.IsValid = (args.Value == "") ? false : isNRIC(args.Value);
}

function isNRIC(value) {      
    return (checkFormat(value) && checkSum(value));

    function checkFormat(nric) {
        return (/^[S|T]{1}\d{7}[A-J|Z]{1}$/g.test(nric.toUpperCase()));
    }	

    function checkSum(nric) {	    
        var prefix = nric.substr(0, 1);
        var digits = nric.substr(1, 7);
        var letter = nric.substr(8, 1);

        var weightages = [ 2,7,6,5,4,3,2 ];
        var alphabets = (prefix == "S") ? [ "A","B","C","D","E","F","G","H","I","Z","J" ] : [ "H","I","Z","J","A","B","C","D","E","F","G" ];
        var sum = 0;

        for (var i=0; i<digits.length; i++)
            sum += parseInt(digits.charAt( i )) * weightages[i];			
        				
        return (letter == alphabets[ 11 - (sum % 11) - 1]);				
    }
}

function checkPRIssuedDate(source, args) {        
    var citizenship = document.all("ddlCitizenship");
    var issued = document.all("txtPRIssuedDate");
    if (citizenship.value == "Singapore PR")
        args.IsValid = (issued.value != "") ? true : false;
}

function checkPassportExpiryDate(source, args) {    
    var passport = document.all("txtPassport");
    var expiry = document.all("txtExpiryDate");
    var citizenship = document.all("ddlCitizenship");
    if (citizenship.value == "Foreigner")
        args.IsValid = (passport.value != "" && expiry.value != "") ? true : false;
}

function checkRaceOthers(source, args) {        
    var race = document.all("ddlRace");
    var others = document.all("txtRaceOthers");
    if (race.value == "Others")
        args.IsValid = (others.value != "") ? true : false;
}

function checkContactNo(source, args) {
    var home = document.all("txtHomeNo");
    var mobile = document.all("txtMobileNo");
    var overseas = document.all("txtOverseasNo");
    args.IsValid = (home.value != "") ? true : (mobile.value != "") ? true : (overseas.value == "") ? false : true;
}

function checkSpouseParticular(source, args) {
    var status = document.all("ddlMaritalStatus");
    var name = document.all("txtSpouseName");
    var nric = document.all("txtSpouseNRIC");
    var dob = document.all("txtSpouseDOB");
    var nationality = document.all("ddlSpouseNationality");
    var employer = document.all("txtSpouseEmployer");
    var occupation = document.all("txtSpouseOccupation");
    if (status.value == "Married")
    {
        if (name.value != "" && nric.value != "" && dob.value != "" && nationality.selectedIndex != 0 && employer.value != "" && occupation.value != "")
            args.IsValid = true;
        else
            args.IsValid = false;
    }
    else
        args.IsValid = true;
}

function checkIdenticalNRIC(source, args) {
    var applicant = document.all("txtNRIC");
    var spouse = document.all("txtSpouseNRIC");
    args.IsValid = (applicant.value.toUpperCase() == spouse.value.toUpperCase()) ? false : true;
}

function validateSpouseNRIC(source, args) {
    var nationality = document.all("ddlSpouseNationality");
    if (nationality.value == "Singapore")
        args.IsValid = (args.Value == "") ? false : isNRIC(args.Value);
}

function checkContactPersonContactNo(source, args) {
    var local = document.all("txtContactPersonNo");
    var overseas = document.all("txtContactPersonOverseasNo");
    args.IsValid = (local.value != "") ? true : (overseas.value == "") ? false : true;
}

function checkNSStatus(source, args) {
    var status = document.all("ddlNSStatus");
    var citizenship = document.all("ddlCitizenship");
    var gender = document.all("ddlGender");
    if ((citizenship.value == "Singaporean" && gender.value == "Male") || (citizenship.value == "Singapore PR" && gender.value == "Male"))
        args.IsValid = (status.selectedIndex == 0) ? false : true;
}

function checkNSInfo(source, args) {
    var status = document.all("ddlNSStatus");
    var beganDate = document.all("txtNSDateBegan");
    var completedDate = document.all("txtNSDateCompletion");
    var rank = document.all("txtNSRank");
    var unit = document.all("txtNSUnit");
    var nextDate = document.all("txtNSNextDate");
    var nextLength = document.all("txtNSNextLength");
    if (status.value == "Completed")
    {
//        if (beganDate.value != "" && completedDate.value != "" && rank.value != "" && unit.value != "" && nextDate.value != "" && nextLength.value != "")
        if (beganDate.value != "" && completedDate.value != "" && rank.value != "" && unit.value != "")
            args.IsValid = true;
        else
            args.IsValid = false;
    }
    else
        args.IsValid = true;
}

function checkEndDate(source, args) {
    var totalExperience = document.all("ddlTotalWorkExperience");
    var currentWorking = document.all("chkCurrentWorking");
    var endDate = document.all("txtEndDate1");   
    if (totalExperience.selectedIndex != 0 || totalExperience.selectedIndex != 1)
        args.IsValid = (currentWorking.checked == false && endDate.value == "") ? false : true;
}

function checkStaffInfo(source, args) {
    var name = document.all("txtStaffName");
    var relationship = document.all("txtStaffRelationship");
    var known = document.getElementsByName("rdListKnownStaff");
    for (var i = 0; i < known.length; i++)
    {
        if (known[i].checked && known[i].value == "Yes")
            args.IsValid = (name.value != "" && relationship.value != "") ? true : false;
    }
}

function checkDateInfo(source, args) {
    var start = document.all("txtTCStartDate");
    var end = document.all("txtTCEndDate");
    var position = document.all("txtTCPosition");
    var workbefore = document.getElementsByName("rdListWorkTeleCentreBefore");
    for (var i = 0; i < workbefore.length; i++)
    {
        if (workbefore[i].checked && workbefore[i].value == "Yes")
            args.IsValid = (start.value != "" && end.value != "" && position.value != "") ? true : false;
    }
}

function checkDetailsAnswer(source, args) {
    var physicalImpairment = document.getElementsByName("rdListPhysicalImpairment");
    var majorIllness = document.getElementsByName("rdListMajorDiseaseIllness");
    var minorIllness = document.getElementsByName("rdListMinorDiseaseIllness");
    var medication = document.getElementsByName("rdListLongtermMedication");
    var bankruptcy = document.getElementsByName("rdListBankruptcyStatus");
    var convictedOffence = document.getElementsByName("rdListConvictedOffence");
    var dismissedSuspended = document.getElementsByName("rdListDismissedSuspended");
    var details = document.all("txtDetailsAnswer");
    for (var i = 0; i < 2; i++)
    {
        if ((physicalImpairment[i].checked && physicalImpairment[i].value == "Yes")
        || (majorIllness[i].checked && majorIllness[i].value == "Yes")
        || (minorIllness[i].checked && minorIllness[i].value == "Yes")
        || (medication[i].checked && medication[i].value == "Yes")
        || (bankruptcy[i].checked && bankruptcy[i].value == "Yes")
        || (convictedOffence[i].checked && convictedOffence[i].value == "Yes")
        || (dismissedSuspended[i].checked && dismissedSuspended[i].value == "Yes"))
            args.IsValid = (details.value != "") ? true : false;
    }
}

function validateDeclarationStatement(source, args) {
    //if(document.all["<%=chkDeclarationStatement.ClientID%>"].checked == false)
    if(!document.all("chkDeclarationStatement").checked)
        args.IsValid = false;
}

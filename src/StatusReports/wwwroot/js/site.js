// Write your Javascript code.
$(document).ready(function () {
    $("#editForm").validate();
    $("#createForm").validate({
        onfocusout: false,
        errorClass: "error",
        validClass: "validEntry",
        messages: {
            mondayHours: {
                min: " number > 0 ",
                max: " number < 24"
            }
            
        },
        invalidHandler: function(form, validator){
            var errors = validator.numberOfInvalids();
            if(errors){
                validator.errorList[0].element.focus();
            }
        }
        
    });
});
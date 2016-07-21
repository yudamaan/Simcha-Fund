$(function () {
    $(".edit").click(function () {
        var row = $(this).closest("tr");
        var td = row.find("td:eq(1)").text().split(' ');
        var checked = $(this).closest("tr").find("td:eq(4)").data("always-include");
        console.log(checked);
        console.log(td[0]);
        $(".input").remove();
        $('.modal-body').append($("<input type='text' name='firstName' class='form-control input'value='" +td[0]+ "' />"));
        $('.modal-body').append($("<input type='text' name='lastName' class='form-control input' value='" + td[1] + "'/>"));
        $('.modal-body').append($("<input type='text' name='phoneNum' class='form-control input'value='" + row.find("td:eq(2)").text() + "'/>"));
        $('.modal-body').append($('<div><input type="checkbox" name="alwaysInclude" id="include" class="input"' + checked + ' />Always Include</div>'));
        console.log('<input type="checkbox" name="alwaysInclude" id="include" class="input"' + checked + ' />"Always Include"');
        $('.modal-body').append($("<input type='hidden' name='id' class='input' value='"+$(this).closest("tr").data("contributor-id")+"'/>"));
        $(".modal-title").html("Edit Contributor");
        $("#submit").html("Edit");
        $("form").attr("action", "/home/EditContributor");
        $(".modal").modal("show");
    });

    $(".deposit").click(function () {
        $(".input").remove();
        $("#submit").html("Deposit");
        $(".modal-title").html("Deposit");
        $('.modal-body').append($("<input type='text' name='amount' class='form-control input' placeholder='Amount'/>"));
        $('.modal-body').append($("<input type='hidden' name='id' class='input' value='" + $(this).closest("tr").data("contributor-id") + "'/>"));
        $("form").attr("action", "/home/deposit");
        $(".modal").modal("show");
    });
    $(".add").click(function () {
        $(".input").remove();
        $('.modal-body').append($("<input type='text' name='firstName' class='form-control input'placeholder='FirstName' />"));
        $('.modal-body').append($("<input type='text' name='lastName' class='form-control input' placeholder='LastName'/>"));
        $('.modal-body').append($("<input type='text' name='phoneNum' class='form-control input' placeholder='Phone Number'/>"));
        $('.modal-body').append($('<input type="checkbox" name="alwaysInclude" id="include" class="input" />"Always Include"'));
        $(".modal-title").html("Add Contributor");
        $("#submit").html("Add");
        $("form").attr("action", "/home/AddContributor");
        $(".modal").modal("show");
    });
    $(".modal-body").on('click','#include',function () {
        if ($('#include').is(':checked')) {
            $(".modal-body").find('#include').attr('value', "true");
        } else {
            $(".modal-body").find('#include').attr('value', "false");
        }
    });
   
});
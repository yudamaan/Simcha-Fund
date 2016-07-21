$(function () {
    //$("tr").click(function () {
    //   // console.log($(this).data("contibutor-id"));
    //});
    if ($('.checkbox').is(':checked')) {
        $(".checkbox").attr('value', "true");
    }
    $(".checkbox").click(function () {
        if ($('.checkbox').is(':checked')) {
            console.log(true);
            $(".checkbox").attr('value', "true");
            //} else {
            //    $(".checkbox").attr('value', "false");
            //    console.log("false");

            //}
        }
    });
});
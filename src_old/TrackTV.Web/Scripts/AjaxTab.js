(function () {

    var showId = $('#show-id').attr('data-show-id');

    var numberOfSeasones = $('#number-of-seasones').attr('data-number-of-seasones');

    $('#tabstrip a').click(function (e) {
        e.stopImmediatePropagation();
        e.preventDefault();

        var tabID = $(this).attr("href").substr(1);

        var seasonNumber = $(this).attr('data-season-number');

        //$(".tab-pane").each(function () {
        //    console.log("clearing " + $(this).attr("id") + " tab");
        //    $(this).empty();
        //});

        $.ajax({
            url : "/ShowDetails/Season/" + showId + "?seasonNumber=" + seasonNumber,
            cache : true,
            type : "get",
            dataType : "html",
            success : function (result) {
                $("#" + tabID).html(result);
            }
        });

        $(this).tab('show');

        return false;
    });

    $.ajax({
        url : "/ShowDetails/Season/" + showId + "?seasonNumber=" + numberOfSeasones,
        cache : true,
        type : "get",
        dataType : "html",
        success : function (result) {
            $("#Season" + numberOfSeasones).html(result);
        }
    });

})();
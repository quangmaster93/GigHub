
var GigsController = function() {

    var attendanceToggle = function () {
        $(".js-toggle-attendance").click(function (e) {
            var button = $(e.target);
            var done = function () {
                var text = (button.text == "Going") ? "Going?" : "Going";
                button.toggleClass("btn-default").toggleClass("btn-info").text(text);
            }
            var fail = function () {
                alert("Something failed!");
            }
            if (button.hasClass("btn-default")) {
                    $.post("/api/attendances/attend", { GigId: button.attr("data-gigId") })
                        .done(done)
                        .fail(fail);
            } else {
                    $.ajax(
                        {
                            url: "/api/attendances/deleteattendance",
                            type: "delete",
                            data: { GigId: button.attr("data-gigId") },
                            success: done,
                            error: fail
                         });
                    }   
        });
    };

    //end attendanceToggle funtion

    var followingToggle=function() {
        $(".js-toggle-following").click(function(e) {
            var button = $(e.target);
            var done=function() {
                var text = (button.text() == "Follow") ? "Following" : "Follow";
                button.toggleClass("btn-default").toggleClass("btn-info").text(text);
            }

            var fail=function() {
                alert("something fail");
            }

            if (button.hasClass("btn-default")) {
                $.post("/api/followings/follow", { followeeId: button.attr("data-userId") })
                    .done(done)
                    .fail(fail);
            } else {
                $.ajax({
                    url: "/api/followings/deleteFollowing",
                    type: "delete",
                    data: { followeeId: button.attr("data-userId") },
                    success: done,
                    error: fail
                });
            }
        });
    }
    ////end followingToggle funtion

    var cancelToggle=
        function () {
            $(".js-cancel-gig").click(function (e) {
                var button = $(e.target);
                if (button.hasClass("btn-danger")) {
                    return;
                }
                bootbox.dialog({
                    message: "Are you sure you want to cancel this gig?",
                    title:"Confirm",
                    buttons: {
                        No: {
                            label: 'No'
                        },
                        Yes: {
                            label: 'Yes',
                            callback: function () {
                                var done = function () {
                                    button.text("Canceled");
                                    button.addClass("btn-danger");
                                }
                                var fail = function () {
                                    alert("something fail");
                                }
                                var id = button.attr("data-gigId");
                                $.ajax(
                                        {
                                            url: "/api/gigs/cancel",
                                            type: "delete",
                                            data: {Id: id},
                                            success: done,
                                            error: fail
                                        }
                                    );
                            }
                        }
                    }
                });
            });   
        }
    return {
        attendanceToggle: attendanceToggle,
        followingToggle: followingToggle,
        cancelToggle: cancelToggle
    }
}();


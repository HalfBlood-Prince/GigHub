
var notifications;
var flag = false;

$(document).ready(function () {


     var notification = function () {


        $.getJSON("/api/notifications",
            function (notifications) {


                if (notifications.length === 0)
                    return;



                $(".js-noti-count")
                    .removeClass("hide")
                    .text(notifications.length)
                    .addClass("animated bounceInDown");


                if (flag) {
                    $(".notification").popover("destroy");
                    console.log("Popover");
                }


                $(".notification").popover({
                    html: true,

                    title: "Notifications",

                    content: function () {

                        var compiled = _.template($("#noti-id").html());

                        return compiled({ notifications: notifications });
                    },

                    placement: "bottom",
                    animation: true,
                    template:
                        '<div class="popover pop-noti" role="tooltip"><div class="arrow"></div><h3 class="popover-title"></h3><div class="popover-content"></div></div>'

                }).on("shown.bs.popover",
                    function () {


                        $.ajax({
                                url: "/api/Notifications",
                                method: "PUT"

                            }).done(function () {

                                $(".js-noti-count")
                                    .text("")
                                    .addClass("hide");

                            })
                            .fail(function () {

                                alert("Failed");

                            });

                    });

                flag = true;




            });

         $(".js-noti-count").removeClass("animated bounceInDown");
     };

    notification();


    notifications = notification;


});
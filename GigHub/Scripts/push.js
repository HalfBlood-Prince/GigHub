

$(document).ready(function() {



    var bridge = $.connection.gigHub;

    var con = (function () {

        var con;

        $.connection.hub.start()
            .done(function () {

                con = true;
                console.log("connected");
                bridge.server.start();

            })
            .fail(function () {

                con = false;
                alert("Error !");

            });


        return con;

    })();



    bridge.client.updateNotification = function() {

        notifications();

    }

    bridge.client.started = function () {

        console.log("Started");

    }


    bridge.client.ended = function () {

        console.log("Ended");

    }


    bridge.client.error = function () {

        console.log("Error");

    }

});
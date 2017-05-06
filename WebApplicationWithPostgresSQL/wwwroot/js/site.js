// Write your Javascript code.
$("#user").click(function () {
    $(".user").show();
    $(".admin").hide();
    $("#admin").removeClass("active");
    $("#user").addClass("active");
});

$("#admin").click(function () {
    $(".user").hide();
    $(".admin").show();
    $("#user").removeClass("active");
    $("#admin").addClass("active");
});

$("#search").click(function () {
    $(".left-bar").css("transform", "translateX(-1000px)");
    var passport = $("#clientPassport").val();
    var password = $("#clientPassword").val();
    var contract = $("#clientContract").val();

    if ((password == null) || (password == "")) return;
    if ((passport == null) || (passport == "")) return;
    if ((contract == null) || (contract == "")) return;
    
    var url = '/Deposits/ClientSearch?id=' + contract + '&passport=' + passport + '&password=' + password;
    var block = "main-block";
    setTimeout(SendGet(url), 300);
});

$("#enter").click(function () {
    $(".left-bar").css("transform", "translateX(-1500px)");
    $(".right-bar").css("transform", "translateX(500px)");
    var login = $("#staffLogin").val();
    var password = $("#staffPassword").val();

    if ((login == null) || (login == "")) return; 
    if ((password == null) || (password == "")) return;

    var url = '/Home/AdminPanel'; //TODO: ?login=' + login + '&password=' + password;
    setTimeout(SendGet(url), 300);
});

//=====================================================

function SendGet(url) {
    $.ajax({
        url: url,
        type: "GET",
        success: function (result) {   
            $(".main-block").html(result);
            $(".admin-panel").css("display", "none");
            $(".admin-panel").fadeIn(1000);
            //var lol = $.parseHTML(result);
            //var lol = lol[11].children[1];
            
        },
        error: function (ts) { alert(ts.responseText) }
    });
}

$(() => {
    $("#site-header").find("a").each(function () {
        let url = $(this).attr("href");
        let currentLocation = window.location + "";
        let pageUrl = '/' + currentLocation.replace(
            window.location.protocol + "//" + window.location.host + "/",
            "");

        if (url === pageUrl)
            $(this).addClass("active");
    })
    
    $("#userpanel-sidebar").find("li").each(function () {
        let url = $(this).find("a").attr("href");
        let currentLocation = window.location + "";
        let pageUrl = '/' + currentLocation.replace(
            window.location.protocol + "//" + window.location.host + "/",
            "")
            .split('?')[0]
            .replace('#', '')
            .replace('/#', '')
            .replace(/\/$/, '')
            .toLowerCase();

        if (url === pageUrl)
            $(this).addClass("btn-sidebar-active");
    })
});
var roles;

$(document).ready(function () {
    var showMenu = false;

    $("footer .dropdown-menu li a").click(function () {
        $("#footer-dropdown").html($(this).text() + ' <span class="glyphicon glyphicon-chevron-down">');
    });

    $(".menu-control").click(function () {
        if (showMenu) {
            $(".menu").css({ "visibility": "hidden", "opacity": "0" });
        } else {
            $(".menu").css({ "visibility": "visible", "opacity": "1" });
        }

        showMenu = !showMenu;
    });
});

function getRoles(callback) {
    $.ajax({
        url: 'http://localhost:21790/api/RoleApi/GetRoles',
        type: 'get',
        dataType: 'json',
        success: function (data) {
            roles = data.roles;
            callback(roles);
        },
        error: function (err) {
            console.log(err.message);
        }
    });
}

function getUserProjects(user_id, callback) {
    $.ajax({
        url: 'http://localhost:21790/api/ProjectApi/GetUserProjects',
        type: "get",
        dataType: 'json',
        data: {
            'user_id': user_id
        },
        success: function (data) {
            callback(data.projects);
        },
        error: function (err) {
            console.log(err.message);
        }
    });
}

function getFollowers(user_id, callback) {
    $.ajax({
        url: 'http://localhost:21790/api/FollowingApi/GetFollowers',
        type: "get",
        dataType: 'json',
        data: {
            'user_id': user_id
        },
        success: function (data) {
            callback(data.followers);
        },
        error: function (err) {
            console.log(err.message);
        }
    });
}

function getFollowings(follower_id, callback) {
    $.ajax({
        url: 'http://localhost:21790/api/FollowingApi/GetFollowings',
        type: "get",
        dataType: 'json',
        data: {
            'follower_id': follower_id
        },
        success: function (data) {
            callback(data.followings);
        },
        error: function (err) {
            console.log(err.message);
        }
    });
}

function getCategories(callback) {
    $.ajax({
        async: false,
        url: 'http://localhost:21790/api/ProjectApi/GetCategories',
        type: 'get',
        dataType: 'json',
        success: function(data) {
            callback(data.categories);
        },
        error: function(err) {
            console.log(err);
        }
    });
}

function getUserInfo(user_id, callback) {
    $.ajax({
        async: false,
        url: 'http://localhost:21790/api/ProjectApi/GetCategories',
        type: 'get',
        dataType: 'json',
        data: {
            'user_id': user_id
        },
        success: function (data) {
            callback(data.user);
        },
        error: function (err) {
            console.log(err);
        }
    });
}
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
            callback(data.roles);
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

function getCountries(callback) {
    $.ajax({
        async: false,
        url: 'http://localhost:21790/api/AddressApi/GetCountries',
        type: 'get',
        dataType: 'json',
        success: function (data) {
            callback(data.countries);
        },
        error: function (err) {
            console.log(err);
        }
    });
}

function getStates(country_id, callback) {
    $.ajax({
        async: false,
        url: 'http://localhost:21790/api/AddressApi/GetStates',
        type: 'get',
        dataType: 'json',
        data: {
            'country_id': country_id
        },
        success: function (data) {
            callback(data.states);
        },
        error: function (err) {
            console.log(err);
        }
    });
}

function getDistricts(state_id, callback) {
    $.ajax({
        async: false,
        url: 'http://localhost:21790/api/AddressApi/GetDistricts',
        type: 'get',
        dataType: 'json',
        data: {
            'state_id': state_id
        },
        success: function (data) {
            callback(data.districts);
        },
        error: function (err) {
            console.log(err);
        }
    });
}

function getUserInfo(user_id, callback) {
    $.ajax({
        async: false,
        url: 'http://localhost:21790/api/UserApi/GetUserInfo',
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

function GetProjectRoles(project_id, limit, callback) {
    $.ajax({
        async: false,
        url: 'http://localhost:21790/api/RoleApi/GetProjectRoles',
        type: 'get',
        dataType: 'json',
        data: {
            'project_id': project_id,
            'limit': limit
        },
        success: function (data) {
            callback(data.roles);
        },
        error: function (err) {
            console.log(err);
        }
    });
}
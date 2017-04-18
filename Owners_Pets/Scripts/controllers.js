app.controller('usersController', ['$scope', '$http', '$route', 'usersData', function ($scope, $http, $route, usersData) {
    $scope.Users = usersData;

    $scope.addUser = function () {
        $http.post('http://localhost:54146/api/User', '"' + $scope.userName + '"')
            .then(function () {
                $route.reload();
            });
    };

    $scope.deleteUser = function (userId) {
        $http.delete('http://localhost:54146/api/User/' + userId)
            .then(function () {
                $route.reload();
            });
    };
    
    $scope.totalItems = usersData.length;
    $scope.currentPage = 1;
    $scope.itemsPerPage = 3;

    $scope.$watch('currentPage', function () {
        setPagingData($scope.currentPage);
    });

    function setPagingData(page) {
        $scope.currentPage = page;
        var pagedData = usersData.slice((page - 1) * $scope.itemsPerPage, page * $scope.itemsPerPage);
        
        $scope.filteredUsers = pagedData;
    }

    setPagingData($scope.currentPage);
}]);

app.controller('userInfoController', ['$scope', '$http', '$route', '$routeParams', 'userInfoList', 'userName', function ($scope, $http, $route, $routeParams, userInfoList, userName) {
    $scope.userId = $routeParams.Id;

    $scope.userName = userName;
    $scope.userInfo = userInfoList;

    $scope.addPet = function () {
        $http.post('http://localhost:54146/api/UserInfo', '"' + $scope.userId + ',' + $scope.petName + '"')
            .then(function () {
                $route.reload();
            });
    };

    $scope.deletePet = function (userInfoId) {
        $http.delete('http://localhost:54146/api/UserInfo/' + userInfoId)
            .then(function () {
                $route.reload();
            });
    };

    $scope.totalItems = userInfoList.length;
    $scope.currentPage = 1;
    $scope.itemsPerPage = 3;

    $scope.$watch('currentPage', function () {
        setPagingData($scope.currentPage);
    });

    function setPagingData(page) {
        $scope.currentPage = page;
        var pagedData = userInfoList.slice((page - 1) * $scope.itemsPerPage, page * $scope.itemsPerPage);
        $scope.filteredUserInfo = pagedData;
    }

    setPagingData($scope.currentPage);
}]);
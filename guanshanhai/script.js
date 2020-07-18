var app = angular.module('StarterApp', ['ngMaterial']); 
const BTN1 = document.getElementById('tab_page');
function alertObj(obj) {
    var str = "";
    for (var item in obj) {
        str += item + ":" + obj[item] + "\n";
    }
    alert(str);
}
app.controller('AppCtrl', ['$scope', function($scope){
    $scope.test = function (e) {
        var searchinput = $("#searchinput").val(); 
        $.ajax({
            type: "Post",
            url: "WebService1.asmx/SelectByNametojson",
            data: "{name:'" + searchinput + "'}",
            dataType: "json",
            contentType: "application/json",
            success: function (result) {
                var jsonObj = JSON.parse(result.d);//转换为json对象
                $scope.content = jsonObj;
                //$scope.content.push(jsonObj);
                console.log($scope.content)
                BTN1.click();
                //console.log(jsonObj)
                //var jsonStr1 = JSON.stringify(jsonObj)
                //console.log(jsonStr1 + "jsonStr1")
            }, error: function (xhr) { alert(xhr.responseText) }
        })
        
    };
  $scope.headers = [
    {
      name:'',
      field:'image'
    },{
      name: 'Name', 
      field: 'name'
    },{
      name:'省', 
      field: 'p'
    },{
      name: '地区', 
      field: 'ad'
    }
  ];
  
    $scope.content = [];
  
  $scope.custom = {name: 'bold', description:'grey',last_modified: 'grey'};
  $scope.sortable = ['name', 'p', 'ad'];
  $scope.thumbs = 'image';
    $scope.count = 10;
   
}]);

app.directive('mdTable', function () {
  return {
    restrict: 'E',
    scope: { 
      headers: '=', 
      content: '=', 
      sortable: '=', 
      filters: '=',
      customClass: '=customClass',
      thumbs:'=', 
      count: '=' 
    },
    controller: function ($scope,$filter,$window) {
      var orderBy = $filter('orderBy');
        $scope.tablePage = 0;
        $scope.showitemid = function (name) {
            window.location.href = "card.html?id="+name
        }
      $scope.nbOfPages = function () {
        return Math.ceil($scope.content.length / $scope.count);
      },
      	$scope.handleSort = function (field) {
          if ($scope.sortable.indexOf(field) > -1) { return true; } else { return false; }
      };
      $scope.order = function(predicate, reverse) {
          $scope.content = orderBy($scope.content, predicate, reverse);
          $scope.predicate = predicate;
      };
      $scope.order($scope.sortable[0],false);
      $scope.getNumber = function (num) {
      			    return new Array(num);
      };
      $scope.goToPage = function (page) {
        $scope.tablePage = page;
      };
    },
    template: angular.element(document.querySelector('#md-table-template')).html()
  }
});


app.directive('showFocus', function($timeout) {
  return function(scope, element, attrs) {
    scope.$watch(attrs.showFocus, 
      function (newValue) { 
        $timeout(function() {
            newValue && element.focus();
        });
      },true);
  };    
});

app.filter('startFrom',function (){
  return function (input,start) {
    start = +start;
    return input.slice(start);
  }
});
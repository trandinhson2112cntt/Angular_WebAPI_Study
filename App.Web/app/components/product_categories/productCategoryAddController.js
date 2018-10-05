(function (app) {
    app.controller('productCategoryAddController', productCategoryAddController);

    productCategoryAddController.$inject = ['apiService','$scope','$state'];

    function productCategoryAddController(apiService, $scope, $state) {
        $scope.productCategory = {
            CreatedDate: new Date(),
            Status:true,
        }
        $scope.AddProductCategory = AddProductCategory;
        function AddProductCategory() {
            apiService.post('api/productcategory/create',
                $scope.productCategory,
                function (result) {
                    console.log('Success!');
                    $state.go('product_categories');
                },
                function (error) {
                    console.log('Fail!');
                });
        }
        function loadParentCategory() {
            apiService.get('api/productcategory/getallparents', null, function (result) {
                $scope.parentCategories = result.data;
            }, function () {
                console.log('Cannot get list parents');
            });
        }
        loadParentCategory();
    }
})(angular.module('app.product_categories'));
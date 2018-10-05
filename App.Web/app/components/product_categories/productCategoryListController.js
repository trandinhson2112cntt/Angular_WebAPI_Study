(function (app) {
    app.controller('productCategoryListController', productCategoryListController);

    productCategoryListController.$inject = ['$scope', 'apiService'];

    function productCategoryListController($scope, apiService) {
        $scope.productCategories = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getProductCategories = getProductCategories;
        $scope.keyword = '';

        $scope.search = search;

        $scope.deleteProductCategory = deleteProductCategory;
        function deleteProductCategory(id) {
            bootbox.confirm({
                title:' ',
                message: 'Bạn có chắc muốn xóa?',
                buttons: {
                    cancel: {
                        label: '<i class="fa fa-times"></i> Cancel'
                    },
                    confirm: {
                        label: '<i class="fa fa-check"></i> Confirm',

                    }
                },
                callback: function (result) {
                    if (result == true) {
                        var config = { params: { id: id } };
                        apiService.del('api/productcategory/delete', config, function () {
                            search();
                        }, function () {
                            console.log('Xóa không thành công!!!');
                        });
                    }
                }
            });
        }

        function search() {
            getProductCategories();
        }

        function getProductCategories(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 10
                }
            };
            apiService.get('/api/productcategory/getall', config,
                function (result) {
                    //if (result.data.TotalCount == 0) {
                    //    notificationService.displayWarning("Không có bản ghi nào tìm thấy");
                    //}
                    //else {
                    //    notificationService.displaySuccess("Đã tìm thấy" + result.data.TotalCount + " bản ghi.");
                    //}
                    $scope.productCategories = result.data.Items;
                    $scope.page = result.data.Page;
                    $scope.pagesCount = result.data.TotalPages;
                    $scope.totalCount = result.data.TotalCount;
                },
                function () {
                    console.log('Load product categories failed.');
                });
        }

        $scope.getProductCategories();
    }
})(angular.module('app.product_categories'));
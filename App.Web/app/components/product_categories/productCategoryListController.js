(function (app) {
    app.controller('productCategoryListController', productCategoryListController);

    productCategoryListController.$inject = ['$scope', 'apiService','$filter'];

    function productCategoryListController($scope, apiService, $filter) {
        $scope.productCategories = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getProductCategories = getProductCategories;
        $scope.keyword = '';

        $scope.search = search;

        $scope.deleteProductCategory = deleteProductCategory;

        $scope.selectAll = selectAll;

        $scope.deleteMultiple = deleteMultiple;

        function deleteMultiple() {
            var listId = [];
            $.each($scope.selected, function (i, item) {
                listId.push(item.ID);
            });
            var config = {
                params: {
                    checkedProductCategories: JSON.stringify(listId)
                }
            }
            apiService.del('api/productcategory/deletemulti', config,
                function (result) {
                    search();
                },
                function (error) {
                    console.log('Xoá thất bại!');
            });
        }

        $scope.isAll = false;
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.productCategories, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.productCategories, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("productCategories", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDeleteMulti').removeAttr('disabled');
            } else {
                $('#btnDeleteMulti').attr('disabled', 'disabled');
            }
        },true);

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
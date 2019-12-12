(function () {
    //设备管理
    appModule.controller('common.views.baseinfo.audioTemplte.index', [
        '$scope', '$uibModal', '$stateParams', 'uiGridConstants', 'abp.services.app.audioTemplte',
        'abp.services.app.commonLookup',
        function ($scope, $uibModal, $stateParams, uiGridConstants, audioTemplte, commonLookup) {
            var vm = this;

            $scope.$on('$viewContentLoaded', function () {
                App.initAjax();
            });

            vm.loading = false;
            vm.requestParams = {
                skipCount: 0,
                maxResultCount: app.consts.grid.defaultPageSize,
            };

            vm.options = {
                enableHorizontalScrollbar: uiGridConstants.scrollbars.WHEN_NEEDED,
                enableVerticalScrollbar: uiGridConstants.scrollbars.WHEN_NEEDED,
                paginationPageSizes: app.consts.grid.defaultPageSizes,
                paginationPageSize: app.consts.grid.defaultPageSize,
                useExternalPagination: true,
                appScopeProvider: vm,
                columnDefs: [
                    {
                        name: app.localize('Actions'),
                        enableSorting: false,
                        width: 80,
                        cellTemplate:
                            '<div class=\"ui-grid-cell-contents\">' +
                            '  <div class="btn-group dropdown" uib-dropdown="" dropdown-append-to-body>' +
                            '    <button class="btn btn-xs btn-primary blue" uib-dropdown-toggle="" aria-haspopup="true" aria-expanded="false"><i class="fa fa-cog"></i> ' + app.localize('Actions') + ' <span class="caret"></span></button>' +
                            '    <ul uib-dropdown-menu>' + 
                            '      <li><a  ng-click="grid.appScope.edit(row.entity)">修改</a></li>' +
                            '      <li><a ng-click="grid.appScope.delete(row.entity)">删除</a></li>' +
                            '    </ul>' +
                            '  </div>' +
                            '</div>'
                    },
                    {
                        name: '语种',
                        field: 'audioLanguageName',
                        minWidth: 100
                    },  
                    {
                        name: '明细',
                        enableSorting: false,
                        width: 100,
                        cellTemplate:
                            '<div class=\"ui-grid-cell-contents\" >' +
                            '    <button class="btn btn-xs btn-info"  ng-click="grid.appScope.showDetail(row.entity)"><i class="fa fa-bars"></i>明细管理</button>' +
                            '</div>'
                    }, 
                    {
                        name: '类型编号',
                        field: 'type',
                        minWidth: 80
                    },  
                    
                    {
                        name: '内容',
                        field: 'content',
                        minWidth: 200
                    },
                    {
                        name: '备注',
                        field: 'remark',
                        minWidth: 140
                    }
                    

                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    $scope.gridApi.core.on.sortChanged($scope, function (grid, sortColumns) {
                        if (!sortColumns.length || !sortColumns[0].field) {
                            vm.requestParams.sorting = null;
                        } else {
                            vm.requestParams.sorting = sortColumns[0].field + ' ' + sortColumns[0].sort.direction;
                        }

                        vm.getAll();
                    });
                    gridApi.pagination.on.paginationChanged($scope, function (pageNumber, pageSize) {
                        vm.requestParams.skipCount = (pageNumber - 1) * pageSize;
                        vm.requestParams.maxResultCount = pageSize;

                        vm.getAll();
                    });
                },
                data: []
            };

            vm.getAll = function () {
                vm.loading = true;
                audioTemplte.getAll(vm.requestParams)
                    .then(function (result) {
                        vm.options.data = result.data.items;
                        vm.options.totalItems = result.data.totalCount;
                    }).finally(function () {
                        vm.loading = false;
                    });
            };

            vm.edit = function (entity) {
                openCreateOrEditModal(entity.id);
            };

            vm.create = function () { 
                openCreateOrEditModal(null);
            };

            vm.delete = function (entity) {
              
                var msg = "ID：" + entity.id + "，名称：" + entity.name;
                abp.message.confirm(
                    msg,
                    "确定删除？",
                    function (isConfirmed) {
                        if (isConfirmed) {
                            audioTemplte.delete({
                                id: entity.id
                            }).then(function () {
                                vm.getAll();
                                abp.notify.success(app.localize('SuccessfullyDeleted'));
                            });
                        }
                    }
                );
            }; 

            vm.showDetail = function (entity) {
                var modalInstance = $uibModal.open({
                    templateUrl: '~/App/common/views/baseinfo/audioTemplte/detail.cshtml',
                    controller: 'common.views.baseinfo.audioTemplte.detail as vm',
                    backdrop: 'static',
                    size: 'lg',
                    resolve: {
                        entityId: function () {
                            return entity.id;
                        }
                    }
                });

                modalInstance.result.then(function (result) {
                    vm.getAll();
                });
                
                 
            };

            function openCreateOrEditModal(entityId) {
                var modalInstance = $uibModal.open({
                    templateUrl: '~/App/common/views/baseinfo/audioTemplte/createOrEdit.cshtml',
                    controller: 'common.views.baseinfo.audioTemplte.createOrEdit as vm',
                    backdrop: 'static',
                    size: 'lg',
                    resolve: {
                        entityId: function () {
                            return entityId;
                        }
                    }
                });

                modalInstance.result.then(function (result) {
                    vm.getAll();
                });
            }

            vm.init = function() {
                commonLookup.getAudioLanguageForCombobox().then(function (result) {
                    vm.languages = result.data.items;
                });
                vm.getAll();
            };
            vm.init();
             
        }]);
})();
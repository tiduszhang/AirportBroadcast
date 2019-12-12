(function () {
    //设备管理
    appModule.controller('common.views.adioSets.playSet.index', [
        '$scope', '$uibModal', '$stateParams', 'uiGridConstants', 'abp.services.app.audioPlaySet',
        function ($scope, $uibModal, $stateParams, uiGridConstants, audioPlaySet) {
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
                        name: app.localize('Pages_BaseInfo_PlaySet_Code'),
                        field: 'code',
                        width: 120
                    },
                    {
                        name: '自动播放',
                        width: 80,
                        cellTemplate:
                            '<div class=\"ui-grid-cell-contents\">' +
                            '  <span ng-show="row.entity.autoPlay" class="label label-success">是</span>' +
                            '  <span ng-show="!row.entity.autoPlay" class="label label-default">否</span>' +
                            '</div>'
                    }, {
                        name: '语句模版',
                        enableSorting: false,
                        width: 100,
                        cellTemplate:
                            '<div class=\"ui-grid-cell-contents\" >' +
                            '    <button class="btn btn-xs btn-info"  ng-click="grid.appScope.showTempleDetail(row.entity)"><i class="fa fa-bars"></i>详情</button>' +
                            '</div>'
                    }, {
                        name: app.localize('Pages_AudioSets_Device'),
                        enableSorting: false,
                        width: 100,
                        cellTemplate:
                            '<div class=\"ui-grid-cell-contents\" >' +
                            '    <button class="btn btn-xs btn-info"  ng-click="grid.appScope.showDeviceDetail(row.entity)"><i class="fa fa-bars"></i>详情</button>' +
                            '</div>'
                    }, {
                        name: app.localize('Pages_AudioSets_TopPwrPort_cn'),
                        enableSorting: false,
                        width: 120,
                        cellTemplate:
                            '<div class=\"ui-grid-cell-contents\" >' +
                            '    <button class="btn btn-xs btn-info"  ng-click="grid.appScope.showTopPwrPortDetail(row.entity,1)"><i class="fa fa-bars"></i>详情</button>' +
                            '</div>'
                    }, {
                        name: app.localize('Pages_AudioSets_TopPwrPort_en'),
                        enableSorting: false,
                        width: 120,
                        cellTemplate:
                            '<div class=\"ui-grid-cell-contents\" >' +
                            '    <button class="btn btn-xs btn-info"  ng-click="grid.appScope.showTopPwrPortDetail(row.entity,2)"><i class="fa fa-bars"></i>详情</button>' +
                            '</div>'
                    },
                    {
                        name: '说明',
                        field: 'remark',
                        minWidth: 200
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
                audioPlaySet.getAll(vm.requestParams)
                    .then(function (result) {
                        vm.options.data = result.data.items;
                        vm.options.totalItems = result.data.totalCount;
                    }).finally(function () {
                        vm.loading = false;
                    });
            };

            vm.edit = function (entity) {
                openCreateOrEditModal(entity.id, 'createOrEdit', 'md');
            };

            vm.create = function () {
                openCreateOrEditModal(null, 'createOrEdit', 'md');
            };

            vm.delete = function (entity) {

                var msg = "ID：" + entity.id + "，名称：" + entity.name;
                abp.message.confirm(
                    msg,
                    "确定删除？",
                    function (isConfirmed) {
                        if (isConfirmed) {
                            audioPlaySet.delete({
                                id: entity.id
                            }).then(function () {
                                vm.getAll();
                                abp.notify.success(app.localize('SuccessfullyDeleted'));
                            });
                        }
                    }
                );
            };

            vm.showTempleDetail = function (entity) {
                openCreateOrEditModal(entity.id, 'detail', 'lg');
            };

            vm.showDeviceDetail = function (entity) {
                openCreateOrEditModal(entity.id, 'EditDevices', 'md');
            };

            vm.showTopPwrPortDetail = function (entity, type) {
                openCreateOrEditModal2(entity.id, type,'EditTopPwrPorts', 'md');
            };

            vm.setGobalSettings = function () {
                var modalInstance = $uibModal.open({
                    templateUrl: '~/App/common/views/audioSets/playSet/gobalSetting.cshtml',
                    controller: 'common.views.audioSets.playSet.gobalSetting as vm',
                    backdrop: 'static',
                    size: 'md',
                    resolve: { }
                });
            }

            function openCreateOrEditModal2(entityId, type, urlname, size) {
                var eid = entityId;
                if (type) {
                    eid = { entityId: entityId, type: type };
                }

                var modalInstance = $uibModal.open({
                    templateUrl: '~/App/common/views/audioSets/playSet/' + urlname + '.cshtml',
                    controller: 'common.views.audioSets.playSet.' + urlname + ' as vm',
                    backdrop: 'static',
                    size: size,
                    resolve: {
                        entityId: function () {
                            return eid;
                        }
                    }
                });

                modalInstance.result.then(function (result) {
                    vm.getAll();
                });
            }

            function openCreateOrEditModal(entityId, urlname, size) {
                openCreateOrEditModal2(entityId, null, urlname, size);
            }

            vm.getAll();
        }]);
})();
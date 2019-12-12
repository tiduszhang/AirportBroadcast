(function () {
    //设备管理
    appModule.controller('common.views.baseinfo.audioLanguage.index', [
        '$scope', '$uibModal', '$stateParams', 'uiGridConstants', 'abp.services.app.audioLanguage',
        function ($scope, $uibModal, $stateParams, uiGridConstants, audioLanguage) {
            var vm = this;

            $scope.$on('$viewContentLoaded', function () {
                App.initAjax();
            });

            vm.loading = false;


            vm.options = {
                enableHorizontalScrollbar: uiGridConstants.scrollbars.WHEN_NEEDED,
                enableVerticalScrollbar: uiGridConstants.scrollbars.WHEN_NEEDED,
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
                        name: 'ID',
                        field: 'id',
                        minWidth: 100
                    },

                    {
                        name: '编号',
                        field: 'code',
                        minWidth: 100
                    }, {
                        name: '名称',
                        field: 'name',
                        minWidth: 100
                    },
                    {
                        name: '说明',
                        field: 'remark',
                        minWidth: 200
                    } 

                ],
                data: []
            };

            vm.getAll = function () {
                vm.loading = true;
                audioLanguage.getAll({})
                    .then(function (result) {
                        vm.options.data = result.data.items;
                    }).finally(function () {
                        vm.loading = false;
                    });
            };

            vm.edit = function (entity) {
                openCreateOrEditModal(entity.id);
            };

            vm.create = function () {
                var p = abp.localization.abpWeb('Cancel');
                console.log(p);
                openCreateOrEditModal(null);
            };

            vm.delete = function (entity) {
              
                var msg = "ID：" + entity.id + "，名称：" + entity.name;
                abp.message.confirm(
                    msg,
                    "确定删除？",
                    function (isConfirmed) {
                        if (isConfirmed) {
                            audioLanguage.delete({
                                id: entity.id
                            }).then(function () {
                                vm.getAll();
                                abp.notify.success(app.localize('SuccessfullyDeleted'));
                            });
                        }
                    }
                );
            }; 

            function openCreateOrEditModal(entityId) {
                var modalInstance = $uibModal.open({
                    templateUrl: '~/App/common/views/baseinfo/audioLanguage/createOrEdit.cshtml',
                    controller: 'common.views.baseinfo.audioLanguage.createOrEdit as vm',
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

            vm.getAll();
        }]);
})();
(function () {
    //设备管理
    appModule.controller('tenant.views.dashboard.dep.dep', [
        '$scope', '$uibModal', '$stateParams', 'uiGridConstants', 'abp.services.app.receiveJson',
        function ($scope, $uibModal, $stateParams, uiGridConstants, receiveJson) {
            var vm = this;

            $scope.$on('$viewContentLoaded', function () {
                App.initAjax();
            });

            vm.loading = false;
            vm.editSchd = false;

            //到达
            vm.options = {
                enableHorizontalScrollbar: uiGridConstants.scrollbars.WHEN_NEEDED,
                enableVerticalScrollbar: uiGridConstants.scrollbars.WHEN_NEEDED,
                enableGridMenu: false,
                showGridFooter: false,
                appScopeProvider: vm,
                columnDefs: [
                    {
                        name: app.localize('Actions'),
                        enableSorting: false,
                        enableColumnMenu: false,// 是否显示列头部菜单按钮
                        enableHiding: false,
                        minWidth: 85,
                        visible: true,
                        cellTemplate:
                            '<div class=\"ui-grid-cell-contents\" >' +
                            '  <div class="btn-group dropdown"   uib-dropdown="" dropdown-append-to-body>' +
                            '    <button class="btn btn-xs" ng-class=\'{"red":grid.appScope.editSchd,"btn-primary":!grid.appScope.editSchd}\' ng-disabled=\'!grid.appScope.editSchd\' uib-dropdown-toggle="" aria-haspopup="true" aria-expanded="false"><i class="fa fa-cog"></i> ' + app.localize('Actions') + ' <span class="caret"></span></button>' +
                            '    <ul uib-dropdown-menu>' +
                            '      <li><a ng-click="grid.appScope.edit(row.entity,\'modifyCheckIn\')"><i class="fa fa-ticket" style="color:red;"></i>值机</a></li>' +
                            '      <li><a ng-click="grid.appScope.edit(row.entity,\'modifyBorad\')"><i class="fa fa-hand-lizard-o"  style="color:red;"></i>登机</a></li>' +
                            '      <li><a ng-click="grid.appScope.edit(row.entity,\'modifyArr\')"><i class="fa fa-plane fa-rotate-90"></i>进港</a></li>' +
                            '      <li><a ng-click="grid.appScope.edit(row.entity,\'modifyDep\')"><i class="fa fa-plane" style="color:blue;"></i>出港</a></li>' +
                            '    </ul>' +
                            '  </div>' +
                            '</div>'
                    },
                    {
                        name: '航班号',
                        field: 'flightNo2',
                        enableColumnMenu: false,
                        minWidth: 60
                    },
                    {
                        name: '预计起飞时间',
                        field: 'depForecastTime',
                        enableColumnMenu: false,
                        minWidth: 130,
                        cellFilter: 'momentFormat: \'YYYY-MM-DD HH:mm\'',
                    },
                    {
                        name: '目的站',
                        field: 'FestNo4',
                        minWidth: 80
                    },

                    {
                        name: '经停站',
                        field: 'fileName',
                        minWidth: 120
                    },
                    {
                        name: '值机柜台',
                        field: 'checkinCounter',
                        minWidth: 140
                    },
                    {
                        name: '值机开始时间',
                        field: 'checkinTimeStart',
                        minWidth: 130,
                        cellFilter: 'momentFormat: \'YYYY-MM-DD HH:mm\'',
                    }, {
                        name: '登机口',
                        field: 'gate',
                        minWidth: 80
                    },
                    {
                        name: '登机时间',
                        field: 'gateopentime',
                        minWidth: 130,
                        cellFilter: 'momentFormat: \'YYYY-MM-DD HH:mm\'',
                    },
                    {
                        name: '航班状态',
                        field: 'flightStatus',
                        minWidth: 80
                    },
                    {
                        name: '延误原因',
                        field: 'flightAbnormalReason',
                        minWidth: 200
                    },
                    {
                        name: '延误时间',
                        field: 'dlytimeStr',
                        minWidth: 130,
                        cellFilter: 'momentFormat: \'YYYY-MM-DD HH:mm\'',
                    }
                ],
                data: []
            };
             
            vm.getAll = function () {
                vm.loading = true;
                receiveJson.getAllArr()
                    .then(function (result) {
                        vm.options.data = result.data;
                    }).finally(function () {
                        vm.loading = false;
                    });
            };
             
            vm.playlocal = function (entity) {
                var modalInstance = $uibModal.open({
                    templateUrl: '~/App/common/views/play/broadlist/playing.cshtml',
                    controller: 'common.views.play.broadlist.playing as vm',
                    backdrop: 'static',
                    size: 'lg',
                    resolve: {
                        entityId: function () {
                            return entity.id;
                        }
                    }
                });

                modalInstance.result.then(function (result) {

                }); 
            };

            vm.edit = function (entity, type) {
                //type 1值机  2登机  modifyArr进港  4出港
                console.log(type);
                openModifyModal(entity, type);

            };

            function openModifyModal(entity, type) {
                var modalInstance = $uibModal.open({
                    templateUrl: '~/App/tenant/views/dashboard/' + type + '/index.cshtml',
                    controller: 'tenant.views.dashboard.' + type + '.index as vm',
                    backdrop: 'static',
                    size: 'md',
                    resolve: {
                        entity: function () {
                            return entity;
                        }
                    }
                });

                modalInstance.result.then(function (result) {
                    vm.getAll();
                });
            };

            abp.event.on('app.playaudio.RefreshData', function (message) {
                console.log("sinalr rc:" + message);
                if (message === "J") {
                    vm.getAll();
                }
            });

            abp.event.on('app.playaudio.UnAutoPlay', function (message) {
                console.log("unautoPlay rc:" + message);
                vm.editSchd = message;
            });

            vm.init = function () {
                vm.getAll();
            };
            vm.init();

        }]);
})();
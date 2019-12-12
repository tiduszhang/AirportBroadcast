(function () {
    //设备管理
    appModule.controller('common.views.artificial.artificial.index', [
        '$scope', '$uibModal', '$stateParams', 'uiGridConstants', 'abp.services.app.receiveJson',
        'abp.services.app.tenantSettings',
        function ($scope, $uibModal, $stateParams, uiGridConstants, receiveJson, tenantSettings) {
            var vm = this;

            $scope.$on('$viewContentLoaded', function () {
                App.initAjax();
            });
            vm.editSchd = false;

            vm.loading = false;
            vm.requestParams = {
                skipCount: 0,
                maxResultCount: app.consts.grid.defaultPageSize,
            };

            vm.optionsArr = {
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
                          //  '      <li><a ng-click="grid.appScope.edit(row.entity,\'modifyCheckIn\')"><i class="fa fa-ticket" style="color:red;"></i>值机</a></li>' +
                          //  '      <li><a ng-click="grid.appScope.edit(row.entity,\'modifyBorad\')"><i class="fa fa-hand-lizard-o"  style="color:red;"></i>登机</a></li>' +
                            '      <li><a ng-click="grid.appScope.edit(row.entity,\'modifyArr\')"><i class="fa fa-plane fa-rotate-90"></i>进港</a></li>' +
                         //   '      <li><a ng-click="grid.appScope.edit(row.entity,\'modifyDep\')"><i class="fa fa-plane" style="color:blue;"></i>出港</a></li>' +
                            '    </ul>' +
                            '  </div>' +
                            '</div>'
                    },
                    {
                        name: '进港航班',
                        field: 'flightNo2',
                        enableColumnMenu: false,
                        minWidth: 75
                    },
                    {
                        name: '计落',
                        field: 'arrPlanTime',
                        enableColumnMenu: false,
                        minWidth: 50,
                        cellFilter: 'momentFormat: \'HH:mm\''
                    },
                    {
                        name: '预落',
                        field: 'arrForecastTime',
                        enableColumnMenu: false,
                        minWidth: 50,
                        cellFilter: 'momentFormat: \'HH:mm\''
                    },
                    {
                        name: '实落',
                        field: 'arriveTime',
                        enableColumnMenu: false,
                        minWidth: 50,
                        cellFilter: 'momentFormat: \'HH:mm\''
                    },
                    {
                        name: '起场',
                        field: 'forgNo4',
                        enableColumnMenu: false,
                        minWidth: 60
                    },

                    //{
                    //    name: '经停',
                    //    field: 'festNo4',
                    //    enableColumnMenu: false,
                    //    minWidth: 60
                    //},
                    {
                        name: '状态',
                        enableColumnMenu: false,
                        field: 'flightStatus_Cn',
                        minWidth: 60
                    },
                    {
                        name: '流转状态',
                        enableColumnMenu: false,
                        field: 'flightCirculationStatus_Cn',
                        minWidth: 60
                    },
                    {
                        name: '行李转盘',
                        enableColumnMenu: false,
                        field: 'carousel',
                        minWidth: 60
                    },
                    {
                        name: '提取时间',
                        enableColumnMenu: false,
                        field: 'xlxjtimeStr',
                        minWidth: 50,
                        cellFilter: 'momentFormat: \'HH:mm\''
                    }, {
                        name: '延误时间',
                        enableColumnMenu: false,
                        field: 'dlytime',
                        minWidth: 50,
                        cellFilter: 'momentFormat: \'HH:mm\''
                    },
                    {
                        name: '延误原因',
                        enableColumnMenu: false,
                        field: 'flightAbnormalReason',
                        minWidth: 200
                    }
                ],
                data: []
            };

            vm.optionsDep = {
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
                          //  '      <li><a ng-click="grid.appScope.edit(row.entity,\'modifyArr\')"><i class="fa fa-plane fa-rotate-90"></i>进港</a></li>' +
                            '      <li><a ng-click="grid.appScope.edit(row.entity,\'modifyDep\')"><i class="fa fa-plane" style="color:blue;"></i>出港</a></li>' +
                            '    </ul>' +
                            '  </div>' +
                            '</div>'
                    },
                    {
                        name: '出港航班',
                        field: 'flightNo2',
                        enableColumnMenu: false,
                        minWidth: 75
                    },
                    {
                        name: '计起',
                        field: 'depPlanTime',
                        enableColumnMenu: false,
                        minWidth: 50,
                        cellFilter: 'momentFormat: \'HH:mm\''
                    }, {
                        name: '预起',
                        field: 'depForecastTime',
                        enableColumnMenu: false,
                        minWidth: 50,
                        cellFilter: 'momentFormat: \'HH:mm\''
                    }, {
                        name: '实起',
                        field: 'departTime',
                        enableColumnMenu: false,
                        minWidth: 50,
                        cellFilter: 'momentFormat: \'HH:mm\''
                    },
                    {
                        name: '落场',
                        field: 'festNo4',
                        minWidth: 60
                    },
                    //{
                    //    name: '经停',
                    //    field: 'fiv1No4',
                    //    minWidth: 60
                    //},
                    {
                        name: '状态',
                        field: 'flightStatus_Cn',
                        minWidth: 60
                    }, {
                        name: '流转状态',
                        enableColumnMenu: false,
                        field: 'flightCirculationStatus_Cn',
                        minWidth: 60
                    },
                    {
                        name: '值机柜台',
                        field: 'checkinCounter',
                        minWidth: 60
                    },
                    {
                        name: '值机开始时间',
                        field: 'checkinTimeStart',
                        minWidth: 60,
                        cellFilter: 'momentFormat: \'HH:mm\''
                    }, {
                        name: '登机口',
                        field: 'gate',
                        minWidth: 80
                    },
                    {
                        name: '登机时间',
                        field: 'gateopentime',
                        minWidth: 60,
                        cellFilter: 'momentFormat: \'HH:mm\''
                    },
                    {
                        name: '延误时间',
                        enableColumnMenu: false,
                        field: 'dlytime',
                        minWidth: 50,
                        cellFilter: 'momentFormat: \'HH:mm\''
                    },
                    {
                        name: '延误原因',
                        enableColumnMenu: false,
                        field: 'flightAbnormalReason',
                        minWidth: 200
                    }
                ],
                data: []
            };

            vm.getAll = function () {
                vm.loading = true;
                receiveJson.getAllArr()
                    .then(function (result) {
                        vm.optionsArr.data = result.data;
                    }).finally(function () {
                        vm.loading = false;
                    });

                receiveJson.getAllDep()
                    .then(function (result) {
                        vm.optionsDep.data = result.data;
                    }).finally(function () {
                        vm.loading = false;
                    });
            };

            vm.edit = function (entity, type) {
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
                        Aid: function () {
                            return entity.id;
                        }
                    }
                });

                modalInstance.result.then(function (result) {
                    vm.getAll();
                });
            }

            vm.ChangeAutoPlay = function () {               
                var str = "";
                if (vm.editSchd) {
                    str = "是否确认切换为【自动播放】";
                } else {
                    str = "是否确认切换为【人工播放】";
                }
                abp.message.confirm(
                    "",
                    str,
                    function (isConfirmed) {
                        if (isConfirmed) {
                          var edd = !vm.editSchd; 
                            tenantSettings.setPlayWay({ playWay: edd ? "2" : "1" })
                                .then(succ => {
                                vm.editSchd = edd;
                              });                          
                        }
                    }
                ); 
            };

            vm.HandPlay = function () {
                console.log('hand play');
            };

            vm.PlayDliaog = function () {
                var modalInstance = $uibModal.open({
                    templateUrl: '~/App/common/views/artificial/artificial/dialog.cshtml',
                    controller: 'common.views.artificial.artificial.dialog as vm',
                    backdrop: 'static',
                    size: 'md',
                    resolve: {
                       
                    }
                });

            }

            vm.init = function () {            
                tenantSettings.getPlayWay().then(succ => {
                    console.log(succ.data);
                    vm.editSchd = succ.data.playWay == "2" ? true : false;
                });
                vm.getAll();
            };
            vm.init();
             
        }]);
})();
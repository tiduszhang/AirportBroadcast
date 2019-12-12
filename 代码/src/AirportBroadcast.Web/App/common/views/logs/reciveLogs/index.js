(function () {
    //设备管理
    appModule.controller('common.views.logs.reciveLogs.index', [
        '$scope', '$uibModal', '$stateParams', 'uiGridConstants', 'abp.services.app.receiveJson', 
        function ($scope, $uibModal, $stateParams, uiGridConstants, receiveJson) {
            var vm = this;

            $scope.$on('$viewContentLoaded', function () {
                App.initAjax();
            });

            vm.loading = false;
         
     

            //到达
            vm.options  = {
                enableHorizontalScrollbar: uiGridConstants.scrollbars.WHEN_NEEDED,
                enableVerticalScrollbar: uiGridConstants.scrollbars.WHEN_NEEDED,             
                appScopeProvider: vm,
                columnDefs: [   
                    {
                        name: 'Actions',
                        enableSorting: false,
                        width: 50,
                        headerCellTemplate: '<span></span>',
                        cellTemplate:
                            '<div class=\"ui-grid-cell-contents text-center\">' +
                            '  <button class="btn btn-default btn-xs" ng-click="grid.appScope.showDetails(row.entity)"><i class="fa fa-search"></i></button>' +
                            '</div>'
                    },
                    {
                        name: '接收时间',
                        field: 'reciveTime',
                        width: 150,
                        cellFilter: 'date: \'yyyy-MM-dd HH:mm:ss\''
                    },                   
                    {
                        name: '文件内容',
                        field: 'content',
                        minWidth: 280
                    },
                    {
                        name: '备注',
                        field: 'remark',
                        width: 120
                    }
        
                ],                
                data: []
            };
             
            vm.getAll = function () {
                vm.loading = true;
                receiveJson.getAllRecivelogs()
                    .then(function (result) {
                        vm.options.data = result.data; 
                    }).finally(function () {
                        vm.loading = false;
                    });
            };

            vm.showDetails = function (entity) {
                var modalInstance = $uibModal.open({
                    templateUrl: '~/App/common/views/logs/reciveLogs/Detail.cshtml',
                    controller: 'common.views.logs.reciveLogs.Detail as vm',
                    backdrop: 'static',
                    size: 'lg',
                    resolve: {
                        content: function () {
                            return entity;
                        }
                    }
                });
            };
             

            vm.init = function() {
             
                vm.getAll();
            };
            vm.init();
             
        }]);
})();
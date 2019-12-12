(function () {
    //设备管理
    appModule.controller('common.views.logs.playLogs.index', [
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
                paginationPageSizes: app.consts.grid.defaultPageSizes,
                paginationPageSize: app.consts.grid.defaultPageSize,
                useExternalPagination: true,
                appScopeProvider: vm,
                columnDefs: [{
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
                        name: '播放时间',
                        field: 'creationTime',
                        width: 150,
                        cellFilter: 'date: \'yyyy-MM-dd HH:mm:ss\''
                    },
                    {
                        name: '播放内容', 
                        field: 'remark',
                        minWidth: 180                      
                    }
                   
                ],                
                data: []
            };

            vm.showDetails = function (entity) {
                var modalInstance = $uibModal.open({
                    templateUrl: '~/App/common/views/logs/playLogs/Detail.cshtml',
                    controller: 'common.views.logs.playLogs.Detail as vm',
                    backdrop: 'static',
                    size: 'lg',
                    resolve: {
                        content: function () {
                            return entity;
                        }
                    }
                });
            };

            vm.getAll = function () {
                vm.loading = true;
                receiveJson.getAllPlaylogs()
                    .then(function (result) {
                        vm.options.data = result.data; 
                    }).finally(function () {
                        vm.loading = false;
                    });
            };
             
            vm.getAll(); 
             
        }]);
})();
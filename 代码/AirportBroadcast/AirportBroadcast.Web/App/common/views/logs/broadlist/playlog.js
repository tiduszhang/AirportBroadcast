(function () {
    //设备管理
    appModule.controller('common.views.play.broadlist.playlog', [
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
                columnDefs: [                    
                    {
                        name: '播放时间',
                        field: 'startPlayTime',
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
             

            vm.getAll = function () {
                vm.loading = true;
                receiveJson.getAllPlaylogs()
                    .then(function (result) {
                        vm.options.data = result.data; 
                    }).finally(function () {
                        vm.loading = false;
                    });
            };

            
             

            vm.init = function() {               
                vm.getAll();
            };
            vm.init();
             
        }]);
})();
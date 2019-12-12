(function () {
    //设备管理
    appModule.controller('common.views.play.broadlist.broading', [
        '$scope', '$uibModalInstance', 'uiGridConstants', 'abp.services.app.receiveJson', 
        function ($scope, $uibModalInstance, uiGridConstants, receiveJson) {
            var vm = this; 
            $scope.$on('$viewContentLoaded', function () {
                App.initAjax();
              

            });

            vm.loading = false;

            //到达
            vm.options = {
                enableHorizontalScrollbar: uiGridConstants.scrollbars.WHEN_NEEDED,
                enableVerticalScrollbar: uiGridConstants.scrollbars.WHEN_NEEDED,              
                appScopeProvider: vm,
                columnDefs: [
                    {
                        name: '等待/开始播放时间',
                        field: 'startPlayTime',
                        width: 160,
                        cellFilter: 'date: \'yyyy-MM-dd HH:mm\''
                    },
                    {
                        name: '播放状态', 
                        width: 160, 
                        cellTemplate:
                            '<div class=\"ui-grid-cell-contents\">' +
                            '  <span ng-show="row.entity.playStatus==\'等待播放\'" class="label label-success">{{row.entity.playStatus}}</span>' +
                            '  <span ng-show="row.entity.playStatus==\'开始播放\'" class="label label-danger">{{row.entity.playStatus}}</span>' +
                            '  <span ng-show="row.entity.playStatus==\'播放完成\'" class="label label-default">{{row.entity.playStatus}}</span>' +
                            '</div>'
                    },
                    {
                        name: '播放内容',
                        field: 'remark',
                        minWidth: 180
                    }

                ],
                data: []
            };
             
            vm.cancel = function () {
                $uibModalInstance.dismiss();
            };

            var st12 = $("#startTime12");
            console.log(st12);
            st12.timepicker(
                {
                    container: "#addModal",   //模态框
                    showMeridian: false,
                    minuteStep: 1,
                    secondStep: 1,
                    template: 'dropdown'
                });
            console.log(st12);
       

        }]);
})();
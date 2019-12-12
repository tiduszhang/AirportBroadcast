 (function () {
    appModule.controller('common.views.play.broadlist.index', [
        '$scope', '$state', '$uibModal',
        function ($scope, $state, $uibModal) {
            var vm = this;
            $scope.$on('$viewContentLoaded', function () {

               
                 
                vm.loading = false;
                vm.tabSelected = function (url) {
                    $state.go(url);
                };


            });

            //var datepicker1 = $('#datetimepicker1').datetimepicker({
            //    format: 'yyyy-MM-dd HH:mm',
            //    locale: moment.locale('zh-cn')
            //}).on('dp.change', function (e) {
            //    var result = new moment(e.date).format('YYYY-MM-DD');
            //    $scope.dateOne = result;
            //    $scope.$apply();
            //    }); 

           
            $("#startTime1").timepicker(
                {                   
                    container: "#addModal",   //模态框
                    showMeridian: false,                  
                    minuteStep: 1,
                    secondStep: 1,
                    template:  'dropdown'
                });

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
                        Aid: function () {
                            return 0;
                        }
                    }
                });

                modalInstance.result.then(function (result) {
                    vm.getAll();
                });
            }


            vm.playlist = function () {
                var modalInstance = $uibModal.open({
                    templateUrl: '~/App/common/views/play/broadlist/broading.cshtml',
                    controller: 'common.views.play.broadlist.broading as vm',
                    backdrop: 'static',
                    size: 'lg',
                    resolve: {}
                });                 
            };
        }
    ]);
})();